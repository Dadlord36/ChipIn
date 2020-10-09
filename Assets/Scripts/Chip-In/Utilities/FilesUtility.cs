using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public static class FilesUtility
    {
        public static async Task<byte[]> ReadFileBytesAsync(string filePath)
        {
            try
            {
                using (var sourceStream = File.Open(filePath, FileMode.Open, FileAccess.Read))
                {
                    var result = new byte[sourceStream.Length];
                    await sourceStream.ReadAsync(result, 0, (int) sourceStream.Length).ConfigureAwait(false);
                    return result;
                }
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }
        }

        public static async Task<string> ReadFileTextAsync(string filePath)
        {
            try
            {
                using (var sourceStream = File.Open(filePath, FileMode.Open, FileAccess.Read))
                {
                    var result = new byte[sourceStream.Length];
                    await sourceStream.ReadAsync(result, 0, (int) sourceStream.Length).ConfigureAwait(false);
                    return Encoding.ASCII.GetString(result);
                }
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }
        }

        public static async Task WriteBytesToFile(byte[] dataToWrite, string filePath)
        {
            try
            {
                using (var sourceStream = File.Open(filePath, FileMode.OpenOrCreate))
                {
                    sourceStream.Seek(0, SeekOrigin.End);
                    await sourceStream.WriteAsync(dataToWrite, 0, dataToWrite.Length).ConfigureAwait(false);
                }
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }
        }

        public static async Task WriteTextToFileAsync(string text, string filePath)
        {
            try
            {
                using (var writer = File.CreateText(filePath))
                {
                    await writer.WriteAsync(text).ConfigureAwait(false);
                }
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }
        }
    }
}