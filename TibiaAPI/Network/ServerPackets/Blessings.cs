using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class Blessings : ServerPacket
    {
        public ushort BlessingId { get; set; }

        public byte Amount { get; set; }

        public Blessings()
        {
            PacketType = ServerPacketType.Blessings;
        }

        public override bool ParseFromNetworkMessage(Client client, NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.Blessings)
            {
                return false;
            }

            BlessingId = message.ReadUInt16();
            Amount = message.ReadByte();
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.Blessings);
            message.Write(BlessingId);
            message.Write(Amount);
        }
    }
}
