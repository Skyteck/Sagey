using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace ExtendedTest
{
    public class Tile
    {
        public Texture2D _Texture;
        public Vector2 _Position;
        int _TileWidth;
        int _TileHeight;
        Rectangle _SourceRec;
        int _Col;
        int _Row;
        public bool visible = false;
        public bool active = false;
        public Rectangle destRect;

        public Vector2 tileCenter;

        public Vector2 localPos;

        public Tile(Texture2D texture, Vector2 Pos, int width, int height, int col, int row, bool draw, Vector2 tilemapPos)
        {
            _Texture = texture;
            _Position = Pos;
            _TileWidth = width;
            _TileHeight = height;
            _Col = col;
            _Row = row;
            visible = draw;
            _SourceRec = new Rectangle(_TileWidth * _Col, _TileHeight * _Row, _TileWidth, _TileHeight);
            destRect = new Rectangle((int)_Position.X, (int)_Position.Y, _TileWidth, _TileHeight);
            localPos = tilemapPos;

            tileCenter.X = this._Position.X + this._TileWidth / 2;
            tileCenter.Y = this._Position.Y + this._TileHeight / 2;
        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_Texture, destRect, _SourceRec, Color.White);
        }
    }
}
