using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Json;

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
            if (obj == null)
                throw new ArgumentNullException(nameof(obj), "The object cannot be null.");

            // Use System.Text.Json to serialize the object to a byte array
            return JsonSerializer.SerializeToUtf8Bytes(obj);
        }
        public static object FromByteArray(this byte[] byteArray)
        {
            if (byteArray == null || byteArray.Length == 0)
                throw new ArgumentNullException(nameof(byteArray), "The byte array cannot be null or empty.");

            // Deserialize the byte array back to the object
            return JsonSerializer.Deserialize<object>(byteArray);
        }
    }
}
