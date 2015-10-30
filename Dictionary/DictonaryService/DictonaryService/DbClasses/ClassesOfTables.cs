using System.Collections.Generic;
using DictonaryService.DbClasses;

namespace DictonaryService
{
    public class RusDictonary : Dictonary
    {
        public virtual ICollection<EngDictonary> Words
        {
            get;
            set;
        }
    }

    public class EngDictonary : Dictonary
    {
        public virtual ICollection<RusDictonary> Words
        {
            get;
            set;
        }
    }

    public class RelationsTable
    {
        public int Id 
        { 
            get;
            set;
        }

        public virtual int IdEngWord
        { 
            get;
            set;
        }

        public virtual int IdRusWord 
        {
            get;
            set;
        }
    }
}