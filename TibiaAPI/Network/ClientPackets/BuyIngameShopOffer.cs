using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class BuyIngameShopOffer : ClientPacket
    {
        public StoreServiceType ServiceType { get; set; }

        public string DesiredCharacterName { get; set; }
        public string TournamentContinent { get; set; }
        public string TournamentTown { get; set; }

        public uint OfferId { get; set; }

        public byte TournamentVocation { get; set; }

        public BuyIngameShopOffer(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.BuyIngameShopOffer;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            OfferId = message.ReadUInt32();
            ServiceType = (StoreServiceType)message.ReadByte();
            if (ServiceType == StoreServiceType.CharacterNameChange)
            {
                DesiredCharacterName = message.ReadString();
            }
            else if (ServiceType == StoreServiceType.TournamentTicket)
            {
                TournamentContinent = message.ReadString();
                TournamentVocation = message.ReadByte();
                TournamentTown = message.ReadString();
            }
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.BuyIngameShopOffer);
            message.Write(OfferId);
            message.Write((byte)ServiceType);
            if (ServiceType == StoreServiceType.CharacterNameChange)
            {
                message.Write(DesiredCharacterName);
            }
            else if (ServiceType == StoreServiceType.TournamentTicket)
            {
                message.Write(TournamentContinent);
                message.Write(TournamentVocation);
                message.Write(TournamentTown);
            }
        }
    }
}
