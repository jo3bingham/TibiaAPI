using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class Follow : ClientPacket
    {
        public uint CreatureId { get; set; }

        public Follow(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.Follow;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            CreatureId = message.ReadUInt32();
            message.ReadUInt32(); // Creature ID again.
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.Follow);
            message.Write(CreatureId);
            message.Write(CreatureId);
        }
    }
}
