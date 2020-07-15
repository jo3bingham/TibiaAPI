using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class SpecialContainersAvailable : ServerPacket
    {
        public SpecialContainersAvailable(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.SpecialContainersAvailable;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            // TODO
            message.ReadUInt16(); // 01 01
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            // TODO
            // message.Write((byte)ServerPacketType.SpecialContainersAvailable);
            // //message.Write(Unknown);
        }
    }
}
