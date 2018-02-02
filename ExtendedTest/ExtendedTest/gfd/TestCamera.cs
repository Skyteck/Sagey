using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sagey
{
    public class TestCamera 
    {
        protected float Zoom;
        public Matrix _Transform;
        public Vector2 _Position;
        protected float Rotation;

        private GraphicsDevice _GraphicsDevice;

        public float _Zoom
        {
            get
            {
                return Zoom;
            }
            set
            {
                Zoom = value;
                if (Zoom < 0.1f) Zoom = 0.1f;
            }
        }

        public float _Rotation
        {
            get
            {
                return Rotation;
            }
            set
            {
                Rotation = value;
            }
        }

        public TestCamera(GraphicsDevice gD)
        {
            Zoom = 1.0f;
            Rotation = 0f;
            _Position = Vector2.Zero;
            _GraphicsDevice = gD;
        }

        public void MoveCamera(Vector2 amt)
        {
            _Position += amt;
        }

        public Matrix GetTransform()
        {
            Vector3 v = new Vector3(-_Position.X, -_Position.Y, 0);
            _Transform = Matrix.CreateTranslation(v) *
                                         Matrix.CreateRotationZ(Rotation) *
                                         Matrix.CreateScale(new Vector3(Zoom, Zoom, 1)) *
                                         Matrix.CreateTranslation(new Vector3(_GraphicsDevice.Viewport.Width * 0.5f, _GraphicsDevice.Viewport.Height * 0.5f, 0));
            return _Transform;
        }

        public Vector2 ToScreen(float x, float y)
        {
            return new Vector2(x, y);
        }
        public Vector2 ToScreen(Vector2 worldPos)
        {
            return Vector2.Transform(worldPos, GetTransform());
        }

        public Vector2 ToWorld(float x, float y)
        {
            return ToWorld(new Vector2(x, y));
        }

        public Vector2 ToWorld(Vector2 sPos)
        {
            return Vector2.Transform(sPos, Matrix.Invert(GetTransform()));
        }
    }

}
