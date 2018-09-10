using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class GetOutfit : ClientPacket
    {
        public ushort OutfitId { get; set; }

        public GetOutfit()
        {
            PacketType = ClientPacketType.GetOutfit;
        }

        public override bool ParseFromNetworkMessage(Client client, NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ClientPacketType.GetOutfit)
            {
                return false;
            }

            OutfitId = message.ReadUInt16();
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.GetOutfit);
            message.Write(OutfitId);
        }
    }
}
