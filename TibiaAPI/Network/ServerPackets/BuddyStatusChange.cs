using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class BuddyStatusChange : ServerPacket
    {
        public uint PlayerId { get; set; }

        public byte ConnectionStatus { get; set; }

        public BuddyStatusChange(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.BuddyStatusChange;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            PlayerId = message.ReadUInt32();
            ConnectionStatus = message.ReadByte();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.BuddyStatusChange);
            message.Write(PlayerId);
            message.Write(ConnectionStatus);
        }
    }
}
