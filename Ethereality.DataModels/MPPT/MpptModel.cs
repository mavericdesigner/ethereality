using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.UI.Core;

namespace Ethereality.DataModels.MPPT
{
    public enum MpptMsgNum
    {
        ArrayVolt = 0x00,
        ArrayCurrent = 0x02,
        BatteryVolt = 0x04,
        MpptTemp = 0x06
    }
    public struct MpptMsgStruct
    {
        public DateTime TimeStamp { get; set; }
        public int Voltage { get; set; }
        public int Current { get; set; }
        public int Battery { get; set; }
        public int Temperature { get; set; }
    }
    public enum PanelNum
    {
        Mppt1Num = 0x771,
        Mppt2Num = 0x772,
        Mppt3Num = 0x773,
        Mppt4Num = 0x774,
        Mppt5Num = 0x775,
        Mppt6Num = 0x776
    }

    public class MpptModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public static MpptModel CurrentMpptModel;
        #region MpptMsg0

        /// <summary>
        /// The <see cref="MpptMsg0" /> property's name.
        /// </summary>
        public const string MpptMsg0PropertyName = "MpptMsg0";

        private MpptMsgStruct _mpptMsg0;

        /// <summary>
        /// Sets and gets the MpptMsg0 property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public MpptMsgStruct MpptMsg0
        {
            get
            {
                return _mpptMsg0;
            }

            set
            {
                NotifyPropertyChanged(MpptMsg0PropertyName);
                _mpptMsg0 = value;
                NotifyPropertyChanged(MpptMsg0PropertyName);
            }
        }

        #endregion MpptMsg0

        #region MpptMsg1

        /// <summary>
        /// The <see cref="MpptMsg1" /> property's name.
        /// </summary>
        public const string MpptMsg1PropertyName = "MpptMsg1";

        private MpptMsgStruct _mpptMsg1;

        /// <summary>
        /// Sets and gets the MpptMsg1 property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>

        public MpptMsgStruct MpptMsg1
        {
            get
            {
                return _mpptMsg1;
            }

            set
            {
                _mpptMsg1 = value;
                NotifyPropertyChanged(MpptMsg1PropertyName);
            }
        }

        #endregion MpptMsg1

        #region MpptMsg2

        /// <summary>
        /// The <see cref="MpptMsg2" /> property's name.
        /// </summary>
        public const string MpptMsg2PropertyName = "MpptMsg3";

        private MpptMsgStruct _mpptMsg2;

        /// <summary>
        /// Sets and gets the MpptMsg3 property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public MpptMsgStruct MpptMsg2
        {
            get
            {
                return _mpptMsg2;
            }

            set
            {
                _mpptMsg2 = value;
                NotifyPropertyChanged(MpptMsg2PropertyName);
            }
        }

        #endregion MpptMsg2

        #region MpptMsg3

        /// <summary>
        /// The <see cref="MpptMsg3" /> property's name.
        /// </summary>
        public const string MpptMsg3PropertyName = "MpptMsg3";

        private MpptMsgStruct _mpptMsg3;

        /// <summary>
        /// Sets and gets the MpptMsg3 property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public MpptMsgStruct MpptMsg3
        {
            get
            {
                return _mpptMsg3;
            }

            set
            {
                _mpptMsg3 = value;
                NotifyPropertyChanged(MpptMsg3PropertyName);
            }
        }

        #endregion MpptMsg3

        #region MpptMsg4

        /// <summary>
        /// The <see cref="MpptMsg4" /> property's name.
        /// </summary>
        public const string MpptMsg4PropertyName = "MpptMsg4";

        private MpptMsgStruct _mpptMsg4;

        /// <summary>
        /// Sets and gets the MpptMsg4 property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public MpptMsgStruct MpptMsg4
        {
            get
            {
                return _mpptMsg4;
            }

            set
            {
                _mpptMsg4 = value;
                NotifyPropertyChanged(MpptMsg4PropertyName);
            }
        }

        #endregion MpptMsg4

        #region MpptMsg5

        /// <summary>
        /// The <see cref="MpptMsg5" /> property's name.
        /// </summary>
        public const string MpptMsg5PropertyName = "MpptMsg5";

        private MpptMsgStruct _mpptMsg5;

        /// <summary>
        /// Sets and gets the MpptMsg5 property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public MpptMsgStruct MpptMsg5
        {
            get
            {
                return _mpptMsg5;
            }

            set
            {
                _mpptMsg5 = value;
                NotifyPropertyChanged(MpptMsg5PropertyName);
            }
        }

        #endregion MpptMsg5

        public MpptModel()
        {
            CurrentMpptModel = this;
            _mpptMsg0 = new MpptMsgStruct();
            _mpptMsg1 = new MpptMsgStruct();
            _mpptMsg2 = new MpptMsgStruct();
            _mpptMsg3 = new MpptMsgStruct();
            _mpptMsg4 = new MpptMsgStruct();
            _mpptMsg5 = new MpptMsgStruct();
        }

        public long MpptModelId { get; set; }

        private async void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
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