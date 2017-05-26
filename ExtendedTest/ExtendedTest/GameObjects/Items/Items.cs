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

        public override String Use(Item item)
        {
            return "None";
        }

        public override String Use(WorldObject worldObject)
        {
            switch(worldObject._Tag)
            {
                case Sprite.SpriteType.kFireType:
                    return "Cook";
            }
            return "None";
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

        public override String Use(Item item)
        {
            String command = String.Empty;
            switch(item.myType)
            {
                case ItemType.kItemMatches:
                    command = "Create Fire";
                    break;
            }
            return command;
        }

        public override String Use(WorldObject worldObject)
        {
            return "None";
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

        public override String Use(Item item)
        {
            return "None";
        }

        public override String Use(WorldObject worldObject)
        {
            return "None";
        }
    }

    class CookedFish : Item
    {
        public CookedFish()
        {
            this.ID = 4;
            this.myType = ItemType.kItemMatches;
            this._Weight = 2;
            this._SaleValue = 1;
            this._Name = "CookedFish";
        }
        public override String Use(Item item)
        {
            return "None";
        }

        public override String Use(WorldObject worldObject)
        {
            return "None";
        }
    }

    class Matches : Item
    {
        public Matches()
        {
            this.ID = 5;
            this.myType = ItemType.kItemMatches;
            this._Weight = 2;
            this._SaleValue = 1;
            this._Name = "Matches";
            this.Uses = 5;
        }

        public override String Use(Item item)
        {
            String command = String.Empty;
            switch (item.myType)
            {
                case ItemType.kItemLog:
                    command = "Create Fire";
                    break;
            }
            return command;
        }

        public override String Use(WorldObject worldObject)
        {
            return "None";
        }
    }
}
