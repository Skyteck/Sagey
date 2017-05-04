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

        CombatManager _CBManager;
        public Monster(NpcManager manager, CombatManager cbManager) : base(manager)
        {
            _TargetList = new List<Character>();
            _CBManager = cbManager;
            attack = 6;
            defense = 5;
        }

        public override void UpdateActive(GameTime gameTime)
        {
            if(this.Agressive)
            {
                foreach(Character target in _TargetList)
                {
                    if (target._BoundingBox.Intersects(huntZone))
                    {
                        HuntTarget(target);
                        if(Vector2.Distance(this._Center, target._Center) <= attackRange)
                        {
                            if (this.attackCD <= 0)
                            {
                                _CBManager.PerformAttack(this, target);
                                attackCD = attackSpeed;
                            }
                        }
                    }
                }                
            }
            else
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
