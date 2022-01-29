using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomServer.SubFuncs
{
    class ToByteArrConverter
    {
        public static byte[] ToByteArr(string value)
        {
            byte[] data = Encoding.Unicode.GetBytes(value);
            byte[] dataLength = BitConverter.GetBytes(data.Length);

            return dataLength.Concat(data).ToArray();
        }
        public static byte[] ToByteArr(int value)
        {
            byte[] data = BitConverter.GetBytes(value);
            byte[] dataLength = BitConverter.GetBytes(data.Length);

            return dataLength.Concat(data).ToArray();
        }
        public static byte[] ToByteArr(decimal value)
        {
            byte[] data = BitConverter.GetBytes(Convert.ToDouble(value));
            byte[] dataLength = BitConverter.GetBytes(data.Length);
            
            return dataLength.Concat(data).ToArray();
        }
        public static byte[] ToByteArr(bool value)
        {
            byte[] data = BitConverter.GetBytes(value);
            byte[] dataLength = BitConverter.GetBytes(data.Length);

            return dataLength.Concat(data).ToArray();
        }
        public static byte[] ToByteArr(int? value)
        {
            byte[] data = BitConverter.GetBytes((int)value);
            byte[] dataLength = BitConverter.GetBytes(data.Length);

            return dataLength.Concat(data).ToArray();
        }
        public static byte[] ToByteArr(byte[] value)
        {
            byte[] dataLength = BitConverter.GetBytes(value.Length);

            return dataLength.Concat(value).ToArray();
        }
        public static byte[] ToByteArr(DateTime value)
        {
            byte[] data = BitConverter.GetBytes(value.Ticks);
            byte[] dataLength = BitConverter.GetBytes(data.Length);

            return dataLength.Concat(data).ToArray();
        }
    }
}
