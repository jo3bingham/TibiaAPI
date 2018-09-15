using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class Stash : ServerPacket
    {
        public Stash()
        {
            PacketType = ServerPacketType.Stash;
        }

        public override bool ParseFromNetworkMessage(Client client, NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.Stash)
            {
                return false;
            }

            var numberOfStashedItems = message.ReadUInt16();
            for (var i = 0; i < numberOfStashedItems; ++i)
            {
                var itemId = message.ReadUInt16();
                var itemCount = message.ReadUInt32();
            }
            var unknown = message.ReadUInt16();
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.Stash);
        }
    }
}
