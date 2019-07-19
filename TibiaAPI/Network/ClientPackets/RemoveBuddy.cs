using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class RemoveBuddy : ClientPacket
    {
        public uint PlayerId { get; set; }

        public RemoveBuddy(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.RemoveBuddy;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            PlayerId = message.ReadUInt32();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.RemoveBuddy);
            message.Write(PlayerId);
        }
    }
}
