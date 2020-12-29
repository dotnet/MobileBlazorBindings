// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;

namespace Microsoft.MobileBlazorBindings.ProtectedStorage
{
    /// <summary>
    /// A stream That uses the DataProtection API to encrypt / decrypt AES
    /// keys and store them into a stream.
    /// </summary>
    /// <remarks>With the entropy inside isolated storage and
    /// the Key + IV protected using DPAPI we have reasonable protection against
    /// other processes accessing this data. Any process being able to circumvent isolated
    /// storage and running the same credentials as our own process can decrypt this
    /// data however.</remarks>
    internal class DataProtectionStream : Stream
    {
        /// <summary>
        /// The inner stream that is encapsulated.
        /// </summary>
        private readonly Stream _innerStream;

        /// <summary>
        /// The crypto stream.
        /// </summary>
        private readonly CryptoStream _cryptoStream;

        /// <summary>
        /// Initializes a new instance of the <see cref="DataProtectionStream"/> class.
        /// </summary>
        /// <param name="innerStream">The stream to encapsulate.</param>
        /// <param name="dataProtectionScope">The data protection scope.</param>
        public DataProtectionStream(Stream innerStream, DataProtectionScope dataProtectionScope)
        {
            _innerStream = innerStream ?? throw new ArgumentNullException(nameof(innerStream));

            using var aes = Aes.Create();
            var formatter = new BinaryFormatter();

            if (innerStream.CanWrite)
            {
                aes.GenerateIV();
                aes.GenerateKey();
                aes.Padding = PaddingMode.PKCS7;

                var keyAndIv = new KeyAndIv()
                {
                    Entropy = new byte[16],
                };

                using (var rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(keyAndIv.Entropy);
                }

                keyAndIv.Key = ProtectedData.Protect(aes.Key, keyAndIv.Entropy, dataProtectionScope);
                keyAndIv.IV = ProtectedData.Protect(aes.IV, keyAndIv.Entropy, dataProtectionScope);
                keyAndIv.KeySize = aes.KeySize;
                keyAndIv.BlockSize = aes.BlockSize;
                keyAndIv.PaddingMode = aes.Padding;
                keyAndIv.CipherMode = aes.Mode;

                formatter.Serialize(_innerStream, keyAndIv);

                var encryptor = aes.CreateEncryptor();
                _cryptoStream = new CryptoStream(_innerStream, encryptor, CryptoStreamMode.Write);
            }
            else
            {
                var keyAndIv = (KeyAndIv)formatter.Deserialize(_innerStream);
                aes.KeySize = keyAndIv.KeySize;
                aes.BlockSize = keyAndIv.BlockSize;
                aes.Padding = keyAndIv.PaddingMode;
                aes.Mode = keyAndIv.CipherMode;

                aes.Key = ProtectedData.Unprotect(keyAndIv.Key, keyAndIv.Entropy, dataProtectionScope);
                aes.IV = ProtectedData.Unprotect(keyAndIv.IV, keyAndIv.Entropy, dataProtectionScope);

                var decryptor = aes.CreateDecryptor();
                _cryptoStream = new CryptoStream(_innerStream, decryptor, CryptoStreamMode.Read);
            }
        }

        /// <summary>
        /// Gets a value indicating whether the stream can read.
        /// </summary>
        public override bool CanRead => _innerStream.CanRead;

        /// <summary>
        /// Gets a value indicating whether the stream can seek.
        /// </summary>
        public override bool CanSeek => false;

        /// <summary>
        /// Gets a value indicating whether the stream can write.
        /// </summary>
        public override bool CanWrite => _innerStream.CanWrite;

        /// <summary>
        /// Gets the length of the stream.
        /// </summary>
        /// <remarks>Is not supported.</remarks>
        public override long Length => throw new NotSupportedException();

        /// <summary>
        /// Gets or sets the position in the stream.
        /// </summary>
        /// <remarks>Is not supported.</remarks>
        public override long Position { get => throw new NotSupportedException(); set => throw new NotSupportedException(); }

        /// <summary>
        /// Flushes the stream.
        /// </summary>
        public override void Flush()
        {
            _cryptoStream.Flush();
        }

        /// <summary>
        /// Flushes and pads the final block in the stream.
        /// </summary>
        public void FlushFinalBlock()
        {
            if (!_cryptoStream.HasFlushedFinalBlock)
            {
                _cryptoStream.FlushFinalBlock();
            }
        }

        /// <summary>
        /// Reads a sequence of bytes from the stream.
        /// </summary>
        /// <param name="buffer">The buffer where the read bytes are stored.</param>
        /// <param name="offset">The offset in the buffer where the bytes are stored.</param>
        /// <param name="count">The maximum amount of bytes to be read.</param>
        /// <returns>The actual number of bytes read.</returns>
        public override int Read(byte[] buffer, int offset, int count)
        {
            return _cryptoStream.Read(buffer, offset, count);
        }

        /// <summary>
        /// Seeks inside the stream and updates the Position.
        /// </summary>
        /// <param name="offset">The offset to seek to.</param>
        /// <param name="origin">The origin.</param>
        /// <returns>The number of bytes the position was moved.</returns>
        /// <remarks>Is not supported.</remarks>
        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Sets the length of the stream.
        /// </summary>
        /// <param name="value">The length.</param>
        /// <remarks>Is not supported.</remarks>
        public override void SetLength(long value)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Writes a sequence of bytes to the current System.Security.Cryptography.CryptoStream
        /// and advances the current position within the stream by the number of bytes written.
        /// </summary>
        /// <param name="buffer"> An array of bytes. This method copies count bytes from buffer to the current stream.</param>
        /// <param name="offset"> The byte offset in buffer at which to begin copying bytes to the current stream.</param>
        /// <param name="count">The number of bytes to be written to the current stream.</param>
        public override void Write(byte[] buffer, int offset, int count)
        {
            _cryptoStream.Write(buffer, offset, count);
        }

        /// <summary>
        /// Disposes the stream.
        /// </summary>
        /// <param name="disposing">A boolean indicating whether the stream is disposing, or it's called by the garbage collector.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (!_cryptoStream.HasFlushedFinalBlock)
                {
                    _cryptoStream.FlushFinalBlock();
                }

                _cryptoStream.Dispose();
                _innerStream.Dispose();
            }

            base.Dispose(disposing);
        }

        [Serializable]
        private class KeyAndIv
        {
            public byte[] Entropy { get; set; }

            public byte[] Key { get; set; }

            public byte[] IV { get; set; }

            public int KeySize { get; set; }

            public int BlockSize { get; set; }

            public PaddingMode PaddingMode { get; set; }

            public CipherMode CipherMode { get; set; }
        }
    }
}
