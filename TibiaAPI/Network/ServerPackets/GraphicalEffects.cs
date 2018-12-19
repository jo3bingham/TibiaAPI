using OXGaming.TibiaAPI.Constants;
using OXGaming.TibiaAPI.Utilities;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class GraphicalEffects : ServerPacket
    {
        public Position Position { get; set; }

        public uint Unknown { get; set; }

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

            // Apparently, there's more information than I thought, and it doesn't make sense.
            //83 53 7E D6 7D 06 01 61 03 04 03 09 00
            //83 54 7E D7 7D 07 01 1D 03 26 01 01 03 26 01 01 03 26 01 01 03 26 01 01 03 26 01 01 03 26 01 01 03 26 00

            Position = message.ReadPosition();
            Effect = message.ReadByte();
            if (Client.VersionNumber >= 12000000)
            {
                Unknown = message.ReadUInt32();
            }
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.GraphicalEffects);
            message.Write(Position);
            message.Write(Effect);
            if (Client.VersionNumber >= 12000000)
            {
                message.Write(Unknown);
            }
        }
    }
}
