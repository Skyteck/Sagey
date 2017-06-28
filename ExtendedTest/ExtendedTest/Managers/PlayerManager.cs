using ExtendedTest.GameObjects.Objects.Gatherables;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtendedTest.Managers
{
    public class PlayerManager
    {
        Player player;
        WorldObject _CurrentWOTarget;
        //Managers
        CombatManager _CombatManager;
        InventoryManager _InventoryManager;
        KeyboardState _PrevKBState;
        WorldObjectManager _WorldObjectManager;
        NPCManager _NPCManager;
        public Vector2 _PlayerPos;

        private bool bankerGo = false;
        public bool _BankerGo { get => bankerGo; set => bankerGo = value; }
        public enum PlayerState
        {
            kStateIdle = 0,
            kStateWalk,
            kStateWC,
            kStateRun,
            kStateFish,
            kStateMine,
            kStateAttack,
            kStateDefend
        }

        private PlayerState playerState = PlayerState.kStateIdle;
        public PlayerState _PlayerState { get => playerState; set => playerState = value; }

        public float _PlayerSpeed { get => playerSpeed; set => playerSpeed = value; }
        private float playerSpeed = 128.0f;

        private Rectangle CheckRect
        {
            get
            {
                if (player._Direction == Sprite.Direction.kDirectionDown)
                {
                    return new Rectangle((int)player._Position.X - 32, (int)player._Position.Y + 32, 64, 32);
                }
                else if (player._Direction == Sprite.Direction.kDirectionUp)
                {
                    return new Rectangle((int)player._Position.X - 32, (int)player._Position.Y - 64, 64, 32);
                }
                else if (player._Direction == Sprite.Direction.kDirectionLeft)
                {
                    return new Rectangle((int)player._Position.X - 64, (int)player._Position.Y - 32, 32, 642);
                }
                else if (player._Direction == Sprite.Direction.kDirectionRight)
                {
                    return new Rectangle((int)player._Position.X + 32, (int)player._Position.Y - 32, 32, 64);
                }
                else
                {
                    Console.WriteLine("Error creating player check rectangle");
                    return new Rectangle();
                }
            }
        }


        public PlayerManager(Player p, InventoryManager IM, CombatManager CM, WorldObjectManager WOM, NPCManager NPCM)
        {
            player = p;
            _CombatManager = CM;
            _InventoryManager = IM;
            _WorldObjectManager = WOM;
            _NPCManager = NPCM;
        }

        public void Update(GameTime gt)
        {
            ProcessKeyboard(gt);
            player.UpdateActive(gt);

            if(_CurrentWOTarget != null)
            {
                if(_CurrentWOTarget._CurrentState != Sprite.SpriteState.kStateInActive)
                {
                    ProcessWorldObject(_CurrentWOTarget);
                }
                else
                {
                    ClearTargets();
                    ChangePlayerState(PlayerState.kStateIdle);
                }
            }

            _PlayerPos = player._Position;
        }

        public void ProcessKeyboard(GameTime gt)
        {
            KeyboardState kbState = Keyboard.GetState();
            bool moved = false;
            Vector2 newPos = player._Position;
            if (kbState.IsKeyDown(Keys.A) || kbState.IsKeyDown(Keys.Left))
            {
                //player._Position.X -= player._Speed;
                newPos.X -= (float)(playerSpeed * gt.ElapsedGameTime.TotalSeconds);
                player._Direction = Sprite.Direction.kDirectionLeft;
                moved = true;
            }
            else if (kbState.IsKeyDown(Keys.D) || kbState.IsKeyDown(Keys.Right))
            {
                //player._Position.X += player._Speed;
                newPos.X += (float)(playerSpeed * gt.ElapsedGameTime.TotalSeconds);
                player._Direction = Sprite.Direction.kDirectionRight;
                moved = true;
            }
            if (kbState.IsKeyDown(Keys.W) || kbState.IsKeyDown(Keys.Up))
            {
                //player._Position.Y -= player._Speed;
                newPos.Y -= (float)(playerSpeed * gt.ElapsedGameTime.TotalSeconds);
                player._Direction = Sprite.Direction.kDirectionUp;
                moved = true;
            }
            else if (kbState.IsKeyDown(Keys.S) || kbState.IsKeyDown(Keys.Down))
            {
                //player._Position.Y += player._Speed;
                newPos.Y += (float)(playerSpeed * gt.ElapsedGameTime.TotalSeconds);
                player._Direction = Sprite.Direction.kDirectionDown;
                moved = true;
            }

            player._Position = newPos;
            if ( kbState.IsKeyDown(Keys.Space) && _PrevKBState.IsKeyUp(Keys.Space))
            {
                //This is for interacting with NPCs or other objects.
                //first check if we talked to an NPC;

                NPC npcHit = _NPCManager.CheckCollisions(CheckRect);
                if(npcHit != null)
                {
                    ProcessNPC(npcHit);
                }
                else
                {
                    _CurrentWOTarget = _WorldObjectManager.checkCollision(CheckRect);
                }
            }
            if(moved)
            {
                ChangePlayerState(PlayerState.kStateIdle);
                ClearTargets();
            }
            _PrevKBState = kbState;
        }

        private void ProcessWorldObject(WorldObject woHit)
        {
            if(woHit.IsGatherable)
            {
                Gather(woHit as Gatherable);
                ChangePlayerState(PlayerState.kStateWC);
            }
        }

        private void Gather(Gatherable gatherable)
        {
            Item.ItemType itemGet = gatherable.GetGathered();
            if(itemGet != Item.ItemType.kItemNone)
            {
                _InventoryManager.AddItem(itemGet);
            }
        }
        private void ProcessNPC(NPC npcHit)
        {
            _BankerGo = true;
        }

        public void Draw(SpriteBatch SB)
        {
            player.Draw(SB);
        }

        public void ChangePlayerState(PlayerState state)
        {
            _PlayerState = state;
            player.ChangeState(state);
        }

        public void ClearTargets()
        {
            _CurrentWOTarget = null;
            _BankerGo = false;
        }
    }
}
