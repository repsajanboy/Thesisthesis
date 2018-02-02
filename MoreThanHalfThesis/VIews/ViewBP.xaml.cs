using MoreThanHalfThesis.Helpers;
using MoreThanHalfThesis.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage.Streams;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace MoreThanHalfThesis.VIews
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ViewBP : Page
    {
        DatabaseHelperClass Db_Helper = new DatabaseHelperClass();
        Person currentUser = new Person();
        private DispatcherTimer timer;
        public ViewBP()
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
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            currentUser = e.Parameter as Person;
            string Iconuser = currentUser.AvatarPath;
            img.Source = new BitmapImage(new Uri(Iconuser, UriKind.Absolute));

            NametxtBx.Text = currentUser.Name;//get user Name  
            AgetxtBx.Text = currentUser.Age;//get user Age
            GendertxtBx.Text = currentUser.Gender;//get user gender
        }

        private void DeleteContact_Click(object sender, RoutedEventArgs e)
        {
            Db_Helper.DeletePerson(currentUser.Id);//Delete selected DB contact Id.
            Frame.Navigate(typeof(ReadPersonList));
        }

        private void backButton2_Click(object sender, RoutedEventArgs e)
        {
            if (Frame.CanGoBack)
            {
                Frame.GoBack();
            }
        }

        private void HealthtipsButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Healthtips));
        }

        private void ModeButton_Click(object sender, RoutedEventArgs e)
        {
            ModePopup.IsOpen = true;
        }

        private void PopupDonebtn_Click(object sender, RoutedEventArgs e)
        {
            ModePopup.IsOpen = false;
        }
    }
}
