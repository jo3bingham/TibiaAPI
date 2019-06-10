using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class LookNpcTrade : ClientPacket
    {
        public ushort ObjectId { get; set; }

        public byte Data { get; set; }

        public LookNpcTrade(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.LookNpcTrade;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            ObjectId = message.ReadUInt16();
            Data = message.ReadByte();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.LookNpcTrade);
            message.Write(ObjectId);
            message.Write(Data);
        }
    }
}
