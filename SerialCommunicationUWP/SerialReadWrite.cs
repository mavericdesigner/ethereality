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
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace SerialCommunicationUWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>

    public class SerialReadWrite : IDisposable, INotifyPropertyChanged
    {
        // A pointer back to the main page.  This is needed if you want to call methods in MainPage such
        // as NotifyUser()
        private SerialMain SerialMain = SerialMain.Current;

        private Timer readTimer;

        // Track Read Operation
        private CancellationTokenSource ReadCancellationTokenSource;

        public event PropertyChangedEventHandler PropertyChanged;

        private Object ReadCancelLock = new Object();

        private Boolean IsReadTaskPending;
        private uint ReadBytesCounter = 0;
        private DataReader DataReaderObject = null;

        // Track Write Operation
        private CancellationTokenSource WriteCancellationTokenSource;

        private Object WriteCancelLock = new Object();

        private Boolean IsWriteTaskPending;
        private uint WriteBytesCounter = 0;
        private DataWriter DataWriteObject = null;

        private bool WriteBytesAvailable = false;

        // Indicate if we navigate away from this page or not.
        private Boolean IsNavigatedAway;

        public Visibility ReadWriteScollViewerVisibility { get; private set; }

        public string WriteBytesCounterValueText { get; private set; }
        public string WriteTimeoutValueText { get; private set; }
        public string ReadBytesCounterValueText { get; private set; }
        public string ReadTimeoutValueText { get; private set; }
        public string ReadTimeoutInputValueText { get; private set; }
        public string WriteTimeoutInputValueText { get; private set; }
        public bool ReadCancelButtonIsEnabled { get; private set; }
        public bool ReadButtonIsEnabled { get; private set; }
        public bool ReadTimeoutInputValueIsEnabled { get; private set; }
        public bool ReadTimeoutButtonIsEnabled { get; private set; }
        public bool WriteTimeoutButtonIsEnabled { get; private set; }
        public bool WriteButtonIsEnabled { get; private set; }
        public bool WriteTimeoutInputValueIsEnabled { get; private set; }
        public bool WriteCancelButtonIsEnabled { get; private set; }
        public bool WriteBytesInputValueIsEnabled { get; private set; }

        private float floatTest;

        public float FloatTest
        {
            get { return floatTest; }
            set
            {
                if (floatTest.Equals(value))
                {
                    return;
                }
                floatTest = value;
                NotifyPropertyChanged("FloatTest");
            }
        }

        private ConcurrentQueue<byte> _readSerialQueue = new ConcurrentQueue<byte>();

        public ConcurrentQueue<byte> ReadSerialQueue
        {
            get { return _readSerialQueue; }
            set
            {
                if (_readSerialQueue.Equals(value))
                {
                    return;
                }
                _readSerialQueue = value;
                NotifyPropertyChanged("SerialReadQueue");
            }
        }

        private ConcurrentQueue<byte> _writeSerialQueue = new ConcurrentQueue<byte>();

        public ConcurrentQueue<byte> WriteSerialQueue
        {
            get { return _writeSerialQueue; }
            set
            {
                if (_writeSerialQueue.Equals(value))
                {
                    return;
                }
                _writeSerialQueue = value;
                NotifyPropertyChanged("SerialWriteQueue");
            }
        }

        public string ReadBytesTextBlockText { get; private set; }
        public string WriteBytesInputValueText { get; private set; }
        public string WriteBytesTextBlockText { get; private set; }

        public SerialReadWrite()
        {
        }

        public void Dispose()
        {
            if (ReadCancellationTokenSource != null)
            {
                ReadCancellationTokenSource.Dispose();
                ReadCancellationTokenSource = null;
            }

            if (WriteCancellationTokenSource != null)
            {
                WriteCancellationTokenSource.Dispose();
                WriteCancellationTokenSource = null;
            }
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        ///
        /// We will enable/disable parts of the UI if the device doesn't support it.
        /// </summary>
        /// <param name="eventArgs">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        //protected override void OnNavigatedTo(NavigationEventArgs eventArgs)
        public void Initialize()
        {
            IsNavigatedAway = false;
            if (EventHandlerForDevice.Current.Device == null)
            {
                ReadWriteScollViewerVisibility = Visibility.Collapsed;
                SerialMain.NotifyUser("Device is not connected", NotifyType.ErrorMessage);
            }
            else
            {
                if (EventHandlerForDevice.Current.Device.PortName != "")
                {
                    SerialMain.NotifyUser("Connected to " + EventHandlerForDevice.Current.Device.PortName,
                                                NotifyType.StatusMessage);
                }

                // So we can reset future tasks

                ResetReadCancellationTokenSource();
                ResetWriteCancellationTokenSource();

                UpdateReadButtonStates();
                UpdateWriteButtonStates();

                UpdateReadBytesCounterView();
                UpdateReadTimeoutView();

                UpdateWriteBytesCounterView();
                UpdateWriteTimeoutView();
            }
        }

        /// <summary>
        /// Cancel any on going tasks when navigating away from the page so the device is in a consistent state throughout
        /// all the scenarios
        /// </summary>
        /// <param name="eventArgs"></param>
        //protected override void OnNavigatedFrom(NavigationEventArgs eventArgs)
        public void OnNavigatedForm(NavigationEventArgs eventArgs)
        {
            IsNavigatedAway = true;

            CancelAllIoTasks();
        }

        private void UpdateWriteBytesCounterView()
        {
            WriteBytesCounterValueText = " " + WriteBytesCounter.ToString() + " bytes";
        }

        private void UpdateWriteTimeoutView()
        {
            WriteTimeoutValueText = EventHandlerForDevice.Current.Device.WriteTimeout.TotalMilliseconds.ToString();
        }

        private void UpdateReadBytesCounterView()
        {
            ReadBytesCounterValueText = " " + ReadBytesCounter.ToString() + " bytes";
        }

        private void UpdateReadTimeoutView()
        {
            ReadTimeoutValueText = EventHandlerForDevice.Current.Device.ReadTimeout.TotalMilliseconds.ToString();
        }

        private void ReadTimeoutButton_Click(object sender, RoutedEventArgs e)
        {
            int ReadTimeoutInput = int.Parse(ReadTimeoutInputValueText);
            EventHandlerForDevice.Current.Device.ReadTimeout = new System.TimeSpan(ReadTimeoutInput * 10000);
            UpdateReadTimeoutView();
        }

        private void WriteTimeoutButton_Click(object sender, RoutedEventArgs e)
        {
            int WriteTimeoutInput = int.Parse(WriteTimeoutInputValueText);
            EventHandlerForDevice.Current.Device.WriteTimeout = new System.TimeSpan(WriteTimeoutInput * 10000);
            UpdateWriteTimeoutView();
        }

        public async void ReadButton_Click()
        {
            if (EventHandlerForDevice.Current.IsDeviceConnected)
            {
                // AutoResetEvent autoEvent = (AutoResetEvent)stateinfo;
                try
                {
                    SerialMain.NotifyUser("Reading...", NotifyType.StatusMessage);

                    // We need to set this to true so that the buttons can be updated to disable the read button. We will not be able to
                    // update the button states until after the read completes.
                    IsReadTaskPending = true;
                    DataReaderObject = new DataReader(EventHandlerForDevice.Current.Device.InputStream);

                    UpdateReadButtonStates();
                    await ReadAsync(ReadCancellationTokenSource.Token);
                }
                catch (OperationCanceledException /*exception*/)
                {
                    NotifyReadTaskCanceled();
                }
                catch (Exception exception)
                {
                    SerialMain.NotifyUser(exception.Message.ToString(), NotifyType.ErrorMessage);
                    Debug.WriteLine(exception.Message.ToString());
                }
                finally
                {
                    IsReadTaskPending = false;
                    DataReaderObject.DetachStream();
                    DataReaderObject = null;
                    UpdateReadButtonStates();
                    //  autoEvent.Set();
                }
            }
            else
            {
                Utilities.NotifyDeviceNotConnected();
            }
        }

        public async void WriteButton_Click()
        {
            if (EventHandlerForDevice.Current.IsDeviceConnected)
            {
                try
                {
                    SerialMain.NotifyUser("Writing...", NotifyType.StatusMessage);

                    // We need to set this to true so that the buttons can be updated to disable the write button. We will not be able to
                    // update the button states until after the write completes.
                    IsWriteTaskPending = true;
                    DataWriteObject = new DataWriter(EventHandlerForDevice.Current.Device.OutputStream);
                    UpdateWriteButtonStates();

                    await WriteAsync(WriteCancellationTokenSource.Token);
                }
                catch (OperationCanceledException /*exception*/)
                {
                    NotifyWriteTaskCanceled();
                }
                catch (Exception exception)
                {
                    SerialMain.NotifyUser(exception.Message.ToString(), NotifyType.ErrorMessage);
                    Debug.WriteLine(exception.Message.ToString());
                }
                finally
                {
                    IsWriteTaskPending = false;
                    DataWriteObject.DetachStream();
                    DataWriteObject = null;

                    UpdateWriteButtonStates();
                }
            }
            else
            {
                Utilities.NotifyDeviceNotConnected();
            }
        }

        /// <summary>
        /// Allow for one operation at a time
        /// </summary>
        private void UpdateReadButtonStates()
        {
            if (IsPerformingRead())
            {
                ReadButtonIsEnabled = false;
                ReadCancelButtonIsEnabled = true;
                ReadTimeoutButtonIsEnabled = false;
                ReadTimeoutInputValueIsEnabled = false;
            }
            else
            {
                ReadButtonIsEnabled = true;
                ReadCancelButtonIsEnabled = false;
                ReadTimeoutButtonIsEnabled = true;
                ReadTimeoutInputValueIsEnabled = true;
            }
        }

        private void UpdateWriteButtonStates()
        {
            if (IsPerformingWrite())
            {
                WriteButtonIsEnabled = false;
                WriteCancelButtonIsEnabled = true;
                WriteTimeoutButtonIsEnabled = false;
                WriteTimeoutInputValueIsEnabled = false;
                WriteBytesInputValueIsEnabled = false;
            }
            else
            {
                WriteButtonIsEnabled = true;
                WriteCancelButtonIsEnabled = false;
                WriteTimeoutButtonIsEnabled = true;
                WriteTimeoutInputValueIsEnabled = true;
                WriteBytesInputValueIsEnabled = true;
            }
        }

        public void TimedAsyncRead()
        {
            //  var autoEvent = new AutoResetEvent(false);
            ////  var stateTimer = new Timer(ReadButton_Click, autoEvent, 0, 50);
            //  autoEvent.WaitOne();
            //  stateTimer.Dispose();
        }

        private async Task ReadAsync(CancellationToken cancellationToken)
        {
            Task<UInt32> loadAsyncTask;

            uint ReadBufferLength = 1024;

            // Don't start any IO if we canceled the task
            lock (ReadCancelLock)
            {
                cancellationToken.ThrowIfCancellationRequested();

                // Cancellation Token will be used so we can stop the task operation explicitly
                // The completion function should still be called so that we can properly handle a canceled task
                DataReaderObject.InputStreamOptions = InputStreamOptions.Partial;

                loadAsyncTask = DataReaderObject.LoadAsync(ReadBufferLength).AsTask(cancellationToken);
            }
            loadAsyncTask.Wait();
            UInt32 bytesRead = loadAsyncTask.GetAwaiter().GetResult();

            int k = 0;
            if (bytesRead > 0)
            {
                byte[] temp = new byte[bytesRead];
                DataReaderObject.ReadBytes(temp);

                ReadSerialQueue = new ConcurrentQueue<byte>(temp);
                NotifyPropertyChanged("ReadSerialQueue");
            }

            // SerialMain.NotifyUser("Read completed - " + bytesRead.ToString() + " bytes were read", NotifyType.StatusMessage);
        }

        private async Task WriteAsync(CancellationToken cancellationToken)
        {
            Task<UInt32> storeAsyncTask;

            //if ((WriteBytesAvailable) && (WriteBytesInputValueText.Length != 0))
            //{
            //char[] buffer = new char[WriteBytesInputValueText.Length];
            //WriteBytesInputValueText.CopyTo(0, buffer, 0, WriteBytesInputValueText.Length);
            //String InputString = new string(buffer);
            DataWriteObject.WriteByte(224);
            WriteBytesInputValueText = "";

            // Don't start any IO if we canceled the task
            lock (WriteCancelLock)
            {
                cancellationToken.ThrowIfCancellationRequested();

                // Cancellation Token will be used so we can stop the task operation explicitly
                // The completion function should still be called so that we can properly handle a canceled task
                storeAsyncTask = DataWriteObject.StoreAsync().AsTask(cancellationToken);
            }
            storeAsyncTask.GetAwaiter().GetResult();
            //UInt32 bytesWritten = await storeAsyncTask;
            //if (bytesWritten > 0)
            //{
            //    WriteBytesTextBlockText += InputString.Substring(0, (int)bytesWritten) + '\n';
            //    WriteBytesCounter += bytesWritten;
            //    UpdateWriteBytesCounterView();
            //}
            //SerialMain.NotifyUser("Write completed - " + bytesWritten.ToString() + " bytes written", NotifyType.StatusMessage);
            //}
            //else
            //{
            //    SerialMain.NotifyUser("No input received to write", NotifyType.StatusMessage);
            //}
        }

        /// <summary>
        /// It is important to be able to cancel tasks that may take a while to complete. Cancelling tasks is the only way to stop any pending IO
        /// operations asynchronously. If the Serial Device is closed/deleted while there are pending IOs, the destructor will cancel all pending IO
        /// operations.
        /// </summary>
        ///

        private void CancelReadTask()
        {
            lock (ReadCancelLock)
            {
                if (ReadCancellationTokenSource != null)
                {
                    if (!ReadCancellationTokenSource.IsCancellationRequested)
                    {
                        ReadCancellationTokenSource.Cancel();

                        // Existing IO already has a local copy of the old cancellation token so this reset won't affect it
                        ResetReadCancellationTokenSource();
                    }
                }
            }
        }

        private void CancelWriteTask()
        {
            lock (WriteCancelLock)
            {
                if (WriteCancellationTokenSource != null)
                {
                    if (!WriteCancellationTokenSource.IsCancellationRequested)
                    {
                        WriteCancellationTokenSource.Cancel();

                        // Existing IO already has a local copy of the old cancellation token so this reset won't affect it
                        ResetWriteCancellationTokenSource();
                    }
                }
            }
        }

        private void CancelAllIoTasks()
        {
            CancelReadTask();
            CancelWriteTask();
        }

        /// <summary>
        /// Determines if we are reading, writing, or reading and writing.
        /// </summary>
        /// <returns>If we are doing any of the above operations, we return true; false otherwise</returns>
        private Boolean IsPerformingRead()
        {
            return (IsReadTaskPending);
        }

        private Boolean IsPerformingWrite()
        {
            return (IsWriteTaskPending);
        }

        private void ResetReadCancellationTokenSource()
        {
            // Create a new cancellation token source so that can cancel all the tokens again
            ReadCancellationTokenSource = new CancellationTokenSource();

            // Hook the cancellation callback (called whenever Task.cancel is called)
            ReadCancellationTokenSource.Token.Register(() => NotifyReadCancelingTask());
        }

        private void ResetWriteCancellationTokenSource()
        {
            // Create a new cancellation token source so that can cancel all the tokens again
            WriteCancellationTokenSource = new CancellationTokenSource();

            // Hook the cancellation callback (called whenever Task.cancel is called)
            WriteCancellationTokenSource.Token.Register(() => NotifyWriteCancelingTask());
        }

        /// <summary>
        /// Print a status message saying we are canceling a task and disable all buttons to prevent multiple cancel requests.
        /// <summary>
        private async void NotifyReadCancelingTask()
        {
            // Setting the dispatcher priority to high allows the UI to handle disabling of all the buttons
            // before any of the IO completion callbacks get a chance to modify the UI; that way this method
            // will never get the opportunity to overwrite UI changes made by IO callbacks
            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.High,
                new DispatchedHandler(() =>
                {
                    ReadButtonIsEnabled = false;
                    ReadCancelButtonIsEnabled = false;

                    if (!IsNavigatedAway)
                    {
                        SerialMain.NotifyUser("Canceling Read... Please wait...", NotifyType.StatusMessage);
                    }
                }));
        }

        private async void NotifyWriteCancelingTask()
        {
            // Setting the dispatcher priority to high allows the UI to handle disabling of all the buttons
            // before any of the IO completion callbacks get a chance to modify the UI; that way this method
            // will never get the opportunity to overwrite UI changes made by IO callbacks
            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.High,
                new DispatchedHandler(() =>
                {
                    WriteButtonIsEnabled = false;
                    WriteCancelButtonIsEnabled = false;

                    if (!IsNavigatedAway)
                    {
                        SerialMain.NotifyUser("Canceling Write... Please wait...", NotifyType.StatusMessage);
                    }
                }));
        }

        /// <summary>
        /// Notifies the UI that the operation has been cancelled
        /// </summary>
        private async void NotifyReadTaskCanceled()
        {
            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                new DispatchedHandler(() =>
                {
                    if (!IsNavigatedAway)
                    {
                        SerialMain.NotifyUser("Read request has been cancelled", NotifyType.StatusMessage);
                    }
                }));
        }

        private async void NotifyWriteTaskCanceled()
        {
            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                new DispatchedHandler(() =>
                {
                    if (!IsNavigatedAway)
                    {
                        SerialMain.NotifyUser("Write request has been cancelled", NotifyType.StatusMessage);
                    }
                }));
        }

        private void ReadCancelButton_Click(object sender, RoutedEventArgs e)
        {
            if (EventHandlerForDevice.Current.IsDeviceConnected)
            {
                CancelReadTask();
            }
            else
            {
                Utilities.NotifyDeviceNotConnected();
            }
        }

        private void WriteCancelButton_Click(object sender, RoutedEventArgs e)
        {
            if (EventHandlerForDevice.Current.IsDeviceConnected)
            {
                CancelWriteTask();
            }
            else
            {
                Utilities.NotifyDeviceNotConnected();
            }
        }

        private void WriteBytesInputValue_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (WriteBytesAvailable == false)
            {
                WriteBytesInputValueText = "";
            }
            WriteBytesAvailable = true;
        }

        private void WriteBytesInputValue_GotFocus(object sender, RoutedEventArgs e)
        {
            if (WriteBytesAvailable == false)
            {
                WriteBytesInputValueText = "";
            }
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