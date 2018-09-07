using System;
using System.IO;
using System.Text;

using OXGaming.TibiaAPI.Utilities;

namespace OXGaming.TibiaAPI.Network
{
    /// <summary>
    /// The <see cref="NetworkMessage"/> class contains methods for reading from, and writing to, a fix-sized byte array.
    /// </summary>
    /// <remarks>
    /// This is useful for parsing, and creating, Tibia packets.
    /// </remarks>
    public class NetworkMessage
    {
        /// <summary>
        /// The full length of a Tibia packet is stored in two bytes at the beginning of the packet.
        /// This means that a Tibia packet can never be larger than 65535 + 2. Using a max size of 65535
        /// ensures that limit is never exceeded.
        /// </summary>
        public const ushort MaxMessageSize = ushort.MaxValue;

        private const uint PayloadDataPosition = 8;

        private readonly byte[] _buffer = new byte[MaxMessageSize];

        private uint _size = PayloadDataPosition;

        /// <value>
        /// Gets the current position in the buffer.
        /// </value>
        public uint Position { get; private set; } = PayloadDataPosition;

        /// <value>
        /// Get/set the size of the message.
        /// </value>
        public uint Size { get => _size; set => _size = value; }

        public bool AddSequenceNumber { get; set; } = true;

        /// <summary>
        /// Gets the underlying buffer.
        /// </summary>
        /// <returns></returns>
        public byte[] GetBuffer()
        {
            return _buffer;
        }

        /// <value>
        /// Gets the actual data from the underlying buffer.
        /// </value>
        /// <remarks>
        /// Get this only when necessary as it creates a new byte array.
        /// </remarks>
        public byte[] GetData()
        {
            var data = new byte[Size];
            Array.Copy(_buffer, data, Size);
            return data;
        }

        public void Reset()
        {
            for (var i = 0; i < 8; ++i)
            {
                _buffer[i] = 0;
            }

            Position = 8;
            Size = 8;
        }

        /// <summary>
        /// Reads the specified number of bytes from the buffer into an array of bytes
        /// and advances the position by that number of bytes.
        /// </summary>
        /// <param name="count">
        /// The number of bytes to read.
        /// This value must be greater than 0 or an exception will occur.
        /// </param>
        /// <returns>
        /// An array of bytes containing the data read from the buffer.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when the 'count' parameter is less-than-or-equal-to 0.
        /// </exception>
        /// <exception cref="IndexOutOfRangeException">
        /// Thrown when the position + the 'count' parameter exceeds the bounds of the buffer.
        /// </exception>
        public byte[] ReadBytes(uint count)
        {
            if (count == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count),
                    "[NetworkMessage.ReadBytes] 'count' must be greater than 0.");
            }

            if (Position + count > _buffer.Length)
            {
                throw new IndexOutOfRangeException($"[NetworkMessage.ReadBytes] " +
                    $"'count' cannot exceed buffer size from current index. " +
                    $"index:{Position}, count:{count}, size:{Size}");
            }

