using OXGaming.TibiaAPI.Appearances;
using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class CreatureOutfit : ServerPacket
    {
        AppearanceInstance Mount { get; set; }
        AppearanceInstance Outfit { get; set; }

        public uint CreatureId { get; set; }

        public CreatureOutfit()
        {
            PacketType = ServerPacketType.CreatureOutfit;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.CreatureOutfit)
            {
                return false;
            }

            CreatureId = message.ReadUInt32();
            Outfit = message.ReadCreatureOutfit();
            Mount = message.ReadMountOutfit();
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.CreatureOutfit);
            message.Write(CreatureId);
            message.Write(Mount.Id);
            if (Outfit is OutfitInstance)
            {
                message.Write((OutfitInstance)Outfit);
            }
            else if (Outfit is ObjectInstance)
            {
                message.Write((ushort)0);
                message.Write(Outfit.Id);
            }
        }
    }
}
