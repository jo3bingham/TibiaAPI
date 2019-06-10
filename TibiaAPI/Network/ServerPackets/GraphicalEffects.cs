using System.Collections.Generic;

using OXGaming.TibiaAPI.Constants;
using OXGaming.TibiaAPI.Utilities;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class GraphicalEffects : ServerPacket
    {
        public List<(GraphicalEffectsType Type, byte TilesToMove, ushort Delay, byte Id, sbyte DistanceX, sbyte DistanceY)> Effects { get; } =
            new List<(GraphicalEffectsType Type, byte TilesToMove, ushort Delay, byte Id, sbyte DistanceX, sbyte DistanceY)>();

        public Position Position { get; set; }

        public byte Effect { get; set; }

        public GraphicalEffects(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.GraphicalEffects;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            Position = message.ReadPosition();
            if (Client.VersionNumber < 12000000)
            {
                Effect = message.ReadByte();
            }
            else
            {
                var type = GraphicalEffectsType.None;
                while (true)
                {
                    type = (GraphicalEffectsType)message.ReadByte();
                    if (type == GraphicalEffectsType.None)
                    {
                        break;
                    }
                    else if (type == GraphicalEffectsType.Move)
                    {
                        var tilesToMove = message.ReadByte();
                        Effects.Add((type, tilesToMove, 0, 0, 0, 0));
                    }
                    else if (type == GraphicalEffectsType.Delay)
                    {
                        var delay = message.ReadUInt16();
                        Effects.Add((type, 0, delay, 0, 0, 0));
                    }
                    else if (type == GraphicalEffectsType.Effect)
                    {
                        var effectId = message.ReadByte();
                        Effects.Add((type, 0, 0, effectId, 0, 0));
                    }
                    else if (type == GraphicalEffectsType.MissileXY)
                    {
                        var missileId = message.ReadByte();
                        var distanceAxisX = message.ReadSByte();
                        var distanceAxisY = message.ReadSByte();
                        Effects.Add((type, 0, 0, missileId, distanceAxisX, distanceAxisY));
                    }
                    else if (type == GraphicalEffectsType.MissileYX)
                    {
                        var missileId = message.ReadByte();
                        var distanceAxisY = message.ReadSByte();
                        var distanceAxisX = message.ReadSByte();
                        Effects.Add((type, 0, 0, missileId, distanceAxisX, distanceAxisY));
                    }
                    else
                    {
                        throw new System.Exception($"[ServerPackets.GraphicalEffects] Unknown type: {type}");
                    }
                }
            }
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.GraphicalEffects);
            message.Write(Position);
            if (Client.VersionNumber < 12000000)
            {
                message.Write(Effect);
            }
            else
            {
                foreach (var (Type, TilesToMove, Delay, Id, DistanceX, DistanceY) in Effects)
                {
                    message.Write((byte)Type);
                    if (Type == GraphicalEffectsType.Move)
                    {
                        message.Write(TilesToMove);
                    }
                    else if (Type == GraphicalEffectsType.Delay)
                    {
                        message.Write(Delay);
                    }
                    else if (Type == GraphicalEffectsType.Effect)
                    {
                        message.Write(Id);
                    }
                    else if (Type == GraphicalEffectsType.MissileXY)
                    {
                        message.Write(Id);
                        message.Write(DistanceX);
                        message.Write(DistanceY);
                    }
                    else if (Type == GraphicalEffectsType.MissileYX)
                    {
                        message.Write(Id);
                        message.Write(DistanceY);
                        message.Write(DistanceX);
                    }
                }
                message.Write((byte)GraphicalEffectsType.None);
            }
        }
    }
}
