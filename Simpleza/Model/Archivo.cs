using GalaSoft.MvvmLight;
using ICSharpCode.AvalonEdit.Document;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simpleza.Model
{
    public class Archivo : ObservableObject, ICloneable
    {
        public static string TEMPLATE 
            = new StringBuilder().AppendLine("procedimiento PRINCIPAL()").AppendLine("comenzar").AppendLine().AppendLine("finproc;").ToString();

        private TextDocument ultimoDocumentoGuardado;

        TextDocument documentoActual;
        public TextDocument DocumentoActual
        {
            get
            {
                return documentoActual;
            }
            set
            {
                Set<TextDocument>(() => this.documentoActual, ref documentoActual, value);
            }
        }

        string ubicacion;
        public string Ubicacion
        {
            get
            {
                return ubicacion;
            }
            set
            {
                Set<string>(() => this.ubicacion, ref ubicacion, value);
            }
        }


        public string Nombre
        {
            get
            {
                if (!string.IsNullOrEmpty(Ubicacion))
                {
                    return Ubicacion.Split('\\').Last();
                }
                return string.Empty;

            }

        }



        public bool TextoActualGuardado
        {
            get
            {
                return DocumentoActual.Text.Equals(ultimoDocumentoGuardado.Text);
            }
        }

        public Archivo()
        {
            ultimoDocumentoGuardado = new TextDocument();
            Ubicacion = string.Empty;
            DocumentoActual = new TextDocument();

        
        }

        public void AsignarTemplate()
        {
            DocumentoActual.Text = TEMPLATE;
        }




        public override bool Equals(object obj)
        {
            Archivo archComparar = obj as Archivo;

            if (archComparar != null)
            {
                return archComparar.DocumentoActual.Text.Equals(DocumentoActual.Text)
                    && archComparar.Ubicacion.Equals(Ubicacion);
            }

            return false;
        }





        public object Clone()
        {
            Archivo arch = new Archivo();
            arch.DocumentoActual = new TextDocument();
            arch.DocumentoActual.Text = DocumentoActual.Text;

            arch.Ubicacion = Ubicacion;

            return arch;
        }

        internal void SalvarADisco()
        {
            if (!string.IsNullOrEmpty(Ubicacion))
            {
                using (StreamWriter strWriter = File.CreateText(Ubicacion))
                {
                    strWriter.Write(DocumentoActual.Text);

                }


            }
        }

        static internal Archivo AbrirDeDisco(string path)
        {
            Archivo arch = null;
            if (File.Exists(path))
            {
                using (StreamReader strReader = File.OpenText(path))
                {
                    arch = new Archivo();
                    arch.DocumentoActual.Text = strReader.ReadToEnd();
                    arch.Ubicacion = path;
                }


            }
            return arch;
        }
    }
}
