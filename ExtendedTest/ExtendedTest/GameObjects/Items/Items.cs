using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtendedTest
{
    class Fish : Item
    {
        public Fish()
        {
            this.ID = 1;
            this.myType = ItemType.kItemFish;
            this._Weight = 1;
            this._SaleValue = 1;
            this._Name = "Fish";
            this._Stackable = true;
        }
    }
    class Log : Item
    {
        public Log()
        {
            this.ID = 2;
            this.myType = ItemType.kItemLog;
            this._Weight = 1;
            this._SaleValue = 1;
            this._Name = "Log";
        }
    }

    class Ore : Item
    {
        public Ore()
        {
            this.ID = 3;
            this.myType = ItemType.kItemOre;
            this._Weight = 2;
            this._SaleValue = 1;
            this._Name = "Ore";
        }
    }
}
