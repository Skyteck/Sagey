using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExtendedTest.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ExtendedTest
{
    public class Projectile : Sprite
    {
        public Vector2 _Heading;
        public double _Speed;
        double TTL;

        public Rectangle _ProjectileTip
        {

            get
            {
                float x = _Position.X;
                float y = _Position.Y;
                Vector2 newPos = _Position;

                x = newPos.X;
                y = newPos.Y;
                int radias = frameHeight/2;
                double mathSin = Math.Sin(_Rotation);
                double mathCos = Math.Cos(_Rotation);
                newPos.X = (float)(x + (radias * mathSin));
                newPos.Y = (float)(y - (radias * mathCos));

                Rectangle newRect = new Rectangle((int)(newPos.X), (int)(newPos.Y), 1, 1);
                return newRect;
            }
        }

        Sprite collisionBox;

        public Projectile()
        {
            _Speed = 360;
            collisionBox = new Sprite();
            collisionBox.Activate();
            AddChild(collisionBox);
            TTL = 5;
        }

        public override void LoadContent(string path, ContentManager content)
        {
            base.LoadContent(path, content);
            collisionBox.LoadContent("Art/Collision", content);
            collisionBox.frameWidth = 10; //this._ProjectileTip.Width;
            collisionBox.frameHeight = 10; //this._ProjectileTip.Height;
        }

        public override void UpdateActive(GameTime gameTime)
        {
            collisionBox._Position = HelperFunctions.PointToVector(this._ProjectileTip.Location);
            if (this._CurrentState != SpriteState.kStateActive) return;
            this._Position.X += (float)(_Heading.X * (_Speed * gameTime.ElapsedGameTime.TotalSeconds));
            this._Position.Y += (float)(_Heading.Y * (_Speed * gameTime.ElapsedGameTime.TotalSeconds));

            if(TTL>0)
            {
                TTL -= gameTime.ElapsedGameTime.TotalSeconds;
                if(TTL<0)
                {
                    this.Deactivate();
                }
            }

            base.UpdateActive(gameTime);
        }

        public override void Activate(Vector2 pos)
        {
            base.Activate(pos);
            TTL = 2;
        }

        public void setDirection(Vector2 dir)
        {
            //float HeadingDir = MathHelper.ToRadians(dir);
            _Heading = dir;
            _Rotation = (float)Math.Atan2(dir.X, -dir.Y);
        }
    }
}
