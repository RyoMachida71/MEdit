using Windows.System;

namespace TDD_ThermoConverter {
    class ViewModel {

        private TemperatureModel model;
        public ViewModel() {
            model = new TemperatureModel();
        }

        public TemperatureModel TemperatureModel => model;

        public void KeyDown(VirtualKey key) {
            if (key == VirtualKey.U) {
                model.Celsius += 10;
            }
            if (key == VirtualKey.D) {
                model.Celsius -= 10;
            }
        }
    }
}
