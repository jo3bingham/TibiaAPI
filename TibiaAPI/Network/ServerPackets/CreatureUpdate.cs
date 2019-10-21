using System;
using OXGaming.TibiaAPI.Appearances;
using OXGaming.TibiaAPI.Constants;
using OXGaming.TibiaAPI.Creatures;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class CreatureUpdate : ServerPacket
    {
        public Creature OutdatedCreature { get; set; }

        public uint CreatureId { get; set; }

        public byte ManaPercent { get; set; }
        public byte Type { get; set; }
        public byte Unknown { get; set; }

        public CreatureUpdate(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.CreatureUpdate;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            CreatureId = message.ReadUInt32();
            Type = message.ReadByte();
            if (Type == 0x00) // Outdated Creature
            {
                var id = message.ReadUInt16();
                if (id == (ushort)CreatureInstanceType.OutdatedCreature)
                {
                    var creatureId = message.ReadUInt32();
                    OutdatedCreature = Client.CreatureStorage.GetCreature(creatureId);
                    if (OutdatedCreature == null)
                    {
                        throw new Exception("[CreatureUpdate.ParseFromNetworkMessage] Outdated creature not found.");
                    }

                    OutdatedCreature.InstanceType = (CreatureInstanceType)id;
                    OutdatedCreature.HealthPercent = message.ReadByte();
                    OutdatedCreature.Direction = (Direction)message.ReadByte();
                    OutdatedCreature.Outfit = message.ReadCreatureOutfit();
                    OutdatedCreature.Mount = message.ReadMountOutfit();
                    OutdatedCreature.Brightness = message.ReadByte();
                    OutdatedCreature.LightColor = message.ReadByte();
                    OutdatedCreature.Speed = message.ReadUInt16();
                    OutdatedCreature.PkFlag = message.ReadByte();
                    OutdatedCreature.PartyFlag = message.ReadByte();
                    OutdatedCreature.GuildFlag = message.ReadByte();

                    OutdatedCreature.Type = (Constants.CreatureType)message.ReadByte();
                    if (OutdatedCreature.Type == Constants.CreatureType.Player)
                    {
                        OutdatedCreature.Vocation = message.ReadByte();
                    }
                    else if (OutdatedCreature.IsSummon)
                    {
                        OutdatedCreature.SummonerCreatureId = message.ReadUInt32();
                    }

                    OutdatedCreature.SpeechCategory = message.ReadByte();
                    OutdatedCreature.Mark = message.ReadByte();
                    OutdatedCreature.InspectionState = message.ReadByte();
                    OutdatedCreature.IsUnpassable = message.ReadBool();
                }
            }
            else if (Type == 0x0B) // ManaPercent
            {
                ManaPercent = message.ReadByte();
            }
            else if (Type == 0x0C) // Unknown
            {
                Unknown = message.ReadByte();
            }
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.CreatureUpdate);
            message.Write(CreatureId);
            message.Write(Type);
            if (Type == 0x00 && OutdatedCreature != null) // Outdated Creature
            {
                message.Write((ushort)OutdatedCreature.InstanceType);
                if (OutdatedCreature.InstanceType == CreatureInstanceType.OutdatedCreature)
                {
                    message.Write(OutdatedCreature.Id);
                    message.Write(OutdatedCreature.HealthPercent);
                    message.Write((byte)OutdatedCreature.Direction);
                    if (OutdatedCreature.Outfit is OutfitInstance)
                    {
                        message.Write((OutfitInstance)OutdatedCreature.Outfit);
                    }
                    else
                    {
                        message.Write((ushort)0);
                        message.Write((ushort)OutdatedCreature.Outfit.Id);
                    }
                    message.Write((ushort)OutdatedCreature.Mount.Id);
                    message.Write(OutdatedCreature.Brightness);
                    message.Write(OutdatedCreature.LightColor);
                    message.Write(OutdatedCreature.Speed);
                    message.Write(OutdatedCreature.PkFlag);
                    message.Write(OutdatedCreature.PartyFlag);
                    message.Write(OutdatedCreature.GuildFlag);
                    message.Write((byte)OutdatedCreature.Type);
                    if (OutdatedCreature.Type == Constants.CreatureType.Player)
                    {
                        message.Write(OutdatedCreature.Vocation);
                    }
                    else if (OutdatedCreature.IsSummon)
                    {
                        message.Write(OutdatedCreature.SummonerCreatureId);
                    }
                    message.Write(OutdatedCreature.SpeechCategory);
                    message.Write(OutdatedCreature.Mark);
                    message.Write(OutdatedCreature.InspectionState);
                    message.Write(OutdatedCreature.IsUnpassable);
                }
            }
            else if (Type == 0x0B) // ManaPercent
            {
                message.Write(ManaPercent);
            }
            else if (Type == 0x0C) // Unknown
            {
                message.Write(Unknown);
            }
        }
    }
}