            var data = new byte[count];
            Array.Copy(_buffer, Position, data, 0, count);
            Position += count;
            return data;
        }

        /// <summary>
        /// Reads the next byte from the buffer and advances the position by one.
        /// </summary>
        /// <returns>
        /// The next byte read from the buffer.
        /// </returns>
        public byte ReadByte() => ReadBytes(1)[0];

        /// <summary>
        /// Reads the next byte from the buffer and advances the position by one.
        /// </summary>
        /// <returns>
        /// A boolean value indicating whether the read byte was 0 or not.
        /// 0 returns false, anything else returns true.
        /// </returns>
        public bool ReadBool() => ReadByte() != 0;

        /// <summary>
        /// Reads a 2-byte signed integer from the buffer and advances the position by two.
        /// </summary>
        /// <returns>
        /// The 2-byte signed integer read from the buffer.
        /// </returns>
        public short ReadInt16() => BitConverter.ToInt16(ReadBytes(2), 0);

        /// <summary>
        /// Reads a 4-byte signed integer from the buffer and advances the position by four.
        /// </summary>
        /// <returns>
        /// The 4-byte signed integer read from the buffer.
        /// </returns>
        public int ReadInt32() => BitConverter.ToInt32(ReadBytes(4), 0);

        /// <summary>
        /// Reads an 8-byte signed integer from the buffer and advances the position by eight.
        /// </summary>
        /// <returns>
        /// The 8-byte signed integer read from the buffer.
        /// </returns>
        public long ReadInt64() => BitConverter.ToInt64(ReadBytes(8), 0);

        /// <summary>
        /// Reads a 2-byte unsigned integer from the buffer and advances the position by two.
        /// </summary>
        /// <returns>
        /// The 2-byte unsigned integer read from the buffer.
        /// </returns>
        public ushort ReadUInt16() => BitConverter.ToUInt16(ReadBytes(2), 0);

        /// <summary>
        /// Reads a 4-byte unsigned integer from the buffer and advances the position by four.
        /// </summary>
        /// <returns>
        /// The 4-byte unsigned integer read from the buffer.
        /// </returns>
        public uint ReadUInt32() => BitConverter.ToUInt32(ReadBytes(4), 0);

        /// <summary>
        /// Reads an 8-byte unsigned integer from the buffer and advances the position by eight.
        /// </summary>
        /// <returns>
        /// The 8-byte unsigned integer read from the buffer.
        /// </returns>
        public ulong ReadUInt64() => BitConverter.ToUInt64(ReadBytes(8), 0);

        /// <summary>
        /// Reads the next byte and the following 4-byte unsigned integer from the buffer and advances the position by five.
        /// </summary>
        /// <returns>
        /// A double value based on arithmetic done on the byte and 4-byte unsigned integer read from the buffer.
        /// </returns>
        public double ReadDouble()
        {
            var num1 = ReadByte();
            var num2 = ReadUInt32();
            return (num2 - int.MaxValue) / Math.Pow(10, num1);
        }

        /// <summary>
        /// Reads a string from the buffer. The string is prefixed with the length in a 2-byte unsigned integer.
        /// The position is advanced by 2 + the length of the string.
        /// </summary>
        /// <returns>
        /// An ASCII encoded string based on the bytes read from the buffer.
        /// </returns>
        public string ReadString()
        {
            var length = ReadUInt16();
            return Encoding.ASCII.GetString(ReadBytes(length));
        }

        public Position ReadPosition(ushort x = ushort.MaxValue, ushort y = ushort.MaxValue, byte z = byte.MaxValue)
        {
            if (x == ushort.MaxValue)
            {
                x = ReadUInt16();
            }
            if (y == ushort.MaxValue)
            {
                y = ReadUInt16();
            }
            if (z == byte.MaxValue)
            {
                z = ReadByte();
            }
            return new Position(x, y, z);
        }

        /// <summary>
        /// Writes a byte array to the buffer.
        /// </summary>
        /// <param name="value">
        /// A byte array containing the data to write.
        /// </param>
        /// <exception cref="ArgumentException">
        /// Thrown when the 'value' parameter is invalid (<see langword="null"/>).
        /// </exception>
        /// <exception cref="IndexOutOfRangeException">
        /// Thrown when the position + the length of 'value' exceeds the bounds of the buffer.
        /// </exception>
        public void Write(byte[] value)
        {
            if (value == null)
            {
                throw new ArgumentException("[NetworkMessage.Write] " +
                    "'value' cannot be null.");
            }

            if (Position + value.Length > _buffer.Length)
            {
                throw new IndexOutOfRangeException($"[NetworkMessage.Write] " +
                    $"'value' length cannot exceed buffer size from current index. " +
                    $"index:{Position}, value length:{value.Length}, size:{Size}");
            }

            Array.Copy(value, 0, _buffer, Position, value.Length);

            Position += (uint)value.Length;
            if (Position > Size)
            {
                Size = Position;
            }
        }

        /// <summary>
        /// Writes an unsigned byte to the buffer and advances the position by one.
        /// </summary>
        /// <param name="value">
        /// The unsigned byte to write.
        /// </param>
        public void Write(byte value) => Write(new byte[] { value });

        /// <summary>
        /// Writes a boolean value, as an unsigned byte, to the buffer and advances the position by one.
        /// </summary>
        /// <param name="value">
        /// The boolean value to write.
        /// </param>
        public void Write(bool value) => Write((byte)(value ? 1 : 0));

        /// <summary>
        /// Writes a two-byte signed integer to the buffer and advances the position by two.
        /// </summary>
        /// <param name="value">
        /// The two-byte signed integer to write.
        /// </param>
        public void Write(short value) => Write(BitConverter.GetBytes(value));

        /// <summary>
        /// Writes a four-byte signed integer to the buffer and advances the position by four.
        /// </summary>
        /// <param name="value">
        /// The four-byte signed integer to write.
        /// </param>
        public void Write(int value) => Write(BitConverter.GetBytes(value));

        /// <summary>
        /// Writes an eight-byte signed integer to the buffer and advances the position by eight.
        /// </summary>
        /// <param name="value">
        /// The eight-byte signed integer to write.
        /// </param>
        public void Write(long value) => Write(BitConverter.GetBytes(value));

        /// <summary>
        /// Writes a two-byte signed integer to the buffer and advances the position by two.
        /// </summary>
        /// <param name="value">
        /// The two-byte signed integer to write.
        /// </param>
        public void Write(ushort value) => Write(BitConverter.GetBytes(value));

        /// <summary>
        /// Writes a four-byte signed integer to the buffer and advances the position by four.
        /// </summary>
        /// <param name="value">
        /// The four-byte signed integer to write.
        /// </param>
        public void Write(uint value) => Write(BitConverter.GetBytes(value));

        /// <summary>
        /// Writes an eight-byte signed integer to the buffer and advances the position by eight.
        /// </summary>
        /// <param name="value">
        /// The eight-byte signed integer to write.
        /// </param>
        public void Write(ulong value) => Write(BitConverter.GetBytes(value));

        /// <summary>
        /// Writes an unsigned byte (precision), followed by a 4-byte unsigned integer based on arithmetic done on 'value',
        /// to the buffer and advances the position by five.
        /// </summary>
        /// <param name="value">
        /// The eight-byte floating point value to write.
        /// </param>
        public void Write(double value)
        {
            const byte precision = 2;
            Write(precision);
            Write((uint)((value * Math.Pow(10, precision)) + int.MaxValue));
        }

        /// <summary>
        /// Writes a length-prefixed string to the buffer with ASCII encoding.
        /// The position is advanced by 2 + the length of 'value'.
        /// </summary>
        /// <param name="value"></param>
        public void Write(string value)
        {
            if (value == null)
            {
                value = string.Empty;
            }

            Write((ushort)value.Length);
            Write(Encoding.ASCII.GetBytes(value));
        }

        public void Write(Position value)
        {
            Write(value.X);
            Write(value.Y);
            Write(value.Z);
        }

        /// <summary>
        /// Reads the specified number of bytes from the buffer into an array of bytes.
        /// </summary>
        /// <param name="count">
        /// The number of bytes to read.
        /// This value must be greater than 0 or an exception will occur.
        /// </param>
        /// <returns>
        /// An array of bytes containing the data read from the buffer.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when the 'count' parameter is less-than-or-equal-to 0.
        /// </exception>
        /// <exception cref="IndexOutOfRangeException">
        /// Thrown when the position + the 'count' parameter exceeds the bounds of the buffer.
        /// </exception>
        public byte[] PeekBytes(uint count)
        {
            if (count == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count),
                    "[NetworkMessage.PeekBytes] 'count' must be greater than 0.");
            }

            if (Position + count > _buffer.Length)
            {
                throw new IndexOutOfRangeException($"[NetworkMessage.PeekBytes] " +
                    $"'count' cannot exceed buffer size from current index. " +
                    $"index:{Position}, count:{count}, size:{Size}");
            }

            var data = new byte[count];
            Array.Copy(_buffer, Position, data, 0, count);
            return data;
        }

        /// <summary>
        /// Reads the next byte from the buffer.
        /// </summary>
        /// <returns>
        /// The next byte Peek from the buffer.
        /// </returns>
        public byte PeekByte() => PeekBytes(1)[0];

        /// <summary>
        /// Reads the next byte from the buffer.
        /// </summary>
        /// <returns>
        /// A boolean value indicating whether the read byte was 0 or not.
        /// 0 returns false, anything else returns true.
        /// </returns>
        public bool PeekBool() => PeekByte() != 0;

        /// <summary>
        /// Reads a 2-byte signed integer from the buffer.
        /// </summary>
        /// <returns>
        /// The 2-byte signed integer read from the buffer.
        /// </returns>
        public short PeekInt16() => BitConverter.ToInt16(PeekBytes(2), 0);

        /// <summary>
        /// Reads a 4-byte signed integer from the buffer.
        /// </summary>
        /// <returns>
        /// The 4-byte signed integer read from the buffer.
        /// </returns>
        public int PeekInt32() => BitConverter.ToInt32(PeekBytes(4), 0);

        /// <summary>
        /// Reads an 8-byte signed integer from the buffer.
        /// </summary>
        /// <returns>
        /// The 8-byte signed integer read from the buffer.
        /// </returns>
        public long PeekInt64() => BitConverter.ToInt64(PeekBytes(8), 0);

        /// <summary>
        /// Reads a 2-byte unsigned integer from the buffer.
        /// </summary>
        /// <returns>
        /// The 2-byte unsigned integer read from the buffer.
        /// </returns>
        public ushort PeekUInt16() => BitConverter.ToUInt16(PeekBytes(2), 0);

        /// <summary>
        /// Reads a 4-byte unsigned integer from the buffer.
        /// </summary>
        /// <returns>
        /// The 4-byte unsigned integer read from the buffer.
        /// </returns>
        public uint PeekUInt32() => BitConverter.ToUInt32(PeekBytes(4), 0);

        /// <summary>
        /// Reads an 8-byte unsigned integer from the buffer.
        /// </summary>
        /// <returns>
        /// The 8-byte unsigned integer read from the buffer.
        /// </returns>
        public ulong PeekUInt64() => BitConverter.ToUInt64(PeekBytes(8), 0);

        /// <summary>
        /// Reads the next byte and the following 4-byte unsigned integer from the buffer.
        /// </summary>
        /// <returns>
        /// A double value based on arithmetic done on the byte and 4-byte unsigned integer read from the buffer.
        /// </returns>
        public double PeekDouble()
        {
            var num1 = PeekByte();
            var num2 = PeekUInt32();
            return (num2 - int.MaxValue) / Math.Pow(10, num1);
        }

        /// <summary>
        /// Reads a string from the buffer. The string is prefixed with the length in a 2-byte unsigned integer.
        /// </summary>
        /// <returns>
        /// An ASCII encoded string based on the bytes read from the buffer.
        /// </returns>
        public string PeekString()
        {
            var length = PeekUInt16();
            return Encoding.ASCII.GetString(PeekBytes(length));
        }

        /// <summary>
        /// Sets the position of the buffer.
        /// </summary>
        /// <param name="offset">
        /// The offset, from the <paramref name="origin"/>, to set the position.
        /// </param>
        /// <param name="origin">
        /// The position in the buffer to seek from. Can be the beginning of the buffer, the current position, or the end of the buffer.
        /// </param>
        public void Seek(int offset, SeekOrigin origin)
        {
            if (origin == SeekOrigin.Begin)
            {
                if (offset < 0 || offset >= MaxMessageSize)
                {
                    throw new ArgumentOutOfRangeException($"");
                }

                Position = (uint)offset;
            }
            else if (origin == SeekOrigin.Current)
            {
                if ((Position + offset < 0) || (Position + offset >= MaxMessageSize))
                {
                    throw new ArgumentOutOfRangeException($"");
                }

                if (offset >= 0)
                {
                    Position += (uint)offset;
                }
                else
                {
                    Position -= (uint)offset;
                }
            }
            else if (origin == SeekOrigin.End)
            {
                if (offset > 0 || offset <= MaxMessageSize)
                {
                    throw new ArgumentOutOfRangeException($"");
                }

                Position = MaxMessageSize - (uint)offset - 1;
            }
        }

        public void PrepareToParse(uint[] xteaKey)
        {
            Xtea.Decrypt(_buffer, Size, xteaKey);

            Position = 6;
            Size = (uint)(ReadUInt16() + 8);
        }

        public void PrepareToSend(uint[] xteaKey, ref uint sequenceNumber)
        {
            Position = 6;
            Write((ushort)(Size - 8));

            Xtea.Encrypt(_buffer, ref _size, xteaKey);

            Position = 0;
            Write((ushort)(_size - 2));

            if (AddSequenceNumber)
            {
                Write(sequenceNumber++);
            }
        }
    }
}
