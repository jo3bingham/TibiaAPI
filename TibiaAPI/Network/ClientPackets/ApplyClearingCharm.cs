using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class ApplyClearingCharm : ClientPacket
    {
        public byte Slot { get; set; }

        public ApplyClearingCharm(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.ApplyClearingCharm;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            Slot = message.ReadByte();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.ApplyClearingCharm);
            message.Write(Slot);
        }
    }
}
