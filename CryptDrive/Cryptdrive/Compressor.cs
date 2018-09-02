using System.IO;
using System.IO.Compression;

namespace Cryptdrive
{
    //Function 1:Compress files (Byte[] -> Byte[])
    //Function 2:Uncompress files (Byte[] -> Byte[])
    class Compressor
    {
        public static byte[] compress(byte[] fileAsByteArray)
        {
            MemoryStream output = new MemoryStream();
            using (DeflateStream dstream = new DeflateStream(output, CompressionLevel.Optimal))
            {
                dstream.Write(fileAsByteArray, 0, fileAsByteArray.Length);
            }
            return output.ToArray();
        }

        public static byte[] decompress(byte[] fileAsByteArray)
        {
            MemoryStream input = new MemoryStream(fileAsByteArray);
            MemoryStream output = new MemoryStream();
            using (DeflateStream dstream = new DeflateStream(input, CompressionMode.Decompress))
            {
                dstream.CopyTo(output);
            }
            return output.ToArray();
        }
    }
}
