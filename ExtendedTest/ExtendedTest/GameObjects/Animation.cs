using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sagey
{
    public class Animation
    {
        public string _Name { get; set ; }
        public int _FrameWidth { get; set; }
        public int _FrameHeight { get; set; }
        public int _FPS { get; set; }
        public int _AnimNum { get; set; }
        public bool _Loops { get; set; }
        public bool _AnimCompleted { get; set; }
        public List<Rectangle> _FramesList { get; set; }
        public int _Frames { get; set; }
        public Animation _NextAnim { get; set; }
        public Animation(String Name, int Width, int Height, int FPS, int frames, bool loops = true, Animation nextAnim = null)
        {
            //Name, FPS, loops, framewidth, frameheight
            _Name = Name;
            _FrameWidth = Width;
            _FrameHeight = Height;
            _Loops = loops;
            _FPS = FPS;
            _FramesList = new List<Rectangle>();
            _Frames = frames;
            _NextAnim = nextAnim;
        }

        public void Setup(int StateNum)
        {
            for(int i = 0; i < _Frames; i++)
            {
                Rectangle newRect = new Rectangle((i * _FrameWidth), (StateNum * _FrameHeight), _FrameWidth, _FrameHeight);
                _FramesList.Add(newRect);
            }
        }
    }
}
