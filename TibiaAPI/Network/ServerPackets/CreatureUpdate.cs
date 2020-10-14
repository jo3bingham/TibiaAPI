using System;
using OXGaming.TibiaAPI.Appearances;
using OXGaming.TibiaAPI.Constants;
using OXGaming.TibiaAPI.Creatures;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class CreatureUpdate : ServerPacket
    {
        // TODO
        public byte UnknownByte1 { get; set; }

        public Creature Creature { get; set; }

        public uint CreatureId { get; set; }

        public byte ManaPercent { get; set; }
        public byte Type { get; set; }

        public CreatureUpdate(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.CreatureUpdate;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            CreatureId = message.ReadUInt32();
            Type = message.ReadByte();
            if (Type == 0x00)
            {
                var id = message.ReadUInt16();
                if (id == (ushort)CreatureInstanceType.UnknownCreature)
                {
                    var removeCreatureId = message.ReadUInt32();
                    var creatureId = message.ReadUInt32();

                    Creature = (creatureId == Client.Player.Id) ? Client.Player : new Creature(creatureId);
                    Creature.RemoveCreatureId = removeCreatureId;
                    Creature.Type = (Constants.CreatureType)message.ReadByte();
                    Creature.InstanceType = (CreatureInstanceType)id;
                    Creature = Client.CreatureStorage.ReplaceCreature(Creature, Creature.RemoveCreatureId);
                    if (Creature == null)
                    {
                        throw new Exception("[NetworkMessage.ReadCreatureInstance] Failed to append creature.");
                    }

                    if (Creature.IsSummon)
                    {
                        Creature.SummonerCreatureId = message.ReadUInt32();
                    }

                    Creature.Name = message.ReadString();
                    Creature.HealthPercent = message.ReadByte();
                    Creature.Direction = (Direction)message.ReadByte();
                    Creature.Outfit = message.ReadCreatureOutfit();
                    Creature.Mount = message.ReadMountOutfit();
                    Creature.Brightness = message.ReadByte();
                    Creature.LightColor = message.ReadByte();
                    Creature.Speed = message.ReadUInt16();

                    if (Client.VersionNumber >= 12409997)
                    {
                        Creature.Unknown = message.ReadByte();
                    }

                    Creature.PkFlag = message.ReadByte();
                    Creature.PartyFlag = message.ReadByte();
                    Creature.GuildFlag = message.ReadByte();

                    Creature.Type = (Constants.CreatureType)message.ReadByte();
                    if (Creature.Type == Constants.CreatureType.Player)
                    {
                        Creature.Vocation = message.ReadByte();
                    }
                    else if (Creature.IsSummon)
                    {
                        Creature.SummonerCreatureId = message.ReadUInt32();
                    }

                    Creature.SpeechCategory = message.ReadByte();
                    Creature.Mark = message.ReadByte();
                    Creature.InspectionState = message.ReadByte();
                    Creature.IsUnpassable = message.ReadBool();
                }
                else if (id == (ushort)CreatureInstanceType.OutdatedCreature)
                {
                    var creatureId = message.ReadUInt32();
                    Creature = Client.CreatureStorage.GetCreature(creatureId);
                    if (Creature == null)
                    {
                        throw new Exception("[CreatureUpdate.ParseFromNetworkMessage] Outdated creature not found.");
                    }

                    Creature.InstanceType = (CreatureInstanceType)id;
                    Creature.HealthPercent = message.ReadByte();
                    Creature.Direction = (Direction)message.ReadByte();
                    Creature.Outfit = message.ReadCreatureOutfit();
                    Creature.Mount = message.ReadMountOutfit();
                    Creature.Brightness = message.ReadByte();
                    Creature.LightColor = message.ReadByte();
                    Creature.Speed = message.ReadUInt16();

                    if (Client.VersionNumber >= 12409997)
                    {
                        Creature.Unknown = message.ReadByte();
                    }

                    Creature.PkFlag = message.ReadByte();
                    Creature.PartyFlag = message.ReadByte();

                    Creature.Type = (Constants.CreatureType)message.ReadByte();
                    if (Creature.Type == Constants.CreatureType.Player)
                    {
                        Creature.Vocation = message.ReadByte();
                    }
                    else if (Creature.IsSummon)
                    {
                        Creature.SummonerCreatureId = message.ReadUInt32();
                    }

                    Creature.SpeechCategory = message.ReadByte();
                    Creature.Mark = message.ReadByte();
                    Creature.InspectionState = message.ReadByte();
                    Creature.IsUnpassable = message.ReadBool();
                }
            }
            else if (Type == 0x0B) // ManaPercent
            {
                ManaPercent = message.ReadByte();
            }
            // TODO
            else if (Type == 0x0C) // Unknown
            {
                UnknownByte1 = message.ReadByte();
            }
            else if (Type == 0x0E) // Unknown
            {
                UnknownByte1 = message.ReadByte();
            }
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.CreatureUpdate);
            message.Write(CreatureId);
            message.Write(Type);
            if (Type == 0x00 && Creature != null)
            {
                message.Write((ushort)Creature.InstanceType);
                if (Creature.InstanceType == CreatureInstanceType.UnknownCreature)
                {
                    message.Write(Creature.RemoveCreatureId);
                    message.Write(Creature.Id);
                    message.Write((byte)Creature.Type);
                    if (Creature.IsSummon)
                    {
                        message.Write(Creature.SummonerCreatureId);
                    }

                    message.Write(Creature.Name);
                    message.Write(Creature.HealthPercent);
                    message.Write((byte)Creature.Direction);

                    if (Creature.Outfit is OutfitInstance)
                    {
                        message.Write((OutfitInstance)Creature.Outfit);
                    }
                    else
                    {
                        message.Write((ushort)0);
                        message.Write((ushort)Creature.Outfit.Id);
                    }

                    message.Write((ushort)Creature.Mount.Id);
                    message.Write(Creature.Brightness);
                    message.Write(Creature.LightColor);
                    message.Write(Creature.Speed);

                    if (Client.VersionNumber >= 12409997)
                    {
                        message.Write(Creature.Unknown);
                    }

                    message.Write(Creature.PkFlag);
                    message.Write(Creature.PartyFlag);
                    message.Write(Creature.GuildFlag);

                    message.Write((byte)Creature.Type);
                    if (Creature.Type == Constants.CreatureType.Player)
                    {
                        message.Write(Creature.Vocation);
                    }
                    else if (Creature.IsSummon)
                    {
                        message.Write(Creature.SummonerCreatureId);
                    }

                    message.Write(Creature.SpeechCategory);
                    message.Write(Creature.Mark);
                    message.Write(Creature.InspectionState);
                    if (Client.VersionNumber < 11900000)
                    {
                        message.Write(Creature.PvpHelpers);
                    }
                    message.Write(Creature.IsUnpassable);
                }
                else if (Creature.InstanceType == CreatureInstanceType.OutdatedCreature)
                {
                    message.Write(Creature.Id);
                    message.Write(Creature.HealthPercent);
                    message.Write((byte)Creature.Direction);
                    if (Creature.Outfit is OutfitInstance)
                    {
                        message.Write((OutfitInstance)Creature.Outfit);
                    }
                    else
                    {
                        message.Write((ushort)0);
                        message.Write((ushort)Creature.Outfit.Id);
                    }
                    message.Write((ushort)Creature.Mount.Id);
                    message.Write(Creature.Brightness);
                    message.Write(Creature.LightColor);
                    message.Write(Creature.Speed);

                    if (Client.VersionNumber >= 12409997)
                    {
                        message.Write(Creature.Unknown);
                    }

                    message.Write(Creature.PkFlag);
                    message.Write(Creature.PartyFlag);
                    message.Write(Creature.GuildFlag);
                    message.Write((byte)Creature.Type);
                    if (Creature.Type == Constants.CreatureType.Player)
                    {
                        message.Write(Creature.Vocation);
                    }
                    else if (Creature.IsSummon)
                    {
                        message.Write(Creature.SummonerCreatureId);
                    }
                    message.Write(Creature.SpeechCategory);
                    message.Write(Creature.Mark);
                    message.Write(Creature.InspectionState);
                    message.Write(Creature.IsUnpassable);
                }
            }
            else if (Type == 0x0B) // ManaPercent
            {
                message.Write(ManaPercent);
            }
            // TODO
            else if (Type == 0x0C) // Unknown
            {
                message.Write(UnknownByte1);
            }
            else if (Type == 0x0E) // Unknown
            {
                message.Write(UnknownByte1);
            }
        }
    }
}
