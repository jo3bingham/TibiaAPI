using System;

namespace OXGaming.TibiaAPI.Network
{
    /// <summary>
    /// The <see cref="Xtea"/> class contains methods for encrypting and decrypting Tibia packets using the XTEA algorithm.
    /// </summary>
    internal class Xtea
    {
        private const uint Delta = 0x9E3779B9;
        private const uint BlockSize = 8;
        private const uint Rounds = 32;

        private readonly uint[] _key;

        /// <summary>
        /// Initializes a new instance of the <see cref="Xtea"/> class.
        /// </summary>
        /// <param name="key">
        /// The key (4 unsigned integers) that is used in the XTEA algorithm.
        /// 
        /// </param>
        public Xtea(uint[] key)
        {
            if (key == null || key.Length != 4)
            {
                throw new ArgumentException("[Xtea] Invalid key. Key must not be null and must have a length of 4.");
            }

            _key = key;
        }

        /// <summary>
        /// Decrypts a <see cref="NetworkMessage"/> using the XTEA algorithm.
        /// </summary>
        /// <param name="message">The <see cref="NetworkMessage"/> to decrypt.</param>
        /// <param name="index">Index of the underlying <see cref="NetworkMessage"/> buffer to start the decryption from.</param>
        /// <returns>
        /// Returns true if decryption was done on the <see cref="NetworkMessage"/>, false otherwise.
        /// </returns>
        public unsafe bool Decrypt(NetworkMessage message, uint index = 6)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message), "[Xtea.Decrypt] 'message' must not be null.");
            }

            if (index >= message.Size || ((message.Size - index) % BlockSize) > 0)
            {
                return false;
            }

            var buffer = message.GetBuffer();
            fixed (byte* bufferPtr = buffer)
            {
                var uintPtr = (uint*)(bufferPtr + index);
                var decryptSize = message.Size - index;
                for (var i = 0; i < (decryptSize / 4); i += 2)
                {
                    var sum = 0xC6EF3720;
                    var count = Rounds;
                    while (count-- > 0)
                    {
                        uintPtr[i + 1] -= ((uintPtr[i] << 4 ^ uintPtr[i] >> 5) + uintPtr[i]) ^ (sum + _key[sum >> 11 & 3]);
                        sum -= Delta;
                        uintPtr[i] -= ((uintPtr[i + 1] << 4 ^ uintPtr[i + 1] >> 5) + uintPtr[i + 1]) ^ (sum + _key[sum & 3]);
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
        public unsafe bool Encrypt(NetworkMessage message, uint index = 6)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message), "[Xtea.Encrypt] 'message' must not be null.");
            }

            if (index >= message.Size)
            {
                return false;
            }

            var encryptSize = message.Size - index;
            var padding = encryptSize % BlockSize;
            if (padding > 0)
            {
                encryptSize += BlockSize - padding;
                var newSize = index + encryptSize;
                if (newSize > NetworkMessage.MaxMessageSize)
                {
                    return false;
                }
                message.Size = newSize;
            }

            var buffer = message.GetBuffer();
            fixed (byte* bufferPtr = buffer)
            {
                var uintPtr = (uint*)(bufferPtr + index);
                for (var i = 0; i < (encryptSize / 4); i += 2)
                {
                    var sum = 0u;
                    var count = Rounds;
                    while (count-- > 0)
                    {
                        uintPtr[i] += ((uintPtr[i + 1] << 4 ^ uintPtr[i + 1] >> 5) + uintPtr[i + 1]) ^ (sum + _key[sum & 3]);
                        sum += Delta;
                        uintPtr[i + 1] += ((uintPtr[i] << 4 ^ uintPtr[i] >> 5) + uintPtr[i]) ^ (sum + _key[sum >> 11 & 3]);
                    }
                }
            }

            return true;
        }
    }
}
