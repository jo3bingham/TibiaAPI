using System;
using System.Collections.Generic;

using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class PlayerDataBasic : ServerPacket
    {
        public List<byte> KnownSpells { get; } = new List<byte>();

        public uint PremiumUntil { get; set; }

        public byte Profession { get; set; }

        public bool HasPremium { get; set; }
        public bool HasReachedMain { get; set; }

        public PlayerDataBasic(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.PlayerDataBasic;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            HasPremium = message.ReadBool();
            PremiumUntil = message.ReadUInt32();
            Profession = message.ReadByte();
            HasReachedMain = message.ReadBool();

            KnownSpells.Capacity = message.ReadUInt16();
            for (var i = 0; i < KnownSpells.Capacity; ++i)
            {
                KnownSpells.Add(message.ReadByte());
            }
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.PlayerDataBasic);
            message.Write(HasPremium);
            message.Write(PremiumUntil);
            message.Write(Profession);
            message.Write(HasReachedMain);

            var count = Math.Min(KnownSpells.Count, ushort.MaxValue);
            message.Write((ushort)count);
            for (var i = 0; i < count; ++i)
            {
                message.Write(KnownSpells[i]);
            }
        }
    }
}
