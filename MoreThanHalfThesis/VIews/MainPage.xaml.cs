using MoreThanHalfThesis.VIews;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MoreThanHalfThesis
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private DispatcherTimer timer;
        public MainPage()
        {
            this.InitializeComponent();
            timer = new DispatcherTimer();
            timer.Tick += timer_Tick;
            timer.Interval = TimeSpan.FromSeconds(30);

            this.Loaded += async (sender, e) =>
            {
                await Dispatcher.RunAsync(CoreDispatcherPriority.Low, () =>
                {
                    UpdateDateTime();

                    timer.Start();
                });
            };
            this.Unloaded += (sender, e) =>
            {
                timer.Stop();
            };
        }
        private void timer_Tick(object sender, object e)
        {
            UpdateDateTime();
        }

        private void UpdateDateTime()
        {
            Utils.SYSTEMTIME tTime;

            Utils.NativeTimeMethod.GetLocalTime(out tTime);
            this.CurrentTime.Text = tTime.ToDateTime().ToString("t", CultureInfo.CurrentCulture) + Environment.NewLine + tTime.ToDateTime().ToString("d", CultureInfo.CurrentCulture);
        }
        private void ShutdownHelper(ShutdownKind kind)
        {
            ShutdownManager.BeginShutdown(kind, TimeSpan.FromSeconds(0.5));
        }

        private void ShutdownButton_Clicked(object sender, RoutedEventArgs e)
        {
            ShutdownDropdown.IsOpen = true;
        }
        private void ShutdownDropdown_Opened(object sender, object e)
        {
            var w = ShutdownListView.ActualWidth;
            if (w == 0)
            {
                // trick to recalculate the size of the dropdown
                ShutdownDropdown.IsOpen = false;
                ShutdownDropdown.IsOpen = true;
            }
            var offset = -(ShutdownListView.ActualWidth - ShutdownButton.ActualWidth);
            ShutdownDropdown.HorizontalOffset = offset;
        }

        private void ShutdownListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var item = e.ClickedItem as FrameworkElement;
            if (item == null)
            {
                return;
            }
            switch (item.Name)
            {
                case "ShutdownOption":
                    ShutdownHelper(ShutdownKind.Shutdown);
                    break;
                case "RestartOption":
                    ShutdownHelper(ShutdownKind.Restart);
                    break;
            }
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ReadPersonList));
        }
    }
}
