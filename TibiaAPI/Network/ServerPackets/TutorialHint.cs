using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class TutorialHint : ServerPacket
    {
        public byte HintId { get; set; }

        public TutorialHint()
        {
            PacketType = ServerPacketType.TutorialHint;
        }

        public override bool ParseFromNetworkMessage(Client client, NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.TutorialHint)
            {
                return false;
            }

            HintId = message.ReadByte();
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.TutorialHint);
            message.Write(HintId);
        }
    }
}
