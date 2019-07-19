using System;
using System.Collections.Generic;

using OXGaming.TibiaAPI.Constants;
using OXGaming.TibiaAPI.Imbuing;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class ImbuingDialogRefresh : ServerPacket
    {
        public List<AstralSource> AvailableAstralSources { get; } = new List<AstralSource>();
        public List<ExistingImbuement> ExistingImbuements { get; } = new List<ExistingImbuement>();
        public List<ImbuementData> AvailableImbuements { get; } = new List<ImbuementData>();
        
        public ushort AppearanceTypeId { get; set; }

        public ImbuingDialogRefresh(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.ImbuingDialogRefresh;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            AppearanceTypeId = message.ReadUInt16();

            ExistingImbuements.Capacity = message.ReadByte();
            for (var i = 0; i < ExistingImbuements.Capacity; i++)
            {
               if (message.ReadBool())
               {
                   var imbuementData = message.ReadImbuementData();
                   var remainingDurationInSeconds = message.ReadUInt32();
                   var clearingGoldCost = message.ReadUInt32();
                   ExistingImbuements.Add(new ExistingImbuement(imbuementData,
                                                                remainingDurationInSeconds,
                                                                clearingGoldCost));
               }
               else
               {
                   ExistingImbuements.Add(new ExistingImbuement());
               }
            }

            AvailableImbuements.Capacity = message.ReadUInt16();
            for (var i = 0; i < AvailableImbuements.Capacity; i++)
            {
               AvailableImbuements.Add(message.ReadImbuementData());
            }

            AvailableAstralSources.Capacity = (int)message.ReadUInt32();
            for (var i = 0; i < AvailableAstralSources.Capacity; i++)
            {
               var astralId = message.ReadUInt16();
               var objectCount = message.ReadUInt16();
               AvailableAstralSources.Add(new AstralSource(astralId, objectCount));
            }
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.ImbuingDialogRefresh);
            message.Write(AppearanceTypeId);

            var existingImbuementsCount = (byte)Math.Min(ExistingImbuements.Count, byte.MaxValue);
            message.Write(existingImbuementsCount);
            for (var i = 0; i < existingImbuementsCount; ++i)
            {
               var existingImbuement = ExistingImbuements[i];
               var hasData = existingImbuement.ImbuementData == null;
               message.Write(hasData);
               if (!hasData)
               {
                   continue;
               }
               message.Write(existingImbuement.ImbuementData);
               message.Write(existingImbuement.RemainingDurationInSeconds);
               message.Write(existingImbuement.ClearingGoldCost);
            }

            var availableImbuementsCount = (ushort)Math.Min(AvailableImbuements.Count, ushort.MaxValue);
            message.Write(availableImbuementsCount);
            for (var i = 0; i < availableImbuementsCount; ++i)
            {
               message.Write(AvailableImbuements[i]);
            }
            
            var availableAstralSourcesCount = (uint)Math.Min(AvailableAstralSources.Count, uint.MaxValue);
            message.Write(availableAstralSourcesCount);
            for (var i = 0; i < availableAstralSourcesCount; ++i)
            {
               var astralSource = AvailableAstralSources[i];
               message.Write(astralSource.AppearanceTypeId);
               message.Write(astralSource.ObjectCount);
            }
        }
    }
}
