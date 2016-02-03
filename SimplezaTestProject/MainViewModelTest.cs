using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Simpleza.ViewModel;
using Simpleza.Interfases;
using System.IO;
using Simpleza.Model;
using Simpleza.Helpers;
using System.Text;
using GalaSoft.MvvmLight.Messaging;

namespace SimplezaTestProject
{
    [TestClass]
    public class MainViewModelTest
    {
        [TestMethod]
        public void AceptarNuevoArchivo()
        {
            MainViewModel vm = new MainViewModel(ArmarMsgService(true).Object, null, null, null);
            vm.ArchivoActual.DocumentoActual.Text = "hola";

            Archivo archViejo = vm.ArchivoActual.Clone() as Archivo;

            vm.New();

            Assert.IsFalse(vm.ArchivoActual.Equals(archViejo));

        }

        [TestMethod]
        public void CancelarNuevoArchivo()
        {
            MainViewModel vm = new MainViewModel(ArmarMsgService(false).Object, null, null, null);
            vm.ArchivoActual.DocumentoActual.Text = "hola";

            Archivo archViejo = vm.ArchivoActual.Clone() as Archivo;

            vm.New();

            Assert.IsTrue(vm.ArchivoActual.Equals(archViejo));

        }

        [TestMethod]
        public void NoSalvarArchivo()
        {
            string path = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            string documento = "Buenos Dias";

            MainViewModel vm = new MainViewModel(null, ArmarDialogService(null, null).Object, null, null);
            vm.ArchivoActual.DocumentoActual.Text = documento;
            vm.Save();

            Assert.IsFalse(File.Exists(path));

        }

        [TestMethod]
        public void SalvarYAbrirArchivo()
        {
            string path = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            string documento = "Buenos Dias";

            MainViewModel vm = new MainViewModel(ArmarMsgService(true).Object, ArmarDialogService(path, path).Object, null, null);
            vm.ArchivoActual.DocumentoActual.Text = documento;
            vm.Save();

            Assert.IsTrue(File.Exists(path));

            MainViewModel vm2 = new MainViewModel(ArmarMsgService(true).Object, ArmarDialogService(path, path).Object, null, null);
            vm2.ArchivoActual.DocumentoActual.Text = "No te voy a permitir abrir";
            vm2.Open();

            Assert.IsTrue(vm.ArchivoActual.Equals(vm2.ArchivoActual));
        }

        [TestMethod]
        public void SalvarYNoAbrirArchivo()
        {
            string path = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            string documento = "Buenos Dias";

            MainViewModel vm = new MainViewModel(ArmarMsgService(false).Object, ArmarDialogService(path, path).Object, null, null);
            vm.ArchivoActual.DocumentoActual.Text = documento;
            vm.Save();

            Assert.IsTrue(File.Exists(path));

            MainViewModel vm2 = new MainViewModel(ArmarMsgService(false).Object, ArmarDialogService(path, path).Object, null, null);
            vm2.ArchivoActual.DocumentoActual.Text = "No te voy a permitir abrir";
            vm2.Open();

            Assert.IsFalse(vm.ArchivoActual.Equals(vm2.ArchivoActual));
        }




        [TestMethod]
        public void NoAbrirArchivo()
        {
            string path = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            string documento = "Buenos Dias";

            MainViewModel vm = new MainViewModel(null, ArmarDialogService(path, null).Object, null, null);
            vm.ArchivoActual.DocumentoActual.Text = documento;
            vm.Save();


            MainViewModel vm2 = new MainViewModel(null, ArmarDialogService(null, null).Object, null, null);
            vm2.Open();

            Assert.IsFalse(vm.ArchivoActual.Equals(vm2.ArchivoActual));

        }

        [TestMethod]
        public void SalvarAsYAbrirArchivo()
        {
            string path = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            string documento = "Buenos Dias";

            MainViewModel vm = new MainViewModel(ArmarMsgService(true).Object, ArmarDialogService(path, path).Object, null, null);
            vm.ArchivoActual.DocumentoActual.Text = documento;
            vm.SaveAs();

            Assert.IsTrue(File.Exists(path));

            MainViewModel vm2 = new MainViewModel(ArmarMsgService(true).Object, ArmarDialogService(path, path).Object, null, null);
            vm2.ArchivoActual.DocumentoActual.Text = "No te voy a permitir abrir";
            vm2.Open();

            Assert.IsTrue(vm.ArchivoActual.Equals(vm2.ArchivoActual));
        }

