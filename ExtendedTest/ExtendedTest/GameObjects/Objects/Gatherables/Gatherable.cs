using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtendedTest.GameObjects.Objects.Gatherable
{
    class Gatherable : WorldObject
    {
        public int difficulty = 300;
        public List<OutputItem> OutputItems;

        public Gatherable()
        {
            OutputItems = new List<OutputItem>();

        }


        public Item.ItemType GetGathered()
        {
            Random ran = new Random();
            int randomNumber = ran.Next(0, difficulty);
            if (randomNumber == 0)
            {
                logCount--;
                if (logCount <= 0)
                {
                    this._CurrentState = SpriteState.kStateInActive;
                    this._Draw = false;
                }
                return Item.ItemType.kItemLog;
            }
            else
            {
                return Item.ItemType.kItemNone;
            }
        }
    }

    public class OutputItem
    {
        public Item.ItemType output;
        public int odds;
        public int amount;

        public OutputItem()
        {

        }
    }
}
