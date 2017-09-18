using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace ExtendedTest
{
    public class AnimatedSprite : Sprite
    {
        bool animLooping = false;
        private int frameNum = 0;
        int FPS = 1;
        int Frames = 1;
        int animStateNum = 0;
        private float timeElapsed = 0;
        bool animationDone = false;
        List<Animation> _AnimList;
        Animation _ActiveAnim = null;

        public override Vector2 _TopLeft
        {
            get
            {
                return new Vector2((int)_Position.X - (_ActiveAnim._FrameWidth / 2), ((int)_Position.Y - (_ActiveAnim._FrameHeight / 2)));
            }
        }

        public override Rectangle _BoundingBox
        {
            get
            {
                return new Rectangle((int)_TopLeft.X, ((int)_TopLeft.Y), _ActiveAnim._FrameWidth, _ActiveAnim._FrameHeight);
            }
        }

        public override Vector2 _Center
        {
            get
            {
                return new Vector2(_ActiveAnim._FrameWidth / 2, _ActiveAnim._FrameHeight / 2);
            }
        }

        public AnimatedSprite()
        {
            _AnimList = new List<Animation>();
        }

        public override void LoadContent(string path, ContentManager content)
        {
            base.LoadContent(path, content);
            if(_AnimList.Count>0)
            {
                for(int i = 0; i < _AnimList.Count; i++)
                {
                    _AnimList[i].Setup(i);
                }
            }
            else
            {
                _ActiveAnim = new Animation("Idle", this._Texture.Width, this._Texture.Height, 1, 1);
                AddAnimation(_ActiveAnim);
                _ActiveAnim.Setup(0);
            }
            _ActiveAnim = _AnimList[0];
        }

        public void SetupAnimation(int frames, int fps, int states, bool looping)
        {
            Frames = frames;
            FPS = fps;
            frameWidth = _Texture.Width / frames;
            frameHeight = _Texture.Height / states;
            animLooping = looping;
        }

        public override void UpdateActive(GameTime gameTime)
        {
            base.UpdateActive(gameTime);
            timeElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;
            Animate();
        }

        public void Animate()
        {

            float TPF = 1.0f / _ActiveAnim._FPS;
            if (timeElapsed >= TPF)
            {
                frameNum++;
                if(frameNum > _ActiveAnim._Frames)
                {
                    if(_ActiveAnim._Loops)
                    {
                        frameNum = 0;
                    }
                    else
                    {
                        animationDone = true;
                        ChangeAnimation(_ActiveAnim._NextAnim._Name);
                    }
                }
                frameNum %= _ActiveAnim._Frames;
                timeElapsed -= TPF;
            }
        }

        public void ChangeAnimation(String animName)
        {
            if(animName != _ActiveAnim._Name)
            {
                _ActiveAnim = _AnimList.Find(x => x._Name == animName);
                frameNum = 0;
            }
        }

        public void AddAnimation(Animation anim)
        {
            anim._AnimNum = animStateNum;
            _AnimList.Add(anim);
            animStateNum++;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (_Draw)
            {
                Rectangle sr = _ActiveAnim._FramesList[frameNum];
                Draw(spriteBatch, sr);
            }
        }
    }
}
