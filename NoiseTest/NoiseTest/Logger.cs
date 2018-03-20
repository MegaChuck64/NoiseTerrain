using System;
using System.IO;
using System.Text;

using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NoiseTest
{
    static class Logger
    {


        static string directory = Path.GetFullPath("..\\..\\..\\..\\..\\Logs\\");
        
        static string todaysLogPath = "";
       
        static StreamWriter writer;
      
        static string spacer = " ____ || ____ ";

        static public void Start()
        {

            todaysLogPath = string.Format(directory + "{0}-{1}-{2}.log", DateTime.Today.Month, DateTime.Today.Day, DateTime.Today.Year);


            if (File.Exists(todaysLogPath))
            {
                writer = new StreamWriter(todaysLogPath,true);
            }
            else
            {
                writer = new StreamWriter(todaysLogPath, true);

                writer.WriteLine("~\t~\t~ NEW DAY ENTERED ~\t~\t~");
                writer.WriteLine("~\t~\t~ ~~ " + DateTime.Today.ToShortDateString() + " ~~ ~\t~\t~");
                writer.WriteLine("*");
                writer.WriteLine("*");
                Log();
                writer.WriteLine();
                writer.WriteLine("*");
                writer.WriteLine("*");


            }
            writer.AutoFlush = true;

            Log("Game Started");

        }



        static public void Log()
        {
            DateTime timeStamp = DateTime.Now;

            writer.Write("  LOG" + spacer + timeStamp.ToShortTimeString() + spacer);
        }

        static public void Log(string message)
        {
            Log();
            writer.WriteLine(message + spacer);
        }


    }
}
