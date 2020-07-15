using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class PreyHuntingTaskAction : ClientPacket
    {
        public PreyHuntingTaskAction(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.PreyHuntingTaskAction;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            // TODO

            // 01 03 00 A2 05

            // 00 03 00 1C 01

            message.ReadByte(); //index
            message.ReadUInt16(); //?
            message.ReadUInt16(); //race id
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            // TODO
            // message.Write((byte)ClientPacketType.PreyHuntingTaskAction);
        }
    }
}
