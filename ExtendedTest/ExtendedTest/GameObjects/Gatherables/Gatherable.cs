using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using ExtendedTest.GameObjects.Items;

namespace ExtendedTest.GameObjects.Gatherables
{
    public class Gatherable : WorldObject
    {
        public int difficulty = 300;
        public List<Items.ItemBundle> OutputItems;
        int ItemGiveCount = 3;
        public int MaxItemGive = 3;

        public Gatherable()
        {
            OutputItems = new List<Items.ItemBundle>();
            IsGatherable = true;
        }


        public Item.ItemType GetGathered()
        {
            Random ran = new Random();
            int randomNumber;
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

        public override void UpdateDead(GameTime gameTime)
        {
            if (_CurrentState == SpriteState.kStateInActive)
            {
                TimeDead += gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (TimeDead >= RespawnTimerStart)
            {
                Revive();
            }
            base.UpdateDead(gameTime);
        }

        public virtual void Revive()
        {
            this._CurrentState = SpriteState.kStateActive;
            this._Draw = true;
            TimeDead = 0;

            Random ran = new Random();
            ItemGiveCount = ran.Next(1, MaxItemGive);
        }
        
    }
}
