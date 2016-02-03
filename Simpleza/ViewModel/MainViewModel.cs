using CompiladorGargar;
using CompiladorGargar.Resultado;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using ICSharpCode.AvalonEdit.Document;
using Simpleza.Helpers;
using Simpleza.Interfases;
using Simpleza.Model;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

namespace Simpleza.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private Compilador compilador;
        private int seleccionStart;
        private int seleccionLength;
        private int posicionActual;
        private Archivo archivoActual;
        private ObservableCollection<MensajeCompilacion> listaMensajesCompilacion;

        

        IMessageBoxService servicioMessageBox;
        IFileManagerDialogService servicioFileManager;
        IPrinterService servicioImpresion;
        IProcessStarterService servicioProcesos;

        public int PosicionActual
        {
            get
            {
                return posicionActual;
            }
            set
            {
                posicionActual = value;
                RaisePropertyChanged("PosicionActual");
                RaisePropertyChanged("LineaActual");
                RaisePropertyChanged("ColumnaActual");
            }
        }

        public int LineaActual
        {
            get
            {
                if (ArchivoActual != null && ArchivoActual.DocumentoActual !=null )
                {
                    TextLocation loc = ArchivoActual.DocumentoActual.GetLocation(PosicionActual);

                    return loc.Line;
                }

                return 0;
            }
        }

        public int ColumnaActual
        {
            get
            {
                if (ArchivoActual != null && ArchivoActual.DocumentoActual != null)
                {
                    TextLocation loc = ArchivoActual.DocumentoActual.GetLocation(PosicionActual);

                    return loc.Column;
                }

                return 0;
            }
        }

        public bool CompilacionCorrecta
        {
            get
            {
                return ListaMensajesCompilacion != null && listaMensajesCompilacion.Count == 0;
                
            }
        }


        public Archivo ArchivoActual
        {
            get
            {
                return archivoActual;
            }
            set
            {
                archivoActual = value;
                RaisePropertyChanged("ArchivoActual");
            }
        }

        public ObservableCollection<MensajeCompilacion> ListaMensajesCompilacion
        {
            get
            {
                return listaMensajesCompilacion;
            }
            set
            {
                listaMensajesCompilacion = value;
                RaisePropertyChanged("ListaMensajesCompilacion");
                

            }
        }


        public string PathManual
        {
            get
            {
                return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Manuales", "ManualGargar_v1.pdf"); 
            }
        }

        public ICommand CommandNew { get; set; }
        public ICommand CommandOpen { get; set; }
        public ICommand CommandSave { get; set; }
        public ICommand CommandSaveAs { get; set; }
        public ICommand CommandPrint { get; set; }
        public ICommand CommandExit { get; set; }

        public ICommand CommandFind { get; set; }
        public ICommand CommandReplace { get; set; }

        public ICommand CommandCompilar { get; set; }
        public ICommand CommandEjecutar { get; set; }

        public ICommand CommandAbrirManual { get; set; }
        public ICommand CommandAboutUs { get; set; }



        public MainViewModel(IMessageBoxService servMsgBox, IFileManagerDialogService servFileManager,
            IProcessStarterService servProcess, IPrinterService servImpr)
        {
            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real"
            ////}

            
            ArchivoActual = new Archivo();            
            ListaMensajesCompilacion = new ObservableCollection<MensajeCompilacion>();

            
            CommandNew = new RelayCommand(New);
            CommandOpen = new RelayCommand(Open);
            CommandSave = new RelayCommand(Save);
            CommandSaveAs = new RelayCommand(SaveAs);
            CommandPrint = new RelayCommand(Print);
            CommandExit = new RelayCommand(Exit);


            CommandCompilar = new RelayCommand(Compilar);
            CommandEjecutar = new RelayCommand(Ejecutar);

            CommandAbrirManual = new RelayCommand(AbrirManual);
            CommandAboutUs = new RelayCommand(AboutUs);


            servicioMessageBox = servMsgBox;
            servicioFileManager = servFileManager;
            servicioImpresion = servImpr;
            servicioProcesos = servProcess;

            compilador = new Compilador();
        }

      

        public void AboutUs()
        {
            Messenger.Default.Send(new NotificationMessage("ShowAboutUs"));
        }

        public void AbrirManual()
        {

            FileInfo fileInfo = new FileInfo(PathManual);

            if (!fileInfo.Exists)
            {
                if (!fileInfo.Directory.Exists)
                {
                    Directory.CreateDirectory(fileInfo.Directory.FullName);
                }

                System.IO.File.WriteAllBytes(PathManual, Properties.Resources.ManualGarGar_v1);
            }

            servicioProcesos.StartProcess(fileInfo.FullName);
        }

        public void Ejecutar()
        {
            ResultadoCompilacion resultado = CompilarTexto();

            if (resultado.CompilacionGarGarCorrecta && resultado.GeneracionEjectuableCorrecto)
            {
                EjecucionManager.EjecutarConVentana(resultado.ArchEjecutableConRuta);
            }
        }

        public void Compilar()
        {
            CompilarTexto();
        }

        public ResultadoCompilacion CompilarTexto()
        {
            ResultadoCompilacion res = compilador.Compilar(ArchivoActual.DocumentoActual.Text);

            ListaMensajesCompilacion.Clear();
            foreach (var item in res.ListaErrores)
            {
                ListaMensajesCompilacion.Add(new MensajeCompilacion(item));
            }

            RaisePropertyChanged("CompilacionCorrecta");
            return res;
        }

        [ExcludeFromCodeCoverage]
        public void Exit()
        {
            Application.Current.Shutdown();
        }

        public void Print()
        {
            
            servicioImpresion.AsignarDocumento(ArchivoActual.Nombre, ArchivoActual.DocumentoActual.Text);

            bool? resultado = servicioImpresion.ElegirImpresora();

            if (resultado.HasValue && resultado.Value)
            {
                servicioImpresion.Imprimir();
            }
            
            //FlowDocument doc = new FlowDocument(new Paragraph(new Run(ArchivoActual.DocumentoActual.Text)));
            //doc.Name = ArchivoActual.Nombre;

            
            //doc.PagePadding = new Thickness(25);

            //// Create IDocumentPaginatorSource from FlowDocument
            //IDocumentPaginatorSource idpSource = doc;

            //// Call PrintDocument method to send document to printer
            //PrintDialog printDialog = new PrintDialog();
            //doc.ColumnWidth = printDialog.PrintableAreaWidth;
            //bool? imprimir = printDialog.ShowDialog();
            //if (imprimir.HasValue && imprimir.Value)
            //{
            //    printDialog.PrintDocument(idpSource.DocumentPaginator, "Documento Gargar");
            //}
        }

        public void Save()
        {

            if (string.IsNullOrEmpty(ArchivoActual.Ubicacion))
            {
                ArchivoActual.Ubicacion = servicioFileManager.SaveFileDialog();

            }

            ArchivoActual.SalvarADisco();
        }

        public void SaveAs()
        {
            ArchivoActual.Ubicacion = servicioFileManager.SaveFileDialog();

            ArchivoActual.SalvarADisco();

        }



        public void Open()
        {
            bool permitirAbrir = true;
            if (!ArchivoActual.TextoActualGuardado)
            {
                permitirAbrir = servicioMessageBox.ShowMessageWithOptions("Esta seguro?", "hola");
            }

            if (permitirAbrir)
            {
                string path = servicioFileManager.OpenFileDialog();

                Archivo archNuevo = Archivo.AbrirDeDisco(path);

                if (archNuevo != null)
                {
                    ArchivoActual = archNuevo;
                    ListaMensajesCompilacion.Clear();
                    RaisePropertyChanged("CompilacionCorrecta");
                }

            }
        }

        public void New()
        {
            bool permitirNuevo = true;
            if (!ArchivoActual.TextoActualGuardado)
            {
                permitirNuevo = servicioMessageBox.ShowMessageWithOptions("Esta seguro?", "hola");
            }

            if (permitirNuevo)
            {
                ArchivoActual = new Archivo();
                ListaMensajesCompilacion.Clear();
                RaisePropertyChanged("CompilacionCorrecta");
            }
        }


    }
}