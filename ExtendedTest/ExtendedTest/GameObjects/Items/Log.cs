using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtendedTest
{
    class Log : Item
    {
        public Log()
        {
            this.myType = ItemType.ItemLog;
            this._Weight = 1;
            this._SaleValue = 1;
            this._Name = "Log";
        }
    }
}
