using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class CreatureType : ServerPacket
    {
        public Constants.CreatureType Type { get; set; }

        public uint CreatureId { get; set; }
        public uint SummonerId { get; set; }

        public CreatureType(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.CreatureType;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            CreatureId = message.ReadUInt32();
            Type = (Constants.CreatureType)message.ReadByte();
            if (Type == Constants.CreatureType.PlayerSummon)
            {
                SummonerId = message.ReadUInt32();
            }
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.CreatureType);
            message.Write(CreatureId);
            message.Write((byte)Type);
            if (Type == Constants.CreatureType.PlayerSummon)
            {
                message.Write(SummonerId);
            }
        }
    }
}
