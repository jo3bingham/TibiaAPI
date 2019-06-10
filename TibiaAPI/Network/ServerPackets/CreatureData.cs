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

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            Creature = message.ReadCreatureInstance((int)CreatureInstanceType.UnknownCreature);
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.CreatureData);
            message.Write(Creature, CreatureInstanceType.UnknownCreature);
        }
    }
}
