using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class Follow : ClientPacket
    {
        public uint CreatureId { get; set; }
        public uint SecondCreatureId { get; set; }

        public Follow(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.Follow;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            CreatureId = message.ReadUInt32();
            SecondCreatureId = message.ReadUInt32(); // Should always be the same as CreatureId.
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.Follow);
            message.Write(CreatureId);
            message.Write(SecondCreatureId);
        }
    }
}