        private Mock<MessageBoxService> ArmarMsgService(bool retorno)
        {
            Mock<MessageBoxService> msgServiceStub = new Mock<MessageBoxService>();
            msgServiceStub.Setup(ioServ => ioServ.ShowMessageWithOptions(It.IsAny<string>(), It.IsAny<string>())).Returns(retorno);

            return msgServiceStub;
        }

        private Mock<ProcessStarterService> ArmarProcessStarterService()
        {
            Mock<ProcessStarterService> msgServiceStub = new Mock<ProcessStarterService>();
            msgServiceStub.Setup(ioServ => ioServ.StartProcess(It.IsAny<string>()));

            return msgServiceStub;
        }

        private Mock<FileManagerDialogService> ArmarDialogService(string retornoSave, string retornoOpen)
        {
            Mock<FileManagerDialogService> ioServiceStub = new Mock<FileManagerDialogService>();
            ioServiceStub.Setup(ioServ => ioServ.SaveFileDialog()).Returns((string)retornoSave);
            ioServiceStub.Setup(ioServ => ioServ.OpenFileDialog()).Returns((string)retornoOpen);

            return ioServiceStub;
        }


        private Mock<PrinterChooserService> ArmarPrinterChooserService(bool? retornoElegirImpresora, string nombre, string texto)
        {
            Mock<PrinterChooserService> ioServiceStub = new Mock<PrinterChooserService>();
            ioServiceStub.Setup(ioServ => ioServ.ElegirImpresora()).Returns((bool?)retornoElegirImpresora);
            ioServiceStub.Setup(ioServ => ioServ.AsignarDocumento(nombre, texto));
            ioServiceStub.Setup(ioServ => ioServ.Imprimir());

            return ioServiceStub;
        }

        [TestMethod]
        public void CompilarCorrecto()
        {
            MainViewModel vm = new MainViewModel(null, null, null, null);
            vm.ArchivoActual.DocumentoActual.Text = "procedimiento principal() comenzar mostrar('hola'); finproc;";
            vm.Compilar();

            Assert.IsTrue(vm.CompilacionCorrecta);
        }

        [TestMethod]
        public void CompilarIncorrecto()
        {
            MainViewModel vm = new MainViewModel(null, null, null, null);
            vm.ArchivoActual.DocumentoActual.Text = "procedimiento principal() comenzar mostrar'hola'); finproc;";
            vm.Compilar();

            Assert.IsFalse(vm.CompilacionCorrecta);
        }


        [TestMethod]
        public void EjecutarCorrecto()
        {
            MainViewModel vm = new MainViewModel(null, null, null, null);
            vm.ArchivoActual.DocumentoActual.Text = "procedimiento principal() comenzar mostrar('hola'); finproc;";
            vm.Ejecutar();

            Assert.IsTrue(vm.ListaMensajesCompilacion.Count == 0);
        }

        [TestMethod]
        public void EjecutarIncorrecto()
        {
            MainViewModel vm = new MainViewModel(null, null, null, null);
            vm.ArchivoActual.DocumentoActual.Text = "procedimiento principal() comenzar mostrar'hola'); finproc;";
            vm.Ejecutar();

            Assert.IsTrue(vm.ListaMensajesCompilacion.Count > 0);
        }

        [TestMethod]
        public void AbrirManualYaCreado()
        {
            MainViewModel vm = new MainViewModel(null, null, ArmarProcessStarterService().Object, null);
            vm.AbrirManual();

            Assert.IsTrue(new FileInfo(vm.PathManual).Exists);
        }

        [TestMethod]
        public void AbrirManualArchivoYDirectorioBorrado()
        {
            MainViewModel vm = new MainViewModel(null, null, ArmarProcessStarterService().Object, null);

            FileInfo fi = new FileInfo(vm.PathManual);

            if (fi.Exists)
            {
                fi.Delete();
                fi.Directory.Delete();
            }

            vm.AbrirManual();

            Assert.IsTrue(new FileInfo(vm.PathManual).Exists);
        }

