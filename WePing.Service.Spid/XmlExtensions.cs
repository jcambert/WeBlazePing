using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WePing.Service.Spid
{
    internal static class XmlExtensions
    {
        /// <summary>
        /// Deserialize an Xml string into Object T
        /// </summary>
        /// <typeparam name="T">Type Param</typeparam>
        /// <param name="input">The string to deserialize</param>
        /// <returns></returns>
        public static T Deserialize<T>(this string input) where T : class
        {
            XmlSerializer ser = new XmlSerializer(typeof(T));

            using StringReader sr = new StringReader(input);
            return (T)ser.Deserialize(sr);
        }

        /// <summary>
        /// Deserialize an Xml string into Object T
        /// </summary>
        /// <typeparam name="T">Type Param</typeparam>
        /// <param name="input">The stream to deserialize</param>
        /// <returns></returns>
        public static T Deserialize<T>(this Stream input) where T : class
        {
            XmlSerializer ser = new XmlSerializer(typeof(T));
            return (T)ser.Deserialize(input);

        }

        /// <summary>
        /// Deserialize an Xml string into Object T
        /// </summary>
        /// <typeparam name="T">Type Param</typeparam>
        /// <param name="input">The bytes to deserialize</param>
        /// <returns></returns>
        public static T Deserialize<T>(this byte[] input) where T : class
        {
                XmlSerializer ser = new XmlSerializer(typeof(T));
                using MemoryStream ms = new MemoryStream(input);
                return (T)ser.Deserialize(ms);
         

        }
    }
}
