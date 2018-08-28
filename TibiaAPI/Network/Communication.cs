using System;

using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network
{
    internal static class Communication
    {
        internal static void ParseClientMessage(NetworkMessage message)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            while (message.Position < message.Size)
            {
                var type = (ClientPacketType)message.ReadByte();
                switch (type)
                {
                    case ClientPacketType.Login:
                        break;
                    case ClientPacketType.SecondaryLogin:
                        break;
                    case ClientPacketType.EnterWorld:
                        break;
                    case ClientPacketType.QuitGame:
                        break;
                    case ClientPacketType.ConnectionPingBack:
                        break;
                    case ClientPacketType.Ping:
                        break;
                    case ClientPacketType.PingBack:
                        break;
                    case ClientPacketType.PerformanceMetrics:
                        break;
                    case ClientPacketType.StashAction:
                        break;
                    case ClientPacketType.ClientCheck:
                        break;
                    case ClientPacketType.GoPath:
                        break;
                    case ClientPacketType.GoNorth:
                        break;
                    case ClientPacketType.GoEast:
                        break;
                    case ClientPacketType.GoSouth:
                        break;
                    case ClientPacketType.GoWest:
                        break;
                    case ClientPacketType.Stop:
                        break;
                    case ClientPacketType.GoNorthEast:
                        break;
                    case ClientPacketType.GoSouthEast:
                        break;
                    case ClientPacketType.GoSouthWest:
                        break;
                    case ClientPacketType.GoNorthWest:
                        break;
                    case ClientPacketType.RotateNorth:
                        break;
                    case ClientPacketType.RotateEast:
                        break;
                    case ClientPacketType.RotateSouth:
                        break;
                    case ClientPacketType.RotateWest:
                        break;
                    case ClientPacketType.Teleport:
                        break;
                    case ClientPacketType.EquipObject:
                        break;
                    case ClientPacketType.MoveObject:
                        break;
                    case ClientPacketType.LookNpcTrade:
                        break;
                    case ClientPacketType.BuyObject:
                        break;
                    case ClientPacketType.SellObject:
                        break;
                    case ClientPacketType.CloseNpcTrade:
                        break;
                    case ClientPacketType.TradeObject:
                        break;
                    case ClientPacketType.LookTrade:
                        break;
                    case ClientPacketType.AcceptTrade:
                        break;
                    case ClientPacketType.RejectTrade:
                        break;
                    case ClientPacketType.UseObject:
                        break;
                    case ClientPacketType.UseTwoObjects:
                        break;
                    case ClientPacketType.UseOnCreature:
                        break;
                    case ClientPacketType.TurnObject:
                        break;
                    case ClientPacketType.CloseContainer:
                        break;
                    case ClientPacketType.UpContainer:
                        break;
                    case ClientPacketType.EditText:
                        break;
                    case ClientPacketType.EditList:
                        break;
                    case ClientPacketType.ToggleWrapState:
                        break;
                    case ClientPacketType.Look:
                        break;
                    case ClientPacketType.LookAtCreature:
                        break;
                    case ClientPacketType.JoinAggression:
                        break;
                    case ClientPacketType.QuickLoot:
                        break;
                    case ClientPacketType.LootContainer:
                        break;
                    case ClientPacketType.QuickLootBlackWhitelist:
                        break;
                    case ClientPacketType.Talk:
                        break;
                    case ClientPacketType.GetChannels:
                        break;
                    case ClientPacketType.JoinChannel:
                        break;
                    case ClientPacketType.LeaveChannel:
                        break;
                    case ClientPacketType.PrivateChannel:
                        break;
                    case ClientPacketType.GuildMessage:
                        break;
                    case ClientPacketType.EditGuildMessage:
                        break;
                    case ClientPacketType.CloseNpcChannel:
                        break;
                    case ClientPacketType.SetTactics:
                        break;
                    case ClientPacketType.Attack:
                        break;
                    case ClientPacketType.Follow:
                        break;
                    case ClientPacketType.InviteToParty:
                        break;
                    case ClientPacketType.JoinParty:
                        break;
                    case ClientPacketType.RevokeInvitation:
                        break;
                    case ClientPacketType.PassLeadership:
                        break;
                    case ClientPacketType.LeaveParty:
                        break;
                    case ClientPacketType.ShareExperience:
                        break;
                    case ClientPacketType.DisbandParty:
                        break;
                    case ClientPacketType.OpenChannel:
                        break;
                    case ClientPacketType.InviteToChannel:
                        break;
                    case ClientPacketType.ExcludeFromChannel:
                        break;
                    case ClientPacketType.Cancel:
                        break;
                    case ClientPacketType.UpdateExivaOptions:
                        break;
                    case ClientPacketType.BrowseField:
                        break;
                    case ClientPacketType.SeekInContainer:
                        break;
                    case ClientPacketType.InspectObject:
                        break;
                    case ClientPacketType.InspectPlayer:
                        break;
                    case ClientPacketType.BlessingsDialog:
                        break;
                    case ClientPacketType.TrackQuestFlags:
                        break;
                    case ClientPacketType.MarketStatistics:
                        break;
                    case ClientPacketType.GetOutfit:
                        break;
                    case ClientPacketType.SetOutfit:
                        break;
                    case ClientPacketType.Mount:
                        break;
                    case ClientPacketType.ApplyImbuement:
                        break;
                    case ClientPacketType.ApplyClearingCharm:
                        break;
                    case ClientPacketType.ClosedImbuingDialog:
                        break;
                    case ClientPacketType.OpenRewardWall:
                        break;
                    case ClientPacketType.DailyRewardHistory:
                        break;
                    case ClientPacketType.CollectDailyReward:
                        break;
                    case ClientPacketType.CyclopediaMapAction:
                        break;
                    case ClientPacketType.AddBuddy:
                        break;
                    case ClientPacketType.RemoveBuddy:
                        break;
                    case ClientPacketType.EditBuddy:
                        break;
                    case ClientPacketType.BuddyGroup:
                        break;
                    case ClientPacketType.MarkGameNewsAsRead:
                        break;
                    case ClientPacketType.OpenMonsterCyclopedia:
                        break;
                    case ClientPacketType.OpenMonsterCyclopediaMonsters:
                        break;
                    case ClientPacketType.OpenMonsterCyclopediaRace:
                        break;
                    case ClientPacketType.MonsterBonusEffectAction:
                        break;
                    case ClientPacketType.BugReport:
                        break;
                    case ClientPacketType.ThankYou:
                        break;
                    case ClientPacketType.GetOfferDescription:
                        break;
                    case ClientPacketType.StoreEvent:
                        break;
                    case ClientPacketType.FeatureEvent:
                        break;
                    case ClientPacketType.PreyAction:
                        break;
                    case ClientPacketType.RequestResourceBalance:
                        break;
                    case ClientPacketType.TransferCurrency:
                        break;
                    case ClientPacketType.GetQuestLog:
                        break;
                    case ClientPacketType.GetQuestLine:
                        break;
                    case ClientPacketType.RuleViolationReport:
                        break;
                    case ClientPacketType.GetObjectInfo:
                        break;
                    case ClientPacketType.MarketLeave:
                        break;
                    case ClientPacketType.MarketBrowse:
                        break;
                    case ClientPacketType.MarketCreate:
                        break;
                    case ClientPacketType.MarketCancel:
                        break;
                    case ClientPacketType.MarketAccept:
                        break;
                    case ClientPacketType.AnswerModalDialog:
                        break;
                    case ClientPacketType.OpenIngameShop:
                        break;
                    case ClientPacketType.RequestShopOffers:
                        break;
                    case ClientPacketType.BuyIngameShopOffer:
                        break;
                    case ClientPacketType.OpenTransactionHistory:
                        break;
                    case ClientPacketType.GetTransactionHistory:
                        break;
                    default:
                        break;
                }
            }
        }

        internal static void ParseServerMessage(NetworkMessage message)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            while (message.Position < message.Size)
            {
                var type = (ServerPacketType)message.ReadByte();
                switch (type)
                {
                    case ServerPacketType.CreatureData:
                        break;
                    case ServerPacketType.PendingStateEntered:
                        break;
                    case ServerPacketType.ReadyForSecondaryConnection:
                        break;
                    case ServerPacketType.WorldEntered:
                        break;
                    case ServerPacketType.LoginError:
                        break;
                    case ServerPacketType.LoginAdvice:
                        break;
                    case ServerPacketType.LoginWait:
                        break;
                    case ServerPacketType.LoginSuccess:
                        break;
                    case ServerPacketType.StoreButtonIndicators:
                        break;
                    case ServerPacketType.Ping:
                        break;
                    case ServerPacketType.PingBack:
                        break;
                    case ServerPacketType.LoginChallenge:
                        break;
                    case ServerPacketType.Dead:
                        break;
                    case ServerPacketType.Stash:
                        break;
                    case ServerPacketType.DepotTileState:
                        break;
                    case ServerPacketType.ClientCheck:
                        break;
                    case ServerPacketType.FullMap:
                        break;
                    case ServerPacketType.TopRow:
                        break;
                    case ServerPacketType.RightColumn:
                        break;
                    case ServerPacketType.BottomRow:
                        break;
                    case ServerPacketType.LeftColumn:
                        break;
                    case ServerPacketType.FieldData:
                        break;
                    case ServerPacketType.CreateOnMap:
                        break;
                    case ServerPacketType.ChangeOnMap:
                        break;
                    case ServerPacketType.DeleteOnMap:
                        break;
                    case ServerPacketType.MoveCreature:
                        break;
                    case ServerPacketType.Container:
                        break;
                    case ServerPacketType.CloseContainer:
                        break;
                    case ServerPacketType.CreateInContainer:
                        break;
                    case ServerPacketType.ChangeInContainer:
                        break;
                    case ServerPacketType.DeleteInContainer:
                        break;
                    case ServerPacketType.ScreenshotEvent:
                        break;
                    case ServerPacketType.InspectionList:
                        break;
                    case ServerPacketType.InspectionState:
                        break;
                    case ServerPacketType.SetInventory:
                        break;
                    case ServerPacketType.DeleteInventory:
                        break;
                    case ServerPacketType.NpcOffer:
                        break;
                    case ServerPacketType.PlayerGoods:
                        break;
                    case ServerPacketType.CloseNpcTrade:
                        break;
                    case ServerPacketType.OwnOffer:
                        break;
                    case ServerPacketType.CounterOffer:
                        break;
                    case ServerPacketType.CloseTrade:
                        break;
                    case ServerPacketType.Ambiente:
                        break;
                    case ServerPacketType.GraphicalEffect:
                        break;
                    case ServerPacketType.RemoveGraphicalEffect:
                        break;
                    case ServerPacketType.MissileEffect:
                        break;
                    case ServerPacketType.Trappers:
                        break;
                    case ServerPacketType.CreatureHealth:
                        break;
                    case ServerPacketType.CreatureLight:
                        break;
                    case ServerPacketType.CreatureOutfit:
                        break;
                    case ServerPacketType.CreatureSpeed:
                        break;
                    case ServerPacketType.CreatureSkull:
                        break;
                    case ServerPacketType.CreatureParty:
                        break;
                    case ServerPacketType.CreatureUnpass:
                        break;
                    case ServerPacketType.CreatureMarks:
                        break;
                    case ServerPacketType.CreaturePvpHelpers:
                        break;
                    case ServerPacketType.CreatureType:
                        break;
                    case ServerPacketType.EditText:
                        break;
                    case ServerPacketType.EditList:
                        break;
                    case ServerPacketType.ShowGameNews:
                        break;
                    case ServerPacketType.BlessingsDialog:
                        break;
                    case ServerPacketType.Blessings:
                        break;
                    case ServerPacketType.SwitchPreset:
                        break;
                    case ServerPacketType.PremiumTrigger:
                        break;
                    case ServerPacketType.PlayerDataBasic:
                        break;
                    case ServerPacketType.PlayerDataCurrent:
                        break;
                    case ServerPacketType.PlayerSkills:
                        break;
                    case ServerPacketType.PlayerState:
                        break;
                    case ServerPacketType.ClearTarget:
                        break;
                    case ServerPacketType.SpellDelay:
                        break;
                    case ServerPacketType.SpellGroupDelay:
                        break;
                    case ServerPacketType.MultiUseDelay:
                        break;
                    case ServerPacketType.SetTactics:
                        break;
                    case ServerPacketType.SetStoreDeepLink:
                        break;
                    case ServerPacketType.RestingAreaState:
                        break;
                    case ServerPacketType.Talk:
                        break;
                    case ServerPacketType.Channels:
                        break;
                    case ServerPacketType.OpenChannel:
                        break;
                    case ServerPacketType.PrivateChannel:
                        break;
                    case ServerPacketType.EditGuildMessage:
                        break;
                    case ServerPacketType.OpenOwnChannel:
                        break;
                    case ServerPacketType.CloseChannel:
                        break;
                    case ServerPacketType.Message:
                        break;
                    case ServerPacketType.SnapBack:
                        break;
                    case ServerPacketType.Wait:
                        break;
                    case ServerPacketType.UnjustifiedPoints:
                        break;
                    case ServerPacketType.PvpSituations:
                        break;
                    case ServerPacketType.TopFloor:
                        break;
                    case ServerPacketType.BottomFloor:
                        break;
                    case ServerPacketType.UpdateLootContainers:
                        break;
                    case ServerPacketType.Outfit:
                        break;
                    case ServerPacketType.ExivaSuppressed:
                        break;
                    case ServerPacketType.UpdateExivaOptions:
                        break;
                    case ServerPacketType.ImpactTracking:
                        break;
                    case ServerPacketType.MarketStatistics:
                        break;
                    case ServerPacketType.ItemWasted:
                        break;
                    case ServerPacketType.ItemLooted:
                        break;
                    case ServerPacketType.TrackQuestFlags:
                        break;
                    case ServerPacketType.KillTracking:
                        break;
                    case ServerPacketType.BuddyData:
                        break;
                    case ServerPacketType.BuddyStatusChange:
                        break;
                    case ServerPacketType.BuddyGroupData:
                        break;
                    case ServerPacketType.MonsterCyclopedia:
                        break;
                    case ServerPacketType.MonsterCyclopediaMonsters:
                        break;
                    case ServerPacketType.MonsterCyclopediaRace:
                        break;
                    case ServerPacketType.MonsterCyclopediaBonusEffects:
                        break;
                    case ServerPacketType.MonsterCyclopediaNewDetails:
                        break;
                    case ServerPacketType.TutorialHint:
                        break;
                    case ServerPacketType.CyclopediaMapData:
                        break;
                    case ServerPacketType.DailyRewardCollectionState:
                        break;
                    case ServerPacketType.CreditBalance:
                        break;
                    case ServerPacketType.IngameShopError:
                        break;
                    case ServerPacketType.RequestPurchaseData:
                        break;
                    case ServerPacketType.OpenRewardWall:
                        break;
                    case ServerPacketType.CloseRewardWall:
                        break;
                    case ServerPacketType.DailyRewardBasic:
                        break;
                    case ServerPacketType.DailyRewardHistory:
                        break;
                    case ServerPacketType.PreyFreeListRerollAvailability:
                        break;
                    case ServerPacketType.PreyTimeLeft:
                        break;
                    case ServerPacketType.PreyData:
                        break;
                    case ServerPacketType.PreyRerollPrice:
                        break;
                    case ServerPacketType.OfferDescription:
                        break;
                    case ServerPacketType.ImbuingDialogRefresh:
                        break;
                    case ServerPacketType.CloseImbuingDialog:
                        break;
                    case ServerPacketType.ShowMessageDialog:
                        break;
                    case ServerPacketType.RequestResourceBalance:
                        break;
                    case ServerPacketType.TibiaTime:
                        break;
                    case ServerPacketType.QuestLog:
                        break;
                    case ServerPacketType.QuestLine:
                        break;
                    case ServerPacketType.UpdatingShopBalance:
                        break;
                    case ServerPacketType.ChannelEvent:
                        break;
                    case ServerPacketType.ObjectInfo:
                        break;
                    case ServerPacketType.PlayerInventory:
                        break;
                    case ServerPacketType.MarketEnter:
                        break;
                    case ServerPacketType.MarketLeave:
                        break;
                    case ServerPacketType.MarketDetail:
                        break;
                    case ServerPacketType.MarketBrowse:
                        break;
                    case ServerPacketType.ShowModalDialog:
                        break;
                    case ServerPacketType.StoreCategories:
                        break;
                    case ServerPacketType.StoreOffers:
                        break;
                    case ServerPacketType.TransactionHistory:
                        break;
                    case ServerPacketType.StoreSuccess:
                        break;
                    default:
                        break;
                }
            }
        }
    }
}