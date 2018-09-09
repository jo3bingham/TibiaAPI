using OXGaming.TibiaAPI.Appearances;
using OXGaming.TibiaAPI.Constants;
using OXGaming.TibiaAPI.Creatures;
using OXGaming.TibiaAPI.Utilities;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class CreateOnMap : ServerPacket
    {
        public Creature Creature { get; set; }

        public ObjectInstance Item { get; set; }

        public Position Position { get; set; }

        public ushort Id { get; set; }

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

            Position = message.ReadPosition();
            StackPosition = message.ReadByte();
            Id = message.ReadUInt16();
            if (Id == (int)CreatureInstanceType.UnknownCreature ||
                Id == (int)CreatureInstanceType.OutdatedCreature ||
                Id == (int)CreatureInstanceType.Creature)
            {
                Creature = message.ReadCreatureInstance(Id, Position);
            }
            else
            {
                Item = message.ReadObjectInstance(Id);
            }
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.CreateOnMap);
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
    }
}
