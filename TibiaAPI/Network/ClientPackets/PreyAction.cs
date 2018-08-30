using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class PreyAction : ClientPacket
    {
        public PreyActionType ActionType { get; set; }

        public byte MonsterIndex { get; set; }
        public byte PreyId { get; set; }

        public PreyAction()
        {
            Type = ClientPacketType.PreyAction;
        }

        public override bool ParseMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)Type)
            {
                return false;
            }

            PreyId = message.ReadByte();
            ActionType = (PreyActionType)message.ReadByte();
            if (ActionType == PreyActionType.MonsterSelection)
            {
                MonsterIndex = message.ReadByte();
            }
            return true;
        }

        public override void AppendToMessage(NetworkMessage message)
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
