using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sagey.Items
{
    class Fish : Item
    {
        public Fish()
        {
            this._ID = Enums.ItemID.kItemFish;
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
            this._ID = Enums.ItemID.kItemLog;
            this._Weight = 1;
            this._SaleValue = 1;
            this._Name = "Log";
        }
    }

    class Ore : Item
    {
        public Ore()
        {
            this._ID = Enums.ItemID.kItemOre;
            this._Weight = 2;
            this._SaleValue = 1;
            this._Name = "Ore";
        }
    }

    class CookedFish : Item
    {
        public CookedFish()
        {
            this._ID = Enums.ItemID.kItemMatches;
            this._Weight = 2;
            this._SaleValue = 1;
            this._Name = "CookedFish";
        }
    }

    class Matches : Item
    {
        public Matches()
        {
            this._ID = Enums.ItemID.kItemMatches;
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
            this._ID = Enums.ItemID.kItemFishStick;
            this._Weight = 2;
            this._SaleValue = 1;
            this._Name = "FishStick";
        }
    }

    class Strawberry : Item
    {
        public Strawberry()
        {
            this._ID = Enums.ItemID.kItemStrawberry;
            this._Weight = 2;
            this._SaleValue = 1;
            this._Name = "Strawberry";
            this._Stackable = true;
        }
    }
}
