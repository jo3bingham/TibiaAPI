using System;

using OXGaming.TibiaAPI.Appearances;
using OXGaming.TibiaAPI.Constants;
using OXGaming.TibiaAPI.Creatures;
using OXGaming.TibiaAPI.Utilities;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class ChangeOnMap : ServerPacket
    {
        public Creature Creature { get; set; }

        public ObjectInstance ObjectInstance { get; set; }

        public Position Position { get; set; } = new Position(0xFFFF, 0xFFFF, 7);

        public ushort Id { get; set; }

        public byte StackPosition { get; set; }

        public ChangeOnMap(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.ChangeOnMap;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            var x = message.ReadUInt16();
            if (x != ushort.MaxValue)
            {
                Position = message.ReadPosition(x);
                if (!Client.WorldMapStorage.IsVisible(Position.X, Position.Y, Position.Z, true))
                {
                    throw new Exception($"[ChangeOnMap.ParseFromNetworkMessage] Co-ordinate {Position} is out of range.");
                }

                var mapPosition = Client.WorldMapStorage.ToMap(Position);
                StackPosition = message.ReadByte();
                var existingObject = Client.WorldMapStorage.GetObject(mapPosition.X, mapPosition.Y, mapPosition.Z, StackPosition);
                if (existingObject == null)
                {
                    throw new Exception("[ChangeOnMap.ParseFromNetworkMessage] Object not found.");
                }

                var existingCreature = Client.CreatureStorage.GetCreature(existingObject.Data);
                if (existingCreature == null && existingObject.Id == (uint)CreatureInstanceType.Creature)
                {
                    throw new Exception($"[ChangeOnMap.ParseFromNetworkMessage] Creature not found: {existingObject.Data}");
                }

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

                Client.WorldMapStorage.ChangeObject(mapPosition.X, mapPosition.Y, mapPosition.Z, StackPosition, ObjectInstance);
            }
            else
            {
                var creatureId = message.ReadUInt32();
                var existingCreature = Client.CreatureStorage.GetCreature(creatureId);
                if (existingCreature == null)
                {
                    throw new Exception($"[ChangeOnMap.ParseFromNetworkMessage] Creature not found: {creatureId}");
                }

                var creaturePosition = existingCreature.Position;
                if (!Client.WorldMapStorage.IsVisible(creaturePosition.X, creaturePosition.Y, creaturePosition.Z, true))
                {
                    throw new Exception($"[ChangeOnMap.ParseFromNetworkMessage] Co-ordinate {creaturePosition} is out of range.");
                }

                Id = message.ReadUInt16();
                if (Id == (int)CreatureInstanceType.UnknownCreature ||
                    Id == (int)CreatureInstanceType.OutdatedCreature ||
                    Id == (int)CreatureInstanceType.Creature)
                {
                    Creature = message.ReadCreatureInstance(Id);
                }
                else
                {
                    throw new Exception($"[ChangeOnMap.ParseFromNetworkMessage] Received object of type {Id} when a creature was expected.");
                }
            }
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.ChangeOnMap);
            if (Position.X != ushort.MaxValue)
            {
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
            else
            {
                if (Id != (int)CreatureInstanceType.UnknownCreature &&
                    Id != (int)CreatureInstanceType.OutdatedCreature &&
                    Id != (int)CreatureInstanceType.Creature)
                {
                    throw new Exception($"[ChangeOnMap.AppendToNetworkMessage] {Id} is not a valid CreatureInstanceType.");
                }

                message.Write(ushort.MaxValue);
                message.Write(Creature.Id);
                message.Write(Id);
                message.Write(Creature, (CreatureInstanceType)Id);
            }
        }
    }
}
