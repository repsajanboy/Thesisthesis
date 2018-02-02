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
using Windows.UI.Core;
using Windows.UI.Popups;
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
    public sealed partial class AddPerson : Page
    {
        private List<Icon> Icons;

        private DispatcherTimer timer;
        public AddPerson()
        {
            this.InitializeComponent();
            Icons = new List<Icon>();
            Icons.Add(new Icon { IconPath = "ms-appx:///Assets/male-01.png" });
            Icons.Add(new Icon { IconPath = "ms-appx:///Assets/male-02.png" });
            Icons.Add(new Icon { IconPath = "ms-appx:///Assets/male-03.png" });
            Icons.Add(new Icon { IconPath = "ms-appx:///Assets/female-01.png" });
            Icons.Add(new Icon { IconPath = "ms-appx:///Assets/female-02.png" });
            Icons.Add(new Icon { IconPath = "ms-appx:///Assets/female-03.png" });

            List<String> gender = new List<String>();
            gender.Add("Male");
            gender.Add("Female");

            GendercmBx.ItemsSource = gender;

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
        private void GendercmBx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;

        }

        private void backButton2_Click(object sender, RoutedEventArgs e)
        {
            if (Frame.CanGoBack)
            {
                Frame.GoBack();
            }
        }
        private async void AddUser_Click(object sender, RoutedEventArgs e)
        {
            DatabaseHelperClass DB_Helper = new DatabaseHelperClass();//Creating object for DatabaseHelperClass.cs in ViewModel/DatabaseHelperClass.cs
            if (NametxtBx.Text != "" && AgetxtBx.Text != "" && GendercmBx.SelectedItem.ToString() != "" && IconList.SelectedItem.ToString() != "")
            {
                string avatar = ((Icon)IconList.SelectedValue).IconPath;
                DB_Helper.Insert(new Person(NametxtBx.Text, AgetxtBx.Text, GendercmBx.SelectedItem.ToString(), avatar));
                Frame.Navigate(typeof(ReadPersonList));//After adding user redirect to list of user
            }
            else
            {
                MessageDialog messageDialog = new MessageDialog("Please fill all the fields");//Text should not be empty
                await messageDialog.ShowAsync();
            }
        }
    }
}
