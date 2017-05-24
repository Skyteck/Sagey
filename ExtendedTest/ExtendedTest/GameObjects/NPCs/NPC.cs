using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace ExtendedTest
{
    /// <summary>
    /// An NPC is any character that has the ability to "patrol" a set area determined by their boundaries.
    /// </summary>
    public class NPC : Character
    {
        double currentMoveTimer = 6f;
        public double leftBoundary { get; private set; }
        public double rightBoundary { get; private set; }
        public double bottomBoundary { get; private set; }
        public double topBoundary { get; private set; }

        NpcManager ParentManager;
        public List<Character> parentList;


        public double respawnTimerStart = 15d;
        public double timeDead = 0d;

        public enum AttackStyle
        {
            kMeleeStyle,
            kRangeStyle,
            kMagicStyle
        }

        public AttackStyle _AttackStyle = AttackStyle.kMeleeStyle;
        public NPC(NpcManager manager, CombatManager cbManager) :base(cbManager)
        {
            ParentManager = manager;
        }

        public override void LoadContent(string path, ContentManager content)
        {
            base.LoadContent(path, content);

            //this.rightBoundary += this._Position.X;
            //this.bottomBoundary += this._Position.Y;
            //this.leftBoundary += this._Texture.Width;
            //this.topBoundary += this._Texture.Height;
            
        }

        public override void UpdateActive(GameTime gameTime)
        {

            base.UpdateActive(gameTime);
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

        public virtual void Roam(GameTime gameTime)
        {
            currentMoveTimer -= gameTime.ElapsedGameTime.TotalSeconds;
            if (currentMoveTimer <= 0)
            {
                Random num = new Random((int)this._Position.X);
                bool move = (num.Next() % 2 == 0) ? true : false;
                if (move)
                {
                    float newX = num.Next((int)this.leftBoundary + this._Texture.Width / 2, (int)this.rightBoundary - this._Texture.Width / 2);
                    float newY = num.Next((int)this.topBoundary + this._Texture.Height / 2, (int)this.bottomBoundary - this._Texture.Height / 2);
                    Tile tileGoal = ParentManager._TilemapManager.findTile(new Vector2(newX, newY));
                    this.setDestination(tileGoal.tileCenter);
                }

                currentMoveTimer += num.Next(0, 3);
            }
        }

        public virtual void SetBoundaries(double lX, double rx, double by, double ty)
        {
            leftBoundary = lX;
            rightBoundary = rx + lX;
            topBoundary = ty;
            bottomBoundary = by + ty;
        }
    }
}
