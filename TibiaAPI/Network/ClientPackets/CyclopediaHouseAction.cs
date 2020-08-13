using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class CyclopediaHouseAction : ClientPacket
    {
        public byte UnknownByte1 { get; set; }

        public string Town { get; set; }

        public CyclopediaHouseAction(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.CyclopediaHouseAction;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            // TODO
            UnknownByte1 = message.ReadByte(); //always 0x00?
            Town = message.ReadString();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.CyclopediaHouseAction);
            message.Write(UnknownByte1);
            message.Write(Town);
        }
    }
}
