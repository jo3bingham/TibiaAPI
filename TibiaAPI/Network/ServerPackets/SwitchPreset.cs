using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class SwitchPreset : ServerPacket
    {
        public uint Profession { get; set; }

        public SwitchPreset(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.SwitchPreset;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            Profession = message.ReadUInt32();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.SwitchPreset);
            message.Write(Profession);
        }
    }
}
