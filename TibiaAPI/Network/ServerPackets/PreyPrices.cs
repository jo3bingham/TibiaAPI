using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class PreyPrices : ServerPacket
    {
        public uint ListRerollPrice { get; set; }
        public uint TaskRerollPrice { get; set; }
        public uint UnknownUInt { get; set; }

        public byte AutomaticBonusReroll { get; set; }
        public byte LockPrey { get; set; }
        public byte TaskWildcards { get; set; }
        public byte UnknownByte { get; set; }

        public PreyPrices(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.PreyPrices;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            ListRerollPrice = message.ReadUInt32();
            if (Client.VersionNumber >= 11900000)
            {
                AutomaticBonusReroll = message.ReadByte();
                LockPrey = message.ReadByte();
                if (Client.VersionNumber >= 1230000)
                {
                    TaskRerollPrice = message.ReadUInt32();
                    UnknownUInt = message.ReadUInt32();
                    TaskWildcards = message.ReadByte();
                    UnknownByte = message.ReadByte();
                }
            }
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.PreyPrices);
            message.Write(ListRerollPrice);
            if (Client.VersionNumber >= 11900000)
            {
                message.Write(AutomaticBonusReroll);
                message.Write(LockPrey);
                if (Client.VersionNumber >= 1230000)
                {
                    message.Write(TaskRerollPrice);
                    message.Write(UnknownUInt);
                    message.Write(TaskWildcards);
                    message.Write(UnknownByte);
                }
            }
        }
    }
}
