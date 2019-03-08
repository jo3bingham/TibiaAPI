using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class OpenCyclopediaCharacterInfo : ClientPacket
    {
        public ushort RequestedPage { get; set; }
        public ushort ItemsPerPage { get; set; }

        public byte State { get; set; }

        public OpenCyclopediaCharacterInfo(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.OpenCyclopediaCharacterInfo;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ClientPacketType.OpenCyclopediaCharacterInfo)
            {
                return false;
            }

            State = message.ReadByte();
            if (State == 3 || State == 4) // Recent Deaths / Recent PvP Kills
            {
                ItemsPerPage = message.ReadUInt16();
                RequestedPage = message.ReadUInt16();
            }
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.OpenCyclopediaCharacterInfo);
            message.Write(State);
            if (State == 3 || State == 4) // Recent Deaths / Recent PvP Kills
            {
                message.Write(ItemsPerPage);
                message.Write(RequestedPage);
            }
        }
    }
}
