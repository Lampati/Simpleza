using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Simpleza.ViewModel;

namespace SimplezaTestProject
{
    [TestClass]
    public class AboutUsViewModelTest
    {
        [TestMethod]
        public void AutorConValor()
        {
            AboutUsViewModel vm = new AboutUsViewModel();

            Assert.IsNotNull(vm.Autor);
        }

        [TestMethod]
        public void VersionAppConValor()
        {
            AboutUsViewModel vm = new AboutUsViewModel();

            Assert.IsNotNull(vm.VersionApp);
            Version v = new Version(vm.VersionApp);

            Assert.IsNotNull(v);
        }

        [TestMethod]
        public void VersionCompiladorConValor()
        {
            AboutUsViewModel vm = new AboutUsViewModel();

            Assert.IsNotNull(vm.VersionCompilador);
            Version v = new Version(vm.VersionCompilador);

            Assert.IsNotNull(v);
        }
    }
}
