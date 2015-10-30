using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace DictonaryService.DbClasses
{
    public interface IDictonary<out T> where T : class
    {
        // int Id
        //{
        //    get;
        //    set;
        //}

        //string Word
        //{
        //    get;
        //    set;
        //}
         ICollection<Dictonary> Words
        {
            get;
            set;
        }
    }
}
