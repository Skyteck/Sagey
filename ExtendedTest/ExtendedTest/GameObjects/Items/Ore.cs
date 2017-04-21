using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtendedTest
{
    class Ore : Item
    {
        public Ore()
        {
            this.myType = ItemType.ItemOre;
            this._Weight = 2;
            this._SaleValue = 1;
            this._Name = "Ore";
        }
    }
}
