using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class SetStoreDeepLink : ServerPacket
    {
        public StoreServiceType StoreServiceType { get; set; }

        public ushort Unknown { get; set; }

        public SetStoreDeepLink(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.SetStoreDeepLink;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.SetStoreDeepLink)
            {
                return false;
            }

            if (Client.VersionNumber >= 11900000)
            {
                // TODO: Figure out this unknown.
                Unknown = message.ReadUInt16();
            }
            StoreServiceType = (StoreServiceType)message.ReadByte();
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.SetStoreDeepLink);
            if (Client.VersionNumber >= 11900000)
            {
                message.Write(Unknown);
            }
            message.Write((byte)StoreServiceType);
        }
    }
}
