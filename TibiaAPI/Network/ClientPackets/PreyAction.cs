using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class PreyAction : ClientPacket
    {
        public PreyActionType ActionType { get; set; }

        public byte MonsterIndex { get; set; }
        public byte PreyId { get; set; }

        public PreyAction(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.PreyAction;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            PreyId = message.ReadByte();
            ActionType = (PreyActionType)message.ReadByte();
            if (ActionType == PreyActionType.MonsterSelection)
            {
                MonsterIndex = message.ReadByte();
            }
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.PreyAction);
            message.Write(PreyId);
            message.Write((byte)ActionType);
            if (ActionType == PreyActionType.MonsterSelection)
            {
                message.Write(MonsterIndex);
            }
        }
    }
}
