using MoreThanHalfThesis.Helpers;
using MoreThanHalfThesis.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public sealed partial class ReadPersonList : Page
    {
        ObservableCollection<Person> DB_PersonList = new ObservableCollection<Person>();
        private ObservableCollection<Person> Persons;
        private DispatcherTimer timer;
        public ReadPersonList()
        {
            this.InitializeComponent();
            this.Loaded += ReadPersonList_Loaded;


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
            Persons = new ObservableCollection<Person>();

        }
        private void AddAccount_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AddPerson));
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
        private void DeleteAll_Click(object sender, RoutedEventArgs e)
        {
            DatabaseHelperClass delete = new DatabaseHelperClass();
            delete.DeleteAllPerson();//delete all DB contacts
            DB_PersonList.Clear();//Clear collections
            btnDelete.IsEnabled = false;
            listBoxobj.ItemsSource = DB_PersonList;
        }

        private void listBoxobj_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listBoxobj.SelectedIndex != -1)
            {
                Person listitem = listBoxobj.SelectedItem as Person;//Get slected listbox item contact ID
                Frame.Navigate(typeof(ViewBP), listitem);
            }
        }
        private void ReadPersonList_Loaded(object sender, RoutedEventArgs e)
        {
            ReadAllPersonsList dbperson = new ReadAllPersonsList();
            DB_PersonList = dbperson.GetAllContacts();//Get all DB persons 
            if (DB_PersonList.Count > 0)
            {
                btnDelete.IsEnabled = true;
            }
            listBoxobj.ItemsSource = DB_PersonList.OrderByDescending(i => i.Id).ToList();//Binding DB data to LISTBOX and Latest person ID can Display first.  
        }
    }
}
