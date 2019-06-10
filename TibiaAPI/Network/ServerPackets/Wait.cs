using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class Wait : ServerPacket
    {
        public ushort Delay { get; set; }

        public Wait(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.Wait;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            Delay = message.ReadUInt16();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.Wait);
            message.Write(Delay);
        }
    }
}
