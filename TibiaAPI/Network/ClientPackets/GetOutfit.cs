using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class GetOutfit : ClientPacket
    {
        public ushort LookType { get; set; }

        public bool IsTrying { get; set; }

        public GetOutfit(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.GetOutfit;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ClientPacketType.GetOutfit)
            {
                return false;
            }

            if (Client.VersionNumber >= 12000000)
            {
                IsTrying = message.ReadBool();
                if (IsTrying)
                {
                    LookType = message.ReadUInt16();
                }
            }
            else if (Client.VersionNumber >= 11706521)
            {
                LookType = message.ReadUInt16();
            }
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.GetOutfit);
            if (Client.VersionNumber >= 12000000)
            {
                message.Write(IsTrying);
                if (IsTrying)
                {
                    message.Write(LookType);
                }
            }
            else if (Client.VersionNumber >= 11706521)
            {
                message.Write(LookType);
            }
        }
    }
}
