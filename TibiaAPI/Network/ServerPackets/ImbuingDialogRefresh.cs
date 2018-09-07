using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class ImbuingDialogRefresh : ServerPacket
    {
        //public List<AstralSource> AvailableAstralSources { get; set; }
        //public List<ImbuementData> AvailableImbuements { get; set; }
        //public List<ExistingImbuement> ExistingImbuements { get; set; }
        //public ushort AppearanceTypeId { get; set; }

        public ImbuingDialogRefresh()
        {
            PacketType = ServerPacketType.ImbuingDialogRefresh;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.ImbuingDialogRefresh)
            {
                return false;
            }

            // TODO
            //AppearanceTypeId = message.ReadUInt16();

            //ExistingImbuements = new List<ExistingImbuement>(message.ReadByte());
            //for (var i = 0; i < ExistingImbuements.Capacity; i++)
            //{
            //    if (message.ReadBool())
            //    {
            //        var imbuementData = message.ReadImbuementData();
            //        var remainingDurationInSeconds = message.ReadUInt32();
            //        var clearingGoldCost = message.ReadUInt32();
            //        ExistingImbuements.Add(new ExistingImbuement(imbuementData,
            //                                                     remainingDurationInSeconds,
            //                                                     clearingGoldCost));
            //    }
            //    else
            //    {
            //        ExistingImbuements.Add(new ExistingImbuement());
            //    }
            //}

            //AvailableImbuements = new List<ImbuementData>(message.ReadUInt16());
            //for (var i = 0; i < AvailableImbuements.Capacity; i++)
            //{
            //    AvailableImbuements.Add(message.ReadImbuementData());
            //}

            //var count = message.ReadUInt32();
            //AvailableAstralSources = new List<AstralSource>((int)Math.Min(count, int.MaxValue));
            //for (var i = 0; i < count; i++)
            //{
            //    var astralId = message.ReadUInt16();
            //    var objectCount = message.ReadUInt16();
            //    AvailableAstralSources.Add(new AstralSource(astralId, objectCount));
            //}
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.ImbuingDialogRefresh);
            // TODO
            //message.WriteUInt16(AppearanceTypeId);
            //var existingImbuementsCount = (byte)Math.Min(ExistingImbuements.Count, byte.MaxValue);
            //message.WriteByte(existingImbuementsCount);
            //for (var i = 0; i < existingImbuementsCount; ++i)
            //{
            //    var existingImbuement = ExistingImbuements[i];
            //    var hasData = existingImbuement.ImbuementData == null;
            //    message.WriteBool(hasData);
            //    if (!hasData)
            //    {
            //        continue;
            //    }
            //    message.WriteImbuementData(existingImbuement.ImbuementData);
            //    message.WriteUInt32(existingImbuement.RemainingDurationInSeconds);
            //    message.WriteUInt32(existingImbuement.ClearingGoldCost);
            //}
            //var availableImbuementsCount = (ushort)Math.Min(AvailableImbuements.Count, ushort.MaxValue);
            //message.WriteUInt16(availableImbuementsCount);
            //for (var i = 0; i < availableImbuementsCount; ++i)
            //{
            //    message.WriteImbuementData(AvailableImbuements[i]);
            //}
            //var availableAstralSourcesCount = (uint)Math.Min(AvailableAstralSources.Count, uint.MaxValue);
            //message.WriteUInt32(availableAstralSourcesCount);
            //for (var i = 0; i < availableAstralSourcesCount; ++i)
            //{
            //    var astralSource = AvailableAstralSources[i];
            //    message.WriteUInt16(astralSource.AppearanceTypeId);
            //    message.WriteUInt16(astralSource.ObjectCount);
            //}
        }
    }
}
