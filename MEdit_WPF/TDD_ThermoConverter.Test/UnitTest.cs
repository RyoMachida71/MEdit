
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Windows.System;

namespace TDD_ThermoConverter.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1() {
            var model = new TemperatureModel();
            model.Celsius = 0;
            Assert.AreEqual(model.Fahrenheit, 32);
        }

        [TestMethod]
        public void TestMethod2() {
            var model = new TemperatureModel();
            model.Celsius = 100;
            Assert.AreEqual(model.Fahrenheit, 212);
        }

        [TestMethod]
        public void TestMethod3() {
            var vm = new ViewModel();
            vm.KeyDown(VirtualKey.U);
            Assert.AreEqual(vm.TemperatureModel.Celsius, 10);
            Assert.AreEqual(vm.TemperatureModel.Fahrenheit, 50);
            vm.KeyDown(VirtualKey.D);
            Assert.AreEqual(vm.TemperatureModel.Celsius, 0);
            Assert.AreEqual(vm.TemperatureModel.Fahrenheit, 32);
        }
    }
}
