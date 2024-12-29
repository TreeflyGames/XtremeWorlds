using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Mirage.Sharp.Asfw.IO.Encryption
{
    public sealed class KeyPair : IDisposable
    {
        private RSACryptoServiceProvider _rsa;
        private bool _disposed = false;

        public void Dispose()
        {
            if (!_disposed)
            {
                _rsa?.Dispose();
                _rsa = null;
                _disposed = true;
            }
        }

        private void CheckDisposed()
        {
            if (_disposed)
                GenerateKeys();
        }

        public bool PublicOnly
        {
            get
            {
                CheckDisposed();
                return _rsa != null ? _rsa.PublicOnly : throw new CryptographicException("Key(s) not found!");
            }
        }

        public void GenerateKeys()
        {
            _rsa = new RSACryptoServiceProvider(2048);
            _disposed = false;
        }

        public string ExportKeyString(bool exportPrivate = false)
        {
            CheckDisposed();
            return _rsa.ToXmlString(exportPrivate);
        }

        public void ExportKey(string file, bool exportPrivate = true)
        {
            CheckDisposed();
            using (var streamWriter = new StreamWriter(file, false))
            {
                streamWriter.Write(_rsa.ToXmlString(exportPrivate));
            }
        }

        public void ImportKeyString(string key)
        {
            CheckDisposed();
            _rsa = new RSACryptoServiceProvider();
            _rsa.FromXmlString(key);
        }

        public void ImportKey(string file)
        {
            CheckDisposed();
            using (var streamReader = new StreamReader(file))
            {
                _rsa = new RSACryptoServiceProvider();
                _rsa.FromXmlString(streamReader.ReadToEnd());
            }
        }

        public byte[] EncryptBytes(byte[] value)
        {
            CheckDisposed();
            if (_rsa == null)
                throw new CryptographicException("Key not set.");

            using (var rijndael = Aes.Create("AesManaged"))
            {
                rijndael.KeySize = 256;
                rijndael.BlockSize = 128;
                rijndael.Mode = CipherMode.CBC;

                using (var memoryStream = new MemoryStream())
                {
                    var encryptedKey = _rsa.Encrypt(rijndael.Key, false);
                    if (encryptedKey.Length != 256)
                        return Array.Empty<byte>();

                    memoryStream.Write(encryptedKey, 0, encryptedKey.Length);
                    memoryStream.Write(rijndael.IV, 0, 16);

                    using (var encryptor = rijndael.CreateEncryptor())
                    using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(value, 0, value.Length);
                        cryptoStream.FlushFinalBlock();
                    }
                    return memoryStream.ToArray();
                }
            }
        }

        public async Task<byte[]> EncryptBytesAsync(byte[] value)
        {
            CheckDisposed();
            if (_rsa == null)
                throw new CryptographicException("Key not set.");

            using (var rijndael = Aes.Create("AesManaged"))
            {
                rijndael.KeySize = 256;
                rijndael.BlockSize = 128;
                rijndael.Mode = CipherMode.CBC;

                using (var memoryStream = new MemoryStream())
                {
                    await memoryStream.WriteAsync(_rsa.Encrypt(rijndael.Key, false), 0, 256);
                    await memoryStream.WriteAsync(rijndael.IV, 0, 16);

                    using (var encryptor = rijndael.CreateEncryptor())
                    using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        await cryptoStream.WriteAsync(value, 0, value.Length);
                        cryptoStream.FlushFinalBlock();
                    }
                    return memoryStream.ToArray();
                }
            }
        }

        public string EncryptString(string value)
        {
            CheckDisposed();
            return _rsa != null ? Convert.ToBase64String(EncryptBytes(Encoding.UTF8.GetBytes(value))) : string.Empty;
        }

        public async Task<string> EncryptStringAsync(string value)
        {
            CheckDisposed();
            if (_rsa == null)
                throw new CryptographicException("Key not set.");
            return Convert.ToBase64String(await EncryptBytesAsync(Encoding.UTF8.GetBytes(value)));
        }

        public string DecryptString(string value)
        {
            if (this._rsa == null)
                throw new CryptographicException("Key not set.");
            if (this._rsa.PublicOnly)
                return (string)null;
            byte[] numArray = Convert.FromBase64String(value);
            return numArray.Length >= 272 ? Encoding.UTF8.GetString(this.DecryptBytes(numArray)) : "";
        }

        public byte[] DecryptBytes(byte[] value)
        {
            if (this._rsa == null)
                throw new CryptographicException("Key not set.");
            if (this._rsa.PublicOnly)
                return (byte[])null;
            if (value.Length < 272)
                return (byte[])null;
            var rijndaelManaged = Aes.Create("AesManaged");
            rijndaelManaged.KeySize = 256;
            rijndaelManaged.BlockSize = 128;
            rijndaelManaged.Mode = CipherMode.CBC;
            byte[] numArray1 = new byte[256];
            byte[] numArray2 = new byte[16];
            int count = value.Length - 272;
            byte[] numArray3 = new byte[count];
            Buffer.BlockCopy((Array)value, 0, (Array)numArray1, 0, 256);
            Buffer.BlockCopy((Array)value, 256, (Array)numArray2, 0, 16);
            Buffer.BlockCopy((Array)value, 272, (Array)numArray3, 0, count);
            using (ICryptoTransform decryptor = rijndaelManaged.CreateDecryptor(this._rsa.Decrypt(numArray1, false), numArray2))
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(numArray3, 0, count);
                        cryptoStream.FlushFinalBlock();
                        return memoryStream.ToArray();
                    }
                }
            }
        }
    }
}
