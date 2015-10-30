using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictonaryService.DbClasses
{
    public class EffortContext : DbContext
    {
         public EffortContext(DbConnection connection) : base(connection, true)
        {
        }
    }
}