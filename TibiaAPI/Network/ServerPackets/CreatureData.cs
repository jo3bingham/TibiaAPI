using OXGaming.TibiaAPI.Constants;
using OXGaming.TibiaAPI.Creatures;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class CreatureData : ServerPacket
    {
        public Creature Creature { get; set; }

        public CreatureData(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.CreatureData;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.CreatureData)
            {
                return false;
            }

            Creature = message.ReadCreatureInstance((int)CreatureInstanceType.UnknownCreature);
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.CreatureData);
            message.Write(Creature, CreatureInstanceType.UnknownCreature);
        }
    }
}
