using OXGaming.TibiaAPI.Appearances;
using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class CreatureOutfit : ServerPacket
    {
        public AppearanceInstance Mount { get; set; }
        public AppearanceInstance Outfit { get; set; }

        public uint CreatureId { get; set; }

        public CreatureOutfit(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.CreatureOutfit;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            CreatureId = message.ReadUInt32();
            Outfit = message.ReadCreatureOutfit();
            Mount = message.ReadMountOutfit();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.CreatureOutfit);
            message.Write(CreatureId);
            if (Outfit is OutfitInstance)
            {
                message.Write((OutfitInstance)Outfit);
            }
            else
            {
                message.Write((ushort)0);
                message.Write((ushort)Outfit.Id);
            }
            message.Write((ushort)Mount.Id);
        }
    }
}
