
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.UI.Core;

namespace Ethereality.DataModels.Battery
{
    //    typedef union _group_64 {
    //float data_fp[2]{get;set;}
    //unsigned char data_u8[8]{get;set;}
    //char data_8[8]{get;set;}
    //unsigned int data_u16[4]{get;set;}
    //int data_16[4]{get;set;}
    //unsigned long data_u32[2]{get;set;}
    //long data_32[2]{get;set;}
    //} group_64{get;set;}

    public enum CmuId : byte
    {
        Cmu1Status = 1,
        Cmu1Volt0 = 2,
        Cmu1Volt1 = 3,

        Cmu2Status = 4,
        Cmu2Volt0 = 5,
        Cmu2Volt1 = 6,

        Cmu3Status = 7,
        Cmu3Volt0 = 8,
        Cmu3Volt1 = 9,

        Cmu4Status = 10,
        Cmu4Volt0 = 11,
        Cmu4Volt1 = 12,

        Cmu5Status = 13,
        Cmu5Volt0 = 14,
        Cmu5Volt1 = 15,
    }

    //public struct CmuStatus
    //{
    //    public UInt32 SerialNumber { get; set; }
    //    public Int16 PcbTemp { get; set; }
    //    public Int16 CellTemp { get; set; }
    //    public Int16 Cell0 { get; set; }
    //    public Int16 Cell1 { get; set; }
    //    public Int16 Cell2 { get; set; }
    //    public Int16 Cell3 { get; set; }
    //    public Int16 Cell4 { get; set; }
    //    public Int16 Cell5 { get; set; }
    //    public Int16 Cell6 { get; set; }
    //    public Int16 Cell7 { get; set; }

    //}

    public struct CmuStatus
    {
        public UInt32 SerialNumber { get; set; }

        public Int16 PcbTemp { get; set; }

        public Int16 CellTemp { get; set; }
    }

    public struct Cell0to3
    {
        public Int16 Cell0 { get; set; }

        public Int16 Cell1 { get; set; }

        public Int16 Cell2 { get; set; }

        public Int16 Cell3 { get; set; }
    }

    public struct Cell4to7
    {
        public Int16 Cell4 { get; set; }

        public Int16 Cell5 { get; set; }

        public Int16 Cell6 { get; set; }

        public Int16 Cell7 { get; set; }
    }

    public class CmuModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        #region Cmu1Status

        /// <summary>
        /// The <see cref="Cmu1Status" /> property's name.
        /// </summary>
        public const string Cmu1StatusPropertyName = "Cmu1Status";

        private CmuStatus _cmu1Status;

        /// <summary>
        /// Sets and gets the Cmu1 property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public CmuStatus Cmu1Status
        {
            get
            {
                return _cmu1Status;
            }

            set
            {
                if (_cmu1Status.Equals(value))
                {
                    return;
                }

               
                _cmu1Status = value;
                NotifyPropertyChanged(Cmu1StatusPropertyName);
            }
        }

        #endregion Cmu1Status

        #region Cmu2Status

        /// <summary>
        /// The <see cref="Cmu2" /> property's name.
        /// </summary>
        public const string Cmu2PropertyName = "Cmu2Status";

        private CmuStatus _cmu2Status;

        /// <summary>
        /// Sets and gets the Cmu2 property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public CmuStatus Cmu2Status
        {
            get
            {
                return _cmu2Status;
            }

            set
            {
                if (_cmu2Status.Equals(value))
                {
                    return;
                }

 
                _cmu2Status = value;
                NotifyPropertyChanged(Cmu2PropertyName);
            }
        }

        #endregion Cmu2Status

        #region Cmu3Status

        /// <summary>
        /// The <see cref="Cmu3" /> property's name.
        /// </summary>
        public const string Cmu3PropertyName = "Cmu3Status";

        private CmuStatus _cmu3Status;

        /// <summary>
        /// Sets and gets the Cmu3 property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public CmuStatus Cmu3Status
        {
            get
            {
                return _cmu3Status;
            }

            set
            {
                if (_cmu3Status.Equals(value))
                {
                    return;
                }

          
                _cmu3Status = value;
                NotifyPropertyChanged(Cmu3PropertyName);
            }
        }

        #endregion Cmu3Status

        #region Cmu4Status

        /// <summary>
        /// The <see cref="Cmu4Status" /> property's name.
        /// </summary>
        public const string Cmu4StatusPropertyName = "Cmu4Status";

        private CmuStatus _cmu4Status;

