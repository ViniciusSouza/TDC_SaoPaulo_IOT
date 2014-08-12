using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace TDC2014WP.DataObjects
{
    public static class Helper
    {

        public static T Deserialize<T>(string jsonStringToDeserialize)
        {
            if (string.IsNullOrEmpty(jsonStringToDeserialize))
            {
                throw new ArgumentException("jsonStringToDeserialize must not be null");
            }

            MemoryStream ms = null;
            ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonStringToDeserialize));
            DataContractJsonSerializerSettings settings = new DataContractJsonSerializerSettings();
            settings.DateTimeFormat = new System.Runtime.Serialization.DateTimeFormat("yyyy-MM-ddTHH:mm:ss");

            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T), settings);

            return (T)serializer.ReadObject(ms);
        }



        public static Stream Serialize<T>(T objectToSerialize) where T : class
        {
            if (objectToSerialize == null)
            {
                throw new ArgumentException("objectToSerialize must not be null");
            }
            MemoryStream ms = null;

            DataContractJsonSerializerSettings settings = new DataContractJsonSerializerSettings();
            settings.DateTimeFormat = new System.Runtime.Serialization.DateTimeFormat("yyyy-MM-ddTHH:mm:ss");
            DataContractJsonSerializer serializer =
            new DataContractJsonSerializer(objectToSerialize.GetType(), settings);
            ms = new MemoryStream();

            serializer.WriteObject(ms, objectToSerialize);
            ms.Seek(0, SeekOrigin.Begin);

            return ms;
        }
    }



}
