using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class Attack : ClientPacket
    {
        public uint CreatureId { get; set; }
        public uint SecondCreatureId { get; set; }

        public Attack(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.Attack;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            CreatureId = message.ReadUInt32();
            SecondCreatureId = message.ReadUInt32(); // Should always be the same as CreatureId.
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.Attack);
            message.Write(CreatureId);
            message.Write(SecondCreatureId);
        }
    }
}
