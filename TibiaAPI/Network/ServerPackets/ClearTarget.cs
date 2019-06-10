using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class ClearTarget : ServerPacket
    {
        public uint CreatureId { get; set; }

        public ClearTarget(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.ClearTarget;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            CreatureId = message.ReadUInt32();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.ClearTarget);
            message.Write(CreatureId);
        }
    }
}
