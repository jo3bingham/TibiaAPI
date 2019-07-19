using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class IngameShopError : ServerPacket
    {
        public string Text { get; set; }

        public byte Error { get; set; }

        public IngameShopError(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.IngameShopError;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            Error = message.ReadByte();
            Text = message.ReadString();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.IngameShopError);
            message.Write(Error);
            message.Write(Text);
        }
    }
}
