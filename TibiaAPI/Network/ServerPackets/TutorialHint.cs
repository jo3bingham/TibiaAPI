using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class TutorialHint : ServerPacket
    {
        public byte HintId { get; set; }

        public TutorialHint(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.TutorialHint;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            HintId = message.ReadByte();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.TutorialHint);
            message.Write(HintId);
        }
    }
}
