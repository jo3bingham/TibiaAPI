﻿using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class PlayerDataCurrent : ServerPacket
    {
        public ulong Experience { get; set; }

        public uint FreeCapacity { get; set; }

        public ushort BaseXpGain { get; set; }
        public ushort CurrentHealth { get; set; }
        public ushort CurrentMana { get; set; }
        public ushort FoodRegeneration { get; set; }
        public ushort GrindingAddend { get; set; }
        public ushort HuntingBoostFactor { get; set; }
        public ushort Level { get; set; }
        public ushort MaxHealth { get; set; }
        public ushort MaxMana { get; set; }
        public ushort OfflineTrainingTime { get; set; }
        public ushort RemainingStoreXpBoostSeconds { get; set; }
        public ushort Speed { get; set; }
        public ushort Stamina { get; set; }
        public ushort StoreBoostAddend { get; set; }

        public byte LevelPercent { get; set; }
        public byte MagicLevel { get; set; }
        public byte MagicLevelBase { get; set; }
        public byte MagicLevelPercent { get; set; }
        public byte Soul { get; set; }
        public bool CanBuyMoreStoreXpBoosts { get; set; }

        public PlayerDataCurrent()
        {
            PacketType = ServerPacketType.PlayerDataCurrent;
        }

        public override bool ParseFromNetworkMessage(Client client, NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.PlayerDataCurrent)
            {
                return false;
            }

            CurrentHealth = message.ReadUInt16();
            MaxHealth = message.ReadUInt16();
            FreeCapacity = message.ReadUInt32();
            Experience = message.ReadUInt64();
            Level = message.ReadUInt16();
            LevelPercent = message.ReadByte();
            BaseXpGain = message.ReadUInt16();
            GrindingAddend = message.ReadUInt16();
            StoreBoostAddend = message.ReadUInt16();
            HuntingBoostFactor = message.ReadUInt16();
            CurrentMana = message.ReadUInt16();
            MaxMana = message.ReadUInt16();
            MagicLevel = message.ReadByte();
            MagicLevelBase = message.ReadByte();
            MagicLevelPercent = message.ReadByte();
            Soul = message.ReadByte();
            Stamina = message.ReadUInt16();
            Speed = message.ReadUInt16();
            FoodRegeneration = message.ReadUInt16();
            OfflineTrainingTime = message.ReadUInt16();
            RemainingStoreXpBoostSeconds = message.ReadUInt16();
            CanBuyMoreStoreXpBoosts = message.ReadBool();
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.PlayerDataCurrent);
            message.Write(CurrentHealth);
            message.Write(MaxHealth);
            message.Write(FreeCapacity);
            message.Write(Experience);
            message.Write(Level);
            message.Write(LevelPercent);
            message.Write(BaseXpGain);
            message.Write(GrindingAddend);
            message.Write(StoreBoostAddend);
            message.Write(HuntingBoostFactor);
            message.Write(CurrentMana);
            message.Write(MaxMana);
            message.Write(MagicLevel);
            message.Write(MagicLevelBase);
            message.Write(MagicLevelPercent);
            message.Write(Soul);
            message.Write(Stamina);
            message.Write(Speed);
            message.Write(FoodRegeneration);
            message.Write(OfflineTrainingTime);
            message.Write(RemainingStoreXpBoostSeconds);
            message.Write(CanBuyMoreStoreXpBoosts);
        }
    }
}
