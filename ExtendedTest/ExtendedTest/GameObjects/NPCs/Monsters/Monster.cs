using System;
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
        List<Character> _TargetList;

        Rectangle huntZone;

        Projectile myShot;
        public Monster(NpcManager manager, CombatManager cbManager) : base(manager)
        {
            _TargetList = new List<Character>();
            _CBManager = cbManager;
            attack = 6;
            defense = 5;
            //manager.CreateMonster()
            myShot = new Projectile(this);
            attackRange = 256;
        }

        public override void UpdateActive(GameTime gameTime)
        {
            bool targetFound = false;
            if (this.Agressive)
            {
                foreach(Character target in _TargetList)
                {
                    if (target._BoundingBox.Intersects(huntZone))
                    {
                        targetFound = true;
                        HuntTarget(target);
                        if(Vector2.Distance(this._Position, target._Position) <= attackRange)
                        {
                            if (this.attackCD <= 0)
                            {
                                myShot.SetTarget(target);
                                _CBManager.PerformAttack(this, target);
                                attackCD = attackSpeed;
                            }
                        }
                    }
                    if (targetFound) break;
                } 

            }
            if (!targetFound)
            {
                Roam(gameTime);
            }


            if (attackCD > 0)
            {
                attackCD -= gameTime.ElapsedGameTime.TotalSeconds;
            }
            base.UpdateActive(gameTime);
        }

        public void AddTarget(Character target)
        {
            _TargetList.Add(target);
            this.Agressive = true;
            huntZone = new Rectangle((int)this.leftBoundary, (int)this.topBoundary, (int)(this.rightBoundary - this.leftBoundary), (int)(this.bottomBoundary - this.topBoundary));
        }

        public void AddTarget(List<Character> targetList)
        {
            _TargetList.AddRange(targetList);
            this.Agressive = true;
            huntZone = new Rectangle((int)this.leftBoundary, (int)this.topBoundary, (int)(this.rightBoundary - this.leftBoundary), (int)(this.bottomBoundary - this.topBoundary));
        }

        public void Tame()
        {
            _TargetList.Clear();
            this.Agressive = false;
        }
        private void HuntTarget(Character target)
        {
            this.setDestination(target._Position);
        }

    }
}
