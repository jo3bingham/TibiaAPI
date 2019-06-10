using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class InviteToParty : ClientPacket
    {
        public uint PlayerId { get; set; }

        public InviteToParty(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.InviteToParty;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            PlayerId = message.ReadUInt32();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.InviteToParty);
            message.Write(PlayerId);
        }
    }
}
