using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class ReadyForSecondaryConnection : ServerPacket
    {
        public string Text { get; set; }

        public ReadyForSecondaryConnection()
        {
            PacketType = ServerPacketType.ReadyForSecondaryConnection;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.ReadyForSecondaryConnection)
            {
                return false;
            }

            Text = message.ReadString();
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.ReadyForSecondaryConnection);
            message.Write(Text);
        }
    }
}
