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
    class AnimatedSprite : Sprite
    {
        bool animLooping = false;
        private int frameNum = 0;
        int FPS = 1;
        int Frames = 1;
        int animStateNum = 0;
        private float timeElapsed = 0;

        public AnimatedSprite()
        {

        }

        public override void LoadContent(string path, ContentManager content)
        {
            SetupAnimation(1, 1, 1, false);
            base.LoadContent(path, content);
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
            Animate(animStateNum);
        }

        public void Animate(int stateNum)
        {
            float TPF = 1.0f / FPS;
            if (timeElapsed >= TPF)
            {
                frameNum++;
                if (animLooping && frameNum > Frames)
                {
                    frameNum = 0;
                }
                else
                {

                }
                frameNum %= Frames;
                timeElapsed -= TPF;
            }
            animStateNum = stateNum;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (_Draw)
            {
                Rectangle sr = new Rectangle((frameWidth * frameNum), (frameHeight * animStateNum), frameWidth, frameHeight);
                if (!_FlipX && !_FlipY)
                {
                    spriteBatch.Draw(_Texture, _Position, sr, _MyColor, _Rotation, _Center, _Scale, SpriteEffects.None, 0f);
                }
                else if (_FlipX)
                {
                    spriteBatch.Draw(_Texture, _Position, sr, _MyColor, _Rotation, _Center, _Scale, SpriteEffects.FlipHorizontally, 0f);
                }
                else if (_FlipY)
                {
                    spriteBatch.Draw(_Texture, _Position, sr, _MyColor, _Rotation, _Center, _Scale, SpriteEffects.FlipVertically, 0f);
                }
                else if (_FlipX && _FlipY)
                {
                    spriteBatch.Draw(_Texture, _Position, sr, _MyColor, (_Rotation + (float)Math.PI), _Center, _Scale, SpriteEffects.None, 0f);
                }

                if (_ChildrenList != null)
                {
                    if (_ChildrenList.Count >= 1)
                    {
                        foreach (Sprite child in _ChildrenList)
                        {
                            child.Draw(spriteBatch);
                        }
                    }
                }
            }
        }
    }
}
