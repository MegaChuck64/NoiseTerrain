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
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;


        FastNoise noise;
        SpriteFont fntBig;
        SpriteFont fntSmall;

        Texture2D blankTile;




        Tile[,] tiles;

        int width = 32;
        int height = 28;



        int peakHeight = 18;
        int scale = 10;
        float smoothness = 50f;
        float freq = 1f;


        int seed;

        System.Random rand = new System.Random((int)System.DateTime.UtcNow.Ticks);

        bool inDebug = false;





        public struct Tile
        {
            public int type;
            public string raw;

            public Tile(int _type, string _raw)
            {
                type = _type;
                raw = _raw;
            }

        }


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }


        protected override void Initialize()
        {
            seed = rand.Next(-10000, 10000);
            noise = new FastNoise();

            noise.SetNoiseType(FastNoise.NoiseType.Perlin);

            noise.SetFrequency(freq);
            noise.SetInterp(FastNoise.Interp.Quintic);

            tiles = new Tile[width, height];

            InitTiles();
          

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

            blankTile = Content.Load<Texture2D>(@"Textures\BlankTile");

            spriteBatch = new SpriteBatch(GraphicsDevice);

        }




        void InitTiles()
        {
            for (int x = 0; x < width; x++)
            {
                for(int y = 0; y < height; y++)
                {
                    tiles[x, y] = new Tile(0, 0.ToString());
                }
            }
        }



        static float xDelta = 0;

        /// <summary>
        /// -1 = left
        ///  1 = right
        /// </summary>
        /// <param name="xDir"></param>
        void GenTiles(int xDir)
        {
            if (System.Math.Abs(xDir) > 1)
            {
                return;
            }

            xDelta+=(xDir);



            for (int x = 0; x < width; x++)
            {
                int h = (int)((noise.GetNoise(seed, (int)(x+(xDelta/2))/smoothness) * scale) + peakHeight);
                for (int y = 0; y < height; y++)
                {
                    if (y >= h - 2)
                    {
                        tiles[x, y] = new Tile(2, h.ToString());
                    }
                    else if (y >= h - 4)
                    {
                        tiles[x, y] = new Tile(1, h.ToString());
                    }
                    else
                    {
                        tiles[x, y] = new Tile(0, h.ToString());
                    }
                }
            }

        }



        MouseState MS;
        MouseState lastMS;

        KeyboardState KS;
        KeyboardState lastKS;

        protected override void Update(GameTime gameTime)
        {
            KS = Keyboard.GetState();
            MS = Mouse.GetState();

            if (KS.IsKeyDown(Keys.Escape))
                Exit();


            if (KS.IsKeyDown(Keys.A))
            {
                GenTiles(-1);
            }

            if(KS.IsKeyDown(Keys.D))
            {
                GenTiles(1);
            }

            if (KS.IsKeyDown(Keys.Space) && lastKS.IsKeyUp(Keys.Space))
            {
                noise.SetSeed(rand.Next());
                GenTiles(0);
            }


            if (KS.IsKeyDown(Keys.OemQuestion) && lastKS.IsKeyUp(Keys.OemQuestion))
            {
                inDebug = !inDebug;
            }


            base.Update(gameTime);


            lastKS = KS;
            lastMS = MS;
        }



        int padding = 2;

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();

            Color col = Color.White;

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    switch (tiles[x,y].type)
                    {
                        case 0:
                            col = Color.DarkSlateBlue;
                            break;
                        case 1:
                            col = Color.ForestGreen;
                            break;
                        case 2:
                            col = Color.SaddleBrown;
                            break;
                    }

                    spriteBatch.Draw(blankTile, new Vector2(x * blankTile.Width + (padding * x) + 24, y * blankTile.Height + (padding * y) + 24), col);
                    if (inDebug) spriteBatch.DrawString(fntSmall, tiles[x, y].raw, new Vector2(x * blankTile.Width + (padding * x) + 4, y * blankTile.Height + (padding * y) + 4), Color.LightGray);
                }
            }


            spriteBatch.End();


            base.Draw(gameTime);

        }
    }
}
