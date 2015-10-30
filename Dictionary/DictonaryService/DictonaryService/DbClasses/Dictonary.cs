using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace DictonaryService.DbClasses
{
    public class Dictonary  
    {
        public int Id
        {
            get;
            set;
        }

        public string Word
        {
            get;
            set;
        }

        public virtual ICollection<Dictonary> Words
        {
            get;
            set;
        }
    }
}
