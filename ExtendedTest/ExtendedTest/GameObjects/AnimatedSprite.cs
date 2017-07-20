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

        public AnimatedSprite()
        {

        }

        public override void LoadContent(string path, ContentManager content)
        {
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
            Animate();
        }

        public void Animate()
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
                    animationDone = true;
                }
                frameNum %= Frames;
                timeElapsed -= TPF;
            }
        }

        public void ChangeAnimation(int animNum)
        {
            animStateNum = animNum;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (_Draw)
            {
                Rectangle sr = new Rectangle((frameWidth * frameNum), (frameHeight * animStateNum), frameWidth, frameHeight);
                Draw(spriteBatch, sr);
            }
        }
    }
}
