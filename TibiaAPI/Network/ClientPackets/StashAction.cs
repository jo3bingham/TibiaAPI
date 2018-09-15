using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class StashAction : ClientPacket
    {
        public StashAction()
        {
            PacketType = ClientPacketType.StashAction;
        }

        public override bool ParseFromNetworkMessage(Client client, NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ClientPacketType.StashAction)
            {
                return false;
            }

            var type = message.ReadByte();
            if (type == 3) // Retrieve
            {
                var itemId = message.ReadUInt16();
                var itemCount = message.ReadUInt32();
            }
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.StashAction);
        }
    }
}
