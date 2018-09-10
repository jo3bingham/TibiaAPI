using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class IngameShopError : ServerPacket
    {
        public string Text { get; set; }

        public byte Error { get; set; }

        public IngameShopError()
        {
            PacketType = ServerPacketType.IngameShopError;
        }

        public override bool ParseFromNetworkMessage(Client client, NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.IngameShopError)
            {
                return false;
            }

            Error = message.ReadByte();
            Text = message.ReadString();
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.IngameShopError);
            message.Write(Error);
            message.Write(Text);
        }
    }
}
