using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace NoiseTest
{
    static class Serializer
    {
        static public void WriteDict(string path, Dictionary<string, Tile> dict)
        {

            FileStream fs = new FileStream(path, FileMode.Create);

            // Construct a BinaryFormatter and use it to serialize the data to the stream.
            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                formatter.Serialize(fs, dict);
            }
            catch (SerializationException e)
            {
                Logger.Log("Failed to serialize. Reason: " + e.Message);
                throw;
            }
            finally
            {
                fs.Close();
            }
        }


        static public Dictionary<string, Tile> ReadDict(string path, Dictionary<string, Tile> dict)
        {
            // Open the file containing the data that you want to deserialize.
            FileStream fs = new FileStream(path, FileMode.Open);
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();

                dict = (Dictionary<string,Tile>)formatter.Deserialize(fs);
            }
            catch (SerializationException e)
            {
                Logger.Log("Failed to deserialize. Reason: " + e.Message);
                throw;
            }
            finally
            {
                fs.Close();
            }

            return dict;

        }

    }
}