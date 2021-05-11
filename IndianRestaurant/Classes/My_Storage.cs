using System;
using System.IO;
using System.Xml.Serialization;

namespace IndianRestaurant
{
    internal class My_Storage
    {
        internal static T ReadXml<T>(string file)
        {
            try
            {
                using (StreamReader sr = new StreamReader(file))
                {
                    XmlSerializer xs = new XmlSerializer(typeof(T));
                    return (T)xs.Deserialize(sr);
                }
            }
            catch //(Exception)
            {

                return default(T);
            }
        }

        public static void WriteXml<T>(T data, string file)
        {
            try
            {
                XmlSerializer xs = new XmlSerializer(typeof(T));
                FileStream fs;
                fs = new FileStream(file, FileMode.Create);
                xs.Serialize(fs, data);
                fs.Close();
            }
            catch (Exception)
            {



            }
        }
    }
}