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

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.ClearTarget)
            {
                return false;
            }

            CreatureId = message.ReadUInt32();
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.ClearTarget);
            message.Write(CreatureId);
        }
    }
}
