using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace MoreThanHalfThesis.VIews
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Healthtips : Page
    {
        private DispatcherTimer timer;
        public Healthtips()
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

        private void backButton2_Click(object sender, RoutedEventArgs e)
        {
            if (Frame.CanGoBack)
            {
                Frame.GoBack();
            }
        }
    }
}
