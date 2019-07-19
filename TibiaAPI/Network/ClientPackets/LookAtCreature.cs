using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class LookAtCreature : ClientPacket
    {
        public uint CreatureId { get; set; }

        public LookAtCreature(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.LookAtCreature;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            CreatureId = message.ReadUInt32();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.LookAtCreature);
            message.Write(CreatureId);
        }
    }
}
