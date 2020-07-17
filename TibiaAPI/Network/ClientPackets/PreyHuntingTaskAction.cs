using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class PreyHuntingTaskAction : ClientPacket
    {
        public ushort RaceId { get; set; }

        public byte Index { get; set; }

        public PreyHuntingTaskAction(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.PreyHuntingTaskAction;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            // TODO
            Index = message.ReadByte();
            message.ReadUInt16(); // 03 00
            RaceId = message.ReadUInt16();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            // TODO
            // message.Write((byte)ClientPacketType.PreyHuntingTaskAction);
        }
    }
}
