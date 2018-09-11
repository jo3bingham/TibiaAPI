using OXGaming.TibiaAPI.Appearances;
using OXGaming.TibiaAPI.Constants;
using OXGaming.TibiaAPI.Creatures;
using OXGaming.TibiaAPI.Utilities;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class ChangeOnMap : ServerPacket
    {
        public Creature Creature { get; set; }

        public ObjectInstance Item { get; set; }

        public Position Position { get; set; }

        public ushort Id { get; set; }

        public byte StackPosition { get; set; }

        public ChangeOnMap()
        {
            PacketType = ServerPacketType.ChangeOnMap;
        }

        public override bool ParseFromNetworkMessage(Client client, NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.ChangeOnMap)
            {
                return false;
            }

            var x = message.ReadUInt16();
            if (x != ushort.MaxValue)
            {
                Position = message.ReadPosition(x);
                StackPosition = message.ReadByte();
                Id = message.ReadUInt16();
                if (Id == (int)CreatureInstanceType.UnknownCreature ||
                    Id == (int)CreatureInstanceType.OutdatedCreature ||
                    Id == (int)CreatureInstanceType.Creature)
                {
                    Creature = message.ReadCreatureInstance(client, Id, Position);
                }
                else
                {
                    Item = message.ReadObjectInstance(client, Id);
                }
            }
            else
            {
                var creatureId = message.ReadUInt32();
                Id = message.ReadUInt16();
                if (Id == (int)CreatureInstanceType.UnknownCreature ||
                    Id == (int)CreatureInstanceType.OutdatedCreature ||
                    Id == (int)CreatureInstanceType.Creature)
                {
                    Creature = message.ReadCreatureInstance(client, Id);
                }
            }
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.ChangeOnMap);
            if (Position != null)
            {
                message.Write(Position);
                message.Write(StackPosition);
                message.Write(Id);
                if (Id == (int)CreatureInstanceType.UnknownCreature ||
                    Id == (int)CreatureInstanceType.OutdatedCreature ||
                    Id == (int)CreatureInstanceType.Creature)
                {
                    message.Write(Creature, (CreatureInstanceType)Id);
                }
                else
                {
                    message.Write(Item);
                }
            }
            else
            {
                message.Write(ushort.MaxValue);
                message.Write(Creature.Id);
                message.Write(Id);
                if (Id == (int)CreatureInstanceType.UnknownCreature ||
                    Id == (int)CreatureInstanceType.OutdatedCreature ||
                    Id == (int)CreatureInstanceType.Creature)
                {
                    message.Write(Creature, (CreatureInstanceType)Id);
                }
            }
        }
    }
}
