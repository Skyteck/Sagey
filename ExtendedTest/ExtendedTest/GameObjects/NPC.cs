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
    /// A NPC is any object in the game world that can move around using its own logic. FindPath is provided to get the character where it's going.
    /// For example the player's destination is set by clicking on the world. NPCs use their logic to determien their destination
    /// findPath gets things where they need to be.
    /// </summary>
    public class NPC : AnimatedSprite
    {
        public List<NPC> parentList;

        Managers.NPCManager ParentManager;
        double currentMoveTimer = 6f;
        Vector2 Destination;
        public bool atDestination = true;
        public bool movingX = false;
        public bool movingY = false;
        public bool moving = false;

        public float _Speed = 5f;

        List<Tile> myPath;


        public double LeftBoundary { get; private set; }
        public double RightBoundary { get; private set; }
        public double BottomBoundary { get; private set; }
        public double TopBoundary { get; private set; }

        public Tile NextTileInPath
        {
            get
            {
                if(myPath.Count>0)
                {
                    return myPath[0];
                }
                else
                {
                    return null;
                }
            }
        }

        public NPC(Managers.NPCManager nm)
        {
            ParentManager = nm;
            myPath = new List<Tile>();
        }

        public override void LoadContent(string path, ContentManager content)
        {
            base.LoadContent(path, content);
            SetupAnimation(1, 1, 1, false);
        }

        public override void UpdateActive(GameTime gameTime)
        {

            if (!atDestination)
            {
                FindPath();
            }
            base.UpdateActive(gameTime);
        }

        public void AddPath(List<Tile> path)
        {
            myPath.AddRange(path);
            if(myPath!= null)
            {
                SetDestination(myPath[0].tileCenter);
            }
        }

        public void AddToPath(Tile tile)
        {
            myPath.Add(tile);
            if (myPath != null)
            {
                SetDestination(myPath[0].tileCenter);
            }
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
                    float newX = num.Next((int)this.LeftBoundary + this._Texture.Width / 2, (int)this.RightBoundary - this._Texture.Width / 2);
                    float newY = num.Next((int)this.TopBoundary + this._Texture.Height / 2, (int)this.BottomBoundary - this._Texture.Height / 2);
                    Tile tileGoal = ParentManager._TilemapManager.findTile(new Vector2(newX, newY));
                    this.SetDestination(tileGoal.tileCenter);
                }

                currentMoveTimer += num.Next(0, 3);
            }
        }

        public void FindPath()
        {
            if (Math.Abs(Destination.X - _Position.X) > _Speed)
            {
                if (Destination.X > _Position.X)
                {
                    _Position.X += _Speed;
                    movingX = true;
                }
                else if (Destination.X < _Position.X)
                {
                    _Position.X -= _Speed;
                    movingX = true;
                }

            }

            if (Math.Abs(Destination.Y - _Position.Y) > _Speed)
            {
                if (Destination.Y > _Position.Y)
                {
                    _Position.Y += _Speed;
                    movingY = true;
                }
                else if (Destination.Y < _Position.Y)
                {
                    _Position.Y -= _Speed;
                    movingY = true;
                }
            }

            float distance = Vector2.Distance(Destination, _Position);
            if (distance <= _Speed + 1)
            {
                _Position = Destination;
                atDestination = true;
                if(myPath!= null)
                {
                    if (myPath.Count > 0)
                    {
                        SetDestination(myPath[0].tileCenter);
                        myPath.RemoveAt(0);
                    }
                }

            }
        }

        public void ClearPath()
        {
            myPath.Clear();
        }

        public virtual void SetDestination(Vector2 dest)
        {
            Destination = dest;
            atDestination = false;
        }

        public virtual void SetBoundaries(double lX, double rx, double by, double ty)
        {
            LeftBoundary = lX;
            RightBoundary = rx + lX;
            TopBoundary = ty;
            BottomBoundary = by + ty;
        }
    }
}
