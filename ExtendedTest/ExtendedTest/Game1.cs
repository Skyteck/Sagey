using Comora;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using TiledSharp;

namespace ExtendedTest
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public Player player;
        //List<Sprite> gameObjectList;
        MouseState previousMouseState;
        KeyboardState previousKBState;
        Sprite mouseCursor;
        Sprite inventoryBG;
        public Camera  camera;
        
        List<TileMap> mapList;

        SpriteFont font;
        bool typingMode = false;
        public KbHandler kbHandler;
        Commander processor;
        //Managers
        InventoryManager invenManager;
        TilemapManager MapManager;
        public NpcManager _NPCManager;
        WorldObjectManager _GameObjectManager;
        CombatManager _CBManager;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Window.AllowUserResizing = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            invenManager = new InventoryManager(Content);
            player = new Player(invenManager);
            MapManager = new TilemapManager();
            _CBManager = new CombatManager();
            _NPCManager = new NpcManager(MapManager, _CBManager,  Content, player);
            _GameObjectManager = new WorldObjectManager(MapManager, invenManager, Content, player);
            mapList = new List<TileMap>();
            camera = new Camera(GraphicsDevice);
            kbHandler = new KbHandler();
            processor = new Commander(this);
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            LoadPlayerContent();
            LoadGUI();

            MapManager.AddMap(LoadMap("0-0"));
            MapManager.AddMap(LoadMap("0-1"));
            font = Content.Load<SpriteFont>("Fonts/Fipps");

            //XDocument xmlTest = XDocument.Load("Content/Items.xml");
            //IEnumerable<XElement> itemList = xmlTest.Elements("Items");
            //IEnumerable<XElement> LogList = itemList.Elements("Logs");
            //foreach(XElement item in LogList.Elements("Item"))
            //{
            //    String test = item.Element("Name").Value;
            //    String test2 = item.Element("SaleValue").Value;
            //    String test3 = item.Element("Weight").Value;
            //}
            //IEnumerable<XElement> oreList = itemList.Elements("Ores");
            //foreach (XElement item in oreList.Elements("Item"))
            //{
            //    String test = item.Element("Name").Value;
            //    String test2 = item.Element("SaleValue").Value;
            //    String test3 = item.Element("Weight").Value;
            //}

            //List<Sprite> test = new List<Sprite>();
            //test = gameObjectList.FindAll(x => x._Tag == Sprite.SpriteType.kTreeType);

            // TODO: use this.Content to load your game content here
        }

        private void LoadPlayerContent()
        {
            player.LoadContent("Art/Player", Content);
            player._Position = new Vector2(0, 0);
            invenManager.AddItem(new Log());
        }

        private void LoadGUI()
        {
            inventoryBG = new Sprite();
            inventoryBG.LoadContent("Art/inventoryBG", Content);
            inventoryBG._Position = new Vector2(450, 450);

            mouseCursor = new Sprite();
            mouseCursor.LoadContent("Art/log", Content);
        }


        private TileMap LoadMap(String mapname)
        {
            TileMap testMap = new TileMap(mapname, Content);

            LoadMapObjects(testMap);
            LoadMapNPCs(testMap);
            testMap.active = true;
            return testMap;

        }

        private void LoadMapNPCs(TileMap testMap)
        {
            TmxList<TmxObject> ObjectList = testMap.findNPCs();
            if (ObjectList != null)
            {
                foreach (TmxObject thing in ObjectList)
                {
                    int adjustThingX = (int)(thing.X + (testMap._Postion.X + (thing.Width / 2)));
                    int adjustThingY = (int)(thing.Y + (testMap._Postion.Y + (thing.Height / 2)));
                    _NPCManager.CreateMonster(thing, new Vector2(adjustThingX, adjustThingY));
                }
            }
        }

        private void LoadMapObjects(TileMap testMap)
        {
            TmxList<TmxObject> ObjectList = testMap.findObjects();
            if (ObjectList != null)
            {
                foreach (TmxObject thing in ObjectList)
                {
                    Vector2 newPos = new Vector2((int)thing.X + testMap._Postion.X, (int)thing.Y + testMap._Postion.Y);
                    _GameObjectManager.CreateObject(thing, newPos);
                }
            }
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if(this.IsActive)
            {
                // TODO: Add your update logic here
                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                    Exit();
                MouseState mouseState = Mouse.GetState();
                if (mouseState.LeftButton == ButtonState.Pressed )
                {
                    mouseCursor._Position = camera.ToWorld(new Vector2(mouseState.Position.X, mouseState.Position.Y));
                    if(mouseCursor._Position.X >= 0 && mouseCursor._Position.Y > 0)
                    {
                        Tile clickedTile = MapManager.findTile(mouseCursor._Position);
                        if (clickedTile != null)
                        {
                            player.setDestination(clickedTile.tileCenter);
                        }
                    }
                }


                if(mouseState.ScrollWheelValue > previousMouseState.ScrollWheelValue)
                {
                    camera.Scale += 0.2f;
                    if (camera.Scale > 2f)
                    {
                        camera.Scale = 2f;
                    }
                }
                else if(mouseState.ScrollWheelValue < previousMouseState.ScrollWheelValue)
                {
                    camera.Scale -= 0.25f;
                    if(camera.Scale < 0.6f)
                    {
                        camera.Scale = 0.6f;
                    }
                }

                kbHandler.Update();
                if(typingMode && !kbHandler.typingMode) //ugly, but should show that input mode ended...?
                {
                    processor.Parsetext(kbHandler.Input);
                    if(processor.currentError!= string.Empty) kbHandler.Input = processor.currentError;
                    //kbHandler.Input = string.Empty;
                }
                player.UpdateActive(gameTime);
                _NPCManager.UpdateNPCs(gameTime);
                _GameObjectManager.Update(gameTime);

                
                if(!kbHandler.typingMode)
                {
                    processor.currentError = string.Empty;
                    ProcessCamera(gameTime);

                }
                
                base.Update(gameTime);
                //Show FPS
                if((1 / gameTime.ElapsedGameTime.TotalSeconds) <= 59)
                {
                    Console.WriteLine("BAD FPS!!!!!!!!!!");

                }
                previousMouseState = mouseState;
                typingMode = kbHandler.typingMode;
            }
        }

        private void ProcessCamera(GameTime gameTime)
        {
            KeyboardState state = Keyboard.GetState();
            if (state.IsKeyDown(Keys.A) || state.IsKeyDown(Keys.Left))
            {
                this.camera.Position = new Vector2(this.camera.Position.X - 5, this.camera.Position.Y);
            }
            else if (state.IsKeyDown(Keys.D) || state.IsKeyDown(Keys.Right))
            {
                this.camera.Position = new Vector2(this.camera.Position.X + 5, this.camera.Position.Y);
            }
            if (state.IsKeyDown(Keys.W) || state.IsKeyDown(Keys.Up))
            {
                this.camera.Position = new Vector2(this.camera.Position.X, this.camera.Position.Y - 5);
            }
            else if (state.IsKeyDown(Keys.S) || state.IsKeyDown(Keys.Down))
            {
                this.camera.Position = new Vector2(this.camera.Position.X, this.camera.Position.Y + 5);
            }

            camera.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            // TODO: Add your drawing code here
            GraphicsDevice.Clear(Color.ForestGreen);
            spriteBatch.Begin(camera);

            MapManager.Draw(spriteBatch);

            player.Draw(spriteBatch);

            _NPCManager.DrawNPCs(spriteBatch);

            _GameObjectManager.Draw(spriteBatch);
            
            invenManager.Draw(spriteBatch, camera.ToWorld(new Vector2(20, 20)));

            mouseCursor.Draw(spriteBatch);

            spriteBatch.DrawString(font, kbHandler.Input, camera.ToWorld(new Vector2(100, 100)), Color.Black);
            spriteBatch.DrawString(font, player._HP.ToString(), camera.ToWorld(new Vector2(200, 200)), Color.White);

            base.Draw(gameTime);
            spriteBatch.End();
        }
    }
}
