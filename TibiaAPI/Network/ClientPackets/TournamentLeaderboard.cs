using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class TournamentLeaderboard : ClientPacket
    {
        private byte[] unknown;

        public string WorldName { get; set; }

        public byte Unknown { get; set; }

        public TournamentLeaderboard(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.TournamentLeaderboard;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            Unknown = message.ReadByte();
            WorldName = message.ReadString();
            unknown = message.ReadBytes(3);
            Client.Logger.Debug($"TournamentLeaderboard unknown data: {System.BitConverter.ToString(unknown).Replace('-', ' ')}");
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.TournamentLeaderboard);
            message.Write(Unknown);
            message.Write(WorldName);
            message.Write(unknown);
        }
    }
}
