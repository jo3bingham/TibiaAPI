using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class CreatureUnpass : ServerPacket
    {
        public uint CreatureId { get; set; }

        public bool IsUnpassable { get; set; }

        public CreatureUnpass(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.CreatureUnpass;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.CreatureUnpass)
            {
                return false;
            }

            CreatureId = message.ReadUInt32();
            IsUnpassable = message.ReadBool();
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.CreatureUnpass);
            message.Write(CreatureId);
            message.Write(IsUnpassable);
        }
    }
}
