using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class SetStoreDeepLink : ServerPacket
    {
        public StoreServiceType StoreServiceType { get; set; }

        public ushort Unknown { get; set; }

        public SetStoreDeepLink()
        {
            PacketType = ServerPacketType.SetStoreDeepLink;
        }

        public override bool ParseFromNetworkMessage(Client client, NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.SetStoreDeepLink)
            {
                return false;
            }

            Unknown = message.ReadUInt16();
            StoreServiceType = (StoreServiceType)message.ReadByte();
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.SetStoreDeepLink);
            message.Write(Unknown);
            message.Write((byte)StoreServiceType);
        }
    }
}
