using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class OpenCyclopediaCharacterInfo : ClientPacket
    {
        public byte State { get; set; }

        public OpenCyclopediaCharacterInfo(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.OpenCyclopediaCharacterInfo;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ClientPacketType.OpenCyclopediaCharacterInfo)
            {
                return false;
            }

            State = message.ReadByte();
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.OpenCyclopediaCharacterInfo);
            message.Write(State);
        }
    }
}
