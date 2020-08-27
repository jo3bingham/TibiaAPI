using System;

using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network
{
    public class ServerPacket : Packet
    {
        public ServerPacketType PacketType { get; set; } = ServerPacketType.Invalid;

        public static ServerPacket CreateInstance(Client client, ServerPacketType type)
        {
            switch (type)
            {
                case ServerPacketType.Invalid:
                    return new ServerPacket();
                case ServerPacketType.CreatureData:
                    return new ServerPackets.CreatureData(client);
                case ServerPacketType.SessionDumpStart:
                    return new ServerPackets.SessionDumpStart(client);
                case ServerPacketType.PendingStateEntered:
                    return new ServerPackets.PendingStateEntered(client);
                case ServerPacketType.ReadyForSecondaryConnection:
                    return new ServerPackets.ReadyForSecondaryConnection(client);
                case ServerPacketType.WorldEntered:
                    return new ServerPackets.WorldEntered(client);
                case ServerPacketType.LoginError:
                    return new ServerPackets.LoginError(client);
                case ServerPacketType.LoginAdvice:
                    return new ServerPackets.LoginAdvice(client);
                case ServerPacketType.LoginWait:
                    return new ServerPackets.LoginWait(client);
                case ServerPacketType.LoginSuccess:
                    return new ServerPackets.LoginSuccess(client);
                case ServerPacketType.StoreButtonIndicators:
                    return new ServerPackets.StoreButtonIndicators(client);
                case ServerPacketType.Ping:
                    return new ServerPackets.Ping(client);
                case ServerPacketType.PingBack:
                    return new ServerPackets.PingBack(client);
                case ServerPacketType.LoginChallenge:
                    return new ServerPackets.LoginChallenge(client);
                case ServerPacketType.Dead:
                    return new ServerPackets.Dead(client);
                case ServerPacketType.Stash:
                    return new ServerPackets.Stash(client);
                case ServerPacketType.SpecialContainersAvailable:
                    if (client.VersionNumber >= 12400000)
                    {
                        return new ServerPackets.SpecialContainersAvailable(client);
                    }
                    else
                    {
                        return new ServerPackets.DepotTileState(client);
                    }
                case ServerPacketType.PartyHuntAnalyser:
                    return new ServerPackets.PartyHuntAnalyser(client);
                case ServerPacketType.TeamFinderTeamLeader:
                    return new ServerPackets.TeamFinderTeamLeader(client);
                case ServerPacketType.TeamFinderTeamMember:
                    return new ServerPackets.TeamFinderTeamMember(client);
                case ServerPacketType.ClientCheck:
                    return new ServerPackets.ClientCheck(client);
                case ServerPacketType.FullMap:
                    return new ServerPackets.FullMap(client);
                case ServerPacketType.TopRow:
                    return new ServerPackets.TopRow(client);
                case ServerPacketType.RightColumn:
                    return new ServerPackets.RightColumn(client);
                case ServerPacketType.BottomRow:
                    return new ServerPackets.BottomRow(client);
                case ServerPacketType.LeftColumn:
                    return new ServerPackets.LeftColumn(client);
                case ServerPacketType.FieldData:
                    return new ServerPackets.FieldData(client);
                case ServerPacketType.CreateOnMap:
                    return new ServerPackets.CreateOnMap(client);
                case ServerPacketType.ChangeOnMap:
                    return new ServerPackets.ChangeOnMap(client);
                case ServerPacketType.DeleteOnMap:
                    return new ServerPackets.DeleteOnMap(client);
                case ServerPacketType.MoveCreature:
                    return new ServerPackets.MoveCreature(client);
                case ServerPacketType.Container:
                    return new ServerPackets.Container(client);
                case ServerPacketType.CloseContainer:
                    return new ServerPackets.CloseContainer(client);
                case ServerPacketType.CreateInContainer:
                    return new ServerPackets.CreateInContainer(client);
                case ServerPacketType.ChangeInContainer:
                    return new ServerPackets.ChangeInContainer(client);
                case ServerPacketType.DeleteInContainer:
                    return new ServerPackets.DeleteInContainer(client);
                case ServerPacketType.FriendSystemData:
                    return new ServerPackets.FriendSystemData(client);
                case ServerPacketType.ScreenshotEvent:
                    return new ServerPackets.ScreenshotEvent(client);
                case ServerPacketType.InspectionList:
                    return new ServerPackets.InspectionList(client);
                case ServerPacketType.InspectionState:
                    return new ServerPackets.InspectionState(client);
                case ServerPacketType.SetInventory:
                    return new ServerPackets.SetInventory(client);
                case ServerPacketType.DeleteInventory:
                    return new ServerPackets.DeleteInventory(client);
                case ServerPacketType.NpcOffer:
                    return new ServerPackets.NpcOffer(client);
                case ServerPacketType.PlayerGoods:
                    return new ServerPackets.PlayerGoods(client);
                case ServerPacketType.CloseNpcTrade:
                    return new ServerPackets.CloseNpcTrade(client);
                case ServerPacketType.OwnOffer:
                    return new ServerPackets.OwnOffer(client);
                case ServerPacketType.CounterOffer:
                    return new ServerPackets.CounterOffer(client);
                case ServerPacketType.CloseTrade:
                    return new ServerPackets.CloseTrade(client);
                case ServerPacketType.CharacterTradeConfiguration:
                    return new ServerPackets.CharacterTradeConfiguration(client);
                case ServerPacketType.Ambiente:
                    return new ServerPackets.Ambiente(client);
                case ServerPacketType.GraphicalEffects:
                    return new ServerPackets.GraphicalEffects(client);
                case ServerPacketType.RemoveGraphicalEffect:
                    return new ServerPackets.RemoveGraphicalEffect(client);
                case ServerPacketType.MissileEffect:
                    return new ServerPackets.MissileEffect(client);
                case ServerPacketType.Trappers:
                    return new ServerPackets.Trappers(client);
                case ServerPacketType.CreatureUpdate:
                    return new ServerPackets.CreatureUpdate(client);
                case ServerPacketType.CreatureHealth:
                    return new ServerPackets.CreatureHealth(client);
                case ServerPacketType.CreatureLight:
                    return new ServerPackets.CreatureLight(client);
                case ServerPacketType.CreatureOutfit:
                    return new ServerPackets.CreatureOutfit(client);
                case ServerPacketType.CreatureSpeed:
                    return new ServerPackets.CreatureSpeed(client);
                case ServerPacketType.CreatureSkull:
                    return new ServerPackets.CreatureSkull(client);
                case ServerPacketType.CreatureParty:
                    return new ServerPackets.CreatureParty(client);
                case ServerPacketType.CreatureUnpass:
                    return new ServerPackets.CreatureUnpass(client);
                case ServerPacketType.CreatureMarks:
                    return new ServerPackets.CreatureMarks(client);
                case ServerPacketType.DepotSearchResults:
                    return new ServerPackets.DepotSearchResults(client);
                case ServerPacketType.CreatureType:
                    return new ServerPackets.CreatureType(client);
                case ServerPacketType.EditText:
                    return new ServerPackets.EditText(client);
                case ServerPacketType.EditList:
                    return new ServerPackets.EditList(client);
                case ServerPacketType.ShowGameNews:
                    return new ServerPackets.ShowGameNews(client);
                case ServerPacketType.DepotSearchDetailList:
                    return new ServerPackets.DepotSearchDetailList(client);
                case ServerPacketType.CloseDepotSearch:
                    return new ServerPackets.CloseDepotSearch(client);
                case ServerPacketType.BlessingsDialog:
                    return new ServerPackets.BlessingsDialog(client);
                case ServerPacketType.Blessings:
                    return new ServerPackets.Blessings(client);
                case ServerPacketType.SwitchPreset:
                    return new ServerPackets.SwitchPreset(client);
                case ServerPacketType.PremiumTrigger:
                    return new ServerPackets.PremiumTrigger(client);
                case ServerPacketType.PlayerDataBasic:
                    return new ServerPackets.PlayerDataBasic(client);
                case ServerPacketType.PlayerDataCurrent:
                    return new ServerPackets.PlayerDataCurrent(client);
                case ServerPacketType.PlayerSkills:
                    return new ServerPackets.PlayerSkills(client);
                case ServerPacketType.PlayerState:
                    return new ServerPackets.PlayerState(client);
                case ServerPacketType.ClearTarget:
                    return new ServerPackets.ClearTarget(client);
                case ServerPacketType.SpellDelay:
                    return new ServerPackets.SpellDelay(client);
                case ServerPacketType.SpellGroupDelay:
                    return new ServerPackets.SpellGroupDelay(client);
                case ServerPacketType.MultiUseDelay:
                    return new ServerPackets.MultiUseDelay(client);
                case ServerPacketType.SetTactics:
                    return new ServerPackets.SetTactics(client);
                case ServerPacketType.SetStoreButtonDeeplink:
                    return new ServerPackets.SetStoreButtonDeeplink(client);
                case ServerPacketType.RestingAreaState:
                    return new ServerPackets.RestingAreaState(client);
                case ServerPacketType.Talk:
                    return new ServerPackets.Talk(client);
                case ServerPacketType.Channels:
                    return new ServerPackets.Channels(client);
                case ServerPacketType.OpenChannel:
                    return new ServerPackets.OpenChannel(client);
                case ServerPacketType.PrivateChannel:
                    return new ServerPackets.PrivateChannel(client);
                case ServerPacketType.EditGuildMessage:
                    return new ServerPackets.EditGuildMessage(client);
                case ServerPacketType.Highscores:
                    return new ServerPackets.Highscores(client);
                case ServerPacketType.OpenOwnChannel:
                    return new ServerPackets.OpenOwnChannel(client);
                case ServerPacketType.CloseChannel:
                    return new ServerPackets.CloseChannel(client);
                case ServerPacketType.Message:
                    return new ServerPackets.Message(client);
                case ServerPacketType.SnapBack:
                    return new ServerPackets.SnapBack(client);
                case ServerPacketType.Wait:
                    return new ServerPackets.Wait(client);
                case ServerPacketType.UnjustifiedPoints:
                    return new ServerPackets.UnjustifiedPoints(client);
                case ServerPacketType.PvpSituations:
                    return new ServerPackets.PvpSituations(client);
                case ServerPacketType.BestiaryTracker:
                    return new ServerPackets.BestiaryTracker(client);
                case ServerPacketType.PreyHuntingTaskBaseData:
                    return new ServerPackets.PreyHuntingTaskBaseData(client);
                case ServerPacketType.PreyHuntingTaskData:
                    return new ServerPackets.PreyHuntingTaskData(client);
                case ServerPacketType.TopFloor:
                    return new ServerPackets.TopFloor(client);
                case ServerPacketType.BottomFloor:
                    return new ServerPackets.BottomFloor(client);
                case ServerPacketType.UpdateLootContainers:
                    return new ServerPackets.UpdateLootContainers(client);
                case ServerPacketType.PlayerDataTournament:
                    return new ServerPackets.PlayerDataTournament(client);
                case ServerPacketType.CyclopediaHouseActionResult:
                    return new ServerPackets.CyclopediaHouseActionResult(client);
                case ServerPacketType.TournamentInformation:
                    return new ServerPackets.TournamentInformation(client);
                case ServerPacketType.TournamentLeaderboard:
                    return new ServerPackets.TournamentLeaderboard(client);
                case ServerPacketType.CyclopediaStaticHouseData:
                    return new ServerPackets.CyclopediaStaticHouseData(client);
                case ServerPacketType.CyclopediaCurrentHouseData:
                    return new ServerPackets.CyclopediaCurrentHouseData(client);
                case ServerPacketType.Outfit:
                    return new ServerPackets.Outfit(client);
                case ServerPacketType.ExivaSuppressed:
                    return new ServerPackets.ExivaSuppressed(client);
                case ServerPacketType.UpdateExivaOptions:
                    return new ServerPackets.UpdateExivaOptions(client);
                case ServerPacketType.TransactionDetails:
                    return new ServerPackets.TransactionDetails(client);
                case ServerPacketType.ImpactTracking:
                    return new ServerPackets.ImpactTracking(client);
                case ServerPacketType.MarketStatistics:
                    return new ServerPackets.MarketStatistics(client);
                case ServerPacketType.ItemWasted:
                    return new ServerPackets.ItemWasted(client);
                case ServerPacketType.ItemLooted:
                    return new ServerPackets.ItemLooted(client);
                case ServerPacketType.TrackQuestflags:
                    return new ServerPackets.TrackQuestflags(client);
                case ServerPacketType.KillTracking:
                    return new ServerPackets.KillTracking(client);
                case ServerPacketType.BuddyData:
                    return new ServerPackets.BuddyData(client);
                case ServerPacketType.BuddyStatusChange:
                    return new ServerPackets.BuddyStatusChange(client);
                case ServerPacketType.BuddyGroupData:
                    return new ServerPackets.BuddyGroupData(client);
                case ServerPacketType.MonsterCyclopedia:
                    return new ServerPackets.MonsterCyclopedia(client);
                case ServerPacketType.MonsterCyclopediaMonsters:
                    return new ServerPackets.MonsterCyclopediaMonsters(client);
                case ServerPacketType.MonsterCyclopediaRace:
                    return new ServerPackets.MonsterCyclopediaRace(client);
                case ServerPacketType.MonsterCyclopediaBonusEffects:
                    return new ServerPackets.MonsterCyclopediaBonusEffects(client);
                case ServerPacketType.MonsterCyclopediaNewDetails:
                    return new ServerPackets.MonsterCyclopediaNewDetails(client);
                case ServerPacketType.CyclopediaCharacterInfo:
                    return new ServerPackets.CyclopediaCharacterInfo(client);
                case ServerPacketType.HirelingNameChange:
                    return new ServerPackets.HirelingNameChange(client);
                case ServerPacketType.TutorialHint:
                    return new ServerPackets.TutorialHint(client);
                case ServerPacketType.CyclopediaMapData:
                    return new ServerPackets.CyclopediaMapData(client);
                case ServerPacketType.DailyRewardCollectionState:
                    return new ServerPackets.DailyRewardCollectionState(client);
                case ServerPacketType.CreditBalance:
                    return new ServerPackets.CreditBalance(client);
                case ServerPacketType.IngameShopError:
                    return new ServerPackets.IngameShopError(client);
                case ServerPacketType.RequestPurchaseData:
                    return new ServerPackets.RequestPurchaseData(client);
                case ServerPacketType.OpenRewardWall:
                    return new ServerPackets.OpenRewardWall(client);
                case ServerPacketType.CloseRewardWall:
                    return new ServerPackets.CloseRewardWall(client);
                case ServerPacketType.DailyRewardBasic:
                    return new ServerPackets.DailyRewardBasic(client);
                case ServerPacketType.DailyRewardHistory:
                    return new ServerPackets.DailyRewardHistory(client);
                case ServerPacketType.PreyFreeListRerollAvailability:
                    return new ServerPackets.PreyFreeListRerollAvailability(client);
                case ServerPacketType.PreyTimeLeft:
                    return new ServerPackets.PreyTimeLeft(client);
                case ServerPacketType.PreyData:
                    return new ServerPackets.PreyData(client);
                case ServerPacketType.PreyPrices:
                    return new ServerPackets.PreyPrices(client);
                case ServerPacketType.OfferDescription:
                    return new ServerPackets.OfferDescription(client);
                case ServerPacketType.ImbuingDialogRefresh:
                    return new ServerPackets.ImbuingDialogRefresh(client);
                case ServerPacketType.CloseImbuingDialog:
                    return new ServerPackets.CloseImbuingDialog(client);
                case ServerPacketType.ShowMessageDialog:
                    return new ServerPackets.ShowMessageDialog(client);
                case ServerPacketType.RequestResourceBalance:
                    return new ServerPackets.RequestResourceBalance(client);
                case ServerPacketType.TibiaTime:
                    return new ServerPackets.TibiaTime(client);
                case ServerPacketType.QuestLog:
                    return new ServerPackets.QuestLog(client);
                case ServerPacketType.QuestLine:
                    return new ServerPackets.QuestLine(client);
                case ServerPacketType.UpdatingShopBalance:
                    return new ServerPackets.UpdatingShopBalance(client);
                case ServerPacketType.ChannelEvent:
                    return new ServerPackets.ChannelEvent(client);
                case ServerPacketType.ObjectInfo:
                    return new ServerPackets.ObjectInfo(client);
                case ServerPacketType.PlayerInventory:
                    return new ServerPackets.PlayerInventory(client);
                case ServerPacketType.MarketEnter:
                    return new ServerPackets.MarketEnter(client);
                case ServerPacketType.MarketLeave:
                    return new ServerPackets.MarketLeave(client);
                case ServerPacketType.MarketDetail:
                    return new ServerPackets.MarketDetail(client);
                case ServerPacketType.MarketBrowse:
                    return new ServerPackets.MarketBrowse(client);
                case ServerPacketType.ShowModalDialog:
                    return new ServerPackets.ShowModalDialog(client);
                case ServerPacketType.StoreCategories:
                    return new ServerPackets.StoreCategories(client);
                case ServerPacketType.StoreOffers:
                    return new ServerPackets.StoreOffers(client);
                case ServerPacketType.TransactionHistory:
                    return new ServerPackets.TransactionHistory(client);
                case ServerPacketType.StoreSuccess:
                    return new ServerPackets.StoreSuccess(client);
                default:
                    throw new Exception($"[ServerPacket.Create] Invalid packet type: {type}");
            }
        }
    }
}
