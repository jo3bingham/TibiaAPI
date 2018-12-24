using OXGaming.TibiaAPI.Constants;
using OXGaming.TibiaAPI.Utilities;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class GraphicalEffects : ServerPacket
    {
        public Position Position { get; set; }

        public byte Effect { get; set; }

        public GraphicalEffects(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.GraphicalEffects;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.GraphicalEffects)
            {
                return false;
            }

            Position = message.ReadPosition();

            if (Client.VersionNumber < 12000000)
            {
                Effect = message.ReadByte();
            }
            else
            {
                // TODO: Store new structure in a sensible manner
                // that is easy to append to a networkmessage.
                while (true)
                {
                    var type = message.ReadByte();
                    if (type == 0)
                    {
                        break;
                    }
                    else if (type == 1)
                    {
                        var tilesToMove = message.ReadByte();
                    }
                    else if (type == 2)
                    {
                        message.ReadUInt16();
                    }
                    else if (type == 3)
                    {
                        var effectId = message.ReadByte();
                    }
                    else if (type == 4 || type == 5)
                    {
                        message.ReadBytes(3);
                    }
                    else
                    {
                        throw new System.Exception($"[ServerPackets.GraphicalEffects] Unknown type: {type}");
                    }
                }
            }
            return true;
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
                // TODO: Write new structure to message.
            }
        }
    }
}
