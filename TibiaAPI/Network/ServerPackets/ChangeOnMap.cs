using OXGaming.TibiaAPI.Appearances;
using OXGaming.TibiaAPI.Constants;
using OXGaming.TibiaAPI.Utilities;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class ChangeOnMap : ServerPacket
    {
        // TODO
        //public Creature Creature { get; set; }

        public ObjectInstance Item { get; set; }

        public Position Position { get; set; }

        public ushort ObjectId { get; set; }

        public byte StackPosition { get; set; }

        public ChangeOnMap()
        {
            PacketType = ServerPacketType.ChangeOnMap;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.ChangeOnMap)
            {
                return false;
            }

            // TODO
            var x = message.ReadUInt16();
            if (x != ushort.MaxValue)
            {
                Position = message.ReadPosition(x);
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
                    Item = message.ReadObjectInstance(ObjectId);
                }
            }
            else
            {
                var creatureId = message.ReadUInt32();
                ObjectId = message.ReadUInt16();
                if (ObjectId == (int)CreatureInstanceType.UnknownCreature ||
                    ObjectId == (int)CreatureInstanceType.OutdatedCreature ||
                    ObjectId == (int)CreatureInstanceType.Creature)
                {
                    //message.ReadCreatureInstance(ObjectId);
                }
            }
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            // TODO
            message.Write((byte)ServerPacketType.ChangeOnMap);
            if (Position.X != ushort.MaxValue)
            {
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
                    message.Write(Item);
                }
            }
            else
            {
                message.Write(ushort.MaxValue);
                //message.WriteUInt32(Creature.Id);
                message.Write(ObjectId);
                if (ObjectId == (int)CreatureInstanceType.UnknownCreature ||
                    ObjectId == (int)CreatureInstanceType.OutdatedCreature ||
                    ObjectId == (int)CreatureInstanceType.Creature)
                {
                    //message.WriteCreatureInstance(Creature);
                }
            }
        }
    }
}
