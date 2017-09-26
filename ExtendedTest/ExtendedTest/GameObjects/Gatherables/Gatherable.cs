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
        public int _Difficulty = 300;
        public List<ItemBundle> OutputItems;
        public List<ItemBundle> _Drops;
        public int ItemGiveCount = 3;
        private int MaxItemGive = 3;
        protected bool _Respawns = true;
        
        public Gatherable()
        {
            OutputItems = new List<ItemBundle>();
            IsGatherable = true;
            _Drops = new List<ItemBundle>();
        }

        public void SetupDrops()
        {
            foreach(ItemBundle bundle in OutputItems)
            {
                for (int i = 0; i < bundle.odds; i++)
                {
                    _Drops.Add(bundle);
                }
            }


            if (_Drops.Count < 100)
            {
                ItemBundle noneBundle = new ItemBundle();
                noneBundle.output = Item.ItemType.kItemNone;
                noneBundle.amount = 1;
                noneBundle.odds = 100 - _Drops.Count;
                for (int i = 0; i < noneBundle.odds; i++)
                {
                    _Drops.Add(noneBundle);
                }
            }
            else if(_Drops.Count > 100)
            {
                Console.Write(this.Name + " too many _Drops!");
            }
        }
        public ItemBundle GetGathered()
        {
            Random ran = new Random();
            int randomNumber;
            if(_Drops.Count != 100)
            {
                Console.WriteLine(this.Name + "output items not at 100");
            }
            randomNumber = ran.Next(0, _Drops.Count);
            ItemGiveCount--;
            if (ItemGiveCount <= 0)
            {
                this.Deactivate();
            }
            return _Drops[randomNumber];
        }

        protected override void UpdateDead(GameTime gameTime)
        {
            if(_Respawns)
            {
                if (_CurrentState == SpriteState.kStateInActive)
                {
                    TimeDead += gameTime.ElapsedGameTime.TotalSeconds;
                }

                if (TimeDead >= RespawnTimerStart)
                {
                    Revive();
                }
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
