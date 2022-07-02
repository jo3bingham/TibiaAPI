using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using ComponentAce.Compression.Libs.zlib;

using OXGaming.TibiaAPI.Appearances;
using OXGaming.TibiaAPI.Constants;
using OXGaming.TibiaAPI.Creatures;
using OXGaming.TibiaAPI.DailyRewards;
using OXGaming.TibiaAPI.Imbuing;
using OXGaming.TibiaAPI.Market;
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
        private const uint PayloadDataPosition = 8;

        private const int GroundLayer = 7;
        private const int UndergroundLayer = 2;
        private const int MapSizeX = 18;
        private const int MapSizeY = 14;
        private const int MapSizeZ = 8;
        private const int MapSizeW = 10;
        private const int MapMaxZ = 15;

        /// <summary>
        /// The full length of a Tibia packet is stored in two bytes at the beginning of the packet.
        /// This means that a Tibia packet can never be larger than 65535 + 2. Using a max size of 65535
        /// ensures that limit is never exceeded.
        /// </summary>
        public const ushort MaxMessageSize = ushort.MaxValue;

        public const uint CompressedFlag = 0xC0000000;

        private readonly byte[] _buffer = new byte[MaxMessageSize];

        private Client _client;

        private uint _size = PayloadDataPosition;

        private bool _wasCompressed = false;

        /// <value>
        /// Gets the current position in the buffer.
        /// </value>
        public uint Position { get; private set; } = PayloadDataPosition;

        public uint SequenceNumber
        {
            get
            {
                return BitConverter.ToUInt32(_buffer, 2);
            }
            set
            {
                var sequenceNumber = IsCompressed ? (value | CompressedFlag) : value;
                var data = BitConverter.GetBytes(sequenceNumber);
                Array.Copy(data, 0, _buffer, 2, data.Length);
            }
        }

        /// <value>
        /// Get/set the size of the message.
        /// </value>
        public uint Size { get => _size; set => _size = value; }

        public bool IsCompressed
        {
            get
            {
                return (BitConverter.ToUInt32(_buffer, 2) & CompressedFlag) != 0;
            }
            set
            {
                if (value && !IsCompressed)
                {
                    SequenceNumber |= CompressedFlag;
                }
                else if (!value && IsCompressed)
                {
                    SequenceNumber ^= CompressedFlag;
                }
            }
        }

        public NetworkMessage(Client client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            SequenceNumber = ~CompressedFlag;
        }

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
        /// Call this only when necessary as it creates a new byte array.
        /// </remarks>
        public byte[] GetData()
        {
            var data = new byte[Size];
            Buffer.BlockCopy(_buffer, 0, data, 0, (int)Size);
            return data;
        }

        public void Reset()
        {
            Position = 0;
            Write(ushort.MinValue);
            Write(~CompressedFlag);
            Write(ushort.MinValue);
            Size = PayloadDataPosition;
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
        /// Reads the next unsigned byte from the buffer and advances the position by one.
        /// </summary>
        /// <returns>
        /// The next unsigned byte read from the buffer.
        /// </returns>
        public byte ReadByte() => ReadBytes(1)[0];

        /// <summary>
        /// Reads the next signed byte from the buffer and advances the position by one.
        /// </summary>
        /// <returns>
        /// The next signed byte read from the buffer.
        /// </returns>
        public sbyte ReadSByte() => unchecked((sbyte)ReadByte());

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
            return length == 0 ? string.Empty : Encoding.ASCII.GetString(ReadBytes(length));
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

        public AppearanceInstance ReadMountOutfit()
        {
            var mountId = ReadUInt16();
            return _client.AppearanceStorage.CreateOutfitInstance(mountId, 0, 0, 0, 0, 0);
        }

        public AppearanceInstance ReadCreatureOutfit()
        {
            var outfitId = ReadUInt16();
            if (outfitId != 0)
            {
                var colorHead = ReadByte();
                var colorTorso = ReadByte();
                var colorLegs = ReadByte();
                var colorDetail = ReadByte();
                var addons = ReadByte();
                return  _client.AppearanceStorage.CreateOutfitInstance(outfitId, colorHead, colorTorso,
                                                                       colorLegs, colorDetail, addons);
            }

            var itemId = ReadUInt16();
            if (itemId == 0)
            {
                return _client.AppearanceStorage.CreateOutfitInstance(0, 0, 0, 0, 0, 0);
            }
            return _client.AppearanceStorage.CreateObjectInstance(itemId, 0);
        }

        public ObjectInstance ReadObjectInstance(ushort id = 0)
        {
            if (id == 0)
            {
                id = ReadUInt16();
            }

            if (id == 0)
            {
                return new ObjectInstance(id, null);
            }

            if (id <= 99)
            {
                throw new Exception($"[NetworkMessage.ReadObjectInstance] Invalid object id: {id}");
            }

            var objectInstance = _client.AppearanceStorage.CreateObjectInstance(id, 0);
            if (objectInstance == null)
            {
                throw new Exception($"[NetworkMessage.ReadObjectInstance] Invalid object id: {id}");
            }

            var objectType = objectInstance.Type;
            if (objectType == null)
            {
                return objectInstance;
            }

            if (_client.VersionNumber < 11887288)
            {
                objectInstance.Mark = ReadByte();
            }

            if (objectType.Flags.Liquidcontainer || objectType.Flags.Liquidpool || objectType.Flags.Cumulative)
            {
                objectInstance.Data = ReadByte();
            }

            if (_client.VersionNumber >= 11506055 && objectType.Flags.Container)
            {
                objectInstance.IsLootContainer = ReadBool();
                if (objectInstance.IsLootContainer)
                {
                    objectInstance.LootCategoryFlags = ReadUInt32();
                }
            }

            if (_client.VersionNumber < 125110323 && objectType.FrameGroup[0].SpriteInfo.Animation != null)
            {
                objectInstance.Phase = ReadByte();
            }

            return objectInstance;
        }

        public Creature ReadCreatureInstance(int id = -1, Position position = null)
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

                        creature = (creatureId == _client.Player.Id) ? _client.Player : new Creature(creatureId);
                        creature.Type = (CreatureType)ReadByte();
                        creature.RemoveCreatureId = removeCreatureId;
                        creature.InstanceType = (CreatureInstanceType)id;

                        creature = _client.CreatureStorage.ReplaceCreature(creature, creature.RemoveCreatureId);
                        if (creature == null)
                        {
                            throw new Exception("[NetworkMessage.ReadCreatureInstance] Failed to append creature.");
                        }

                        if (creature.IsSummon)
                        {
                            creature.SummonerCreatureId = ReadUInt32();
                        }

                        creature.Name = ReadString();
                        creature.HealthPercent = ReadByte();
                        creature.Direction = (Direction)ReadByte();
                        creature.Outfit = ReadCreatureOutfit();
                        creature.Mount = ReadMountOutfit();
                        creature.Brightness = ReadByte();
                        creature.LightColor = ReadByte();
                        creature.Speed = ReadUInt16();

                        if (_client.VersionNumber >= 12409997)
                        {
                            // Always 0. I tried changing it to a different value for players, monsters,
                            // and NPCs (separately), but it would always crash the client.
                            creature.Unknown = ReadByte();
                        }

                        creature.PkFlag = ReadByte();
                        creature.PartyFlag = ReadByte();
                        creature.GuildFlag = ReadByte();

                        creature.Type = (CreatureType)ReadByte();
                        if (creature.Type == CreatureType.Player && _client.VersionNumber >= 12200000)
                        {
                            creature.Vocation = ReadByte();
                        }
                        else if (creature.IsSummon)
                        {
                            creature.SummonerCreatureId = ReadUInt32();
                        }

                        creature.SpeechCategory = ReadByte();
                        creature.Mark = ReadByte();
                        creature.InspectionState = ReadByte();
                        if (_client.VersionNumber < 11900000)
                        {
                            creature.PvpHelpers = ReadUInt16();
                        }
                        creature.IsUnpassable = ReadBool();
                    }
                    break;
                case (int)CreatureInstanceType.OutdatedCreature:
                    {
                        var creatureId = ReadUInt32();
                        creature = _client.CreatureStorage.GetCreature(creatureId);
                        if (creature == null)
                        {
                            // This should never occur on official servers, but has been observed
                            // on Open-Tibia servers via cast system. Log the error and create
                            // a new Creature so that the parser can continue gracefully.
                            _client.Logger.Error("[NetworkMessage.ReadCreatureInstance] Outdated creature not found.");
                            creature = new Creature(creatureId);
                            creature = _client.CreatureStorage.ReplaceCreature(creature);
                        }

                        creature.InstanceType = (CreatureInstanceType)id;
                        creature.HealthPercent = ReadByte();
                        creature.Direction = (Direction)ReadByte();
                        creature.Outfit = ReadCreatureOutfit();
                        creature.Mount = ReadMountOutfit();
                        creature.Brightness = ReadByte();
                        creature.LightColor = ReadByte();
                        creature.Speed = ReadUInt16();

                        if (_client.VersionNumber >= 12409997)
                        {
                            // Always 0. I tried changing it to a different value for players, monsters,
                            // and NPCs (separately), but it would always crash the client.
                            creature.Unknown = ReadByte();
                        }

                        creature.PkFlag = ReadByte();
                        creature.PartyFlag = ReadByte();

                        creature.Type = (CreatureType)ReadByte();
                        if (creature.Type == CreatureType.Player && _client.VersionNumber >= 12200000)
                        {
                            creature.Vocation = ReadByte();
                        }
                        else if (creature.IsSummon)
                        {
                            creature.SummonerCreatureId = ReadUInt32();
                        }

                        creature.SpeechCategory = ReadByte();
                        creature.Mark = ReadByte();
                        creature.InspectionState = ReadByte();
                        if (_client.VersionNumber < 11900000)
                        {
                            creature.PvpHelpers = ReadUInt16();
                        }
                        creature.IsUnpassable = ReadBool();
                    }
                    break;
                case (int)CreatureInstanceType.Creature:
                    {
                        var creatureId = ReadUInt32();
                        creature = _client.CreatureStorage.GetCreature(creatureId);
                        if (creature == null)
                        {
                            // This should never occur on official servers, but has been observed
                            // on Open-Tibia servers via cast system. Log the error and create
                            // a new Creature so that the parser can continue gracefully.
                            _client.Logger.Error("[NetworkMessage.ReadCreatureInstance] Known creature not found.");
                            creature = new Creature(creatureId);
                            creature = _client.CreatureStorage.ReplaceCreature(creature);
                        }

                        creature.InstanceType = (CreatureInstanceType)id;
                        creature.Direction = (Direction)ReadByte();
                        creature.IsUnpassable = ReadBool();
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
            var itemId = typeId;

            if (typeId == (int)MarketRequestType.OwnHistory ||
                typeId == (int)MarketRequestType.OwnOffers)
            {
                itemId = ReadUInt16();
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

            return new Offer(new OfferId(timestamp, counter), kind, itemId, amount, piecePrice, character, terminationReason);
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

        public DailyReward ReadDailyReward()
        {
            var dailyReward = new DailyReward
            {
                Type = ReadByte()
            };

            switch (dailyReward.Type)
            {
                case 1: // Choice
                    {
                        dailyReward.TotalChoiceRewards = ReadByte();
                        dailyReward.ChoiceRewards.Capacity = ReadByte();
                        for (var i = 0; i < dailyReward.ChoiceRewards.Capacity; ++i)
                        {
                            var itemId = ReadUInt16();
                            var name = ReadString();
                            var weight = ReadUInt32();
                            dailyReward.ChoiceRewards.Add((itemId, name, weight));
                        }
                    }
                    break;
                case 2: // Set
                    {
                        dailyReward.SetRewards.Capacity = ReadByte();
                        for (var i = 0; i < dailyReward.SetRewards.Capacity; ++i)
                        {
                            var type = ReadByte();
                            switch (type)
                            {
                                case 1: // Item
                                    {
                                        var itemId = ReadUInt16();
                                        var name = ReadString();
                                        var count = ReadByte();
                                        dailyReward.SetRewards.Add((type, itemId, name, count, 0));
                                    }
                                    break;
                                case 2: // Prey Bonus Reroll
                                    {
                                        var count = ReadByte();
                                        dailyReward.SetRewards.Add((type, 0, "", count, 0));
                                    }
                                    break;
                                case 3: // XP Boost
                                    {
                                        var duration = ReadUInt16();
                                        dailyReward.SetRewards.Add((type, 0, "", 0, duration));
                                    }
                                    break;
                                default:
                                    _client.Logger.Error($"Unknown SetDailyReward type: {type}");
                                    break;
                            }
                        }
                    }
                    break;
                default:
                    _client.Logger.Debug($"Unknown DailyReward type: {dailyReward.Type}");
                    break;
            }

            return dailyReward;
        }

        public int ReadField(int x, int y, int z, List<(int, List<ObjectInstance>, Position)> fields)
        {
            var hasSetEnvironmentalEffect = false;
            var thingsCount = 0;
            var numberOfTilesToSkip = 0;
            var mapPosition = new Position(x, y, z);
            var absolutePosition = _client.WorldMapStorage.ToAbsolute(mapPosition);
            var objects = new List<ObjectInstance>();

            while (true)
            {
                var thingId = ReadUInt16();
                if (thingId >= 0xFF00)
                {
                    numberOfTilesToSkip = thingId - 0xFF00;
                    break;
                }

                if (!hasSetEnvironmentalEffect)
                {
                    hasSetEnvironmentalEffect = true;
                    if (_client.VersionNumber < 11880000)
                    {
                        continue;
                    }
                }

                if (thingId == (int)CreatureInstanceType.UnknownCreature ||
                    thingId == (int)CreatureInstanceType.OutdatedCreature ||
                    thingId == (int)CreatureInstanceType.Creature)
                {
                    var creature = ReadCreatureInstance(thingId, absolutePosition);
                    var objectInstance = _client.AppearanceStorage.CreateObjectInstance((uint)CreatureInstanceType.Creature, creature.Id);

                    if (thingsCount < MapSizeW)
                    {
                        objects.Add(objectInstance);
                        _client.WorldMapStorage.AppendObject(x, y, z, objectInstance);
                    }
                }
                else
                {
                    var objectInstance = ReadObjectInstance(thingId);

                    if (thingsCount < MapSizeW)
                    {
                        objects.Add(objectInstance);
                        _client.WorldMapStorage.AppendObject(x, y, z, objectInstance);
                    }
                    else
                    {
                        throw new Exception("Connection.readField: Expected creatures but received regular object.");
                    }
                }

                thingsCount++;
            }

            fields.Add((numberOfTilesToSkip, objects, absolutePosition));

            return numberOfTilesToSkip;
        }

        public int ReadFloor(int floorNumber, int numberOfTilesToSkip, List<(int, List<ObjectInstance>, Position)> fields)
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
                        numberOfTilesToSkip = ReadField(currentX, currentY, floorNumber, fields);
                    }
                    currentY++;
                }
                currentX++;
            }

            return numberOfTilesToSkip;
        }

        public int ReadArea(int startX, int startY, int endX, int endY, List<(int, List<ObjectInstance>, Position)> fields)
        {
            var endZ = 0;
            var stepZ = 0;
            var numberOfTilesToSkip = 0;
            var currentX = 0;
            var currentY = 0;
            var currentZ = 0;
            var position = _client.WorldMapStorage.GetPosition();

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
                            numberOfTilesToSkip = ReadField(currentX, currentY, currentZ, fields);
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

        public void Write(byte[] value, uint index, uint length)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (length > value.Length || index + length > value.Length)
            {
                throw new IndexOutOfRangeException($"[NetworkMessage.Write] " +
                    $"length cannot exceed value length from index. " +
                    $"index:{index}, value length:{value.Length}, length:{length}");
            }

            var data = new byte[length];
            Array.Copy(value, index, data, 0, length);
            Write(data);
        }

        /// <summary>
        /// Writes an unsigned byte to the buffer and advances the position by one.
        /// </summary>
        /// <param name="value">
        /// The unsigned byte to write.
        /// </param>
        public void Write(byte value) => Write(new byte[] { value });

        /// <summary>
        /// Writes a signed byte to the buffer and advances the position by one.
        /// </summary>
        /// <param name="value">
        /// The signed byte to write.
        /// </param>
        public void Write(sbyte value) => Write(new byte[] { unchecked((byte)value) });

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
            const byte precision = 3;
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
            if (value.Length > 0)
            {
                Write(Encoding.ASCII.GetBytes(value));
            }
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
            if (value.Id == 0)
            {
                Write((ushort)0);
            }
            else
            {
                Write(value.ColorHead);
                Write(value.ColorTorso);
                Write(value.ColorLegs);
                Write(value.ColorDetail);
                Write(value.Addons);
            }
        }

        public void Write(ObjectInstance value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            Write((ushort)value.Id);

            if (value.Type == null)
            {
                return;
            }

            if (_client.VersionNumber < 11900000)
            {
                Write(value.Mark);
            }

            if (value.Type.Flags.Liquidcontainer || value.Type.Flags.Liquidpool || value.Type.Flags.Cumulative)
            {
                Write((byte)value.Data);
            }

            if (value.Type.Flags.Container)
            {
                Write(value.IsLootContainer);
                if (value.IsLootContainer)
                {
                    Write(value.LootCategoryFlags);
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
                            Write((ushort)0);
                            Write((ushort)value.Outfit.Id);
                        }

                        Write((ushort)value.Mount.Id);
                        Write(value.Brightness);
                        Write(value.LightColor);
                        Write(value.Speed);

                        if (_client.VersionNumber >= 12409997)
                        {
                            Write(value.Unknown);
                        }

                        Write(value.PkFlag);
                        Write(value.PartyFlag);
                        Write(value.GuildFlag);

                        Write((byte)value.Type);
                        if (value.Type == CreatureType.Player && _client.VersionNumber >= 12200000)
                        {
                            Write(value.Vocation);
                        }
                        else if (value.IsSummon)
                        {
                            Write(value.SummonerCreatureId);
                        }

                        Write(value.SpeechCategory);
                        Write(value.Mark);
                        Write(value.InspectionState);
                        if (_client.VersionNumber < 11900000)
                        {
                            Write(value.PvpHelpers);
                        }
                        Write(value.IsUnpassable);
                    }
                    break;
                case CreatureInstanceType.OutdatedCreature:
                    {
                        Write(value.Id);
                        Write(value.HealthPercent);
                        Write((byte)value.Direction);

                        if (value.Outfit is OutfitInstance instance)
                        {
                            Write(instance);
                        }
                        else
                        {
                            Write((ushort)0);
                            Write((ushort)value.Outfit.Id);
                        }

                        Write((ushort)value.Mount.Id);
                        Write(value.Brightness);
                        Write(value.LightColor);
                        Write(value.Speed);

                        if (_client.VersionNumber >= 12409997)
                        {
                            Write(value.Unknown);
                        }

                        Write(value.PkFlag);
                        Write(value.PartyFlag);

                        Write((byte)value.Type);
                        if (value.Type == CreatureType.Player && _client.VersionNumber >= 12200000)
                        {
                            Write(value.Vocation);
                        }
                        else if (value.IsSummon)
                        {
                            Write(value.SummonerCreatureId);
                        }

                        Write(value.SpeechCategory);
                        Write(value.Mark);
                        Write(value.InspectionState);
                        if (_client.VersionNumber < 11900000)
                        {
                            Write(value.PvpHelpers);
                        }
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

        public void Write(DailyReward dailyReward)
        {
            Write(dailyReward.Type);
            switch (dailyReward.Type)
            {
                case 1: // Choice
                    {
                        Write(dailyReward.TotalChoiceRewards);

                        var count = Math.Min(dailyReward.ChoiceRewards.Count, byte.MaxValue);
                        Write((byte)count);
                        for (var i = 0; i < count; ++i)
                        {
                            Write(dailyReward.ChoiceRewards[i].ItemId);
                            Write(dailyReward.ChoiceRewards[i].Name);
                            Write(dailyReward.ChoiceRewards[i].Weight);
                        }
                    }
                    break;
                case 2: // Set
                    {
                        var count = Math.Min(dailyReward.SetRewards.Count, byte.MaxValue);
                        Write((byte)count);
                        for (var i = 0; i < count; ++i)
                        {
                            Write(dailyReward.SetRewards[i].Type);
                            switch (dailyReward.SetRewards[i].Type)
                            {
                                case 1: // Item
                                    {
                                        Write(dailyReward.SetRewards[i].ItemId);
                                        Write(dailyReward.SetRewards[i].Name);
                                        Write(dailyReward.SetRewards[i].Count);
                                    }
                                    break;
                                case 2: // Prey Bonus Rerolls
                                    {
                                        Write(dailyReward.SetRewards[i].Count);
                                    }
                                    break;
                                case 3: // XP Boost
                                    {
                                        Write(dailyReward.SetRewards[i].Duration);
                                    }
                                    break;
                            }
                        }
                    }
                    break;
            }
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

        public void PrepareToParse(uint[] xteaKey, ZStream zStream = null)
        {
            if (xteaKey != null)
            {
                Xtea.Decrypt(_buffer, Size, xteaKey);
            }

            if (IsCompressed && zStream != null)
            {
                var compressedSize = BitConverter.ToUInt16(_buffer, 6);
                var inBuffer = new byte[compressedSize];
                var outBuffer = new byte[MaxMessageSize];

                Array.Copy(_buffer, 8, inBuffer, 0, compressedSize);

                zStream.next_in = inBuffer;
                zStream.next_in_index = 0;
                zStream.avail_in = inBuffer.Length;
                zStream.next_out = outBuffer;
                zStream.next_out_index = 0;
                zStream.avail_out = outBuffer.Length;

                var ret = zStream.inflate(zlibConst.Z_SYNC_FLUSH);
                if (ret != zlibConst.Z_OK)
                {
                    throw new Exception($"[NetworkMessage.PrepareToParse] zlib inflate failed: {ret}");
                }

                Position = 2;
                _wasCompressed = true;
                Write(SequenceNumber ^ CompressedFlag);
                Write((ushort)zStream.next_out_index);
                Write(outBuffer, 0, (uint)zStream.next_out_index);
            }

            Position = 6;
            Size = ReadUInt16() + PayloadDataPosition;
        }

        public void PrepareToSend(uint[] xteaKey, ZStream zStream = null)
        {
            if (_wasCompressed && zStream != null)
            {
                var uncompressedSize = _size - 8;
                var inBuffer = new byte[uncompressedSize];
                var outBuffer = new byte[MaxMessageSize];

                Array.Copy(_buffer, 8, inBuffer, 0, uncompressedSize);

                zStream.next_in = inBuffer;
                zStream.next_in_index = 0;
                zStream.avail_in = inBuffer.Length;
                zStream.next_out = outBuffer;
                zStream.next_out_index = 0;
                zStream.avail_out = outBuffer.Length;

                var ret = zStream.deflate(zlibConst.Z_SYNC_FLUSH);
                if (ret != zlibConst.Z_OK)
                {
                    throw new Exception($"[NetworkMessage.PrepareToSend] zlib deflate failed: {ret}");
                }

                // deflate appends a footer (0x00 0x00 0xFF 0xFF) to the end of the data,
                // but we don't need it so ignore it when copying the data.
                var data = new byte[zStream.next_out_index - 4];
                Array.Copy(outBuffer, data, data.Length);

                Position = 8;
                Size = 8;
                Write(outBuffer, 0, (uint)(zStream.next_out_index - 4));

                if (!IsCompressed)
                {
                    Position = 2;
                    Write(SequenceNumber + CompressedFlag);
                }
            }

            Position = 6;
            Write((ushort)(Size - PayloadDataPosition));

            if (xteaKey != null)
            {
                Xtea.Encrypt(_buffer, ref _size, xteaKey);
            }

            Position = 0;
            Write((ushort)(_size - 2));
        }
    }
}
