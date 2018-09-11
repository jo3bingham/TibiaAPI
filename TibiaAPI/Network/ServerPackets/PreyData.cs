using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class PreyData : ServerPacket
    {
        public PreyData()
        {
            PacketType = ServerPacketType.PreyData;
        }

        public override bool ParseFromNetworkMessage(Client client, NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.PreyData)
            {
                return false;
            }

            // TODO
            var preyArrayIndex = message.ReadByte();

            var preyState = (PreyDataState)message.ReadByte();
            switch (preyState)
            {
                case PreyDataState.Locked:
                    {
                        var unlockOption = message.ReadByte();
                        break;
                    }
                case PreyDataState.Active:
                    {
                        var preyName = message.ReadString();
                        message.ReadCreatureOutfit(client);
                        var bonusType = message.ReadByte();
                        var bonusValue = message.ReadUInt16();
                        var bonusGrade = message.ReadByte();
                        var preyTimeLeft = message.ReadUInt16();
                    }
                    break;
                case PreyDataState.Selection:
                    {
                        var preyCount = message.ReadByte();
                        for (var i = 0; i < preyCount; i++)
                        {
                            var preyName = message.ReadString();
                            message.ReadCreatureOutfit(client);
                        }
                    }
                    break;
                case PreyDataState.SelectionChangeMonster:
                    {
                        message.ReadByte();
                        message.ReadUInt16();
                        message.ReadByte();

                        var count = message.ReadByte();
                        for (var i = 0; i < count; i++)
                        {
                            var preyName = message.ReadString();
                            message.ReadCreatureOutfit(client);
                        }
                    }
                    break;
            }

            var timeLeftUntilFreeListReroll = message.ReadUInt16();
            message.ReadByte();
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.PreyData);
            // TODO
        }
    }
}
