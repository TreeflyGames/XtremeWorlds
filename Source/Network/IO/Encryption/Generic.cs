using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Mirage.Sharp.Asfw.IO.Encryption
{
  public static class Generic
  {
    public static byte[] EncryptBytes(byte[] value, string password, int iterations)
    {
      int length1 = value.Length;
      byte[] numArray1 = new byte[32];
      byte[] numArray2 = new byte[32];
      using (var cryptoServiceProvider = RandomNumberGenerator.Create())
      {
        cryptoServiceProvider.GetBytes(numArray1);
        cryptoServiceProvider.GetBytes(numArray2);
      }
      using (Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, numArray1, iterations))
      {
        var rijndaelManaged = Aes.Create("AesManaged");
        rijndaelManaged.BlockSize = 128;
        rijndaelManaged.Mode = CipherMode.CBC;
        rijndaelManaged.Padding = PaddingMode.PKCS7;
        byte[] array;
        using (ICryptoTransform encryptor = rijndaelManaged.CreateEncryptor(rfc2898DeriveBytes.GetBytes(32), numArray2))
        {
          using (MemoryStream memoryStream = new MemoryStream())
          {
            using (CryptoStream cryptoStream = new CryptoStream((Stream) memoryStream, encryptor, CryptoStreamMode.Write))
            {
              cryptoStream.Write(value, 0, length1);
              cryptoStream.FlushFinalBlock();
              array = memoryStream.ToArray();
            }
          }
        }
        int length2 = array.Length;
        value = new byte[64 + length2];
        Buffer.BlockCopy((Array) numArray1, 0, (Array) value, 0, 32);
        Buffer.BlockCopy((Array) numArray2, 0, (Array) value, 32, 32);
        Buffer.BlockCopy((Array) array, 0, (Array) value, 64, length2);
        return value;
      }
    }

    public static async Task<byte[]> EncryptBytesAsync(
      byte[] value,
      string password,
      int iterations)
    {
      int length1 = value.Length;
      byte[] salt = new byte[32];
      byte[] rgbIv = new byte[32];
      using (var cryptoServiceProvider = RandomNumberGenerator.Create())
      {
        cryptoServiceProvider.GetBytes(salt);
        cryptoServiceProvider.GetBytes(rgbIv);
      }
      byte[] numArray;
      using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, salt, iterations))
      {
        var rijndaelManaged = Aes.Create("AesManaged");
        rijndaelManaged.BlockSize = 128;
        rijndaelManaged.Mode = CipherMode.CBC;
        rijndaelManaged.Padding = PaddingMode.PKCS7;
        byte[] array;
        using (ICryptoTransform es = rijndaelManaged.CreateEncryptor(bytes.GetBytes(32), rgbIv))
        {
          using (MemoryStream ms = new MemoryStream())
          {
            using (CryptoStream cs = new CryptoStream((Stream) ms, es, CryptoStreamMode.Write))
            {
              await cs.WriteAsync(value, 0, length1);
              cs.FlushFinalBlock();
              array = ms.ToArray();
            }
          }
        }
        int length2 = array.Length;
        value = new byte[64 + length2];
        Buffer.BlockCopy((Array) salt, 0, (Array) value, 0, 32);
        Buffer.BlockCopy((Array) rgbIv, 0, (Array) value, 32, 32);
        Buffer.BlockCopy((Array) array, 0, (Array) value, 64, length2);
        numArray = value;
      }
      salt = (byte[]) null;
      rgbIv = (byte[]) null;
      return numArray;
    }

    public static string EncryptString(string value, string password, int iterations) => Convert.ToBase64String(Generic.EncryptBytes(Encoding.UTF8.GetBytes(value), password, iterations));

    public static async Task<string> EncryptStringAsync(
      string value,
      string password,
      int iterations)
    {
      return Convert.ToBase64String(await Generic.EncryptBytesAsync(Encoding.UTF8.GetBytes(value), password, iterations));
    }

    public static byte[] DecryptBytes(byte[] value, string password, int iterations)
    {
      int count = value.Length - 64;
      byte[] numArray1 = new byte[32];
      byte[] numArray2 = new byte[32];
      byte[] numArray3 = new byte[count];
      Buffer.BlockCopy((Array) value, 0, (Array) numArray1, 0, 32);
      Buffer.BlockCopy((Array) value, 32, (Array) numArray2, 0, 32);
      Buffer.BlockCopy((Array) value, 64, (Array) numArray3, 0, count);
      var rijndaelManaged1 = Aes.Create("AesManaged");
      rijndaelManaged1.BlockSize = 128;
      rijndaelManaged1.Mode = CipherMode.CBC;
      rijndaelManaged1.Padding = PaddingMode.PKCS7;
      var rijndaelManaged2 = rijndaelManaged1;
      using (Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, numArray1, iterations))
      {
        using (ICryptoTransform decryptor = rijndaelManaged2.CreateDecryptor(rfc2898DeriveBytes.GetBytes(32), numArray2))
        {
          using (MemoryStream memoryStream = new MemoryStream())
          {
            using (CryptoStream cryptoStream = new CryptoStream((Stream) memoryStream, decryptor, CryptoStreamMode.Write))
            {
              cryptoStream.Write(numArray3, 0, count);
              cryptoStream.FlushFinalBlock();
              return memoryStream.ToArray();
            }
          }
        }
      }
    }

    public static async Task<byte[]> DecryptBytesAsync(
      byte[] value,
      string password,
      int iterations)
    {
      int count = value.Length - 64;
      byte[] numArray1 = new byte[32];
      byte[] numArray2 = new byte[32];
      byte[] numArray3 = new byte[count];
      Buffer.BlockCopy((Array) value, 0, (Array) numArray1, 0, 32);
      Buffer.BlockCopy((Array) value, 32, (Array) numArray2, 0, 32);
      Buffer.BlockCopy((Array) value, 64, (Array) numArray3, 0, count);
      var rijndaelManaged1 = Aes.Create("AesManaged");
      rijndaelManaged1.BlockSize = 128;
      rijndaelManaged1.Mode = CipherMode.CBC;
      rijndaelManaged1.Padding = PaddingMode.PKCS7;
      var rijndaelManaged2 = rijndaelManaged1;
      byte[] array;
      using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, numArray1, iterations))
      {
        using (ICryptoTransform ds = rijndaelManaged2.CreateDecryptor(bytes.GetBytes(32), numArray2))
        {
          using (MemoryStream ms = new MemoryStream())
          {
            using (CryptoStream cs = new CryptoStream((Stream) ms, ds, CryptoStreamMode.Write))
            {
              await cs.WriteAsync(numArray3, 0, count);
              cs.FlushFinalBlock();
              array = ms.ToArray();
            }
          }
        }
      }
      return array;
    }

    public static string DecryptString(string value, string password, int iterations) => Encoding.UTF8.GetString(Generic.DecryptBytes(Convert.FromBase64String(value), password, iterations));

    public static async Task<string> DecryptStringAsync(
      string value,
      string password,
      int iterations)
    {
      return Encoding.UTF8.GetString(await Generic.DecryptBytesAsync(Convert.FromBase64String(value), password, iterations));
    }
  }
}
