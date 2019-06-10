using System;

using OXGaming.TibiaAPI.Appearances;
using OXGaming.TibiaAPI.Creatures;
using OXGaming.TibiaAPI.Constants;
using OXGaming.TibiaAPI.Utilities;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class MoveCreature : ServerPacket
    {
        public Position FromPosition { get; set; } = new Position(0xFFFF, 0xFFFF, 7);
        public Position ToPosition { get; set; }

        public uint CreatureId { get; set; }

        public byte StackPosition { get; set; }

        public MoveCreature(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.MoveCreature;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            Creature creature;
            ObjectInstance objectInstance;
            Position fromMapPosition;

            var x = message.ReadUInt16();
            if (x != ushort.MaxValue)
            {
                FromPosition = message.ReadPosition(x);
                if (!Client.WorldMapStorage.IsVisible(FromPosition.X, FromPosition.Y, FromPosition.Z, true))
                {
                    throw new Exception($"[MoveCreature.ParseFromNetworkMessage] Start co-ordinate {FromPosition} is invalid.");
                }

                fromMapPosition = Client.WorldMapStorage.ToMap(FromPosition);
                StackPosition = message.ReadByte();
                objectInstance = Client.WorldMapStorage.GetObject(fromMapPosition.X, fromMapPosition.Y, fromMapPosition.Z, StackPosition);
                creature = Client.CreatureStorage.GetCreature(objectInstance.Data);
                if (creature == null || objectInstance.Id != (uint)CreatureInstanceType.Creature)
                {
                    throw new Exception($"[MoveCreature.ParseFromNetworkMessage] No creature at position {FromPosition}, index {StackPosition}.");
                }
            }
            else
            {
                CreatureId = message.ReadUInt32();
                objectInstance = Client.AppearanceStorage.CreateObjectInstance((uint)CreatureInstanceType.Creature, CreatureId);
                creature = Client.CreatureStorage.GetCreature(objectInstance.Data);
                if (creature == null)
                {
                    throw new Exception($"[MoveCreature.ParseFromNetworkMessage] Creature {CreatureId} not found.");
                }

                FromPosition = creature.Position;
                if (!Client.WorldMapStorage.IsVisible(FromPosition.X, FromPosition.Y, FromPosition.Z, true))
                {
                    throw new Exception($"[MoveCreature.ParseFromNetworkMessage] Start co-ordinate {FromPosition} is invalid.");
                }

                fromMapPosition = Client.WorldMapStorage.ToMap(FromPosition);
            }

            ToPosition = message.ReadPosition();
            if (!Client.WorldMapStorage.IsVisible(ToPosition.X, ToPosition.Y, ToPosition.Z, true))
            {
                throw new Exception($"[MoveCreature.ParseFromNetworkMessage] Target co-ordinate {ToPosition} is invalid.");
            }

            var toMapPosition = Client.WorldMapStorage.ToMap(ToPosition);
            var diffPosition = ToPosition.Subtract(FromPosition);
            var isNotAdjacent = diffPosition.Z != 0 || Math.Abs(diffPosition.X) > 1 || Math.Abs(diffPosition.Y) > 1;
            var ground = Client.WorldMapStorage.GetObject(toMapPosition.X, toMapPosition.Y, toMapPosition.Z, 0);
            if (!isNotAdjacent && (ground == null || ground.Type == null || ground.Type.Flags.Bank == null))
            {
                throw new Exception($"[MoveCreature.ParseFromNetworkMessage] Target field {ToPosition} has no BANK.");
            }

            if (x != ushort.MaxValue)
            {
                Client.WorldMapStorage.DeleteObject(fromMapPosition.X, fromMapPosition.Y, fromMapPosition.Z, StackPosition);
            }

            Client.WorldMapStorage.PutObject(toMapPosition.X, toMapPosition.Y, toMapPosition.Z, objectInstance);
            creature.Position = ToPosition;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.MoveCreature);
            if (FromPosition.X != ushort.MaxValue)
            {
                message.Write(FromPosition);
                message.Write(StackPosition);
            }
            else
            {
                message.Write(ushort.MaxValue);
                message.Write(CreatureId);
            }
            message.Write(ToPosition);
        }
    }
}
