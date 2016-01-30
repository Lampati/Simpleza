using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Simpleza.ViewModel;
using Simpleza.Interfases;
using System.IO;
using Simpleza.Model;
using Simpleza.Helpers;

namespace SimplezaTestProject
{
    [TestClass]
    public class MainViewModelTest
    {
        [TestMethod]
        public void AceptarNuevoArchivo()
        {
            MainViewModel vm = new MainViewModel(ArmarMsgService(true).Object, null);
            vm.ArchivoActual.DocumentoActual.Text = "hola";

            Archivo archViejo = vm.ArchivoActual.Clone() as Archivo;

            vm.New();

            Assert.IsFalse(vm.ArchivoActual.Equals(archViejo));

        }

        [TestMethod]
        public void CancelarNuevoArchivo()
        {
            MainViewModel vm = new MainViewModel(ArmarMsgService(false).Object, null);
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

            MainViewModel vm = new MainViewModel(null, ArmarDialogService(null, null).Object);
            vm.ArchivoActual.DocumentoActual.Text = documento;
            vm.Save();

            Assert.IsFalse(File.Exists(path));

        }

        [TestMethod]
        public void SalvarYAbrirArchivo()
        {
            string path = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            string documento = "Buenos Dias";

            MainViewModel vm = new MainViewModel(ArmarMsgService(true).Object, ArmarDialogService(path, path).Object);
            vm.ArchivoActual.DocumentoActual.Text = documento;
            vm.Save();

            Assert.IsTrue(File.Exists(path));

            MainViewModel vm2 = new MainViewModel(ArmarMsgService(true).Object, ArmarDialogService(path, path).Object);
            vm2.ArchivoActual.DocumentoActual.Text = "No te voy a permitir abrir";
            vm2.Open();

            Assert.IsTrue(vm.ArchivoActual.Equals(vm2.ArchivoActual));
        }

         [TestMethod]
        public void SalvarYNoAbrirArchivo()
        {
            string path = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            string documento = "Buenos Dias";

            MainViewModel vm = new MainViewModel(ArmarMsgService(false).Object, ArmarDialogService(path, path).Object);
            vm.ArchivoActual.DocumentoActual.Text = documento;
            vm.Save();

            Assert.IsTrue(File.Exists(path));

            MainViewModel vm2 = new MainViewModel(ArmarMsgService(false).Object, ArmarDialogService(path, path).Object);
            vm2.ArchivoActual.DocumentoActual.Text = "No te voy a permitir abrir";
            vm2.Open();

            Assert.IsFalse(vm.ArchivoActual.Equals(vm2.ArchivoActual));
        }




        [TestMethod]
        public void NoAbrirArchivo()
        {
            string path = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            string documento = "Buenos Dias";

            MainViewModel vm = new MainViewModel(null, ArmarDialogService(path, null).Object);
            vm.ArchivoActual.DocumentoActual.Text = documento;
            vm.Save();

            
            MainViewModel vm2 = new MainViewModel(null, ArmarDialogService(null, null).Object);
            vm2.Open();

            Assert.IsFalse(vm.ArchivoActual.Equals(vm2.ArchivoActual));

        }

        [TestMethod]
        public void SalvarAsYAbrirArchivo()
        {
            string path = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            string documento = "Buenos Dias";

            MainViewModel vm = new MainViewModel(ArmarMsgService(true).Object, ArmarDialogService(path, path).Object);
            vm.ArchivoActual.DocumentoActual.Text = documento;
            vm.SaveAs();

            Assert.IsTrue(File.Exists(path));

            MainViewModel vm2 = new MainViewModel(ArmarMsgService(true).Object, ArmarDialogService(path, path).Object);
            vm2.ArchivoActual.DocumentoActual.Text = "No te voy a permitir abrir";
            vm2.Open();

            Assert.IsTrue(vm.ArchivoActual.Equals(vm2.ArchivoActual));
        }

        private Mock<MessageBoxService> ArmarMsgService(bool retorno)
        {
            Mock<MessageBoxService> msgServiceStub = new Mock<MessageBoxService>();
            msgServiceStub.Setup(ioServ => ioServ.ShowMessage(It.IsAny<string>(), It.IsAny<string>() )).Returns(retorno);

            return msgServiceStub;
        }

        private Mock<FileManagerDialogService> ArmarDialogService(string retornoSave, string retornoOpen)
        {
            Mock<FileManagerDialogService> ioServiceStub = new Mock<FileManagerDialogService>();
            ioServiceStub.Setup(ioServ => ioServ.SaveFileDialog()).Returns((string)retornoSave);
            ioServiceStub.Setup(ioServ => ioServ.OpenFileDialog()).Returns((string)retornoOpen);

            return ioServiceStub;
        }

        [TestMethod]
        public void CompilarCorrecto()
        {
            MainViewModel vm = new MainViewModel(null, null);
            vm.ArchivoActual.DocumentoActual.Text = "procedimiento principal() comenzar mostrar('hola'); finproc;";
            vm.Compilar();

            Assert.IsTrue( vm.ListaMensajesCompilacion.Count == 0);
        }

        [TestMethod]
        public void CompilarIncorrecto()
        {
            MainViewModel vm = new MainViewModel(null, null);
            vm.ArchivoActual.DocumentoActual.Text = "procedimiento principal() comenzar mostrar'hola'); finproc;";
            vm.Compilar();

            Assert.IsTrue(vm.ListaMensajesCompilacion.Count > 0);
        }


        [TestMethod]
        public void EjecutarCorrecto()
        {
            MainViewModel vm = new MainViewModel(null, null);
            vm.ArchivoActual.DocumentoActual.Text = "procedimiento principal() comenzar mostrar('hola'); finproc;";
            vm.Ejecutar();

            Assert.IsTrue(vm.ListaMensajesCompilacion.Count == 0);
        }

        [TestMethod]
        public void EjecutarIncorrecto()
        {
            MainViewModel vm = new MainViewModel(null, null);
            vm.ArchivoActual.DocumentoActual.Text = "procedimiento principal() comenzar mostrar'hola'); finproc;";
            vm.Ejecutar();

            Assert.IsTrue(vm.ListaMensajesCompilacion.Count > 0);
        }
    }
}
