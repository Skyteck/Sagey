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
            this.myType = ItemType.kItemOre;
            this._Weight = 2;
            this._SaleValue = 1;
            this._Name = "Ore";
        }
    }

    class CookedFish : Item
    {
        public CookedFish()
        {
            this.myType = ItemType.kItemMatches;
            this._Weight = 2;
            this._SaleValue = 1;
            this._Name = "CookedFish";
        }
    }

    class Matches : Item
    {
        public Matches()
        {
            this.myType = ItemType.kItemMatches;
            this._Weight = 2;
            this._SaleValue = 1;
            this._Name = "Matches";
            this.Uses = 5;
        }
    }

    class FishStick : Item
    {
        public FishStick()
        {
            this.myType = ItemType.kItemFishStick;
            this._Weight = 2;
            this._SaleValue = 1;
            this._Name = "FishStick";
        }
    }
}
