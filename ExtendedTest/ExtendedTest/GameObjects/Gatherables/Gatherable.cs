using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using ExtendedTest.GameObjects.Items;

namespace ExtendedTest.GameObjects.Gatherables
{
    public abstract class Gatherable : WorldObject
    {
        public int _Difficulty = 300;
        protected ItemBundle CurrentDrop;
        public int _HP;
        protected int _TrueStartHP;
        public int _StartHP;
        public int ItemGiveCount = 3;
        private int MaxItemGive = 3;
        protected bool _Respawns = true;
        
        public Gatherable()
        {
            IsGatherable = true;
        }

        public void Setup()
        {
            Random ran = new Random((int)this._Position.X);
            _StartHP = _TrueStartHP;// + (ran.Next(0, (int)(_TrueStartHP * 0.5)));
            _HP = _StartHP;
        }
        public ItemBundle GetGathered()
        {
            Deactivate();
            Random ran = new Random((int)this._Position.X);
            CurrentDrop.amount += ran.Next(0, 2);
            return CurrentDrop;
        }

        public void GetHit(int dmg = 1)
        {
            this._HP -= dmg;
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
            Setup();
        }
        
        
    }
}