        /// <summary>
        /// Sets and gets the Cmu4 property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public CmuStatus Cmu4Status
        {
            get
            {
                return _cmu4Status;
            }

            set
            {
                if (_cmu4Status.Equals(value))
                {
                    return;
                }

           
                _cmu4Status = value;
                NotifyPropertyChanged(Cmu4StatusPropertyName);
            }
        }

        #endregion Cmu4Status

        #region Cmu5Status

        /// <summary>
        /// The <see cref="Cmu5" /> property's name.
        /// </summary>
        public const string Cmu5PropertyName = "Cmu5Status";

        private CmuStatus _cmu5Status;

        /// <summary>
        /// Sets and gets the Cmu5 property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public CmuStatus Cmu5Status
        {
            get
            {
                return _cmu5Status;
            }

            set
            {
                if (_cmu5Status.Equals(value))
                {
                    return;
                }

          
                _cmu5Status = value;
                NotifyPropertyChanged(Cmu5PropertyName);
            }
        }

        #endregion Cmu5Status

        #region Cmu1_0C3

        /// <summary>
        /// The <see cref="Cmu1_0C3" /> property's name.
        /// </summary>
        public const string Cmu1_0C3PropertyName = "Cmu1_0C3";

        private Cell0to3 _cmu1_0C3;

        /// <summary>
        /// Sets and gets the Cmu1_0C3 property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public Cell0to3 Cmu1_0C3
        {
            get
            {
                return _cmu1_0C3;
            }

            set
            {
                if (_cmu1_0C3.Equals(value))
                {
                    return;
                }

    
                _cmu1_0C3 = value;
                NotifyPropertyChanged(Cmu1_0C3PropertyName);
            }
        }

        #endregion Cmu1_0C3

        #region Cmu2_0C3

        /// <summary>
        /// The <see cref="Cmu2_0C3" /> property's name.
        /// </summary>
        public const string Cmu2_0C3PropertyName = "Cmu2_0C3";

        private Cell0to3 _cmu2_0C3;

        /// <summary>
        /// Sets and gets the Cmu2_0C3 property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public Cell0to3 Cmu2_0C3
        {
            get
            {
                return _cmu2_0C3;
            }

            set
            {
                if (_cmu2_0C3.Equals(value))
                {
                    return;
                }

         
                _cmu2_0C3 = value;
                NotifyPropertyChanged(Cmu2_0C3PropertyName);
            }
        }

        #endregion Cmu2_0C3

        #region Cmu3_0C3

        /// <summary>
        /// The <see cref="Cmu3_0C3" /> property's name.
        /// </summary>
        public const string Cmu3_0C3PropertyName = "Cmu3_0C3";

        private Cell0to3 _cmu3_0C3;

        /// <summary>
        /// Sets and gets the Cmu3_0C3 property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public Cell0to3 Cmu3_0C3
        {
            get
            {
                return _cmu3_0C3;
            }

            set
            {
                if (_cmu3_0C3.Equals(value))
                {
                    return;
                }

         
                _cmu3_0C3 = value;
                NotifyPropertyChanged(Cmu3_0C3PropertyName);
            }
        }

        #endregion Cmu3_0C3

        #region Cmu4_0C3

        /// <summary>
        /// The <see cref="Cmu4_0C3" /> property's name.
        /// </summary>
        public const string Cmu4_0C3PropertyName = "Cmu4_0C3";

        private Cell0to3 _cmu4_0C3;

        /// <summary>
        /// Sets and gets the Cmu4_0C3 property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public Cell0to3 Cmu4_0C3
        {
            get
            {
                return _cmu4_0C3;
            }

            set
            {
                if (_cmu4_0C3.Equals(value))
                {
                    return;
                }

           
                _cmu4_0C3 = value;
                NotifyPropertyChanged(Cmu4_0C3PropertyName);
            }
        }

        #endregion Cmu4_0C3

        #region Cmu5_0C3

        /// <summary>
        /// The <see cref="Cmu5_0C3" /> property's name.
        /// </summary>
        public const string Cmu5_0C3PropertyName = "Cmu5_0C3";

        private Cell0to3 _cmu5_0C3;

        /// <summary>
        /// Sets and gets the Cmu5_0C3 property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public Cell0to3 Cmu5_0C3
        {
            get
            {
                return _cmu5_0C3;
            }

            set
            {
                if (_cmu5_0C3.Equals(value))
                {
                    return;
                }

      
                _cmu5_0C3 = value;
                NotifyPropertyChanged(Cmu5_0C3PropertyName);
            }
        }

