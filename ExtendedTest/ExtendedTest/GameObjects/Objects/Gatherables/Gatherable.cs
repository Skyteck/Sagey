using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace ExtendedTest.GameObjects.Objects.Gatherables
{
    class Gatherable : WorldObject
    {
        public int difficulty = 300;
        public List<OutputItem> OutputItems;
        int ItemGiveCount = 3;

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
                if(OutputItems.Count != 100)
                {
                    Console.WriteLine(this.Name + "output items not at 100");
                }
                randomNumber = ran.Next(0, OutputItems.Count);
                ItemGiveCount--;
                if (ItemGiveCount <= 0)
                {
                    this._CurrentState = SpriteState.kStateInActive;
                    this._Draw = false;
                }
                return OutputItems[randomNumber].output;
            }
            else
            {
                return Item.ItemType.kItemNone;
            }
        }

        public override void UpdateDead(GameTime gameTime)
        {
            if (_CurrentState == SpriteState.kStateInActive)
            {
                timeDead += gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (timeDead >= respawnTimerStart)
            {
                Revive();
            }
            base.UpdateDead(gameTime);
        }

        public virtual void Revive()
        {
            this._CurrentState = SpriteState.kStateActive;
            this._Draw = true;
            timeDead = 0;
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
