using System;

using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.UI.Core;

namespace Ethereality.Model.EnergyManagement.LocalModels
{
    public struct IsisMechanicParameters
    {
        public double GConstant { get; set; }
        public double Acceleration { get; set; }
        public double VehicleMass { get; set; }
        public double CoefficientOfRr { get; set; }
        public double CoefficientOfDrag { get; set; }
        public double FrontalArea { get; set; }
        public double VehicleVelocity { get; set; }
        public double WheelAngularVelocity { get; set; }
        public double WheelRadius { get; set; }
        public double WheelInertia { get; set; }
        public double RCoefficient2 { get; set; }
        public double RCoefficient1 { get; set; }
        public double RCoefficient0 { get; set; }
        public double SlopeConstant { get; set; }
        public double Clock { get; set; }
        public double Distance { get; set; }
    }

    public class IsisMechanicsModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// The <see cref="MechanicalModelProperties" /> property's name.
        /// </summary>
        public const string MechanicalModelPropertiesPropertyName = "MechanicalModelProperties";

        private IsisMechanicParameters _mechanicalproperties = new IsisMechanicParameters();

        /// <summary>
        /// Sets and gets the MechanicalModelProperties property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public IsisMechanicParameters MechanicalModelProperties
        {
            get
            {
                return _mechanicalproperties;
            }

            set
            {
                if (_mechanicalproperties.Equals(value))
                {
                    return;
                }

                _mechanicalproperties = value;
                NotifyPropertyChanged(MechanicalModelPropertiesPropertyName);
            }
        }

        public void ModelMechCoefficientSetup()
        {
            _mechanicalproperties.VehicleMass = 222;
            _mechanicalproperties.CoefficientOfDrag = 0.08;
            _mechanicalproperties.CoefficientOfRr = 0.006;
            _mechanicalproperties.FrontalArea = 0.988;
            _mechanicalproperties.WheelRadius = 0.259;
            _mechanicalproperties.WheelInertia = 0.5 * 8 * Math.Pow(_mechanicalproperties.WheelRadius, 2);
            _mechanicalproperties.RCoefficient0 = 13.630736842104652;
            _mechanicalproperties.RCoefficient1 = 0.10485182186236133;
            _mechanicalproperties.RCoefficient2 = 0.05052631578947365;
            _mechanicalproperties.GConstant = 9.81;
            _mechanicalproperties.SlopeConstant = 0;
            _mechanicalproperties.Acceleration = 0;
        }

        private async void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                await
                   Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                 () => PropertyChanged(this, new PropertyChangedEventArgs(propertyName)));
            }
        }
    }
}