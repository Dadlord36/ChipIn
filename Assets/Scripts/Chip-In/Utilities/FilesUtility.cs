using System.IO;
using System.Threading.Tasks;

namespace Utilities
{
    public static class FilesUtility
    {
        public static async Task<byte[]> ReadFileBytesAsync(string filePath)
        {
            using (var sourceStream = File.Open(filePath, FileMode.Open))
            {
                var result = new byte[sourceStream.Length];
                await sourceStream.ReadAsync(result, 0, (int) sourceStream.Length);
                return result;
            }
        }

        public static async Task<string> ReadFileTextAsync(string filePath)
        {
            using (var sourceStream = File.Open(filePath, FileMode.Open))
            {
                var result = new byte[sourceStream.Length];
                await sourceStream.ReadAsync(result, 0, (int) sourceStream.Length);
                return System.Text.Encoding.ASCII.GetString(result);
            }
        }

        private static async Task WriteBytesToFile(byte[] dataToWrite, string filePath)
        {
            using (var sourceStream = File.Open(filePath, FileMode.OpenOrCreate))
            {
                sourceStream.Seek(0, SeekOrigin.End);
                await sourceStream.WriteAsync(dataToWrite, 0, dataToWrite.Length);
            }
        }
    }
}