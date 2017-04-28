﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace ExtendedTest
{
    class Monster : NPC
    {
        bool Agressive = false;
        Character target;
        Rectangle huntZone;
        public Monster(double lx, double rx, double by, double ty) : base(lx, rx, by, ty)
        {

        }

        public override void Update(GameTime gameTime, List<Sprite> gameObjectList)
        {

            if (this.Agressive && this.target._BoundingBox.Intersects(huntZone))
            {
                HuntTarget();
                if (this._BoundingBox.Intersects(target._BoundingBox))
                {
                    if (this.attackSpeed <= 0)
                    {
                        target.ReceiveDamage(1);
                        attackCD = attackSpeed;
                    }
                }
            }
            else
            {
                Roam(gameTime);
            }


            if (attackCD > 0)
            {
                attackSpeed -= gameTime.ElapsedGameTime.TotalSeconds;
            }
            base.Update(gameTime, gameObjectList);
        }

        public void SetTarget(Character target)
        {
            this.target = target;
            this.Agressive = true;
            huntZone = new Rectangle((int)this.leftBoundary, (int)this.topBoundary, (int)(this.rightBoundary - this.leftBoundary), (int)(this.bottomBoundary - this.topBoundary));
        }


        private void HuntTarget()
        {
            this.setDestination(this.target._Position);
        }

    }
}