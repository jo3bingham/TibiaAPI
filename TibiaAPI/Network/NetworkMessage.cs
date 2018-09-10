using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using OXGaming.TibiaAPI.Appearances;
using OXGaming.TibiaAPI.Constants;
using OXGaming.TibiaAPI.Creatures;
using OXGaming.TibiaAPI.Imbuing;
using OXGaming.TibiaAPI.Market;
using OXGaming.TibiaAPI.Utilities;
using OXGaming.TibiaAPI.WorldMap;

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

        private const int GroundLayer = 7;
        private const int UndergroundLayer = 2;
        private const int MapSizeX = 18;
        private const int MapSizeY = 14;
        private const int MapSizeZ = 8;
        private const int MapSizeW = 10;
        private const int MapMaxZ = 15;

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

        public Position ReadPosition(int x = -1, int y = -1, int z = -1)
        {
            if (x == -1)
            {
                x = ReadUInt16();
            }
            if (y == -1)
            {
                y = ReadUInt16();
            }
            if (z == -1)
            {
                z = ReadByte();
            }
            return new Position(x, y, z);
        }

        public AppearanceInstance ReadMountOutfit(Client client)
        {
            var mountId = ReadUInt16();
            if (mountId == 0)
            {
                return null;
            }
            return client.AppearanceStorage.CreateOutfitInstance(mountId, 0, 0, 0, 0, 0);
        }

        public AppearanceInstance ReadCreatureOutfit(Client client)
        {
            var outfitId = ReadUInt16();
            if (outfitId != 0)
            {
                var colorHead = ReadByte();
                var colorTorso = ReadByte();
                var colorLegs = ReadByte();
                var colorDetail = ReadByte();
                var addons = ReadByte();
                return client.AppearanceStorage.CreateOutfitInstance(outfitId, colorHead, colorTorso, colorLegs, colorDetail, addons);
            }
            else
            {
                var itemId = ReadUInt16();
                if (itemId == 0)
                {
                    return client.AppearanceStorage.CreateOutfitInstance(0, 0, 0, 0, 0, 0);
                }
                return client.AppearanceStorage.CreateObjectInstance(itemId, 0);
            }
        }

        public ObjectInstance ReadObjectInstance(Client client, ushort id = 0)
        {
            if (id == 0)
            {
                id = ReadUInt16();
            }

            if (id == 0)
            {
                return null;
            }

            if (id <= 99)
            {
                throw new Exception($"[NetworkMessage.ReadObjectInstance] Invalid object id: {id}");
            }

            var objectInstance = client.AppearanceStorage.CreateObjectInstance(id, 0);
            if (objectInstance == null)
            {
                throw new Exception($"[NetworkMessage.ReadObjectInstance] Invalid object id: {id}");
            }

            var objectType = objectInstance.Type;
            if (objectType == null)
            {
                throw new Exception($"[NetworkMessage.ReadObjectInstance] Invalid object id: {id}");
            }

            if (objectType.Flags.Liquidcontainer || objectType.Flags.Liquidpool || objectType.Flags.Cumulative)
            {
                objectInstance.Data = ReadByte();
            }

            if (objectType.Flags.Container)
            {
                objectInstance.IsLootContainer = ReadBool();
                if (objectInstance.IsLootContainer)
                {
                    objectInstance.LootContainerUnknown = ReadUInt32(); // TODO
                }
            }

            if (objectType.FrameGroup[0].SpriteInfo.Animation != null)
            {
                objectInstance.Phase = ReadByte();
            }

            return objectInstance;
        }

        public Creature ReadCreatureInstance(Client client, int id = -1, Position position = null)
        {
            if (id == -1)
            {
                id = ReadUInt16();
            }

            if (id != (int)CreatureInstanceType.UnknownCreature &&
                id != (int)CreatureInstanceType.OutdatedCreature &&
                id != (int)CreatureInstanceType.Creature)
            {
                throw new Exception($"[NetworkMessage.ReadCreatureInstance] Invalid creature type: {id}");
            }

            Creature creature = null;

            switch (id)
            {
                case (int)CreatureInstanceType.UnknownCreature:
                    {
                        var removeCreatureId = ReadUInt32();
                        var creatureId = ReadUInt32();

                        //creature = (creatureId == client.Player.Id) ? client.Player : new Creature(creatureId);
                        creature = new Creature(creatureId)
                        {
                            Type = (CreatureType)ReadByte(),
                            RemoveCreatureId = removeCreatureId
                        };

                        if (creature.IsSummon)
                        {
                            creature.SummonerCreatureId = ReadUInt32();
                        }

                        creature.Name = ReadString();
                        creature.HealthPercent = ReadByte();
                        creature.Direction = (Direction)ReadByte();
                        creature.Outfit = ReadCreatureOutfit(client);
                        creature.Mount = ReadMountOutfit(client);
                        creature.Brightness = ReadByte();
                        creature.LightColor = ReadByte();
                        creature.Speed = ReadUInt16();
                        creature.PkFlag = ReadByte();
                        creature.PartyFlag = ReadByte();
                        creature.GuildFlag = ReadByte();

                        creature.Type = (CreatureType)ReadByte();
                        if (creature.IsSummon)
                        {
                            creature.SummonerCreatureId = ReadUInt32();
                        }

                        creature.SpeechCategory = ReadByte();
                        creature.Mark = ReadByte();
                        creature.InspectionState = ReadByte();
                        creature.IsUnpassable = ReadBool();
                    }
                    break;
                case (int)CreatureInstanceType.OutdatedCreature:
                    {
                        var creatureId = ReadUInt32();
                        creature = new Creature(creatureId)
                        {
                            HealthPercent = ReadByte(),
                            Direction = (Direction)ReadByte()
                        };

                        creature.Outfit = ReadCreatureOutfit(client);
                        creature.Mount = ReadMountOutfit(client);
                        creature.Brightness = ReadByte();
                        creature.LightColor = ReadByte();
                        creature.Speed = ReadUInt16();
                        creature.PkFlag = ReadByte();
                        creature.PartyFlag = ReadByte();

                        creature.Type = (CreatureType)ReadByte();
                        if (creature.IsSummon)
                        {
                            creature.SummonerCreatureId = ReadUInt32();
                        }

                        creature.SpeechCategory = ReadByte();
                        creature.Mark = ReadByte();
                        creature.InspectionState = ReadByte();
                        creature.IsUnpassable = ReadBool();
                    }
                    break;
                case (int)CreatureInstanceType.Creature:
                    {
                        var creatureId = ReadUInt32();
                        creature = new Creature(creatureId)
                        {
                            Direction = (Direction)ReadByte(),
                            IsUnpassable = ReadBool()
                        };
                    }
                    break;
            }

            if (position != null)
            {
                creature.Position = position;
            }

            return creature;
        }

        public Offer ReadMarketOffer(int kind, ushort typeId)
        {
            var timestamp = ReadUInt32();
            var counter = ReadUInt16();

            if (typeId == (int)MarketRequestType.OwnHistory ||
                typeId == (int)MarketRequestType.OwnOffers)
            {
                typeId = ReadUInt16();
            }

            var amount = ReadUInt16();
            var piecePrice = ReadUInt32();
            var character = string.Empty;
            var terminationReason = MarketOfferTerminationReason.Active;

            if (typeId == (int)MarketRequestType.OwnHistory)
            {
                terminationReason = (MarketOfferTerminationReason)ReadByte();
            }
            else if (typeId == (int)MarketRequestType.OwnOffers)
            {
            }
            else
            {
                character = ReadString();
            }

            return new Offer(new OfferId(timestamp, counter), kind, typeId, amount, piecePrice, character, terminationReason);
        }

        public ImbuementData ReadImbuementData()
        {
            var id = ReadUInt32();
            var name = ReadString();
            var imbuementData = new ImbuementData(id, name)
            {
                Description = ReadString(),
                Category = ReadString(),
                IconId = ReadUInt16(),
                DurationInSeconds = ReadUInt32(),
                PremiumOnly = ReadBool()
            };

            var astralSourceCount = ReadByte();
            for (var i = 0; i < astralSourceCount; i++)
            {
                var astralId = ReadUInt16();
                var astralName = ReadString();
                var count = ReadUInt16();
                imbuementData.AstralSources.Add(new AstralSource(astralId, count, astralName));
            }

            imbuementData.GoldCost = ReadUInt32();
            imbuementData.SuccessRatePercent = ReadByte();
            imbuementData.ProtectionGoldCost = ReadUInt32();
            return imbuementData;
        }

        public void ReadDailyReward()
        {
            // TODO: Work out switch case values, variable names, and
            // which case corresponds to which reward.
            var rewardType = ReadByte();
            switch (rewardType)
            {
                case 1:
                    {
                        var maxReward = ReadByte();
                        var numberOfRewardItems = ReadByte();
                        for (var j = 0; j < numberOfRewardItems; ++j)
                        {
                            var itemId = ReadUInt16();
                            var itemName = ReadString();
                            var itemWeight = ReadUInt32();
                        }
                    }
                    break;
                case 2:
                    {
                        var numberOfRewardItems = ReadByte();
                        for (var j = 0; j < numberOfRewardItems; ++j)
                        {
                            var rewardId = ReadByte();
                            switch (rewardId)
                            {
                                case 1:
                                    {
                                        var itemId = ReadUInt16();
                                        var itemName = ReadString();
                                        var itemCount = ReadByte();
                                    }
                                    break;
                                case 2:
                                    {
                                        var rewardCount = ReadByte();
                                    }
                                    break;
                                case 3:
                                    {
                                        var duration = ReadUInt16();
                                    }
                                    break;
                            }
                        }
                    }
                    break;
                case 3:
                    break;
            }
        }

        public int ReadField(Client client, int x, int y, int z, List<(Field, Position)> fields)
        {
            var thingsCount = 0;
            var numberOfTilesToSkip = 0;
            var mapPosition = new Position(x, y, z);
            var absolutePosition = client.WorldMapStorage.ToAbsolute(mapPosition);

            while (true)
            {
                var thingId = ReadUInt16();
                if (thingId >= 65280)
                {
                    numberOfTilesToSkip = thingId - 65280;
                    break;
                }

                if (thingId == (int)CreatureInstanceType.UnknownCreature ||
                    thingId == (int)CreatureInstanceType.OutdatedCreature ||
                    thingId == (int)CreatureInstanceType.Creature)
                {
                    var creature = ReadCreatureInstance(client, thingId, absolutePosition);
                    var objectInstance = client.AppearanceStorage.CreateObjectInstance(99, creature.Id);

                    if (thingsCount < MapSizeW)
                    {
                        client.WorldMapStorage.AppendObject(x, y, z, objectInstance);
                    }
                }
                else
                {
                    var objectInstance = ReadObjectInstance(client, thingId);

                    if (thingsCount < MapSizeW)
                    {
                        client.WorldMapStorage.AppendObject(x, y, z, objectInstance);
                    }
                    else
                    {
                        throw new Exception("Connection.readField: Expected creatures but received regular object.");
                    }
                }

                thingsCount++;
            }

            var field = client.WorldMapStorage.GetField(x, y, z);
            if (field != null)
            {
                fields.Add((field, absolutePosition));
            }

            return numberOfTilesToSkip;
        }

        public int ReadFloor(Client client, int floorNumber, int numberOfTilesToSkip, List<(Field, Position)> fields)
        {
            if (floorNumber < 0 || floorNumber >= MapSizeZ)
            {
                throw new Exception("ReadFloor: Floor number out of range.");
            }

            var currentX = 0;
            var currentY = 0;
            while (currentX <= MapSizeX - 1)
            {
                currentY = 0;
                while (currentY <= MapSizeY - 1)
                {
                    if (numberOfTilesToSkip > 0)
                    {
                        numberOfTilesToSkip--;
                    }
                    else
                    {
                        numberOfTilesToSkip = ReadField(client, currentX, currentY, floorNumber, fields);
                    }
                    currentY++;
                }
                currentX++;
            }

            return numberOfTilesToSkip;
        }

        public int ReadArea(Client client, int startX, int startY, int endX, int endY, List<(Field, Position)> fields)
        {
            var endZ = 0;
            var stepZ = 0;
            var numberOfTilesToSkip = 0;
            var currentX = 0;
            var currentY = 0;
            var currentZ = 0;
            var position = client.WorldMapStorage.GetPosition();

            if (position.Z <= GroundLayer)
            {
                currentZ = 0;
                endZ = GroundLayer + 1;
                stepZ = 1;
            }
            else
            {
                currentZ = 2 * UndergroundLayer;
                endZ = Math.Max(-1, position.Z - MapMaxZ + 1);
                stepZ = -1;
            }

            while (currentZ != endZ)
            {
                currentX = startX;
                while (currentX <= endX)
                {
                    currentY = startY;
                    while (currentY <= endY)
                    {
                        if (numberOfTilesToSkip > 0)
                        {
                            numberOfTilesToSkip--;
                        }
                        else
                        {
                            numberOfTilesToSkip = ReadField(client, currentX, currentY, currentZ, fields);
                        }
                        currentY++;
                    }
                    currentX++;
                }
                currentZ += stepZ;
            }

            return numberOfTilesToSkip;
        }

        /// <summary>
        /// Writes a byte array to the buffer.
        /// </summary>
        /// <param name="value">
        /// A byte array containing the data to write.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <see cref="value"/> is null.
        /// </exception>
        /// <exception cref="IndexOutOfRangeException">
        /// Thrown when the position + the length of 'value' exceeds the bounds of the buffer.
        /// </exception>
        public void Write(byte[] value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
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
            Write((ushort)value.X);
            Write((ushort)value.Y);
            Write((byte)value.Z);
        }

        public void Write(OutfitInstance value)
        {
            Write((ushort)value.Id);
            Write(value.ColorHead);
            Write(value.ColorTorso);
            Write(value.ColorLegs);
            Write(value.ColorDetail);
            Write(value.Addons);
        }

        public void Write(ObjectInstance value)
        {
            if (value == null || value.Type == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            Write((ushort)value.Id);

            if (value.Type.Flags.Liquidcontainer || value.Type.Flags.Liquidpool || value.Type.Flags.Cumulative)
            {
                Write((byte)value.Data);
            }

            if (value.Type.Flags.Container)
            {
                Write(value.IsLootContainer);
                if (value.IsLootContainer)
                {
                    Write(value.LootContainerUnknown);
                }
            }

            if (value.Type.FrameGroup[0].SpriteInfo.Animation != null)
            {
                Write(value.Phase);
            }
        }

        public void Write(Creature value, CreatureInstanceType type)
        {
            switch (type)
            {
                case CreatureInstanceType.UnknownCreature:
                    {
                        Write(value.RemoveCreatureId);
                        Write(value.Id);
                        Write((byte)value.Type);
                        if (value.IsSummon)
                        {
                            Write(value.SummonerCreatureId);
                        }

                        Write(value.Name);
                        Write(value.HealthPercent);
                        Write((byte)value.Direction);

                        if (value.Outfit is OutfitInstance)
                        {
                            Write((OutfitInstance)value.Outfit);
                        }
                        else
                        {
                            Write((ObjectInstance)value.Outfit);
                        }

                        Write(value.Mount.Id);
                        Write(value.Brightness);
                        Write(value.LightColor);
                        Write(value.Speed);
                        Write(value.PkFlag);
                        Write(value.PartyFlag);
                        Write(value.GuildFlag);

                        Write((byte)value.Type);
                        if (value.IsSummon)
                        {
                            Write(value.SummonerCreatureId);
                        }

                        Write(value.SpeechCategory);
                        Write(value.Mark);
                        Write(value.InspectionState);
                        Write(value.IsUnpassable);
                    }
                    break;
                case CreatureInstanceType.OutdatedCreature:
                    {
                        Write(value.Id);
                        Write(value.HealthPercent);
                        Write((byte)value.Direction);

                        if (value.Outfit is OutfitInstance)
                        {
                            Write((OutfitInstance)value.Outfit);
                        }
                        else
                        {
                            Write((ObjectInstance)value.Outfit);
                        }

                        Write(value.Mount.Id);
                        Write(value.Brightness);
                        Write(value.LightColor);
                        Write(value.Speed);
                        Write(value.PkFlag);
                        Write(value.PartyFlag);

                        Write((byte)value.Type);
                        if (value.IsSummon)
                        {
                            Write(value.SummonerCreatureId);
                        }

                        Write(value.SpeechCategory);
                        Write(value.Mark);
                        Write(value.InspectionState);
                        Write(value.IsUnpassable);
                    }
                    break;
                case CreatureInstanceType.Creature:
                    {
                        Write(value.Id);
                        Write((byte)value.Direction);
                        Write(value.IsUnpassable);
                    }
                    break;
            }
        }

        public void Write(Offer value, ushort typeId)
        {
            Write(value.OfferId.Timestamp);
            Write(value.OfferId.Counter);

            if (typeId == (int)MarketRequestType.OwnHistory ||
                typeId == (int)MarketRequestType.OwnOffers)
            {
                Write(value.TypeId);
            }

            Write(value.Amount);
            Write(value.PiecePrice);

            if (typeId == (int)MarketRequestType.OwnHistory)
            {
                Write((byte)value.TerminationReason);
            }
            else if (typeId == (int)MarketRequestType.OwnOffers)
            {
            }
            else
            {
                Write(value.Character);
            }
        }
        
        public void Write(ImbuementData value)
        {
            Write(value.Id);
            Write(value.Name);
            Write(value.Description);
            Write(value.Category);
            Write(value.IconId);
            Write(value.DurationInSeconds);
            Write(value.PremiumOnly);

            var count = (byte)Math.Min(value.AstralSources.Count, byte.MaxValue);
            Write(count);
            for (var i = 0; i < count; ++i)
            {
                var astralSource = value.AstralSources[i];
                Write(astralSource.AppearanceTypeId);
                Write(astralSource.Name);
                Write(astralSource.ObjectCount);
            }

            Write(value.GoldCost);
            Write(value.SuccessRatePercent);
            Write(value.ProtectionGoldCost);
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
