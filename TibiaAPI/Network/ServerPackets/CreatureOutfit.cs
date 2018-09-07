using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class CreatureOutfit : ServerPacket
    {
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
            // TODO
            //message.ReadCreatureOutfit();
            //message.ReadMountOutfit();
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.CreatureOutfit);
            message.Write(CreatureId);
            // TODO
            //message.WriteCreateOutfit(Outfit);
            //message.WriteMountOutfit(Mount);
        }
    }
}
