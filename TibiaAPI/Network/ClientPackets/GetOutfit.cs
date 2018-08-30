using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class GetOutfit : ClientPacket
    {
        public ushort OutfitId { get; set; }

        public GetOutfit()
        {
            Type = ClientPacketType.GetOutfit;
        }

        public override bool ParseMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)Type)
            {
                return false;
            }

            OutfitId = message.ReadUInt16();
            return true;
        }

        public override void AppendToMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.GetOutfit);
            message.Write(OutfitId);
        }
    }
}
