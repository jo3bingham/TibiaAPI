using System;

using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network
{
    public class ClientPacket : Packet
    {
        public ClientPacketType PacketType { get; set; } = ClientPacketType.Invalid;

        public static ClientPacket CreateInstance(Client client, ClientPacketType type)
        {
            switch (type)
            {
                case ClientPacketType.Invalid:
                    return new ClientPacket();
                case ClientPacketType.Login:
                    return new ClientPackets.Login(client);
                case ClientPacketType.SecondaryLogin:
                    return new ClientPackets.SecondaryLogin(client);
                case ClientPacketType.EnterWorld:
                    return new ClientPackets.EnterWorld(client);
                case ClientPacketType.QuitGame:
                    return new ClientPackets.QuitGame(client);
                case ClientPacketType.ConnectionPingBack:
                    return new ClientPackets.ConnectionPingBack(client);
                case ClientPacketType.Ping:
                    return new ClientPackets.Ping(client);
                case ClientPacketType.PingBack:
                    return new ClientPackets.PingBack(client);
                case ClientPacketType.PerformanceMetrics:
                    return new ClientPackets.PerformanceMetrics(client);
                case ClientPacketType.StashAction:
                    return new ClientPackets.StashAction(client);
                case ClientPacketType.DepotSearchRetrieve:
                    return new ClientPackets.DepotSearchRetrieve(client);
                case ClientPacketType.TrackBestiaryRace:
                    return new ClientPackets.TrackBestiaryRace(client);
                case ClientPacketType.PartyHuntAnalyser:
                    return new ClientPackets.PartyHuntAnalyser(client);
                case ClientPacketType.TeamFinderAssembleTeam:
                    return new ClientPackets.TeamFinderAssembleTeam(client);
                case ClientPacketType.TeamFinderJoinTeam:
                    return new ClientPackets.TeamFinderJoinTeam(client);
                case ClientPacketType.ClientCheck:
                    return new ClientPackets.ClientCheck(client);
                case ClientPacketType.GoPath:
                    return new ClientPackets.GoPath(client);
                case ClientPacketType.GoNorth:
                    return new ClientPackets.GoNorth(client);
                case ClientPacketType.GoEast:
                    return new ClientPackets.GoEast(client);
                case ClientPacketType.GoSouth:
                    return new ClientPackets.GoSouth(client);
                case ClientPacketType.GoWest:
                    return new ClientPackets.GoWest(client);
                case ClientPacketType.Stop:
                    return new ClientPackets.Stop(client);
                case ClientPacketType.GoNorthEast:
                    return new ClientPackets.GoNorthEast(client);
                case ClientPacketType.GoSouthEast:
                    return new ClientPackets.GoSouthEast(client);
                case ClientPacketType.GoSouthWest:
                    return new ClientPackets.GoSouthWest(client);
                case ClientPacketType.GoNorthWest:
                    return new ClientPackets.GoNorthWest(client);
                case ClientPacketType.RotateNorth:
                    return new ClientPackets.RotateNorth(client);
                case ClientPacketType.RotateEast:
                    return new ClientPackets.RotateEast(client);
                case ClientPacketType.RotateSouth:
                    return new ClientPackets.RotateSouth(client);
                case ClientPacketType.RotateWest:
                    return new ClientPackets.RotateWest(client);
                case ClientPacketType.Teleport:
                    return new ClientPackets.Teleport(client);
                case ClientPacketType.CharacterTradeConfigurationAction:
                    return new ClientPackets.CharacterTradeConfigurationAction(client);
                case ClientPacketType.EquipObject:
                    return new ClientPackets.EquipObject(client);
                case ClientPacketType.MoveObject:
                    return new ClientPackets.MoveObject(client);
                case ClientPacketType.LookNpcTrade:
                    return new ClientPackets.LookNpcTrade(client);
                case ClientPacketType.BuyObject:
                    return new ClientPackets.BuyObject(client);
                case ClientPacketType.SellObject:
                    return new ClientPackets.SellObject(client);
                case ClientPacketType.CloseNpcTrade:
                    return new ClientPackets.CloseNpcChannel(client);
                case ClientPacketType.TradeObject:
                    return new ClientPackets.TradeObject(client);
                case ClientPacketType.LookTrade:
                    return new ClientPackets.LookTrade(client);
                case ClientPacketType.AcceptTrade:
                    return new ClientPackets.AcceptTrade(client);
                case ClientPacketType.RejectTrade:
                    return new ClientPackets.RejectTrade(client);
                case ClientPacketType.FriendSystemAction:
                    return new ClientPackets.FriendSystemAction(client);
                case ClientPacketType.UseObject:
                    return new ClientPackets.UseObject(client);
                case ClientPacketType.UseTwoObjects:
                    return new ClientPackets.UseTwoObjects(client);
                case ClientPacketType.UseOnCreature:
                    return new ClientPackets.UseOnCreature(client);
                case ClientPacketType.TurnObject:
                    return new ClientPackets.TurnObject(client);
                case ClientPacketType.CloseContainer:
                    return new ClientPackets.CloseContainer(client);
                case ClientPacketType.UpContainer:
                    return new ClientPackets.UpContainer(client);
                case ClientPacketType.EditText:
                    return new ClientPackets.EditText(client);
                case ClientPacketType.EditList:
                    return new ClientPackets.EditList(client);
                case ClientPacketType.ToggleWrapState:
                    return new ClientPackets.ToggleWrapState(client);
                case ClientPacketType.Look:
                    return new ClientPackets.Look(client);
                case ClientPacketType.LookAtCreature:
                    return new ClientPackets.LookAtCreature(client);
                case ClientPacketType.JoinAggression:
                    return new ClientPackets.JoinAggression(client);
                case ClientPacketType.QuickLoot:
                    return new ClientPackets.QuickLoot(client);
                case ClientPacketType.LootContainer:
                    return new ClientPackets.LootContainer(client);
                case ClientPacketType.QuickLootBlackWhitelist:
                    return new ClientPackets.QuickLootBlackWhitelist(client);
                case ClientPacketType.OpenDepotSearch:
                    return new ClientPackets.OpenDepotSearch(client);
                case ClientPacketType.CloseDepotSearch:
                    return new ClientPackets.CloseDepotSearch(client);
                case ClientPacketType.DepotSearchType:
                    return new ClientPackets.DepotSearchType(client);
                case ClientPacketType.OpenParentContainer:
                    return new ClientPackets.OpenParentContainer(client);
                case ClientPacketType.Talk:
                    return new ClientPackets.Talk(client);
                case ClientPacketType.GetChannels:
                    return new ClientPackets.GetChannels(client);
                case ClientPacketType.JoinChannel:
                    return new ClientPackets.JoinChannel(client);
                case ClientPacketType.LeaveChannel:
                    return new ClientPackets.LeaveChannel(client);
                case ClientPacketType.PrivateChannel:
                    return new ClientPackets.PrivateChannel(client);
                case ClientPacketType.GuildMessage:
                    return new ClientPackets.GuildMessage(client);
                case ClientPacketType.EditGuildMessage:
                    return new ClientPackets.EditGuildMessage(client);
                case ClientPacketType.CloseNpcChannel:
                    return new ClientPackets.CloseNpcChannel(client);
                case ClientPacketType.SetTactics:
                    return new ClientPackets.SetTactics(client);
                case ClientPacketType.Attack:
                    return new ClientPackets.Attack(client);
                case ClientPacketType.Follow:
                    return new ClientPackets.Follow(client);
                case ClientPacketType.InviteToParty:
                    return new ClientPackets.InviteToParty(client);
                case ClientPacketType.JoinParty:
                    return new ClientPackets.JoinParty(client);
                case ClientPacketType.RevokeInvitation:
                    return new ClientPackets.RevokeInvitation(client);
                case ClientPacketType.PassLeadership:
                    return new ClientPackets.PassLeadership(client);
                case ClientPacketType.LeaveParty:
                    return new ClientPackets.LeaveParty(client);
                case ClientPacketType.ShareExperience:
                    return new ClientPackets.ShareExperience(client);
                case ClientPacketType.DisbandParty:
                    return new ClientPackets.DisbandParty(client);
                case ClientPacketType.OpenChannel:
                    return new ClientPackets.OpenChannel(client);
                case ClientPacketType.InviteToChannel:
                    return new ClientPackets.InviteToChannel(client);
                case ClientPacketType.ExcludeFromChannel:
                    return new ClientPackets.ExcludeFromChannel(client);
                case ClientPacketType.CyclopediaHouseAction:
                    return new ClientPackets.CyclopediaHouseAction(client);
                case ClientPacketType.Highscores:
                    return new ClientPackets.Highscores(client);
                case ClientPacketType.PreyHuntingTaskAction:
                    return new ClientPackets.PreyHuntingTaskAction(client);
                case ClientPacketType.Cancel:
                    return new ClientPackets.Cancel(client);
                case ClientPacketType.ClaimTournamentReward:
                    return new ClientPackets.ClaimTournamentReward(client);
                case ClientPacketType.TournamentInformation:
                    return new ClientPackets.TournamentInformation(client);
                case ClientPacketType.SubscribeToUpdates:
                    return new ClientPackets.SubscribeToUpdates(client);
                case ClientPacketType.TournamentLeaderboard:
                    return new ClientPackets.TournamentLeaderboard(client);
                case ClientPacketType.TournamentTicketAction:
                    return new ClientPackets.TournamentTicketAction(client);
                case ClientPacketType.GetTransactionDetails:
                    return new ClientPackets.GetTransactionDetails(client);
                case ClientPacketType.UpdateExivaOptions:
                    return new ClientPackets.UpdateExivaOptions(client);
                case ClientPacketType.BrowseField:
                    return new ClientPackets.BrowseField(client);
                case ClientPacketType.SeekInContainer:
                    return new ClientPackets.SeekInContainer(client);
                case ClientPacketType.InspectObject:
                    return new ClientPackets.InspectObject(client);
                case ClientPacketType.InspectPlayer:
                    return new ClientPackets.InspectPlayer(client);
                case ClientPacketType.BlessingsDialog:
                    return new ClientPackets.BlessingsDialog(client);
                case ClientPacketType.TrackQuestflags:
                    return new ClientPackets.TrackQuestflags(client);
                case ClientPacketType.MarketStatistics:
                    return new ClientPackets.MarketStatistics(client);
                case ClientPacketType.GetOutfit:
                    return new ClientPackets.GetOutfit(client);
                case ClientPacketType.SetOutfit:
                    return new ClientPackets.SetOutfit(client);
                case ClientPacketType.Mount:
                    return new ClientPackets.Mount(client);
                case ClientPacketType.ApplyImbuement:
                    return new ClientPackets.ApplyImbuement(client);
                case ClientPacketType.ApplyClearingCharm:
                    return new ClientPackets.ApplyClearingCharm(client);
                case ClientPacketType.ClosedImbuingDialog:
                    return new ClientPackets.ClosedImbuingDialog(client);
                case ClientPacketType.OpenRewardWall:
                    return new ClientPackets.OpenRewardWall(client);
                case ClientPacketType.DailyRewardHistory:
                    return new ClientPackets.DailyRewardHistory(client);
                case ClientPacketType.CollectDailyReward:
                    return new ClientPackets.CollectDailyReward(client);
                case ClientPacketType.CyclopediaMapAction:
                    return new ClientPackets.CyclopediaMapAction(client);
                case ClientPacketType.AddBuddy:
                    return new ClientPackets.AddBuddy(client);
                case ClientPacketType.RemoveBuddy:
                    return new ClientPackets.RemoveBuddy(client);
                case ClientPacketType.EditBuddy:
                    return new ClientPackets.EditBuddy(client);
                case ClientPacketType.BuddyGroup:
                    return new ClientPackets.BuddyGroup(client);
                case ClientPacketType.MarkGameNewsAsRead:
                    return new ClientPackets.MarkGameNewsAsRead(client);
                case ClientPacketType.OpenMonsterCyclopedia:
                    return new ClientPackets.OpenMonsterCyclopedia(client);
                case ClientPacketType.OpenMonsterCyclopediaMonsters:
                    return new ClientPackets.OpenMonsterCyclopediaMonsters(client);
                case ClientPacketType.OpenMonsterCyclopediaRace:
                    return new ClientPackets.OpenMonsterCyclopediaRace(client);
                case ClientPacketType.MonsterBonusEffectAction:
                    return new ClientPackets.MonsterBonusEffectAction(client);
                case ClientPacketType.OpenCyclopediaCharacterInfo:
                    return new ClientPackets.OpenCyclopediaCharacterInfo(client);
                case ClientPacketType.BugReport:
                    return new ClientPackets.BugReport(client);
                case ClientPacketType.ThankYou:
                    return new ClientPackets.ThankYou(client);
                case ClientPacketType.GetOfferDescription:
                    return new ClientPackets.GetOfferDescription(client);
                case ClientPacketType.StoreEvent:
                    return new ClientPackets.StoreEvent(client);
                case ClientPacketType.FeatureEvent:
                    return new ClientPackets.FeatureEvent(client);
                case ClientPacketType.PreyAction:
                    return new ClientPackets.PreyAction(client);
                case ClientPacketType.SetHirelingName:
                    return new ClientPackets.SetHirelingName(client);
                case ClientPacketType.RequestResourceBalance:
                    return new ClientPackets.RequestResourceBalance(client);
                case ClientPacketType.Greet:
                    return new ClientPackets.Greet(client);
                case ClientPacketType.TransferCurrency:
                    return new ClientPackets.TransferCurrency(client);
                case ClientPacketType.GetQuestLog:
                    return new ClientPackets.GetQuestLog(client);
                case ClientPacketType.GetQuestLine:
                    return new ClientPackets.GetQuestLine(client);
                case ClientPacketType.RuleViolationReport:
                    return new ClientPackets.RuleViolationReport(client);
                case ClientPacketType.GetObjectInfo:
                    return new ClientPackets.GetObjectInfo(client);
                case ClientPacketType.MarketLeave:
                    return new ClientPackets.MarketLeave(client);
                case ClientPacketType.MarketBrowse:
                    return new ClientPackets.MarketBrowse(client);
                case ClientPacketType.MarketCreate:
                    return new ClientPackets.MarketCreate(client);
                case ClientPacketType.MarketCancel:
                    return new ClientPackets.MarketCancel(client);
                case ClientPacketType.MarketAccept:
                    return new ClientPackets.MarketAccept(client);
                case ClientPacketType.AnswerModalDialog:
                    return new ClientPackets.AnswerModalDialog(client);
                case ClientPacketType.OpenIngameShop:
                    return new ClientPackets.OpenIngameShop(client);
                case ClientPacketType.RequestShopOffers:
                    return new ClientPackets.RequestShopOffers(client);
                case ClientPacketType.BuyIngameShopOffer:
                    return new ClientPackets.BuyIngameShopOffer(client);
                case ClientPacketType.OpenTransactionHistory:
                    return new ClientPackets.OpenTransactionHistory(client);
                case ClientPacketType.GetTransactionHistory:
                    return new ClientPackets.GetTransactionHistory(client);
                default:
                    throw new Exception($"[ClientPacket.Create] Invalid packet type: {type}");
            }
        }
    }
}
