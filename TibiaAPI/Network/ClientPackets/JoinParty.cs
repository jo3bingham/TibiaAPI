using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class JoinParty : ClientPacket
    {
        public uint PlayerId { get; set; }

        public JoinParty(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.JoinParty;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            PlayerId = message.ReadUInt32();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.JoinParty);
            message.Write(PlayerId);
        }
    }
}
