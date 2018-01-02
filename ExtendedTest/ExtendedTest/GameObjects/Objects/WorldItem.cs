using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sagey.GameObjects.Objects
{
    class WorldItem : WorldObject
    {
        public Item MyItem;
        public Sprite chaseTarget = null;

        public WorldItem(Item it, Vector2 pos)
        {
            MyItem = it;
            _Position = pos;
            this.LoadContent(it.itemtexture);
        }

        protected override void UpdateActive(GameTime gt)
        {
            if(chaseTarget != null)
            {
                int speed = 160;
                if (chaseTarget._Position.X < this._Position.X)
                {
                    this._Position.X -= (float)(speed * gt.ElapsedGameTime.TotalSeconds);
                }
                else
                {
                    this._Position.X += (float)(speed * gt.ElapsedGameTime.TotalSeconds);
                }

                if (chaseTarget._Position.Y < this._Position.Y)
                {
                    this._Position.Y -= (float)(speed * gt.ElapsedGameTime.TotalSeconds);
                }
                else
                {
                    this._Position.Y += (float)(speed * gt.ElapsedGameTime.TotalSeconds);
                }
            }

            base.UpdateActive(gt);
        }

        public void SetTarget(Sprite target)
        {
            chaseTarget = target;
        }

        public override void Deactivate()
        {
            base.Deactivate();
            chaseTarget = null;
        }
    }
}
