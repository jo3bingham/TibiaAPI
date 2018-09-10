using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class BuddyStatusChange : ServerPacket
    {
        public uint PlayerId { get; set; }

        public byte ConnectionStatus { get; set; }

        public BuddyStatusChange()
        {
            PacketType = ServerPacketType.BuddyStatusChange;
        }

        public override bool ParseFromNetworkMessage(Client client, NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.BuddyStatusChange)
            {
                return false;
            }

            PlayerId = message.ReadUInt32();
            ConnectionStatus = message.ReadByte();
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.BuddyStatusChange);
            message.Write(PlayerId);
            message.Write(ConnectionStatus);
        }
    }
}
