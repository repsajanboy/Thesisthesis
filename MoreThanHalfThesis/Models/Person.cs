using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace MoreThanHalfThesis.Models
{
    class Person
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Age { get; set; }
        public string Gender { get; set; }
        public string AvatarPath { get; set; }
        public string CreationDate { get; set; }

        public Person()
        {

        }

        public Person(string name, string age, string gender, string avatar)
        {
            Name = name;
            Age = age;
            Gender = gender;
            AvatarPath = avatar;
            CreationDate = DateTime.Now.ToString();
            
        }

    }
}
