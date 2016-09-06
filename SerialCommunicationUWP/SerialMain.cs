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

using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace SerialCommunicationUWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public class SerialMain
    {
        //public static MainPage Current;
        public static SerialMain Current;

        public SolidColorBrush StatusBorderBackground { get; private set; }
        public string StatusBlockText { get; private set; }
        public Visibility StatusBorderVisibility { get; private set; }
        public Visibility StatusPanelVisibility { get; private set; }
        public bool SplitterIsPaneOpen { get; private set; }

        public SerialMain()
        {
            //this.InitializeComponent();
            Current = this;
            // This is a static public property that allows downstream pages to get a handle to the MainPage instance
            // in order to call methods that are in this class.

            //SampleTitle.Text = FEATURE_NAME;
        }

        /// <summary>
        /// Called whenever the user changes selection in the scenarios list.  This method will navigate to the respective
        /// sample scenario page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        /// <summary>
        /// Used to display messages to the user
        /// </summary>
        /// <param name="strMessage"></param>
        /// <param name="type"></param>
        public void NotifyUser(string strMessage, NotifyType type)
        {
            switch (type)
            {
                case NotifyType.StatusMessage:
                    StatusBorderBackground = new SolidColorBrush(Windows.UI.Colors.Green);
                    break;

                case NotifyType.ErrorMessage:
                    StatusBorderBackground = new SolidColorBrush(Windows.UI.Colors.Red);
                    break;
            }
            StatusBlockText = strMessage;

            // Collapse the StatusBlock if it has no text to conserve real estate.
            StatusBorderVisibility = (StatusBlockText != String.Empty) ? Visibility.Visible : Visibility.Collapsed;
            if (StatusBlockText != String.Empty)
            {
                StatusBorderVisibility = Visibility.Visible;
                StatusPanelVisibility = Visibility.Visible;
            }
            else
            {
                StatusBorderVisibility = Visibility.Collapsed;
                StatusPanelVisibility = Visibility.Collapsed;
            }
        }

        private async void Footer_Click(object sender, RoutedEventArgs e)
        {
            await Windows.System.Launcher.LaunchUriAsync(new Uri(((HyperlinkButton)sender).Tag.ToString()));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SplitterIsPaneOpen = !SplitterIsPaneOpen;
        }
    }

    public enum NotifyType
    {
        StatusMessage,
        ErrorMessage
    };
}