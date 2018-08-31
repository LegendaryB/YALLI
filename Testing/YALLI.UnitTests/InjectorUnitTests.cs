using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace YALLI.UnitTests
{
    [TestClass]
    public class InjectorUnitTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void LoadModule_WhenProcessIsNull_ThrowsArgumentNullException()
        {
            Injector.LoadModule(
                null,
                "user32.dll");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void LoadModule_WhenModuleNameIsNull_ThrowsArgumentNullException()
        {
            string moduleName = null;

            Injector.LoadModule(
                Process.GetCurrentProcess(),
                moduleName);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void LoadModule_WhenModuleNameIsEmpty_ThrowsArgumentException()
        {
            string moduleName = "";

            Injector.LoadModule(
                Process.GetCurrentProcess(),
                moduleName);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void LoadModule_WhenModuleNameIsWhitespace_ThrowsArgumentException()
        {
            string moduleName = " ";

            Injector.LoadModule(
                Process.GetCurrentProcess(),
                moduleName);
        }

        [TestMethod]
        public void NormalizeModuleName_WhenModuleNameIsNullOrWhitespace_ReturnsModuleName()
        {
            string moduleName = "";

            string normalizedModuleName = Injector.NormalizeModuleName(
                moduleName);

            Assert.AreEqual(moduleName, normalizedModuleName);
        }

        [TestMethod]
        public void NormalizeModuleName_WhenModuleNameHasNoExtension_ReturnsModuleNameWithExtension()
        {
            string moduleName = "TEST";

            string normalizedModuleName = Injector.NormalizeModuleName(
                moduleName);

            Assert.AreEqual($"{moduleName}.DLL", normalizedModuleName);
        }
    }
}
