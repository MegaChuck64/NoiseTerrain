using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Noise;

namespace NoiseTest
{
    public class Terrain
    {

        FastNoise noise;

        List<Texture2D> tileTextures = new List<Texture2D>();

        string mapFile;

        Dictionary<string, Tile> map = new Dictionary<string, Tile>();

        int width;
        int height;



        int peakHeight = 18;
        int scale = 10;
        float smoothness = 50f;
        float freq = .7f;


        int seed;


        public Terrain(int _width, int _height, string _mapFile, int textureCount)
        {
            mapFile = _mapFile;
            width = _width;
            height = _height;

            GenTextures(textureCount);            

            Start();

        }

        public void GenTextures(int count)
        {
            for (int i = 0; i < count; i++)
            {

                Texture2D text = new Texture2D(Game1.graphics.GraphicsDevice, 16, 16);
                Color[] cols = new Color[16 * 16];
                for (int x = 0; x < 16; x++)
                {
                    for (int y = 0; y < 16; y++)
                    {
                        if (Game1.rand.Next(0, 100) <= 15 && i != 0)
                        {
                            cols[x * 16 + y] = Color.Gray;
                        }
                        else
                        {
                            cols[x * 16 + y] = Color.White;
                        }
                    }
                }

                text.SetData<Color>(cols);
                           
                tileTextures.Add(text);
            }
        }

        public void Start()
        {
            seed = Game1.rand.Next(-10000, 10000);
            noise = new FastNoise();

            noise.SetNoiseType(FastNoise.NoiseType.Perlin);

            noise.SetFrequency(freq);
            noise.SetInterp(FastNoise.Interp.Quintic);

            //TimeSpan time = DateTime.Now.TimeOfDay;

            //map = Serializer.ReadDict(mapFile, map);

            //Logger.Log(String.Format("Reading {0}x{1} tiles took {2} seconds", width, height, DateTime.Now.TimeOfDay.Subtract(time).TotalSeconds));


            InitTiles();

            GenTiles(0);

            TimeSpan time = DateTime.Now.TimeOfDay;
            Serializer.WriteDict(mapFile, map);
            Logger.Log(String.Format("Writing {0}x{1} tiles took {2} seconds", width, height, DateTime.Now.TimeOfDay.Subtract(time).TotalSeconds));

        }




        void InitTiles()
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    map.Add(x + "_" + y, new Tile());
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

            xDelta += (xDir);



            for (int x = 0; x < width; x++)
            {
                int h = (int)((noise.GetNoise(seed, (int)(x + (xDelta / 2)) / smoothness) * scale) + peakHeight);
                for (int y = 0; y < height; y++)
                {
                    Tile newTile;
                    if (y >= h - 2)
                    {
                        newTile = new Tile(0, (byte)Game1.rand.Next(1, tileTextures.Count));

                    }
                    else if (y >= h - 4)
                    {
                        newTile = new Tile(1, (byte)Game1.rand.Next(1, tileTextures.Count));

                    }
                    else
                    {
                        newTile = new Tile(2, 0);
                    }

                    map[x + "_" + y] = newTile;
                }
            }

        }



        public void Update(GameTime gameTime)
        {

        }



        int padding = 1;


        public void Draw(SpriteBatch spriteBatch)
        {
            Color col = Color.White;


            for (int x = 0; x < 50; x++)
            {
                for (int y = 0; y < 50; y++)
                {


                    switch (map[x + "_" + y].type)
                    {
                        case 0:
                            col = Color.SaddleBrown;

                            break;
                        case 1:
                            col = Color.ForestGreen;
                            break;
                        case 2:
                            col = Color.DarkSlateBlue;
                            break;
                        case 3:
                            col = Color.SlateGray;
                            break;
                    }                   



                    spriteBatch.Draw(tileTextures[map[x+"_"+y].textureIndex], new Vector2(x * tileTextures[0].Width + (padding * x) + 24, y * tileTextures[0].Height + (padding * y) + 24), col);
                }
            }

        }

    }
}
