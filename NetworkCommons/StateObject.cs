using System.Runtime.Serialization.Formatters.Binary;
namespace NetworkCommons
{
    /// <summary>
    /// State object for reading client data asynchronously  
    /// </summary>
    public static class StateObject
    {
        // Size of receive buffer.  
        public const int BufferSize = 1024;

        public static byte[] ToByteArray(this object obj)
        {
            BinaryFormatter bf = new();
            using (var ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }
        public static object FromByteArray(this byte[] data)
        {
            if (data == null)
                return default;
            BinaryFormatter bf = new();
            using (MemoryStream ms = new MemoryStream(data))
            {
                object obj = bf.Deserialize(ms);
                return obj;
            }
        }
    }
}
