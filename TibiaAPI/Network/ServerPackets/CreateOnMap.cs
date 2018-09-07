using OXGaming.TibiaAPI.Constants;
using OXGaming.TibiaAPI.Utilities;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class CreateOnMap : ServerPacket
    {
        public Position Position { get; set; }
        public ushort ObjectId { get; set; }
        public byte StackPosition { get; set; }

        public CreateOnMap()
        {
            PacketType = ServerPacketType.CreateOnMap;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.CreateOnMap)
            {
                return false;
            }

            // TODO
            Position = message.ReadPosition();
            StackPosition = message.ReadByte();
            ObjectId = message.ReadUInt16();
            if (ObjectId == (int)CreatureInstanceType.UnknownCreature ||
                ObjectId == (int)CreatureInstanceType.OutdatedCreature ||
                ObjectId == (int)CreatureInstanceType.Creature)
            {
                //message.ReadCreatureInstance(ObjectId, Position);
            }
            else
            {
                //message.ReadObjectInstance(ObjectId);
            }
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            // TODO
            message.Write((byte)ServerPacketType.CreateOnMap);
            message.Write(Position);
            message.Write(StackPosition);
            message.Write(ObjectId);
            if (ObjectId == (int)CreatureInstanceType.UnknownCreature ||
                ObjectId == (int)CreatureInstanceType.OutdatedCreature ||
                ObjectId == (int)CreatureInstanceType.Creature)
            {
                //message.WriteCreatureInstance(Creature);
            }
            else
            {
                //message.WriteObjectInstance(Item);
            }
        }
    }
}
