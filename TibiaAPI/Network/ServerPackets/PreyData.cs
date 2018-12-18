using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class PreyData : ServerPacket
    {
        public PreyData(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.PreyData;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.PreyData)
            {
                return false;
            }

            var preyArrayIndex = message.ReadByte();

            var preyState = (PreyDataState)message.ReadByte();
            switch (preyState)
            {
                case PreyDataState.Locked:
                    {
                        var unlockOption = message.ReadByte(); // 0 = temporary and permanent, 1 = permanent
                        break;
                    }
                case PreyDataState.Active:
                    {
                        var preyName = message.ReadString();
                        var preyOutfit = message.ReadCreatureOutfit(Client);
                        var bonusType = message.ReadByte(); // 0 = damage, 1 = defense, 2 = exp, 3 = loot
                        var bonusPercentage = message.ReadUInt16();
                        var bonusRarity = message.ReadByte();
                        var timeLeftInSeconds = message.ReadUInt16();
                    }
                    break;
                case PreyDataState.Selection:
                    {
                        var preyCount = message.ReadByte();
                        for (var i = 0; i < preyCount; i++)
                        {
                            var preyName = message.ReadString();
                            var preyOutfit = message.ReadCreatureOutfit(Client);
                        }
                    }
                    break;
                case PreyDataState.SelectionChangeMonster:
                    {
                        var bonusType = message.ReadByte();
                        var bonusPercentage = message.ReadUInt16();
                        var bonusRarity = message.ReadByte();

                        var preyCount = message.ReadByte();
                        for (var i = 0; i < preyCount; i++)
                        {
                            var preyName = message.ReadString();
                            var preyOutfit = message.ReadCreatureOutfit(Client);
                        }
                    }
                    break;
            }

            var timeLeftUntilFreeListReroll = message.ReadUInt16();
            var preyOption = message.ReadByte(); // 0 = none, 1 = automatic reroll, 2 = locked
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.PreyData);
            // TODO
        }
    }
}
