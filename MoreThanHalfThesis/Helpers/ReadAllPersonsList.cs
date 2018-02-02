using MoreThanHalfThesis.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoreThanHalfThesis.Helpers
{
    class ReadAllPersonsList
    {
        DatabaseHelperClass Db_Helper = new DatabaseHelperClass();
        public ObservableCollection<Person> GetAllContacts()
        {
            return Db_Helper.ReadAllContacts();
        }
    }
}
