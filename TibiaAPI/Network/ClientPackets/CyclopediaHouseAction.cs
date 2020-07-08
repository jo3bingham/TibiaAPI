using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class CyclopediaHouseAction : ClientPacket
    {
        public string Town { get; set; }

        public CyclopediaHouseAction(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.CyclopediaHouseAction;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            // TODO
            message.ReadByte(); //always 0x00?
            Town = message.ReadString();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            // TODO
            // message.Write((byte)ClientPacketType.CyclopediaHouseAction);
            // //message.Write(Unknown);
            // message.Write(Town);
        }
    }
}
