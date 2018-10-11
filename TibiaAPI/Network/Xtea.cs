using System;

namespace OXGaming.TibiaAPI.Network
{
    /// <summary>
    /// The <see cref="Xtea"/> class contains methods for encrypting and decrypting Tibia packets using the XTEA algorithm.
    /// </summary>
    public static class Xtea
    {
        private const uint Delta = 0x9E3779B9;
        private const uint BlockSize = 8;
        private const uint Rounds = 32;

        /// <summary>
        /// Decrypts a byte array using the XTEA algorithm.
        /// </summary>
        /// <param name="buffer">The byte array to decrypt.</param>
        /// <param name="index">Index of the underlying <see cref="NetworkMessage"/> buffer to start the decryption from.</param>
        /// <returns>
        /// Returns true if decryption was done on the <see cref="NetworkMessage"/>, false otherwise.
        /// </returns>
        public static unsafe bool Decrypt(byte[] buffer, uint length, uint[] key, uint index = 6)
        {
            if (buffer == null)
            {
                throw new ArgumentNullException(nameof(buffer));
            }

            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (key.Length != 4 || index >= length || ((length - index) % BlockSize) > 0)
            {
                return false;
            }

            fixed (byte* bufferPtr = buffer)
            {
                var uintPtr = (uint*)(bufferPtr + index);
                var decryptSize = length - index;
                for (var i = 0; i < (decryptSize / 4); i += 2)
                {
                    var sum = 0xC6EF3720;
                    var count = Rounds;
                    while (count-- > 0)
                    {
                        uintPtr[i + 1] -= ((uintPtr[i] << 4 ^ uintPtr[i] >> 5) + uintPtr[i]) ^ (sum + key[sum >> 11 & 3]);
                        sum -= Delta;
                        uintPtr[i] -= ((uintPtr[i + 1] << 4 ^ uintPtr[i + 1] >> 5) + uintPtr[i + 1]) ^ (sum + key[sum & 3]);
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Encrypts a <see cref="NetworkMessage"/> using the XTEA algorithm.
        /// </summary>
        /// <param name="message">The <see cref="NetworkMessage"/> to encrypt.</param>
        /// <param name="index">Index of the underlying <see cref="NetworkMessage"/> buffer to start the encryption from.</param>
        /// <returns>
        /// Returns true if encryption was done on the <see cref="NetworkMessage"/>, false otherwise.
        /// </returns>
        public static unsafe bool Encrypt(byte[] buffer, ref uint length, uint[] key, uint index = 6)
        {
            if (buffer == null)
            {
                throw new ArgumentNullException(nameof(buffer));
            }

            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (key.Length != 4 || index >= length)
            {
                return false;
            }

            var encryptSize = length - index;
            var padding = encryptSize % BlockSize;
            if (padding > 0)
            {
                encryptSize += BlockSize - padding;
                var newSize = index + encryptSize;
                if (newSize > NetworkMessage.MaxMessageSize)
                {
                    return false;
                }
                length = newSize;
            }

            fixed (byte* bufferPtr = buffer)
            {
                var uintPtr = (uint*)(bufferPtr + index);
                for (var i = 0; i < (encryptSize / 4); i += 2)
                {
                    var sum = 0u;
                    var count = Rounds;
                    while (count-- > 0)
                    {
                        uintPtr[i] += ((uintPtr[i + 1] << 4 ^ uintPtr[i + 1] >> 5) + uintPtr[i + 1]) ^ (sum + key[sum & 3]);
                        sum += Delta;
                        uintPtr[i + 1] += ((uintPtr[i] << 4 ^ uintPtr[i] >> 5) + uintPtr[i]) ^ (sum + key[sum >> 11 & 3]);
                    }
                }
            }

            return true;
        }
    }
}
