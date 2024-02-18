using System;
using System.ComponentModel;

namespace TDD_ThermoConverter {
    class TemperatureModel :INotifyPropertyChanged {
        double fahrenheit, celsius;

        public double Fahrenheit {
            set {
                fahrenheit = value;
                celsius = (fahrenheit - 32) / 1.8f;
                NotifyPropertyChanged("Celsius");
                NotifyPropertyChanged("Fahrenheit");
            }
            get {
                return fahrenheit;
            }
        }

        public double Celsius {
            set {
                celsius = Math.Max(0, Math.Min(100, value));
                fahrenheit = (celsius * 1.8) + 32;
                NotifyPropertyChanged("Celsius");
                NotifyPropertyChanged("Fahrenheit");
            }
            get {
                return celsius;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propName = "") {
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
    }
}
