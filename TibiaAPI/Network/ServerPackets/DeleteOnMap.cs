using System;

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

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            var x = message.ReadUInt16();
            if (x != ushort.MaxValue)
            {
                Position = message.ReadPosition(x);
                if (!Client.WorldMapStorage.IsVisible(Position.X, Position.Y, Position.Z, true))
                {
                    throw new Exception($"[DeleteOnMap.ParseFromNetworkMessage] Co-ordinate {Position} is out of range.");
                }

                var mapPosition = Client.WorldMapStorage.ToMap(Position);
                StackPosition = message.ReadByte();
                var existingObject = Client.WorldMapStorage.GetObject(mapPosition.X, mapPosition.Y, mapPosition.Z, StackPosition);
                if (existingObject == null)
                {
                    throw new Exception("[DeleteOnMap.ParseFromNetworkMessage] Object not found.");
                }

                var existingCreature = Client.CreatureStorage.GetCreature(existingObject.Data);
                if (existingCreature == null && existingObject.Id == (uint)CreatureInstanceType.Creature)
                {
                    throw new Exception($"[DeleteOnMap.ParseFromNetworkMessage] Creature not found: {existingObject.Data}");
                }

                Client.WorldMapStorage.DeleteObject(mapPosition.X, mapPosition.Y, mapPosition.Z, StackPosition);
            }
            else
            {
                CreatureId = message.ReadUInt32();
                var existingCreature = Client.CreatureStorage.GetCreature(CreatureId);
                if (existingCreature == null)
                {
                    throw new Exception($"[DeleteOnMap.ParseFromNetworkMessage] Creature not found: {CreatureId}");
                }

                var creaturePosition = existingCreature.Position;
                if (!Client.WorldMapStorage.IsVisible(creaturePosition.X, creaturePosition.Y, creaturePosition.Z, true))
                {
                    throw new Exception($"[DeleteOnMap.ParseFromNetworkMessage] Co-ordinate {creaturePosition} is out of range.");
                }
            }
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
