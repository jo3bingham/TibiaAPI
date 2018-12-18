using OXGaming.TibiaAPI.Constants;
using OXGaming.TibiaAPI.Utilities;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class DeleteOnMap : ServerPacket
    {
        public Position Position { get; set; }

        public uint CreatureId { get; set; }

        public byte StackPosition { get; set; }

        public DeleteOnMap(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.DeleteOnMap;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.DeleteOnMap)
            {
                return false;
            }

            var x = message.ReadUInt16();
            if (x != ushort.MaxValue)
            {
                Position = message.ReadPosition(x);
                StackPosition = message.ReadByte();
            }
            else
            {
                CreatureId = message.ReadUInt32();
            }
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.DeleteOnMap);
            if (Position.X != ushort.MaxValue)
            {
                message.Write(Position);
                message.Write(StackPosition);
            }
            else
            {
                message.Write(ushort.MaxValue);
                message.Write(CreatureId);
            }
        }
    }
}
