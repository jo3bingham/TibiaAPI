using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class WorldEntered : ServerPacket
    {
        public WorldEntered(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.WorldEntered;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            if (Client.Connection.ConnectionState == ConnectionState.Pending)
            {
                // I've experienced intermittent instances where the server
                // will resend the whole login process (WorldEntered, FullMap, etc.)
                // after it has already done so, and the creatures in the FullMap
                // packet are tagged as outdated. Because of the following WorldEntered
                // packet, this will reset the creature storage and the parser will be
                // unable to find any existing creatures because they've been cleared.
                // Just commenting out the code and leaving this comment as a reminder.
                //Client.CreatureStorage.Reset();
                Client.WorldMapStorage.ResetMap();
                Client.WorldMapStorage.SetPosition(0, 0, 0);
            }

            Client.Connection.ConnectionState = ConnectionState.Game;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.WorldEntered);
        }
    }
}
