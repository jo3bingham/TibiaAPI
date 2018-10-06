using System;
using System.Collections.Generic;

using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class BlessingsDialog : ServerPacket
    {
        public List<(ushort BlessingId, byte Amount)> Blessings { get; } =
            new List<(ushort BlessingId, byte Amount)>();
        public List<(uint Timestamp, byte Color, string Text)> History { get; } =
            new List<(uint Timestamp, byte Color, string Text)>();

        public byte InventoryLossPveDeath { get; set; }
        public byte InventoryLossPvpDeath { get; set; }
        public byte XpLossLower { get; set; }
        public byte XpLossMaxPvpDeath { get; set; }
        public byte XpLossMinPvpDeath { get; set; }
        public byte XpLossPveDeath { get; set; }

        public bool IsPremium { get; set; }
        public bool IsRedOrBlackSkull { get; set; }
        public bool IsWearingAoL { get; set; }

        public BlessingsDialog()
        {
            PacketType = ServerPacketType.BlessingsDialog;
        }

        public override bool ParseFromNetworkMessage(Client client, NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.BlessingsDialog)
            {
                return false;
            }

            Blessings.Capacity = message.ReadByte();
            for (var i = 0; i < Blessings.Capacity; ++i)
            {
                var blessingId = message.ReadUInt16();
                var amount = message.ReadByte();
                Blessings.Add((blessingId, amount));
            }

            IsPremium = message.ReadBool();
            XpLossLower = message.ReadByte();
            XpLossMinPvpDeath = message.ReadByte();
            XpLossMaxPvpDeath = message.ReadByte();
            XpLossPveDeath = message.ReadByte();
            InventoryLossPvpDeath = message.ReadByte();
            InventoryLossPveDeath = message.ReadByte();
            IsRedOrBlackSkull = message.ReadBool();
            IsWearingAoL = message.ReadBool();

            History.Capacity = message.ReadByte();
            for (var i = 0; i < History.Capacity; ++i)
            {
                var timestamp = message.ReadUInt32();
                var color = message.ReadByte();
                var text = message.ReadString();
                History.Add((timestamp, color, text));
            }
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.BlessingsDialog);
            var count = Math.Min(Blessings.Count, byte.MaxValue);
            message.Write((byte)count);
            for (var i = 0; i < count; ++i)
            {
                var (BlessingId, Amount) = Blessings[i];
                message.Write(BlessingId);
                message.Write(Amount);
            }

            message.Write(IsPremium);
            message.Write(XpLossLower);
            message.Write(XpLossMinPvpDeath);
            message.Write(XpLossMaxPvpDeath);
            message.Write(XpLossPveDeath);
            message.Write(InventoryLossPvpDeath);
            message.Write(InventoryLossPveDeath);
            message.Write(IsRedOrBlackSkull);
            message.Write(IsWearingAoL);

            count = Math.Min(History.Count, byte.MaxValue);
            message.Write((byte)count);
            for (var i = 0; i < count; ++i)
            {
                var (Timestamp, Color, Text) = History[i];
                message.Write(Timestamp);
                message.Write(Color);
                message.Write(Text);
            }
        }
    }
}
