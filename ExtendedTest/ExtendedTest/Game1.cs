using Comora;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
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
        Player player;
        List<Sprite> gameObjectList;
        MouseState previousMouseState;
        Sprite mouseCursor;
        Sprite inventoryBG;
        Camera  camera;
        double fps = 0;
        double elapsedTime = 0;
        List<TileMap> mapList;
        InventoryManager invenManager;
        

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
            gameObjectList = new List<Sprite>();
            mapList = new List<TileMap>();
            camera = new Camera(GraphicsDevice);
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
            //mapList.Add(LoadMap("testmap", new Vector2(-(32 * 64), -(32 * 64)))); //top left
            //mapList.Add(LoadMap("testmap", new Vector2(0, -(32 * 64)))); //top middle
            ////mapList.Add(LoadMap("testmap", new Vector2((32 * 64), -(32 * 64)))); //top right
            //mapList.Add(LoadMap("00", new Vector2(-(32 * 64), 0))); //left
            mapList.Add(LoadMap("testmap", new Vector2(0, 0))); //middle
            //mapList.Add(LoadMap("00", new Vector2((32 * 64), 0))); //right
            //mapList.Add(LoadMap("00", new Vector2(-(32 * 64), (32 * 64)))); //bottom left
            //mapList.Add(LoadMap("testmap", new Vector2(0, (32 * 64)))); // bottom middle
            //mapList.Add(LoadMap("00", new Vector2((32 * 64), (32 * 64)))); //bottom right

            //List<Sprite> test = new List<Sprite>();
            //test = gameObjectList.FindAll(x => x._Tag == Sprite.SpriteType.kTreeType);

            // TODO: use this.Content to load your game content here
        }

        private void LoadPlayerContent()
        {
            player.LoadContent("Art/Player", Content);
            player._Position = new Vector2(0, 0);
        }

        private void LoadGUI()
        {
            inventoryBG = new Sprite();
            inventoryBG.LoadContent("Art/inventoryBG", Content);
            inventoryBG._Position = new Vector2(450, 450);

            mouseCursor = new Sprite();
            mouseCursor.LoadContent("Art/log", Content);
        }


        private TileMap LoadMap(String mapname, Vector2 pos)
        {
            TileMap testMap = new TileMap("Content/Tilemaps/" + mapname + ".tmx", Content, pos);

            TmxList<TmxObject> ObjectList = testMap.findObjects();
            if(ObjectList != null)
            {
                foreach (TmxObject thing in ObjectList)
                {
                    if (thing.Type.Equals("tree"))
                    {
                        Tree anotherTree = new Tree(Tree.TreeType.kNormalTree);
                        anotherTree.LoadContent("Art/tree", Content);
                        anotherTree._Position = new Vector2((int)thing.X + testMap._Postion.X, (int)thing.Y + testMap._Postion.Y);
                        anotherTree._Tag = Sprite.SpriteType.kTreeType;
                        anotherTree._CurrentState = Sprite.SpriteState.kStateActive;
                        anotherTree.parentList = gameObjectList;
                        gameObjectList.Add(anotherTree);
                    }
                    else if (thing.Type.Equals("rock"))
                    {
                        Rock anotherRock = new Rock(Rock.RockType.kNormalRock);
                        anotherRock.LoadContent("Art/rock", Content);
                        anotherRock._Position = new Vector2((int)thing.X + testMap._Postion.X, (int)thing.Y + testMap._Postion.Y);
                        anotherRock._Tag = Sprite.SpriteType.kRockType;
                        anotherRock._CurrentState = Sprite.SpriteState.kStateActive;
                        anotherRock.parentList = gameObjectList;
                        gameObjectList.Add(anotherRock);
                    }
                }
            }

            return testMap;

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
                    mouseCursor._Position = camera.ToWorld(new Vector2(mouseState.Position.X, mouseState.Position.Y)); //;
                    player.setDestination(mouseCursor._Position);
                }

                player.Update(gameTime, gameObjectList);

                foreach (Sprite sprite in gameObjectList)
                {
                    sprite.Update(gameTime, gameObjectList);
                }

                

                ProcessCamera(gameTime);

                Console.WriteLine(camera.Position);
                base.Update(gameTime);
                //Show FPS
                Console.WriteLine(1 / gameTime.ElapsedGameTime.TotalSeconds);
                previousMouseState = mouseState;
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

            foreach(TileMap map in mapList)
            {
                map.Draw(spriteBatch);
            }
            player.Draw(spriteBatch);
            foreach (Sprite sprite in gameObjectList)
            {
                sprite.Draw(spriteBatch);
            }

            int i = 0;
            foreach(Item item in invenManager.itemList)
            {
                item.Draw(spriteBatch, new Vector2(i * 30, -20));
                i++;
            }
            mouseCursor.Draw(spriteBatch);
            base.Draw(gameTime);
            spriteBatch.End();
        }
    }
}
