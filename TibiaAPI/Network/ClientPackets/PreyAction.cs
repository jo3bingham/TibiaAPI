using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class PreyAction : ClientPacket
    {
        public PreyActionType ActionType { get; set; }

        public byte MonsterIndex { get; set; }
        public byte Option { get; set; }
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
            else if (ActionType == PreyActionType.Option)
            {
                Option = message.ReadByte(); // 0 = None, 1 = Automatic Bonus Reroll, 2 = Lock Prey
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
            else if (ActionType == PreyActionType.Option)
            {
                message.Write(Option);
            }
        }
    }
}
