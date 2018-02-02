using MoreThanHalfThesis.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoreThanHalfThesis.Helpers
{
    class DatabaseHelperClass
    {
        //Create Table
        public void CreateDatabase(string DB_PATH)
        {
            if (!CheckFileExists(DB_PATH).Result)
            {
                using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), DB_PATH))
                {
                    conn.CreateTable<Person>();
                }
            }
        }
        private async Task<bool> CheckFileExists(string filename)
        {
            try
            {
                var store = await Windows.Storage.ApplicationData.Current.LocalFolder.GetFileAsync(filename);
                return true;

            }
            catch
            {
                return false;
            }
        }
        //Insert the new person in the Person Table
        public void Insert(Person objPerson)
        {
            using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
            {
                conn.RunInTransaction(() =>
                {
                    conn.Insert(objPerson);
                });
            }
        }
        //Retrive the specific Person from the database
        public Person ReadPerson(int personid)
        {
            using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
            {
                var existingperson = conn.Query<Person>("select * from person where Id =" + personid).FirstOrDefault();
                return existingperson;
            }
        }
        public ObservableCollection<Person> ReadAllContacts()
        {
            try
            {
                using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
                {
                    List<Person> myCollection = conn.Table<Person>().ToList<Person>();
                    ObservableCollection<Person> PersonList = new ObservableCollection<Person>(myCollection);
                    return PersonList;
                }
            }
            catch
            {
                return null;
            }
        }
        //Update Person
        public void UpdateDetails(Person ObjPerson)
        {
            using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
            {
                var existingperson = conn.Query<Person>("select * from Person where Id=" + ObjPerson.Id).FirstOrDefault();
                if (existingperson != null)
                {
                    conn.RunInTransaction(() =>
                    {
                        conn.Update(ObjPerson);
                    });
                }
            }
        }
        //Delete all Person
        public void DeleteAllPerson()
        {
            using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
            {
                conn.DropTable<Person>();
                conn.CreateTable<Person>();
                conn.Dispose();
                conn.Close();
            }
        }
        //Delete Specific Contact
        public void DeletePerson(int Id)
        {
            using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
            {
                var existingperson = conn.Query<Person>("select * from Person where Id=" + Id).FirstOrDefault();
                if (existingperson != null)
                {
                    conn.RunInTransaction(() =>
                    {
                        conn.Delete(existingperson);
                    });
                }
            }
        }
    }
}
