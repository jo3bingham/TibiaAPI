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

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ClientPacketType.Follow)
            {
                return false;
            }

            CreatureId = message.ReadUInt32();
            message.ReadUInt32(); // Creature ID again.
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.Follow);
            message.Write(CreatureId);
            message.Write(CreatureId);
        }
    }
}
