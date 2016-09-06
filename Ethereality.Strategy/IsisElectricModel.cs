using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.UI.Core;

namespace Ethereality.Model.EnergyManagement.LocalModels
{
    public struct IsisElecParameters
    {
        public double MotorCurrent { get; set; }
        public double MotorVoltage { get; set; }
        public double CoulombCount { get; set; }
        public double ElectricalPower { get; set; }
        public double BatteryConstant { get; set; }
        public double Constant1 { get; set; }
        public double Constant2 { get; set; }
        
    }
    public class IsisElectricModel:INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        #region ElectricalModelProperties
        /// <summary>
        /// The <see cref="ElecModelProperties" /> property's name.
        /// </summary>
        public const string ElecModelPropertiesPropertyName = "ElecModelProperties";

        private IsisElecParameters _elecModelProperties = new IsisElecParameters();

        /// <summary>
        /// Sets and gets the ElecModelProperties property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public IsisElecParameters ElecModelProperties
        {
            get
            {
                return _elecModelProperties;
            }

            set
            {
                if (_elecModelProperties.Equals(value))
                {
                    return;
                }

              
                _elecModelProperties = value;
                NotifyPropertyChanged(ElecModelPropertiesPropertyName);
            }
        } 
        #endregion
        
        public void ModelElecCoefficientSetup()
        {
            _elecModelProperties.CoulombCount = 34.0;
            _elecModelProperties.BatteryConstant = 1;
            _elecModelProperties.Constant1 = 0.609 / 38;
            _elecModelProperties.Constant2 = 0.0032 / 38;
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