        #endregion Cmu5_0C3

        #region Cmu1_4C7

        /// <summary>
        /// The <see cref="Cmu1_4C7" /> property's name.
        /// </summary>
        public const string Cmu1_4C7PropertyName = "Cmu1_4C7";

        private Cell4to7 _cmu1_4C7;

        /// <summary>
        /// Sets and gets the Cmu1_4C7 property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public Cell4to7 Cmu1_4C7
        {
            get
            {
                return _cmu1_4C7;
            }

            set
            {
                if (_cmu1_4C7.Equals(value))
                {
                    return;
                }

           
                _cmu1_4C7 = value;
                NotifyPropertyChanged(Cmu1_4C7PropertyName);
            }
        }

        #endregion Cmu1_4C7

        #region Cmu2_4C7

        /// <summary>
        /// The <see cref="Cmu2_4C7" /> property's name.
        /// </summary>
        public const string Cmu2_4C7PropertyName = "Cmu2_4C7";

        private Cell4to7 _cmu2_4C7;

        /// <summary>
        /// Sets and gets the Cmu2_4C7 property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public Cell4to7 Cmu2_4C7
        {
            get
            {
                return _cmu2_4C7;
            }

            set
            {
                if (_cmu2_4C7.Equals(value))
                {
                    return;
                }

            
                _cmu2_4C7 = value;
                NotifyPropertyChanged(Cmu2_4C7PropertyName);
            }
        }

        #endregion Cmu2_4C7

        #region Cmu3_4C7

        /// <summary>
        /// The <see cref="Cmu3_4C7" /> property's name.
        /// </summary>
        public const string Cmu3_4C7PropertyName = "Cmu3_4C7";

        private Cell4to7 _cmu3_4C7;

        /// <summary>
        /// Sets and gets the Cmu3_4C7 property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public Cell4to7 Cmu3_4C7
        {
            get
            {
                return _cmu3_4C7;
            }

            set
            {
                if (_cmu3_4C7.Equals(value))
                {
                    return;
                }

              
                _cmu3_4C7 = value;
                NotifyPropertyChanged(Cmu3_4C7PropertyName);
            }
        }

        #endregion Cmu3_4C7

        #region Cmu4_4C7

        /// <summary>
        /// The <see cref="Cmu4_4C7" /> property's name.
        /// </summary>
        public const string Cmu4_4C7PropertyName = "Cmu4_4C7";

        private Cell4to7 _cmu4_4C7;

        /// <summary>
        /// Sets and gets the Cmu4_4C7 property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public Cell4to7 Cmu4_4C7
        {
            get
            {
                return _cmu4_4C7;
            }

            set
            {
                if (_cmu4_4C7.Equals(value))
                {
                    return;
                }

              
                _cmu4_4C7 = value;
                NotifyPropertyChanged(Cmu4_4C7PropertyName);
            }
        }

        #endregion Cmu4_4C7

        #region Cmu5_4C7

        /// <summary>
        /// The <see cref="Cmu5_4C7" /> property's name.
        /// </summary>
        public const string Cmu5_4C7PropertyName = "Cmu5_4C7";

        private Cell4to7 _cmu5_4C7;

        /// <summary>
        /// Sets and gets the Cmu5_4C7 property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public Cell4to7 Cmu5_4C7
        {
            get
            {
                return _cmu5_4C7;
            }

            set
            {
                if (_cmu5_4C7.Equals(value))
                {
                    return;
                }

        
                _cmu5_4C7 = value;
                NotifyPropertyChanged(Cmu5_4C7PropertyName);
            }
        }

        #endregion Cmu5_4C7

        public CmuModel()
        {
            _cmu1Status = new CmuStatus();
            _cmu2Status = new CmuStatus();
            _cmu3Status = new CmuStatus();
            _cmu4Status = new CmuStatus();
            _cmu5Status = new CmuStatus();

            _cmu1_0C3 = new Cell0to3();
            _cmu2_0C3 = new Cell0to3();
            _cmu3_0C3 = new Cell0to3();
            _cmu4_0C3 = new Cell0to3();
            _cmu5_0C3 = new Cell0to3();

            _cmu1_4C7 = new Cell4to7();
            _cmu2_4C7 = new Cell4to7();
            _cmu3_4C7 = new Cell4to7();
            _cmu4_4C7 = new Cell4to7();
            _cmu5_4C7 = new Cell4to7();
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