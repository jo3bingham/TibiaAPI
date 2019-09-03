using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class CreaturePvpHelpers : ServerPacket
    {
        public uint CreatureId { get; set; }

        public ushort PvpHelpers { get; set; }

        public CreaturePvpHelpers(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.DepotSearchResults;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            CreatureId = message.ReadUInt32();
            PvpHelpers = message.ReadUInt16();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.DepotSearchResults);
            message.Write(CreatureId);
            message.Write(PvpHelpers);
        }
    }
}
