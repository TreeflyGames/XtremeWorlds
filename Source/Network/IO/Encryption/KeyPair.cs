using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Mirage.Sharp.Asfw.IO.Encryption
{
    public sealed class KeyPair : IDisposable
    {
        private RSA _rsa;

        public KeyPair()
        {
            GenerateKeys();
        }

        public void Dispose()
        {
            _rsa?.Dispose();
            _rsa = null;
        }

        private void CheckDisposed()
        {
            if (_rsa == null)
                GenerateKeys();
        }

        public bool PublicOnly
        {
            get
            {
                CheckDisposed();
                try
                {
                    _rsa.ExportParameters(true); // Attempt to export private key parameters
                    return false;
                }
                catch (CryptographicException)
                {
                    return true;
                }
            }
        }

        public void GenerateKeys()
        {
            _rsa?.Dispose();
            _rsa = RSA.Create();
            _rsa.KeySize = 2048;
        }

        public string ExportKeyString(bool exportPrivate = false)
        {
            CheckDisposed();

            if (OperatingSystem.IsWindows())
            {
                return _rsa.ToXmlString(exportPrivate);
            }
            else
            {
                if (exportPrivate)
                {
                    // Export private key as PKCS#8 (Base64)
                    var pkcs8 = _rsa.ExportPkcs8PrivateKey();
                    return Convert.ToBase64String(pkcs8);
                }
                else
                {
                    // Export public key as X.509 SubjectPublicKeyInfo (Base64)
                    var spki = _rsa.ExportSubjectPublicKeyInfo();
                    return Convert.ToBase64String(spki);
                }
            }
        }

        public void ExportKey(string file, bool exportPrivate = true)
        {
            CheckDisposed();
            string keyString = ExportKeyString(exportPrivate);
            File.WriteAllText(file, keyString);
        }

        public void ImportKeyString(string key)
        {
            CheckDisposed();
            try
            {
                byte[] keyBytes = Convert.FromBase64String(key);
                _rsa?.Dispose();
                _rsa = RSA.Create();
                try
                {
                    _rsa.ImportRSAPrivateKey(keyBytes, out _);
                }
                catch (CryptographicException)
                {
                    _rsa.ImportRSAPublicKey(keyBytes, out _);
                }
            }
            catch (Exception ex)
            {
                throw new CryptographicException("Failed to import key.", ex);
            }
        }

        public void ImportKey(string file)
        {
            CheckDisposed();
            string keyString = File.ReadAllText(file);
            ImportKeyString(keyString);
        }

        public byte[] EncryptBytes(byte[] value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value), "Input data cannot be null.");

            CheckDisposed();

            using (var rijndael = Aes.Create())
            {
                rijndael.KeySize = 256;
                rijndael.BlockSize = 128;
                rijndael.Mode = CipherMode.CBC;
                rijndael.Padding = PaddingMode.PKCS7;

                if (rijndael.Key == null || rijndael.IV == null)
                    throw new CryptographicException("Failed to generate AES key or IV.");

                using (var memoryStream = new MemoryStream())
                {
                    try
                    {
                        // Encrypt AES key with RSA
                        var encryptedKey = _rsa.Encrypt(rijndael.Key, RSAEncryptionPadding.OaepSHA256);

                        if (encryptedKey.Length != 256)
                            throw new CryptographicException("Invalid RSA-encrypted key length.");

                        // Write encrypted key and IV to the output stream
                        memoryStream.Write(encryptedKey, 0, encryptedKey.Length);
                        memoryStream.Write(rijndael.IV, 0, rijndael.IV.Length);

                        // Encrypt the data using AES
                        using (var encryptor = rijndael.CreateEncryptor())
                        using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                        {
                            cryptoStream.Write(value, 0, value.Length);
                            cryptoStream.FlushFinalBlock();
                        }

                        return memoryStream.ToArray();
                    }
                    catch (CryptographicException ex)
                    {
                        Console.WriteLine($"Encryption failed: {ex.Message}");
                        return null;
                    }
                }
            }
        }

        public async Task<byte[]> EncryptBytesAsync(byte[] value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value), "Input data cannot be null.");

            CheckDisposed();

            if (_rsa == null)
                throw new CryptographicException("Key not set.");

            using (var rijndael = Aes.Create())
            {
                rijndael.KeySize = 256;
                rijndael.BlockSize = 128;
                rijndael.Mode = CipherMode.CBC;
                rijndael.Padding = PaddingMode.PKCS7;

                if (rijndael.Key == null || rijndael.IV == null)
                    throw new CryptographicException("Failed to generate AES key or IV.");

                using (var memoryStream = new MemoryStream())
                {
                    try
                    {
                        // Encrypt AES key with RSA (using OAEP with SHA256)
                        var encryptedKey = _rsa.Encrypt(rijndael.Key, RSAEncryptionPadding.OaepSHA256);

                        if (encryptedKey.Length != 256)
                            throw new CryptographicException("Invalid RSA-encrypted key length.");

                        // Write encrypted key and IV to the output stream asynchronously
                        await memoryStream.WriteAsync(encryptedKey, 0, encryptedKey.Length);
                        await memoryStream.WriteAsync(rijndael.IV, 0, rijndael.IV.Length);

                        // Encrypt the data using AES
                        using (var encryptor = rijndael.CreateEncryptor())
                        {
                            await Task.Run(() =>
                            {
                                using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                                {
                                    cryptoStream.Write(value, 0, value.Length);
                                    cryptoStream.FlushFinalBlock();
                                }
                            });
                        }

                        return memoryStream.ToArray();
                    }
                    catch (CryptographicException ex)
                    {
                        Console.WriteLine($"Encryption failed: {ex.Message}");
                        return new byte[0];
                    }
                }
            }
        }

        public string EncryptString(string value)
        {
            CheckDisposed();
            var encryptedBytes = EncryptBytes(Encoding.UTF8.GetBytes(value));
            return encryptedBytes != null ? Convert.ToBase64String(encryptedBytes) : string.Empty;
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
            try
            {
                CheckDisposed();
                if (_rsa == null || PublicOnly)
                    return string.Empty;

                byte[] numArray = Convert.FromBase64String(value);
                byte[] decryptedBytes = DecryptBytes(numArray);

                return decryptedBytes != null && numArray.Length >= 272 ? Encoding.UTF8.GetString(decryptedBytes) : string.Empty;
            }
            catch (CryptographicException)
            {
                return string.Empty;
            }
        }

        public byte[] DecryptBytes(byte[] value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value), "Input data cannot be null.");
            if (value.Length < 272)
                throw new ArgumentException("Input data is too short to contain RSA-encrypted key, IV, and payload.", nameof(value));

            CheckDisposed();
            if (_rsa == null || PublicOnly)
                throw new CryptographicException("Private key is required for decryption.");

            try
            {
                // Extract RSA-encrypted AES key, IV, and payload
                byte[] encryptedKey = new byte[256];
                byte[] iv = new byte[16];
                int payloadLength = value.Length - 272;
                byte[] encryptedPayload = new byte[payloadLength];
                Buffer.BlockCopy(value, 0, encryptedKey, 0, 256);
                Buffer.BlockCopy(value, 256, iv, 0, 16);
                Buffer.BlockCopy(value, 272, encryptedPayload, 0, payloadLength);

                // Initialize AES
                using (var rijndael = Aes.Create())
                {
                    rijndael.KeySize = 256;
                    rijndael.BlockSize = 128;
                    rijndael.Mode = CipherMode.CBC;
                    rijndael.Padding = PaddingMode.PKCS7;

                    // Decrypt AES key
                    byte[] aesKey = _rsa.Decrypt(encryptedKey, RSAEncryptionPadding.OaepSHA256);

                    // Decrypt payload
                    using (var decryptor = rijndael.CreateDecryptor(aesKey, iv))
                    using (var memoryStream = new MemoryStream())
                    using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(encryptedPayload, 0, payloadLength);
                        cryptoStream.FlushFinalBlock();
                        return memoryStream.ToArray();
                    }
                }
            }
            catch (CryptographicException)
            {
                return null;
            }
        }
    }
}