        [TestMethod]
        public void AbrirManualSoloArchivoBorrado()
        {
            MainViewModel vm = new MainViewModel(null, null, ArmarProcessStarterService().Object, null);

            FileInfo fi = new FileInfo(vm.PathManual);

            if (fi.Exists)
            {
                fi.Delete();
            }

            vm.AbrirManual();

            Assert.IsTrue(new FileInfo(vm.PathManual).Exists);
        }


        [TestMethod]
        public void PosicionCorrectaDocumento1()
        {
            MainViewModel vm = new MainViewModel(null, null, null, null);
            vm.ArchivoActual.DocumentoActual.Text = "hola buen dia";

            vm.PosicionActual = 0;
            Assert.IsTrue(vm.LineaActual == 1);
            Assert.IsTrue(vm.ColumnaActual == 1);

            vm.PosicionActual = 1;
            Assert.IsTrue(vm.LineaActual == 1);
            Assert.IsTrue(vm.ColumnaActual == 2);

            vm.PosicionActual = 5;
            Assert.IsTrue(vm.LineaActual == 1);
            Assert.IsTrue(vm.ColumnaActual == 6);
        }

        [TestMethod]
        public void PosicionCorrectaDocumento2()
        {
            MainViewModel vm = new MainViewModel(null, null, null, null);
            vm.ArchivoActual.DocumentoActual.Text = new StringBuilder("hola").AppendLine().Append("chau").ToString();

            vm.PosicionActual = 0;
            Assert.IsTrue(vm.LineaActual == 1);
            Assert.IsTrue(vm.ColumnaActual == 1);

            vm.PosicionActual = 6;
            Assert.IsTrue(vm.LineaActual == 2);
            Assert.IsTrue(vm.ColumnaActual == 1);

            vm.PosicionActual = 9;
            Assert.IsTrue(vm.LineaActual == 2);
            Assert.IsTrue(vm.ColumnaActual == 4);
        }

        [TestMethod]
        public void PosicionCorrectaSinArchivo()
        {
            MainViewModel vm = new MainViewModel(null, null, null, null);
            vm.ArchivoActual = null;

            Assert.IsTrue(vm.LineaActual == 0);
            Assert.IsTrue(vm.ColumnaActual == 0);
        }

        [TestMethod]
        public void CompilacionCorrectaSinListaMensajes()
        {
            MainViewModel vm = new MainViewModel(null, null, null, null);
            vm.ListaMensajesCompilacion = null;

            Assert.IsFalse(vm.CompilacionCorrecta);
        }

        [TestMethod]
        public void ImpresionCorrecta()
        {
            Archivo arch = new Archivo();
            arch.Ubicacion = Path.Combine(Path.GetTempPath(), Path.GetTempFileName());
            arch.DocumentoActual.Text = "que tal buenos dias";

            MainViewModel vm = new MainViewModel(null, null, null,
                ArmarPrinterChooserService(true, arch.Nombre, arch.DocumentoActual.Text).Object);

            vm.Print();
        }

        [TestMethod]
        public void ImpresionCancelada()
        {
            Archivo arch = new Archivo();
            arch.Ubicacion = Path.Combine(Path.GetTempPath(), Path.GetTempFileName());
            arch.DocumentoActual.Text = "que tal buenos dias";

            MainViewModel vm = new MainViewModel(null, null, null,
                ArmarPrinterChooserService(false, arch.Nombre, arch.DocumentoActual.Text).Object);

            vm.Print();
        }

        [TestMethod]
        public void ImpresionNulleada()
        {
            Archivo arch = new Archivo();
            arch.Ubicacion = Path.Combine(Path.GetTempPath(), Path.GetTempFileName());
            arch.DocumentoActual.Text = "que tal buenos dias";

            MainViewModel vm = new MainViewModel(null, null, null,
                ArmarPrinterChooserService(null, arch.Nombre, arch.DocumentoActual.Text).Object);

            vm.Print();
        }

        [TestMethod]
        public void AboutUsCorrectamenteLlamado()
        {
            // arrange
            var systemUnderTest = new MainViewModel(null, null, null, null);

            // Set the action to store the message that was sent
            NotificationMessage actual = null;
            Messenger.Default.Register<NotificationMessage>(this, t => actual = t);


            // act
            systemUnderTest.CommandAboutUs.Execute(null);


            // assert
            NotificationMessage expected = new NotificationMessage("ShowAboutUs");
            Assert.AreSame(actual.Notification, expected.Notification);


        }
    }
}
