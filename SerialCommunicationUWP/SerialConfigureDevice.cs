//*********************************************************
//
// Copyright (c) Microsoft. All rights reserved.
// This code is licensed under the MIT License (MIT).
// THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
// ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
// IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
// PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
//
//*********************************************************

using System.Collections.Generic;
using Windows.Devices.SerialCommunication;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace SerialCommunicationUWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public class SerialConfigureDevice
    {
        // A pointer back to the main page.  This is needed if you want to call methods in MainPage such
        // as NotifyUser()
        private SerialMain SerialMain = SerialMain.Current;

        public string BaudRateValue { get; private set; }
        public string BaudRateInputValue { get; private set; }
        public string ParityValue { get; private set; }
        public object SelectedIndex { get; private set; }
        public string StopBitCountValue { get; private set; }
        public object StopBitCountComboBox { get; private set; }
        public string BreakStateSignalValue { get; private set; }
        public bool BreakStateSignalToggleSwitch { get; private set; }
        public string DataTerminalReadyEnabledValue { get; private set; }
        public bool DataTerminalReadyEnabledToggleSwitch { get; private set; }
        public string RequestToSendEnabledValue { get; private set; }
        public bool RequestToSendEnabledToggleSwitch { get; private set; }
        public string CarrierDetectStateValue { get; private set; }
        public string DataSetReadyStateValue { get; private set; }
        public string DataBitsValue { get; private set; }
        public string HandShakeValue { get; private set; }
        public List<object> ParityComboBoxItems { get; private set; }
        public object ParityComboBoxSelectedItem { get; private set; }
        public object StopBitCountComboBoxSelectedIndex { get; private set; }
        public List<object> StopBitCountComboBoxItems { get; private set; }
        public ushort DataBitsComboBoxSelectedIndex { get; private set; }
        public object DataBitsComboBoxSelectedValue { get; private set; }
        public List<object> HandShakeComboBoxItems { get; private set; }
        public object HandShakeComboBoxSelectedIndex { get; private set; }
        public object StopBitCountComboBoxSelectedItem { get; private set; }
        public object HandShakeComboBoxSelectedItem { get; private set; }
        public Visibility SerialConfigureDeviceVisibility { get; private set; }

        public SerialConfigureDevice()
        {
            Initialize();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        ///
        /// We will enable/disable parts of the UI if the device doesn't support it.
        /// </summary>
        /// <param name="eventArgs">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        private void Initialize()
        {
            if (EventHandlerForDevice.Current.Device == null)
            {
                SerialConfigureDeviceVisibility = Visibility.Collapsed;
                SerialMain.NotifyUser("Device is not connected", NotifyType.ErrorMessage);
            }
            else
            {
                if (EventHandlerForDevice.Current.Device.PortName != "")
                {
                    SerialMain.NotifyUser("Connected to " + EventHandlerForDevice.Current.Device.PortName,
                                                NotifyType.StatusMessage);
                }

                UpdateBaudRateView();
                UpdateParityView();
                UpdateHandShakeView();
                UpdateBreakStateSignalView();
                UpdateCarrierDetectStateView();
                UpdateDataBitsView();
                UpdateDataSetReadyStateView();
                UpdateDataTerminalReadyEnabledView();
                UpdateRequestToSendEnabledView();
                UpdateStopBitCountView();
            }
        }

        private void UpdateBaudRateView()
        {
            BaudRateValue = EventHandlerForDevice.Current.Device.BaudRate.ToString();
        }

        private void BaudRateSetButton_Click(object sender, RoutedEventArgs e)
        {
            uint BaudRateInput = uint.Parse(BaudRateInputValue);
            if (BaudRateInput != 0)
            {
                EventHandlerForDevice.Current.Device.BaudRate = BaudRateInput;
            }
            UpdateBaudRateView();
        }

        private void UpdateParityView()
        {
            ParityValue = EventHandlerForDevice.Current.Device.Parity.ToString();
            SelectedIndex = ParityComboBoxItems.IndexOf(ParityValue);
        }

        private void ParityComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ParityComboBoxSelectedItem.Equals("None"))
            {
                EventHandlerForDevice.Current.Device.Parity = SerialParity.None;
            }
            else if (ParityComboBoxSelectedItem.Equals("Even"))
            {
                EventHandlerForDevice.Current.Device.Parity = SerialParity.Even;
            }
            else if (ParityComboBoxSelectedItem.Equals("Odd"))
            {
                EventHandlerForDevice.Current.Device.Parity = SerialParity.Odd;
            }
            else if (ParityComboBoxSelectedItem.Equals("Mark"))
            {
                EventHandlerForDevice.Current.Device.Parity = SerialParity.Mark;
            }
            else if (ParityComboBoxSelectedItem.Equals("Space"))
            {
                EventHandlerForDevice.Current.Device.Parity = SerialParity.Space;
            }
            UpdateParityView();
        }

        private void UpdateStopBitCountView()
        {
            StopBitCountValue = EventHandlerForDevice.Current.Device.StopBits.ToString();
            if (StopBitCountValue.Equals("None") == false)
            {
                StopBitCountComboBoxSelectedIndex = StopBitCountComboBoxItems.IndexOf(StopBitCountValue);
            }
        }

        private void StopBitCountComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (StopBitCountComboBoxSelectedItem.Equals("One"))
            {
                EventHandlerForDevice.Current.Device.StopBits = SerialStopBitCount.One;
            }
            else if (StopBitCountComboBoxSelectedItem.Equals("OnePointFive"))
            {
                EventHandlerForDevice.Current.Device.StopBits = SerialStopBitCount.OnePointFive;
            }
            else if (StopBitCountComboBoxSelectedItem.Equals("Two"))
            {
                EventHandlerForDevice.Current.Device.StopBits = SerialStopBitCount.Two;
            }
            UpdateStopBitCountView();
        }

        private void UpdateHandShakeView()
        {
            HandShakeValue = EventHandlerForDevice.Current.Device.Handshake.ToString();
            HandShakeComboBoxSelectedIndex = HandShakeComboBoxItems.IndexOf(HandShakeValue);
        }

        private void HandShakeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (HandShakeComboBoxSelectedItem.Equals("None"))
            {
                EventHandlerForDevice.Current.Device.Handshake = SerialHandshake.None;
            }
            else if (HandShakeComboBoxSelectedItem.Equals("RequestToSend"))
            {
                EventHandlerForDevice.Current.Device.Handshake = SerialHandshake.RequestToSend;
            }
            else if (HandShakeComboBoxSelectedItem.Equals("XOnXOff"))
            {
                EventHandlerForDevice.Current.Device.Handshake = SerialHandshake.RequestToSendXOnXOff;
            }
            else if (HandShakeComboBoxSelectedItem.Equals("RequestToSendXOnXOff"))
            {
                EventHandlerForDevice.Current.Device.Handshake = SerialHandshake.RequestToSendXOnXOff;
            }
            UpdateHandShakeView();
        }

        private void UpdateBreakStateSignalView()
        {
            bool currentState = EventHandlerForDevice.Current.Device.BreakSignalState;
            if (currentState)
            {
                BreakStateSignalValue = " On";
            }
            else
            {
                BreakStateSignalValue = " Off";
            }
            BreakStateSignalToggleSwitch = currentState;
        }

        private void BreakStateSignalToggleSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            EventHandlerForDevice.Current.Device.BreakSignalState = BreakStateSignalToggleSwitch;
            UpdateBreakStateSignalView();
        }

        private void UpdateDataTerminalReadyEnabledView()
        {
            bool currentState = EventHandlerForDevice.Current.Device.IsDataTerminalReadyEnabled;
            if (currentState)
            {
                DataTerminalReadyEnabledValue = "On";
            }
            else
            {
                DataTerminalReadyEnabledValue = "Off";
            }
            DataTerminalReadyEnabledToggleSwitch = currentState;
        }

        private void DataTerminalReadyEnabledToggleSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            EventHandlerForDevice.Current.Device.IsDataTerminalReadyEnabled = DataTerminalReadyEnabledToggleSwitch;
            UpdateDataTerminalReadyEnabledView();
        }

        private void UpdateRequestToSendEnabledView()
        {
            bool currentState = EventHandlerForDevice.Current.Device.IsRequestToSendEnabled;
            if (currentState)
            {
                RequestToSendEnabledValue = "On";
            }
            else
            {
                RequestToSendEnabledValue = "Off";
            }
            RequestToSendEnabledToggleSwitch = currentState;
        }

        private void RequestToSendEnabledToggleSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            EventHandlerForDevice.Current.Device.IsRequestToSendEnabled = RequestToSendEnabledToggleSwitch;
            UpdateRequestToSendEnabledView();
        }

        private void UpdateCarrierDetectStateView()
        {
            if (EventHandlerForDevice.Current.Device.CarrierDetectState)
            {
                CarrierDetectStateValue = "On";
            }
            else
            {
                CarrierDetectStateValue = "Off";
            }
        }

        private void UpdateDataSetReadyStateView()
        {
            if (EventHandlerForDevice.Current.Device.DataSetReadyState)
            {
                DataSetReadyStateValue = "On";
            }
            else
            {
                DataSetReadyStateValue = "Off";
            }
        }

        private void UpdateDataBitsView()
        {
            ushort dataBits = EventHandlerForDevice.Current.Device.DataBits;
            DataBitsValue = dataBits.ToString();
            if (dataBits <= 8)
            {
                DataBitsComboBoxSelectedIndex = dataBits;
            }
        }

        private void DataBitsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EventHandlerForDevice.Current.Device.DataBits = ushort.Parse(DataBitsComboBoxSelectedValue.ToString());
            UpdateDataBitsView();
        }

        public void ShortConfig()
        {
            if (EventHandlerForDevice.Current.Device == null)
            {
                SerialConfigureDeviceVisibility = Visibility.Collapsed;
                SerialMain.NotifyUser("Device is not connected", NotifyType.ErrorMessage);
            }
            else
            {
                if (EventHandlerForDevice.Current.Device.PortName != "")
                {
                    SerialMain.NotifyUser("Connected to " + EventHandlerForDevice.Current.Device.PortName,
                                                NotifyType.StatusMessage);
                }
                EventHandlerForDevice.Current.Device.BaudRate = 57600;
                EventHandlerForDevice.Current.Device.DataBits = 8;
                EventHandlerForDevice.Current.Device.Handshake = SerialHandshake.None;
                EventHandlerForDevice.Current.Device.Parity = SerialParity.Odd;
                EventHandlerForDevice.Current.Device.StopBits = SerialStopBitCount.One;
            }
        }
    }
}