using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class Attack : ClientPacket
    {
        public uint CreatureId { get; set; }

        public Attack(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.Attack;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            CreatureId = message.ReadUInt32();
            message.ReadUInt32(); // Creature ID again.
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.Attack);
            message.Write(CreatureId);
            message.Write(CreatureId);
        }
    }
}
