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
            this._Type = ItemType.kItemFish;
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
            this._Type = ItemType.kItemLog;
            this._Weight = 1;
            this._SaleValue = 1;
            this._Name = "Log";
        }
    }

    class Ore : Item
    {
        public Ore()
        {
            this._Type = ItemType.kItemOre;
            this._Weight = 2;
            this._SaleValue = 1;
            this._Name = "Ore";
        }
    }

    class CookedFish : Item
    {
        public CookedFish()
        {
            this._Type = ItemType.kItemMatches;
            this._Weight = 2;
            this._SaleValue = 1;
            this._Name = "CookedFish";
        }
    }

    class Matches : Item
    {
        public Matches()
        {
            this._Type = ItemType.kItemMatches;
            this._Weight = 2;
            this._SaleValue = 1;
            this._Name = "Matches";
            this._Uses = 5;
        }
    }

    class FishStick : Item
    {
        public FishStick()
        {
            this._Type = ItemType.kItemFishStick;
            this._Weight = 2;
            this._SaleValue = 1;
            this._Name = "FishStick";
        }
    }
}
