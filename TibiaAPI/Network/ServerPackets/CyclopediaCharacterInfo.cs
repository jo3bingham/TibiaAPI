using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class CyclopediaCharacterInfo : ServerPacket
    {
        public CyclopediaCharacterInfo(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.CyclopediaCharacterInfo;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.CyclopediaCharacterInfo)
            {
                return false;
            }

            //01 type
            //19 00 00 00 00 00 00 00 exp
            //01 00 level
            //19
            //64 00 base xp gain
            //64 00 grinding addend
            //00 00 store boost
            //64 00 hunting boost factor
            //00 00 food ?
            //01
            //96 00 health
            //96 00 max health
            //37 00 mana
            //37 00 max mana
            //64 soul
            //D8 09 00 00 stamina
            //D0 02 offline training time
            //6E 00 speed
            //6E 00 base speed
            //40 9C 00 00
            //40 9C 00 00
            //78 82 00 00 08 01
            //02 00 02 00 02 00 A8 04 0B magic
            //0A 00 0A 00 0A 00 00 00 09 fist
            //0C 00 0C 00 0C 00 82 14 08 club
            //0C 00 0C 00 0C 00 B0 1D 0A sword
            //0C 00 0C 00 0C 00 82 14 07 axe
            //0C 00 0C 00 0C 00 82 14 06 distance
            //0A 00 0A 00 0A 00 00 00 0D shielding
            //0A 00 0A 00 0A 00 00 00 DA fishing
            //00
            //07 00 4E 69 6B 6F 6C 75 73 player name
            //04 00 4E 6F 6E 65 vocation
            //01 00 80 00 4E 45 3A 4C 0

            var type = message.ReadByte();
            if (type == 1)
            {
                var experience = message.ReadUInt64();
                var level = message.ReadUInt16();

                message.ReadBytes(1);

                var baseXpGain = message.ReadUInt16();
                var grindingAddend = message.ReadUInt16();
                var storeBoostAddend = message.ReadUInt16();
                var huntingBoostFactor = message.ReadUInt16();
                var foodRegeneration = message.ReadUInt16();

                message.ReadBytes(1);

                var currentHealth = message.ReadUInt16();
                var maxHealth = message.ReadUInt16();
                var currentMana = message.ReadUInt16();
                var maxMana = message.ReadUInt16();
                var soul = message.ReadByte();
                var stamina = message.ReadUInt16();
                var offlineTrainingTime = message.ReadUInt16();
                var currentSpeed = message.ReadUInt16();
                var baseSpeed = message.ReadUInt16();

                message.ReadBytes(4);
                message.ReadBytes(4);
                message.ReadBytes(6);

                // (Level, Base, Unknown, Progress, Unknown)
                var magic = (message.ReadUInt16(), message.ReadUInt16(), message.ReadUInt16(), message.ReadUInt16(), message.ReadByte());
                var fistFighting = (message.ReadUInt16(), message.ReadUInt16(), message.ReadUInt16(), message.ReadUInt16(), message.ReadByte());
                var clubFighting = (message.ReadUInt16(), message.ReadUInt16(), message.ReadUInt16(), message.ReadUInt16(), message.ReadByte());
                var swordFighting = (message.ReadUInt16(), message.ReadUInt16(), message.ReadUInt16(), message.ReadUInt16(), message.ReadByte());
                var axeFighting = (message.ReadUInt16(), message.ReadUInt16(), message.ReadUInt16(), message.ReadUInt16(), message.ReadByte());
                var distanceFighting = (message.ReadUInt16(), message.ReadUInt16(), message.ReadUInt16(), message.ReadUInt16(), message.ReadByte());
                var shielding = (message.ReadUInt16(), message.ReadUInt16(), message.ReadUInt16(), message.ReadUInt16(), message.ReadByte());
                var fishing = (message.ReadUInt16(), message.ReadUInt16(), message.ReadUInt16(), message.ReadUInt16(), message.ReadByte());

                message.ReadBytes(1);

                var playerName = message.ReadString();
                var vocation = message.ReadString();

                message.ReadBytes(9);
            }
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.CyclopediaCharacterInfo);
        }
    }
}
