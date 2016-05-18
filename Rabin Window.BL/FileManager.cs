using System.IO;
using System.Text;
using RabinLib;
using System.Numerics;
using System.Linq;
namespace Rabin_Window.BL
{
    public interface IFileManager
    {
        bool isExist(string filepath);
        void SaveContent(string content, string filepath);
        string GetContent(string path);
        string GetContent(string path, BigInteger SecretKeyOne, BigInteger SecretKeyTwo);
        string GetContent(string path, Encoding encoding, BigInteger SecretKeyOne, BigInteger SecretKeyTwo);
        void SaveContent(string content, string filepath, BigInteger OpenKey);
        void SaveContent(string content, string filepath, Encoding encoding, BigInteger OpenKey);
        int GetSymbloCount(string content);
    }

    public class FileManager : IFileManager
    {
        private readonly Encoding _defaultEncoding = Encoding.GetEncoding(1251);

        public bool isExist(string filepath)
        {
            return File.Exists(filepath);
        }

        public string GetContent(string path, BigInteger SecretKeyOne, BigInteger SecretKeyTwo)
        {
            return GetContent(path, _defaultEncoding, SecretKeyOne, SecretKeyTwo);
        }

        public string GetContent(string path)
        {
           return File.ReadAllText(path, _defaultEncoding);
        }

        public string GetContent(string path, Encoding encoding, BigInteger SecretKeyOne, BigInteger SecretKeyTwo)
        {
            BigInteger[] conten = File.ReadAllLines(path, encoding).Select(p => BigInteger.Parse(p)).ToArray();

            string result = Rabin.DecryptionBigText(conten, SecretKeyOne, SecretKeyTwo);

            return result;
        }

        public void SaveContent(string content, string filepath, BigInteger OpenKey)
        {
            SaveContent(content, filepath, _defaultEncoding, OpenKey);
        }

        public void SaveContent(string content, string filepath, Encoding encoding, BigInteger OpenKey)
        {
            BigInteger[] EncrypArr = Rabin.EncryptionBigText(content, OpenKey);


            string[] res = EncrypArr.Select(p => p + "").ToArray();

            File.WriteAllLines(filepath, res, encoding);

        }

        public void SaveContent(string content, string filepath)
        {
            File.WriteAllText(filepath, content,_defaultEncoding);
        }

        public int GetSymbloCount(string content)
        {
            return content.Length;
        }

    }
}
