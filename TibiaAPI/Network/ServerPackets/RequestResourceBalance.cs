using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class RequestResourceBalance : ServerPacket
    {
        public ResourceType ResourceType { get; set; }

        public long Balance { get; set; }

        public RequestResourceBalance(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.RequestResourceBalance;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.RequestResourceBalance)
            {
                return false;
            }

            ResourceType = (ResourceType)message.ReadByte();
            switch (ResourceType)
            {
                case ResourceType.BankGold:
                case ResourceType.InventoryGold:
                case ResourceType.PreyBonusRerolls:
                case ResourceType.CollectionTokens:
                    {
                        Balance = message.ReadInt64();
                    }
                    break;
                case ResourceType.CharmPoints:
                    {
                        if (Client.VersionNumber > 11586239)
                        {
                            Balance = message.ReadUInt32();
                        }
                        else
                        {
                            Balance = message.ReadInt64();
                        }
                    }
                    break;
                default:
                    break;
            }
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.RequestResourceBalance);
            message.Write((byte)ResourceType);
            if (ResourceType == ResourceType.CharmPoints)
            {
                if (Client.VersionNumber > 11586239)
                {
                    message.Write((uint)Balance);
                }
                else
                {
                    message.Write(Balance);
                }
            }
            else
            {
                message.Write(Balance);
            }
        }
    }
}
