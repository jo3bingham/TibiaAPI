using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class SetStoreDeepLink : ServerPacket
    {
        public ushort Unknown { get; set; }

        public byte StoreServiceType { get; set; }

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

            if (Client.VersionNumber >= 11887288)
            {
                // TODO: Figure out this unknown.
                Unknown = message.ReadUInt16();
            }
            StoreServiceType = message.ReadByte();
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.SetStoreDeepLink);
            if (Client.VersionNumber >= 11887288)
            {
                message.Write(Unknown);
            }
            message.Write(StoreServiceType);
        }
    }
}
