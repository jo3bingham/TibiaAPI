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

            //01 type: character stats
            //19 00 00 00 00 00 00 00 exp
            //01 00 level
            //19
            //64 00 base xp gain
            //64 00 grinding addend
            //00 00 store boost
            //64 00 hunting boost factor
            //00 00 food ?
            //01 show store xp boost button?
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
            //78 82 00 00
            //08 number of skills
              //01 02 00 02 00 02 00 A8 04 magic
              //0B 0A 00 0A 00 0A 00 00 00 fist
              //09 0C 00 0C 00 0C 00 82 14 club
              //08 0C 00 0C 00 0C 00 B0 1D sword
              //0A 0C 00 0C 00 0C 00 82 14 axe
              //07 0C 00 0C 00 0C 00 82 14 distance
              //06 0A 00 0A 00 0A 00 00 00 shielding
              //0D 0A 00 0A 00 0A 00 00 00 fishing
            //00 DA
            //07 00 4E 69 6B 6F 6C 75 73 player name
            //04 00 4E 6F 6E 65 vocation
            //01 00 level
            //80 00 4E 45 3A 4C 0 outfit

            var type = message.ReadByte();
            if (type == 1) //character stats
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
                message.ReadBytes(4);

                var numberOfSkills = message.ReadByte();

                // (Icon/Skill Id?, Level, Base, Unknown, Progress)
                var magic = (message.ReadByte(), message.ReadUInt16(), message.ReadUInt16(), message.ReadUInt16(), message.ReadUInt16());
                var fistFighting = (message.ReadByte(), message.ReadUInt16(), message.ReadUInt16(), message.ReadUInt16(), message.ReadUInt16());
                var clubFighting = (message.ReadByte(), message.ReadUInt16(), message.ReadUInt16(), message.ReadUInt16(), message.ReadUInt16());
                var swordFighting = (message.ReadByte(), message.ReadUInt16(), message.ReadUInt16(), message.ReadUInt16(), message.ReadUInt16());
                var axeFighting = (message.ReadByte(), message.ReadUInt16(), message.ReadUInt16(), message.ReadUInt16(), message.ReadUInt16());
                var distanceFighting = (message.ReadByte(), message.ReadUInt16(), message.ReadUInt16(), message.ReadUInt16(), message.ReadUInt16());
                var shielding = (message.ReadByte(), message.ReadUInt16(), message.ReadUInt16(), message.ReadUInt16(), message.ReadUInt16());
                var fishing = (message.ReadByte(), message.ReadUInt16(), message.ReadUInt16(), message.ReadUInt16(), message.ReadUInt16());

                message.ReadBytes(2);

                var playerName = message.ReadString();
                var vocation = message.ReadString();

                var levelAgain = message.ReadUInt16();
                var outfit = message.ReadCreatureOutfit();
            }

            //02 type: combat stats
            //00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 07 57 00 00 00 00 contains crit/leech, attack value, and converted damage
            //1F 00 armor value
            //44 00 defence value
            //01 number of damage reductions
            //00 damage reduction type (physical)
            //02 amount

            //02 type: combat stats
            //00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 07 04 00 00 00 00 contains crit/leech, attack value, and converted damage
            //02 00 armor value
            //03 00 defence value
            //00 number of damage reductions

            //03 type: recent deaths
            //01 00 01 00 01 00
            //E1 8D F5 5B date and time
            //19 00 44 69 65 64 20 61 74 20 4C 65 76 65 6C 20 31 20 62 79 20 61 20 72 61 74 2E cause of death
            //63 03 00 00 00 09 01 00

            //04 type: recent pvp kills
            //01 00 01 00 00 00

            //05 type: achievements
            //03 00 achievement points
            //B3 00 total secret
            //02 00 accomplished
              //C4 00 id
              //13 7D 4C 58 timestamp
              //01 type
                //12 00 53 61 66 65 6C 79 20 53 74 6F 72 65 64 20 41 77 61 79 achievement name
                //3F 00 44 6F 6E 27 74 20 77 6F 72 72 79 2C 20 6E 6F 20 6F 6E 65 20 77 69 6C 6C 20 62 65 20 61 62 6C 65 20 74 6F 20 74 61 6B 65 20 69 74 20 66 72 6F 6D 20 79 6F 75 2E 20 50 72 6F 62 61 62 6C 79 2E achievement description
                //01 B4 01 0F 52 6A 
              //5A 00 id
              //8D 32 FE 53 timestamp
              //03 type
                //06 65

            //05 type: achievements
            //00 00 achievement points
            //B3 00 total secret
            //00 00 accomplished

            //05 type: achievements
            //08 00 achivement points
            //B3 00 total secret
            //05 00 accomplished
              //25 00 id
              //8C 0C 39 4C timestamp
              //00 is type
              //28 00 id
              //8C 0C 39 4C timestamp
              //00 is type
              //2E 00 id
              //8C 0C 39 4C timestamp
              //00 is type
              //35 00 id
              //8C 0C 39 4C timestamp
              //00 is type
              //36 00 id
              //8C 0C 39 4C timestamp
              //00 is type
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.CyclopediaCharacterInfo);
        }
    }
}
