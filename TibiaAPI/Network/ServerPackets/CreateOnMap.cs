using System;

using OXGaming.TibiaAPI.Appearances;
using OXGaming.TibiaAPI.Constants;
using OXGaming.TibiaAPI.Creatures;
using OXGaming.TibiaAPI.Utilities;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class CreateOnMap : ServerPacket
    {
        private const int MapSizeW = 10;

        public Creature Creature { get; set; }

        public ObjectInstance ObjectInstance { get; set; }

        public Position Position { get; set; }

        public ushort Id { get; set; }

        public byte StackPosition { get; set; }

        public CreateOnMap(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.CreateOnMap;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            Position = message.ReadPosition();
            if (!Client.WorldMapStorage.IsVisible(Position.X, Position.Y, Position.Z, true))
            {
                throw new Exception($"[CreateOnMap.ParseFromNetworkMessage] Co-ordinate {Position} is out of range.");
            }

            var mapPosition = Client.WorldMapStorage.ToMap(Position);
            StackPosition = message.ReadByte();
            Id = message.ReadUInt16();
            if (Id == (int)CreatureInstanceType.UnknownCreature ||
                Id == (int)CreatureInstanceType.OutdatedCreature ||
                Id == (int)CreatureInstanceType.Creature)
            {
                Creature = message.ReadCreatureInstance(Id, Position);
                ObjectInstance = Client.AppearanceStorage.CreateObjectInstance((uint)CreatureInstanceType.Creature, Creature.Id);
            }
            else
            {
                ObjectInstance = message.ReadObjectInstance(Id);
            }

            if (StackPosition == 255)
            {
                Client.WorldMapStorage.PutObject(mapPosition.X, mapPosition.Y, mapPosition.Z, ObjectInstance);
            }
            else
            {
                if (StackPosition > MapSizeW)
                {
                    throw new Exception("[CreateOnMap.ParseFromNetworkMessage] Invalid position.");
                }
                Client.WorldMapStorage.InsertObject(mapPosition.X, mapPosition.Y, mapPosition.Z, StackPosition, ObjectInstance);
            }
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.CreateOnMap);
            message.Write(Position);
            message.Write(StackPosition);
            if (Id == (int)CreatureInstanceType.UnknownCreature ||
                Id == (int)CreatureInstanceType.OutdatedCreature ||
                Id == (int)CreatureInstanceType.Creature)
            {
                message.Write(Id);
                message.Write(Creature, (CreatureInstanceType)Id);
            }
            else
            {
                message.Write(ObjectInstance);
            }
        }
    }
}
