using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class Highscores : ClientPacket
    {
        public string GameWorld { get; set; }

        public uint VocationId { get; set; }

        public byte CategoryId { get; set; }

        public Highscores(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.Highscores;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            // TODO
            message.ReadByte(); // 00
            CategoryId = message.ReadByte();
            VocationId = message.ReadUInt32();
            GameWorld = message.ReadString();
            message.ReadBytes(3); // 01 00 14
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            // TODO
            // message.Write((byte)ClientPacketType.Highscores);
        }
    }
}
