using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace WCMS.Common.Utilities
{
    public abstract class SerializationUtil
    {
        public static void Serialize<T>(string filename, T o)
        {
            using (Stream stream = File.Open(filename, FileMode.Create, FileAccess.ReadWrite))
            {
                BinaryFormatter b = new BinaryFormatter();
                b.Serialize(stream, o);
            }
        }

        public static T Deserialize<T>(string filename)
        {
            using (Stream stream = File.Open(filename, FileMode.Open, FileAccess.Read))
            {
                BinaryFormatter b = new BinaryFormatter();
                return (T)b.Deserialize(stream);
            }
        }
    }
}
