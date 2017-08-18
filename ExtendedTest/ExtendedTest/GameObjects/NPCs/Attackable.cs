using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace ExtendedTest.GameObjects.NPCs
{
    /// <summary>
    /// Attackables are NPCs with combat logic. They can be agressive or not. 
    /// If they are agressive they know how to hunt their pray and attack them.
    /// they can also be attacked.
    /// </summary>
    public class Attackable : NPC
    {

        //Combat values
        public Managers.CombatManager _CBManager;
        public double attackSpeed = 3.0;
        public double attackCD = 0;
        public int startHP = 2;
        public int _HP;
        public int defense;
        public int attack;
        public float attackRange = 64f; // tileWidth

        public double respawnTimerStart = 15d;
        public double timeDead = 0d;

        bool Agressive = false;
        List<Sprite> _TargetList;

        Rectangle huntZone;

        public Projectile myShot;

        public bool _Stunned = false;
        public bool _Invuln = false;
        public double _StunTime = 0;

        public enum AttackStyle
        {
            kMeleeStyle,
            kRangeStyle,
            kMagicStyle
        }

        public AttackStyle _AttackStyle = AttackStyle.kMeleeStyle;
        public Attackable(Managers.NPCManager manager, Managers.CombatManager cbManager) : base(manager)
        {
            _CBManager = cbManager;
            _TargetList = new List<Sprite>();
        }

        public override void LoadContent(string path, ContentManager content)
        {
            base.LoadContent(path, content);
            
        }

        public override void UpdateActive(GameTime gameTime)
        {
            bool targetFound = false;
            if(_StunTime <= 0)
            {

                if (this.Agressive)
                {
                    foreach (Sprite target in _TargetList)
                    {
                        if (target._BoundingBox.Intersects(huntZone))
                        {
                            targetFound = true;
                            if (this._AttackStyle == AttackStyle.kMeleeStyle)
                            {
                                HuntTarget(target);

                            }
                            if (Vector2.Distance(this._Position, target._Position) <= attackRange)
                            {
                                if (this.attackCD <= 0)
                                {
                                    if (this._AttackStyle == AttackStyle.kMeleeStyle)
                                    {
                                        //_CBManager.PerformAttack(this, target);
                                    }
                                    else
                                    {
                                        //myShot.SetTarget(target);
                                    }
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
            base.UpdateActive(gameTime);
            }
            else
            {
                Console.Write("Stunned");
            }


            if (attackCD > 0)
            {
                attackCD -= gameTime.ElapsedGameTime.TotalSeconds;
            }
            

            if(_StunTime > 0)
            {
                _StunTime -= gameTime.ElapsedGameTime.TotalSeconds;
            }
            else
            {
                _Invuln = false;
                _Stunned = false;
                _MyColor = Color.White;
            }


        }

        public virtual void ReceiveDamage(int amt)
        {
            if (_Invuln) return;
            _HP -= amt;
            _StunTime = 0.3;
            _Invuln = true;
            _Stunned = true;
            _MyColor = Color.Red;
            if (_HP <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            _CurrentState = SpriteState.kStateInActive;
            _Draw = false;
        }

        public override void UpdateDead(GameTime gameTime)
        {
            if (_CurrentState == SpriteState.kStateInActive)
            {
                timeDead += gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (timeDead >= respawnTimerStart)
            {
                this._CurrentState = SpriteState.kStateActive;
                this._Draw = true;
                timeDead = 0;
            }
            base.UpdateDead(gameTime);
        }

        public void AddTarget(Sprite target)
        {
            _TargetList.Add(target);
            this.Agressive = true;
            huntZone = new Rectangle((int)this.LeftBoundary, (int)this.TopBoundary, (int)(this.RightBoundary - this.LeftBoundary), (int)(this.BottomBoundary - this.TopBoundary));
        }

        public void AddTarget(List<Attackable> targetList)
        {
            _TargetList.AddRange(targetList);
            this.Agressive = true;
            huntZone = new Rectangle((int)this.LeftBoundary, (int)this.TopBoundary, (int)(this.RightBoundary - this.LeftBoundary), (int)(this.BottomBoundary - this.TopBoundary));
        }

        public void Tame()
        {
            _TargetList.Clear();
            this.Agressive = false;
        }

        private void HuntTarget(Sprite target)
        {
            this.SetDestination(target._Position);
        }

    }
}
