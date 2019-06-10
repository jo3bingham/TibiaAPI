using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class OpenCyclopediaCharacterInfo : ClientPacket
    {
        public uint PlayerId { get; set; }

        public ushort RequestedPage { get; set; }
        public ushort ItemsPerPage { get; set; }

        public byte State { get; set; }

        public OpenCyclopediaCharacterInfo(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.OpenCyclopediaCharacterInfo;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            if (Client.VersionNumber >= 12158493)
            {
                PlayerId = message.ReadUInt32();
            }

            State = message.ReadByte();
            if (State == 3 || State == 4) // Recent Deaths / Recent PvP Kills
            {
                ItemsPerPage = message.ReadUInt16();
                RequestedPage = message.ReadUInt16();
            }
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.OpenCyclopediaCharacterInfo);
            if (Client.VersionNumber >= 12158493)
            {
                message.Write(PlayerId);
            }
            message.Write(State);
            if (State == 3 || State == 4) // Recent Deaths / Recent PvP Kills
            {
                message.Write(ItemsPerPage);
                message.Write(RequestedPage);
            }
        }
    }
}
