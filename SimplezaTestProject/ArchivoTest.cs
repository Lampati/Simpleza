using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Simpleza.Model;
using System.IO;

namespace SimplezaTestProject
{
    [TestClass]
    public class ArchivoTest
    {
        [TestMethod]
        public void ArchivoNuevoVacio()
        
        {
            Archivo arch = new Archivo();

            Assert.IsTrue(arch.DocumentoActual != null && string.IsNullOrEmpty(arch.DocumentoActual.Text));

        }

        [TestMethod]
        public void ArchivosIguales()
        {
            Archivo arch1 = new Archivo();
            Archivo arch2 = new Archivo();

            arch1.DocumentoActual.Text = "hola";
            arch2.DocumentoActual.Text = "hola";

            arch1.Ubicacion = Path.Combine(Path.GetTempPath(), "prueba.gar");
            arch2.Ubicacion = Path.Combine(Path.GetTempPath(), "prueba.gar");

            Assert.IsTrue(arch1.Equals(arch2));

        }

        [TestMethod]
        public void ArchivosDiferentes1()
        {
            Archivo arch1 = new Archivo();
            Archivo arch2 = new Archivo();

            arch1.DocumentoActual.Text = "hola2";
            arch2.DocumentoActual.Text = "hola";

            arch1.Ubicacion = Path.Combine(Path.GetTempPath(), "prueba.gar");
            arch2.Ubicacion = Path.Combine(Path.GetTempPath(), "prueba.gar");

            Assert.IsFalse(arch1.Equals(arch2));

        }

        [TestMethod]
        public void ArchivosDiferentes2()
        {
            Archivo arch1 = new Archivo();
            Archivo arch2 = new Archivo();

            arch1.DocumentoActual.Text = "hola";
            arch2.DocumentoActual.Text = "hola";

            arch1.Ubicacion = Path.Combine(Path.GetTempPath(), "prueba1.gar");
            arch2.Ubicacion = Path.Combine(Path.GetTempPath(), "prueba.gar");

            Assert.IsFalse(arch1.Equals(arch2));

        }

        [TestMethod]
        public void ArchivosDiferentes3()
        {
            Archivo arch1 = new Archivo();
            object arch2 = new object();

            arch1.DocumentoActual.Text = "hola";

            arch1.Ubicacion = Path.Combine(Path.GetTempPath(), "prueba1.gar");

            Assert.IsFalse(arch1.Equals(arch2));

        }

        [TestMethod]
        public void NombreCorrecto()
        {
            string filename = Path.GetRandomFileName();
            Archivo arch1 = new Archivo();
            arch1.Ubicacion = Path.Combine(Path.GetTempPath(), filename);

            Assert.IsTrue(arch1.Nombre.Equals(filename));

        }

        [TestMethod]
        public void NombreVacioCorrecto()
        {
            Archivo arch1 = new Archivo();

            Assert.IsTrue(arch1.Nombre.Equals(string.Empty));

        }

        [TestMethod]
        public void TemplateAsignadoCorrecto()
        {
            Archivo arch1 = new Archivo();
            arch1.AsignarTemplate();
            Assert.IsTrue(arch1.DocumentoActual.Text.Equals(Archivo.TEMPLATE));


        }


    }
}
