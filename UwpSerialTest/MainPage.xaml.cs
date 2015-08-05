using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Enumeration;
using Windows.Devices.SerialCommunication;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UwpSerialTest
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void Devices_Click(object sender, RoutedEventArgs e)
        {
            this.listBox.Items.Clear();

            var selector = SerialDevice.GetDeviceSelector();
            DeviceInformationCollection devices = await DeviceInformation.FindAllAsync(selector);

            foreach (var device in devices)
            {
                this.listBox.Items.Add(device.Id);
            }
        }

        private async void ListBox_Selection(object sender, RoutedEventArgs e)
        {
            string value = (string)this.listBox.SelectedValue;

            var selector = SerialDevice.GetDeviceSelector();
            DeviceInformationCollection devices = await DeviceInformation.FindAllAsync(selector);

            foreach (var device in devices)
            {
                if (device.Id == value)
                {
                    SerialDevice port = await SerialDevice.FromIdAsync(value);
                    if (port == null)
                    {
                        this.Status.Text = "FromIdAsyncReturned null";
                    }
                    else
                    {
                        this.Status.Text = string.Format("Opened port {0}", port.PortName);
                        port.Dispose();
                    }
                }
            }
        }
    }
}
