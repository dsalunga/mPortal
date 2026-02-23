using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Newtonsoft.Json;

namespace WCMS.Common.Utilities
{
    public abstract class SerializationUtil
    {
        public static void Serialize<T>(string filename, T o)
        {
            var json = JsonConvert.SerializeObject(o);
            File.WriteAllText(filename, json);
        }

        public static T Deserialize<T>(string filename)
        {
            var json = File.ReadAllText(filename);
            return JsonConvert.DeserializeObject<T>(json)
                ?? throw new InvalidOperationException($"Deserialization of '{filename}' returned null.");
        }
    }
}
