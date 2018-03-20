using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


using System.Collections.Generic;
using System.Linq;
using Noise;

namespace NoiseTest
{

    public class Game1 : Game
    {
        public static GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;


        SpriteFont fntBig;
        SpriteFont fntSmall;

        Texture2D blankTile;



        Terrain terrain;

        public static System.Random rand = new System.Random();

        struct Input
        {
            MouseState MS;
            MouseState lastMS;

            KeyboardState KS;
            KeyboardState lastKS;
        }


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }


        protected override void Initialize()
        {

            IsMouseVisible = true;
            graphics.PreferredBackBufferWidth = 1140;
            graphics.PreferredBackBufferHeight = 1000;
            graphics.SynchronizeWithVerticalRetrace = true;
            this.Window.Position = new Point(graphics.PreferredBackBufferWidth/2, 10);
            graphics.ApplyChanges();

            base.Initialize();
        }


        protected override void LoadContent()
        {

            fntBig = Content.Load<SpriteFont>(@"Fonts\consolas_22");
            fntSmall = Content.Load<SpriteFont>(@"Fonts\consolas_12");

            blankTile = Content.Load<Texture2D>(@"Textures\blankTile");

            spriteBatch = new SpriteBatch(GraphicsDevice);

            Logger.Start();


            terrain = new Terrain(500, 500, System.IO.Path.GetFullPath("..\\..\\..\\..\\..\\Maps\\mapTest.map"), 10);

        }




        protected override void Update(GameTime gameTime)
        {

        }




        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();

            terrain.Draw(spriteBatch);
           

            spriteBatch.End();


            base.Draw(gameTime);

        }
    }
}
