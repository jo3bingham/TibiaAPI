using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using OXGaming.TibiaAPI.Constants;
using OXGaming.TibiaAPI.Utilities;

namespace OXGaming.TibiaAPI.Network
{
    public class Communication
    {
        public delegate bool ReceivedPacketEventHandler(Packet packet);

        public event ReceivedPacketEventHandler OnReceivedClientLoginPacket;
        public event ReceivedPacketEventHandler OnReceivedClientSecondaryLoginPacket;
        public event ReceivedPacketEventHandler OnReceivedClientEnterWorldPacket;
        public event ReceivedPacketEventHandler OnReceivedClientQuitGamePacket;
        public event ReceivedPacketEventHandler OnReceivedClientConnectionPingBack;
        public event ReceivedPacketEventHandler OnReceivedClientPingPacket;
        public event ReceivedPacketEventHandler OnReceivedClientPingBackPacket;
        public event ReceivedPacketEventHandler OnReceivedClientPerformanceMetricsPacket;
        public event ReceivedPacketEventHandler OnReceivedClientStashActionPacket;
        public event ReceivedPacketEventHandler OnReceivedClientDepotSearchRetrievePacket;
        public event ReceivedPacketEventHandler OnReceivedClientTrackBestiaryRacePacket;
        public event ReceivedPacketEventHandler OnReceivedClientPartyHuntAnalyserPacket;
        public event ReceivedPacketEventHandler OnReceivedClientTeamFinderAssembleTeamPacket;
        public event ReceivedPacketEventHandler OnReceivedClientTeamFinderJoinTeamPacket;
        public event ReceivedPacketEventHandler OnReceivedClientClientCheckPacket;
        public event ReceivedPacketEventHandler OnReceivedClientGoPathPacket;
        public event ReceivedPacketEventHandler OnReceivedClientGoNorthPacket;
        public event ReceivedPacketEventHandler OnReceivedClientGoEastPacket;
        public event ReceivedPacketEventHandler OnReceivedClientGoSouthPacket;
        public event ReceivedPacketEventHandler OnReceivedClientGoWestPacket;
        public event ReceivedPacketEventHandler OnReceivedClientStopPacket;
        public event ReceivedPacketEventHandler OnReceivedClientGoNorthEastPacket;
        public event ReceivedPacketEventHandler OnReceivedClientGoSouthEastPacket;
        public event ReceivedPacketEventHandler OnReceivedClientGoSouthWestPacket;
        public event ReceivedPacketEventHandler OnReceivedClientGoNorthWestPacket;
        public event ReceivedPacketEventHandler OnReceivedClientRotateNorthPacket;
        public event ReceivedPacketEventHandler OnReceivedClientRotateEastPacket;
        public event ReceivedPacketEventHandler OnReceivedClientRotateSouthPacket;
        public event ReceivedPacketEventHandler OnReceivedClientRotateWestPacket;
        public event ReceivedPacketEventHandler OnReceivedClientTeleportPacket;
        public event ReceivedPacketEventHandler OnReceivedClientCharacterTradeConfigurationActionPacket;
        public event ReceivedPacketEventHandler OnReceivedClientEquipObjectPacket;
        public event ReceivedPacketEventHandler OnReceivedClientMoveObjectPacket;
        public event ReceivedPacketEventHandler OnReceivedClientLookNpcTradePacket;
        public event ReceivedPacketEventHandler OnReceivedClientBuyObjectPacket;
        public event ReceivedPacketEventHandler OnReceivedClientSellObjectPacket;
        public event ReceivedPacketEventHandler OnReceivedClientCloseNpcTradePacket;
        public event ReceivedPacketEventHandler OnReceivedClientTradeObjectPacket;
        public event ReceivedPacketEventHandler OnReceivedClientLookTradePacket;
        public event ReceivedPacketEventHandler OnReceivedClientAcceptTradePacket;
        public event ReceivedPacketEventHandler OnReceivedClientRejectTradePacket;
        public event ReceivedPacketEventHandler OnReceivedClientFriendSystemActionPacket;
        public event ReceivedPacketEventHandler OnReceivedClientUseObjectPacket;
        public event ReceivedPacketEventHandler OnReceivedClientUseTwoObjectsPacket;
        public event ReceivedPacketEventHandler OnReceivedClientUseOnCreaturePacket;
        public event ReceivedPacketEventHandler OnReceivedClientTurnObjectPacket;
        public event ReceivedPacketEventHandler OnReceivedClientCloseContainerPacket;
        public event ReceivedPacketEventHandler OnReceivedClientUpContainerPacket;
        public event ReceivedPacketEventHandler OnReceivedClientEditTextPacket;
        public event ReceivedPacketEventHandler OnReceivedClientEditListPacket;
        public event ReceivedPacketEventHandler OnReceivedClientToggleWarpStatePacket;
        public event ReceivedPacketEventHandler OnReceivedClientLookPacket;
        public event ReceivedPacketEventHandler OnReceivedClientLookAtCreaturePacket;
        public event ReceivedPacketEventHandler OnReceivedClientJoinAggressionPacket;
        public event ReceivedPacketEventHandler OnReceivedClientQuickLootPacket;
        public event ReceivedPacketEventHandler OnReceivedClientLootContainerPacket;
        public event ReceivedPacketEventHandler OnReceivedClientQuickLootBlackWhitelistPacket;
        public event ReceivedPacketEventHandler OnReceivedClientOpenDepotSearchPacket;
        public event ReceivedPacketEventHandler OnReceivedClientCloseDepotSearchPacket;
        public event ReceivedPacketEventHandler OnReceivedClientDepotSearchTypePacket;
        public event ReceivedPacketEventHandler OnReceivedClientOpenParentContainerPacket;
        public event ReceivedPacketEventHandler OnReceivedClientTalkPacket;
        public event ReceivedPacketEventHandler OnReceivedClientGetChannelsPacket;
        public event ReceivedPacketEventHandler OnReceivedClientJoinChannelPacket;
        public event ReceivedPacketEventHandler OnReceivedClientLeaveChannelPacket;
        public event ReceivedPacketEventHandler OnReceivedClientPrivateChannelPacket;
        public event ReceivedPacketEventHandler OnReceivedClientGuildMessagePacket;
        public event ReceivedPacketEventHandler OnReceivedClientEditGuildMessagePacket;
        public event ReceivedPacketEventHandler OnReceivedClientCloseNpcChannelPacket;
        public event ReceivedPacketEventHandler OnReceivedClientSetTacticsPacket;
        public event ReceivedPacketEventHandler OnReceivedClientAttackPacket;
        public event ReceivedPacketEventHandler OnReceivedClientFollowPacket;
        public event ReceivedPacketEventHandler OnReceivedClientInviteToPartyPacket;
        public event ReceivedPacketEventHandler OnReceivedClientJoinPartyPacket;
        public event ReceivedPacketEventHandler OnReceivedClientRevokeInvitationPacket;
        public event ReceivedPacketEventHandler OnReceivedClientPassLeadershipPacket;
        public event ReceivedPacketEventHandler OnReceivedClientLeavePartyPacket;
        public event ReceivedPacketEventHandler OnReceivedClientShareExperiencePacket;
        public event ReceivedPacketEventHandler OnReceivedClientDisbandPartyPacket;
        public event ReceivedPacketEventHandler OnReceivedClientOpenChannelPacket;
        public event ReceivedPacketEventHandler OnReceivedClientInviteToChannelPacket;
        public event ReceivedPacketEventHandler OnReceivedClientExcludeFromChannelPacket;
        public event ReceivedPacketEventHandler OnReceivedClientCyclopediaHouseActionPacket;
        public event ReceivedPacketEventHandler OnReceivedClientHighscoresPacket;
        public event ReceivedPacketEventHandler OnReceivedClientPreyHuntingTaskActionPacket;
        public event ReceivedPacketEventHandler OnReceivedClientCancelPacket;
        public event ReceivedPacketEventHandler OnReceivedClientClaimTournamentRewardPacket;
        public event ReceivedPacketEventHandler OnReceivedClientTournamentInformationPacket;
        public event ReceivedPacketEventHandler OnReceivedClientSubscribeToUpdatesPacket;
        public event ReceivedPacketEventHandler OnReceivedClientTournamentLeaderboardPacket;
        public event ReceivedPacketEventHandler OnReceivedClientTournamentTicketActionPacket;
        public event ReceivedPacketEventHandler OnReceivedClientGetTransactionDetailsPacket;
        public event ReceivedPacketEventHandler OnReceivedClientUpdateExivaOptionsPacket;
        public event ReceivedPacketEventHandler OnReceivedClientBrowseFieldPacket;
        public event ReceivedPacketEventHandler OnReceivedClientSeekInContainerPacket;
        public event ReceivedPacketEventHandler OnReceivedClientInspectObjectPacket;
        public event ReceivedPacketEventHandler OnReceivedClientInspectPlayerPacket;
        public event ReceivedPacketEventHandler OnReceivedClientBlessingsDialogPacket;
        public event ReceivedPacketEventHandler OnReceivedClientTrackQuestFlagsPacket;
        public event ReceivedPacketEventHandler OnReceivedClientMarketStatisticsPacket;
        public event ReceivedPacketEventHandler OnReceivedClientGetOutfitPacket;
        public event ReceivedPacketEventHandler OnReceivedClientSetOutfitPacket;
        public event ReceivedPacketEventHandler OnReceivedClientMountPacket;
        public event ReceivedPacketEventHandler OnReceivedClientApplyImbuementPacket;
        public event ReceivedPacketEventHandler OnReceivedClientApplyClearingCharmPacket;
        public event ReceivedPacketEventHandler OnReceivedClientClosedImbuingDialogPacket;
        public event ReceivedPacketEventHandler OnReceivedClientOpenRewardWallPacket;
        public event ReceivedPacketEventHandler OnReceivedClientDailyRewardHistoryPacket;
        public event ReceivedPacketEventHandler OnReceivedClientCollectDailyRewardPacket;
        public event ReceivedPacketEventHandler OnReceivedClientCyclopediaMapActionPacket;
        public event ReceivedPacketEventHandler OnReceivedClientAddBuddyPacket;
        public event ReceivedPacketEventHandler OnReceivedClientRemoveBuddyPacket;
        public event ReceivedPacketEventHandler OnReceivedClientEditBuddyPacket;
        public event ReceivedPacketEventHandler OnReceivedClientBuddyGroupPacket;
        public event ReceivedPacketEventHandler OnReceivedClientMarkGameNewsAsReadPacket;
        public event ReceivedPacketEventHandler OnReceivedClientOpenMonsterCyclopediaPacket;
        public event ReceivedPacketEventHandler OnReceivedClientOpenMonsterCyclopediaMonstersPacket;
        public event ReceivedPacketEventHandler OnReceivedClientOpenMonsterCyclopediaRacePacket;
        public event ReceivedPacketEventHandler OnReceivedClientMonsterBonusEffectActionPacket;
        public event ReceivedPacketEventHandler OnReceivedClientOpenCyclopediaCharacterInfoPacket;
        public event ReceivedPacketEventHandler OnReceivedClientBugReportPacket;
        public event ReceivedPacketEventHandler OnReceivedClientThankYouPacket;
        public event ReceivedPacketEventHandler OnReceivedClientGetOfferDescription;
        public event ReceivedPacketEventHandler OnReceivedClientStoreEventPacket;
        public event ReceivedPacketEventHandler OnReceivedClientFeatureEventPacket;
        public event ReceivedPacketEventHandler OnReceivedClientPreyActionPacket;
        public event ReceivedPacketEventHandler OnReceivedClientSetHirelingNamePacket;
        public event ReceivedPacketEventHandler OnReceivedClientRequestResourceBalancePacket;
        public event ReceivedPacketEventHandler OnReceivedClientGreetPacket;
        public event ReceivedPacketEventHandler OnReceivedClientTransferCurrencyPacket;
        public event ReceivedPacketEventHandler OnReceivedClientGetQuestLogPacket;
        public event ReceivedPacketEventHandler OnReceivedClientGetQuestLinePacket;
        public event ReceivedPacketEventHandler OnReceivedClientRuleViolationReportPacket;
        public event ReceivedPacketEventHandler OnReceivedClientGetObjectInfoPacket;
        public event ReceivedPacketEventHandler OnReceivedClientMarketLeavePacket;
        public event ReceivedPacketEventHandler OnReceivedClientMarketBrowsePacket;
        public event ReceivedPacketEventHandler OnReceivedClientMarketCreatePacket;
        public event ReceivedPacketEventHandler OnReceivedClientMarketCancelPacket;
        public event ReceivedPacketEventHandler OnReceivedClientMarketAcceptPacket;
        public event ReceivedPacketEventHandler OnReceivedClientAnswerModalDialogPacket;
        public event ReceivedPacketEventHandler OnReceivedClientOpenIngameShopPacket;
        public event ReceivedPacketEventHandler OnReceivedClientRequestShopOffersPacket;
        public event ReceivedPacketEventHandler OnReceivedClientBuyIngameShopOfferPacket;
        public event ReceivedPacketEventHandler OnReceivedClientOpenTransactionHistoryPacket;
        public event ReceivedPacketEventHandler OnReceivedClientGetTransactionHistoryPacket;

        public event ReceivedPacketEventHandler OnReceivedServerCreatureDataPacket;
        public event ReceivedPacketEventHandler OnReceivedServerSessionDumpStartPacket;
        public event ReceivedPacketEventHandler OnReceivedServerPendingStateEnteredPacket;
        public event ReceivedPacketEventHandler OnReceivedServerReadyForSecondaryConnectionPacket;
        public event ReceivedPacketEventHandler OnReceivedServerWorldEnteredPacket;
        public event ReceivedPacketEventHandler OnReceivedServerLoginErrorPacket;
        public event ReceivedPacketEventHandler OnReceivedServerLoginAdvicePacket;
        public event ReceivedPacketEventHandler OnReceivedServerLoginWaitPacket;
        public event ReceivedPacketEventHandler OnReceivedServerLoginSuccessPacket;
        public event ReceivedPacketEventHandler OnReceivedServerStoreButtonIndicatorsPacket;
        public event ReceivedPacketEventHandler OnReceivedServerPingPacket;
        public event ReceivedPacketEventHandler OnReceivedServerPingBackPacket;
        public event ReceivedPacketEventHandler OnReceivedServerLoginChallengePacket;
        public event ReceivedPacketEventHandler OnReceivedServerDeadPacket;
        public event ReceivedPacketEventHandler OnReceivedServerStashPacket;
        public event ReceivedPacketEventHandler OnReceivedServerDepotTileStatePacket;
        public event ReceivedPacketEventHandler OnReceivedServerPartyHuntAnalyserPacket;
        public event ReceivedPacketEventHandler OnReceivedServerSpecialContainersAvailablePacket;
        public event ReceivedPacketEventHandler OnReceivedServerTeamFinderTeamLeaderPacket;
        public event ReceivedPacketEventHandler OnReceivedServerTeamFinderTeamMemberPacket;
        public event ReceivedPacketEventHandler OnReceivedServerClientCheckPacket;
        public event ReceivedPacketEventHandler OnReceivedServerFullMapPacket;
        public event ReceivedPacketEventHandler OnReceivedServerTopRowPacket;
        public event ReceivedPacketEventHandler OnReceivedServerRightColumnPacket;
        public event ReceivedPacketEventHandler OnReceivedServerBottomRowPacket;
        public event ReceivedPacketEventHandler OnReceivedServerLeftColumnPacket;
        public event ReceivedPacketEventHandler OnReceivedServerFieldDataPacket;
        public event ReceivedPacketEventHandler OnReceivedServerCreateOnMapPacket;
        public event ReceivedPacketEventHandler OnReceivedServerChangeOnMapPacket;
        public event ReceivedPacketEventHandler OnReceivedServerDeleteOnMapPacket;
        public event ReceivedPacketEventHandler OnReceivedServerMoveCreaturePacket;
        public event ReceivedPacketEventHandler OnReceivedServerContainerPacket;
        public event ReceivedPacketEventHandler OnReceivedServerCloseContainerPacket;
        public event ReceivedPacketEventHandler OnReceivedServerCreateInContainerPacket;
        public event ReceivedPacketEventHandler OnReceivedServerChangeInContainerPacket;
        public event ReceivedPacketEventHandler OnReceivedServerDeleteInContainerPacket;
        public event ReceivedPacketEventHandler OnReceivedServerFriendSystemDataPacket;
        public event ReceivedPacketEventHandler OnReceivedServerScreenshotEventPacket;
        public event ReceivedPacketEventHandler OnReceivedServerInspectionListPacket;
        public event ReceivedPacketEventHandler OnReceivedServerInspectionStatePacket;
        public event ReceivedPacketEventHandler OnReceivedServerSetInventoryPacket;
        public event ReceivedPacketEventHandler OnReceivedServerDeleteInventoryPacket;
        public event ReceivedPacketEventHandler OnReceivedServerNpcOfferPacket;
        public event ReceivedPacketEventHandler OnReceivedServerPlayerGoodsPacket;
        public event ReceivedPacketEventHandler OnReceivedServerCloseNpcTradePacket;
        public event ReceivedPacketEventHandler OnReceivedServerOwnOfferPacket;
        public event ReceivedPacketEventHandler OnReceivedServerCounterOfferPacket;
        public event ReceivedPacketEventHandler OnReceivedServerCloseTradePacket;
        public event ReceivedPacketEventHandler OnReceivedServerCharacterTradeConfigurationPacket;
        public event ReceivedPacketEventHandler OnReceivedServerAmbientePacket;
        public event ReceivedPacketEventHandler OnReceivedServerGraphicalEffectsPacket;
        public event ReceivedPacketEventHandler OnReceivedServerRemoveGraphicalEffectPacket;
        public event ReceivedPacketEventHandler OnReceivedServerMissileEffectPacket;
        public event ReceivedPacketEventHandler OnReceivedServerTrappersPacket;
        public event ReceivedPacketEventHandler OnReceivedServerCreatureUpdatePacket;
        public event ReceivedPacketEventHandler OnReceivedServerCreatureHealthPacket;
        public event ReceivedPacketEventHandler OnReceivedServerCreatureLightPacket;
        public event ReceivedPacketEventHandler OnReceivedServerCreatureOutfitPacket;
        public event ReceivedPacketEventHandler OnReceivedServerCreatureSpeedPacket;
        public event ReceivedPacketEventHandler OnReceivedServerCreatureSkullPacket;
        public event ReceivedPacketEventHandler OnReceivedServerCreaturePartyPacket;
        public event ReceivedPacketEventHandler OnReceivedServerCreatureUnpassPacket;
        public event ReceivedPacketEventHandler OnReceivedServerCreatureMarksPacket;
        public event ReceivedPacketEventHandler OnReceivedServerCreaturePvpHelpersPacket;
        public event ReceivedPacketEventHandler OnReceivedServerDepotSearchResultsPacket;
        public event ReceivedPacketEventHandler OnReceivedServerCreatureTypePacket;
        public event ReceivedPacketEventHandler OnReceivedServerEditTextPacket;
        public event ReceivedPacketEventHandler OnReceivedServerEditListPacket;
        public event ReceivedPacketEventHandler OnReceivedServerShowGameNewsPacket;
        public event ReceivedPacketEventHandler OnReceivedServerDepotSearchDetailListPacket;
        public event ReceivedPacketEventHandler OnReceivedServerCloseDepotSearchPacket;
        public event ReceivedPacketEventHandler OnReceivedServerBlessingsDialogPacket;
        public event ReceivedPacketEventHandler OnReceivedServerBlessingsPacket;
        public event ReceivedPacketEventHandler OnReceivedServerSwitchPresetPacket;
        public event ReceivedPacketEventHandler OnReceivedServerPremiumTriggerPacket;
        public event ReceivedPacketEventHandler OnReceivedServerPlayerDataBasicPacket;
        public event ReceivedPacketEventHandler OnReceivedServerPlayerDataCurrentPacket;
        public event ReceivedPacketEventHandler OnReceivedServerPlayerSkillsPacket;
        public event ReceivedPacketEventHandler OnReceivedServerPlayerStatePacket;
        public event ReceivedPacketEventHandler OnReceivedServerClearTargetPacket;
        public event ReceivedPacketEventHandler OnReceivedServerSpellDelayPacket;
        public event ReceivedPacketEventHandler OnReceivedServerSpellGroupDelayPacket;
        public event ReceivedPacketEventHandler OnReceivedServerMultiUseDelayPacket;
        public event ReceivedPacketEventHandler OnReceivedServerSetTacticsPacket;
        public event ReceivedPacketEventHandler OnReceivedServerSetStoreDeepLinkPacket;
        public event ReceivedPacketEventHandler OnReceivedServerRestingAreaStatePacket;
        public event ReceivedPacketEventHandler OnReceivedServerTalkPacket;
        public event ReceivedPacketEventHandler OnReceivedServerChannelsPacket;
        public event ReceivedPacketEventHandler OnReceivedServerOpenChannelPacket;
        public event ReceivedPacketEventHandler OnReceivedServerPrivateChannelPacket;
        public event ReceivedPacketEventHandler OnReceivedServerEditGuildMessagePacket;
        public event ReceivedPacketEventHandler OnReceivedServerHighscoresPacket;
        public event ReceivedPacketEventHandler OnReceivedServerOpenOwnChannelPacket;
        public event ReceivedPacketEventHandler OnReceivedServerCloseChannelPacket;
        public event ReceivedPacketEventHandler OnReceivedServerMessagePacket;
        public event ReceivedPacketEventHandler OnReceivedServerSnapBackPacket;
        public event ReceivedPacketEventHandler OnReceivedServerWaitPacket;
        public event ReceivedPacketEventHandler OnReceivedServerUnjustifiedPointsPacket;
        public event ReceivedPacketEventHandler OnReceivedServerPvpSituationsPacket;
        public event ReceivedPacketEventHandler OnReceivedServerBestiaryTrackerPacket;
        public event ReceivedPacketEventHandler OnReceivedServerPreyHuntingTaskBaseDataPacket;
        public event ReceivedPacketEventHandler OnReceivedServerPreyHuntingTaskDataPacket;
        public event ReceivedPacketEventHandler OnReceivedServerTopFloorPacket;
        public event ReceivedPacketEventHandler OnReceivedServerBottomFloorPacket;
        public event ReceivedPacketEventHandler OnReceivedServerUpdateLootContainersPacket;
        public event ReceivedPacketEventHandler OnReceivedServerPlayerDataTournamentPacket;
        public event ReceivedPacketEventHandler OnReceivedServerCyclopediaHouseActionResultPacket;
        public event ReceivedPacketEventHandler OnReceivedServerTournamentInformationPacket;
        public event ReceivedPacketEventHandler OnReceivedServerTournamentLeaderboardPacket;
        public event ReceivedPacketEventHandler OnReceivedServerCyclopediaStaticHouseDataPacket;
        public event ReceivedPacketEventHandler OnReceivedServerCyclopediaCurrentHouseDataPacket;
        public event ReceivedPacketEventHandler OnReceivedServerOutfitPacket;
        public event ReceivedPacketEventHandler OnReceivedServerExivaSuppressedPacket;
        public event ReceivedPacketEventHandler OnReceivedServerUpdateExivaOptionsPacket;
        public event ReceivedPacketEventHandler OnReceivedServerTransactionDetailsPacket;
        public event ReceivedPacketEventHandler OnReceivedServerImpactTrackingPacket;
        public event ReceivedPacketEventHandler OnReceivedServerMarketStatisticsPacket;
        public event ReceivedPacketEventHandler OnReceivedServerItemWastedPacket;
        public event ReceivedPacketEventHandler OnReceivedServerItemLootedPacket;
        public event ReceivedPacketEventHandler OnReceivedServerTrackQuestFlagsPacket;
        public event ReceivedPacketEventHandler OnReceivedServerKillTrackingPacket;
        public event ReceivedPacketEventHandler OnReceivedServerBuddyDataPacket;
        public event ReceivedPacketEventHandler OnReceivedServerBuddyStatusChangePacket;
        public event ReceivedPacketEventHandler OnReceivedServerBuddyGroupDataPacket;
        public event ReceivedPacketEventHandler OnReceivedServerMonsterCyclopediaPacket;
        public event ReceivedPacketEventHandler OnReceivedServerMonsterCyclopediaMonstersPacket;
        public event ReceivedPacketEventHandler OnReceivedServerMonsterCyclopediaRacePacket;
        public event ReceivedPacketEventHandler OnReceivedServerMonsterCyclopediaBonusEffectsPacket;
        public event ReceivedPacketEventHandler OnReceivedServerMonsterCyclopediaNewDetailsPacket;
        public event ReceivedPacketEventHandler OnReceivedServerCyclopediaCharacterInfoPacket;
        public event ReceivedPacketEventHandler OnReceivedServerHirelingNameChangePacket;
        public event ReceivedPacketEventHandler OnReceivedServerTutorialHintPacket;
        public event ReceivedPacketEventHandler OnReceivedServerCyclopediaMapDataPacket;
        public event ReceivedPacketEventHandler OnReceivedServerAutomapFlagPacket;
        public event ReceivedPacketEventHandler OnReceivedServerDailyRewardCollectionStatePacket;
        public event ReceivedPacketEventHandler OnReceivedServerCreditBalancePacket;
        public event ReceivedPacketEventHandler OnReceivedServerIngameShopErrorPacket;
        public event ReceivedPacketEventHandler OnReceivedServerRequestPurchaseDataPacket;
        public event ReceivedPacketEventHandler OnReceivedServerOpenRewardWallPacket;
        public event ReceivedPacketEventHandler OnReceivedServerCloseRewardWallPacket;
        public event ReceivedPacketEventHandler OnReceivedServerDailyRewardBasicPacket;
        public event ReceivedPacketEventHandler OnReceivedServerDailyRewardHistoryPacket;
        public event ReceivedPacketEventHandler OnReceivedServerPreyFreeListRerollAvailabilityPacket;
        public event ReceivedPacketEventHandler OnReceivedServerPreyTimeLeftPacket;
        public event ReceivedPacketEventHandler OnReceivedServerPreyDataPacket;
        public event ReceivedPacketEventHandler OnReceivedServerPreyPricesPacket;
        public event ReceivedPacketEventHandler OnReceivedServerOfferDescription;
        public event ReceivedPacketEventHandler OnReceivedServerImbuingDialogRefreshPacket;
        public event ReceivedPacketEventHandler OnReceivedServerCloseImbuingDialogPacket;
        public event ReceivedPacketEventHandler OnReceivedServerShowMessageDialogPacket;
        public event ReceivedPacketEventHandler OnReceivedServerRequestResourceBalancePacket;
        public event ReceivedPacketEventHandler OnReceivedServerTibiaTimePacket;
        public event ReceivedPacketEventHandler OnReceivedServerQuestLogPacket;
        public event ReceivedPacketEventHandler OnReceivedServerQuestLinePacket;
        public event ReceivedPacketEventHandler OnReceivedServerUpdatingShopBalancePacket;
        public event ReceivedPacketEventHandler OnReceivedServerChannelEventPacket;
        public event ReceivedPacketEventHandler OnReceivedServerObjectInfoPacket;
        public event ReceivedPacketEventHandler OnReceivedServerPlayerInventoryPacket;
        public event ReceivedPacketEventHandler OnReceivedServerMarketEnterPacket;
        public event ReceivedPacketEventHandler OnReceivedServerMarketLeavePacket;
        public event ReceivedPacketEventHandler OnReceivedServerMarketDetailPacket;
        public event ReceivedPacketEventHandler OnReceivedServerMarketBrowsePacket;
        public event ReceivedPacketEventHandler OnReceivedServerShowModalDialogPacket;
        public event ReceivedPacketEventHandler OnReceivedServerStoreCategoriesPacket;
        public event ReceivedPacketEventHandler OnReceivedServerPremiumShopPacket;
        public event ReceivedPacketEventHandler OnReceivedServerStoreOffersPacket;
        public event ReceivedPacketEventHandler OnReceivedServerTransactionHistoryPacket;
        public event ReceivedPacketEventHandler OnReceivedServerStoreSuccessPacket;

        public void ParseClientMessage(Client client, NetworkMessage inMessage, NetworkMessage outMessage)
        {
            if (inMessage == null)
            {
                throw new ArgumentNullException(nameof(inMessage));
            }

            if (outMessage == null)
            {
                throw new ArgumentNullException(nameof(outMessage));
            }

            var packets = new List<(ClientPacketType PacketType, uint Position)>();
            var packetPosition = 0u;
            var currentPacket = ClientPacketType.Invalid;
            var lastKnownPacket = ClientPacketType.Invalid;
            byte[] packetData = null;

            try
            {
                // This may possibly be a Login packet. In that case, we have to move to the correct position
                // as the Login packet doesn't contain the 2 bytes for the payload size as other packets.
                if (inMessage.SequenceNumber == 0)
                {
                    inMessage.Seek(6, SeekOrigin.Begin);
                    if (inMessage.PeekByte() != (byte)ClientPacketType.Login)
                    {
                        inMessage.Seek(2, SeekOrigin.Current);
                    }
                }

                if (client.Logger.Level == Logger.LogLevel.Debug)
                {
                    packetData = inMessage.GetData();
                }

                while (inMessage.Position < inMessage.Size)
                {
                    packetPosition = inMessage.Position;

                    var opcode = inMessage.ReadByte();
                    currentPacket = (ClientPacketType)opcode;

                    client.Logger.Debug($"[CLIENT:{inMessage.SequenceNumber}] {opcode:X2} - {currentPacket}");

                    var packet = ClientPacket.CreateInstance(client, currentPacket);
                    packet.ParseFromNetworkMessage(inMessage);

                    switch (packet.PacketType)
                    {
                        case ClientPacketType.Login:
                            packet.Forward = OnReceivedClientLoginPacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.SecondaryLogin:
                            packet.Forward = OnReceivedClientSecondaryLoginPacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.EnterWorld:
                            packet.Forward = OnReceivedClientEnterWorldPacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.QuitGame:
                            packet.Forward = OnReceivedClientQuitGamePacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.ConnectionPingBack:
                            packet.Forward = OnReceivedClientConnectionPingBack?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.Ping:
                            packet.Forward = OnReceivedClientPingPacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.PingBack:
                            packet.Forward = OnReceivedClientPingBackPacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.PerformanceMetrics:
                            packet.Forward = OnReceivedClientPerformanceMetricsPacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.StashAction:
                            packet.Forward = OnReceivedClientStashActionPacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.DepotSearchRetrieve:
                            packet.Forward = OnReceivedClientDepotSearchRetrievePacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.TrackBestiaryRace:
                            packet.Forward = OnReceivedClientTrackBestiaryRacePacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.PartyHuntAnalyser:
                            packet.Forward = OnReceivedClientPartyHuntAnalyserPacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.TeamFinderAssembleTeam:
                            packet.Forward = OnReceivedClientTeamFinderAssembleTeamPacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.TeamFinderJoinTeam:
                            packet.Forward = OnReceivedClientTeamFinderJoinTeamPacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.ClientCheck:
                            packet.Forward = OnReceivedClientClientCheckPacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.GoPath:
                            packet.Forward = OnReceivedClientGoPathPacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.GoNorth:
                            packet.Forward = OnReceivedClientGoNorthPacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.GoEast:
                            packet.Forward = OnReceivedClientGoEastPacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.GoSouth:
                            packet.Forward = OnReceivedClientGoSouthPacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.GoWest:
                            packet.Forward = OnReceivedClientGoWestPacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.Stop:
                            packet.Forward = OnReceivedClientStopPacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.GoNorthEast:
                            packet.Forward = OnReceivedClientGoNorthEastPacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.GoSouthEast:
                            packet.Forward = OnReceivedClientGoSouthEastPacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.GoSouthWest:
                            packet.Forward = OnReceivedClientGoSouthWestPacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.GoNorthWest:
                            packet.Forward = OnReceivedClientGoNorthWestPacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.RotateNorth:
                            packet.Forward = OnReceivedClientRotateNorthPacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.RotateEast:
                            packet.Forward = OnReceivedClientRotateEastPacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.RotateSouth:
                            packet.Forward = OnReceivedClientRotateSouthPacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.RotateWest:
                            packet.Forward = OnReceivedClientRotateWestPacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.Teleport:
                            packet.Forward = OnReceivedClientTeleportPacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.CharacterTradeConfigurationAction:
                            packet.Forward = OnReceivedClientCharacterTradeConfigurationActionPacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.EquipObject:
                            packet.Forward = OnReceivedClientEquipObjectPacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.MoveObject:
                            packet.Forward = OnReceivedClientMoveObjectPacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.LookNpcTrade:
                            packet.Forward = OnReceivedClientLookNpcTradePacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.BuyObject:
                            packet.Forward = OnReceivedClientBuyObjectPacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.SellObject:
                            packet.Forward = OnReceivedClientSellObjectPacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.CloseNpcTrade:
                            packet.Forward = OnReceivedClientCloseNpcTradePacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.TradeObject:
                            packet.Forward = OnReceivedClientTradeObjectPacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.LookTrade:
                            packet.Forward = OnReceivedClientLookTradePacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.AcceptTrade:
                            packet.Forward = OnReceivedClientAcceptTradePacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.RejectTrade:
                            packet.Forward = OnReceivedClientRejectTradePacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.FriendSystemAction:
                            packet.Forward = OnReceivedClientFriendSystemActionPacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.UseObject:
                            packet.Forward = OnReceivedClientUseObjectPacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.UseTwoObjects:
                            packet.Forward = OnReceivedClientUseTwoObjectsPacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.UseOnCreature:
                            packet.Forward = OnReceivedClientUseOnCreaturePacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.TurnObject:
                            packet.Forward = OnReceivedClientTurnObjectPacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.CloseContainer:
                            packet.Forward = OnReceivedClientCloseContainerPacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.UpContainer:
                            packet.Forward = OnReceivedClientUpContainerPacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.EditText:
                            packet.Forward = OnReceivedClientEditTextPacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.EditList:
                            packet.Forward = OnReceivedClientEditListPacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.ToggleWrapState:
                            packet.Forward = OnReceivedClientToggleWarpStatePacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.Look:
                            packet.Forward = OnReceivedClientLookPacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.LookAtCreature:
                            packet.Forward = OnReceivedClientLookAtCreaturePacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.JoinAggression:
                            packet.Forward = OnReceivedClientJoinAggressionPacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.QuickLoot:
                            packet.Forward = OnReceivedClientQuickLootPacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.LootContainer:
                            packet.Forward = OnReceivedClientLootContainerPacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.QuickLootBlackWhitelist:
                            packet.Forward = OnReceivedClientQuickLootBlackWhitelistPacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.OpenDepotSearch:
                            packet.Forward = OnReceivedClientOpenDepotSearchPacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.CloseDepotSearch:
                            packet.Forward = OnReceivedClientCloseDepotSearchPacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.DepotSearchType:
                            packet.Forward = OnReceivedClientDepotSearchTypePacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.OpenParentContainer:
                            packet.Forward = OnReceivedClientOpenParentContainerPacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.Talk:
                            packet.Forward = OnReceivedClientTalkPacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.GetChannels:
                            packet.Forward = OnReceivedClientGetChannelsPacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.JoinChannel:
                            packet.Forward = OnReceivedClientJoinChannelPacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.LeaveChannel:
                            packet.Forward = OnReceivedClientLeaveChannelPacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.PrivateChannel:
                            packet.Forward = OnReceivedClientPrivateChannelPacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.GuildMessage:
                            packet.Forward = OnReceivedClientGuildMessagePacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.EditGuildMessage:
                            packet.Forward = OnReceivedClientEditGuildMessagePacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.CloseNpcChannel:
                            packet.Forward = OnReceivedClientCloseNpcChannelPacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.SetTactics:
                            packet.Forward = OnReceivedClientSetTacticsPacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.Attack:
                            packet.Forward = OnReceivedClientAttackPacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.Follow:
                            packet.Forward = OnReceivedClientFollowPacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.InviteToParty:
                            packet.Forward = OnReceivedClientInviteToPartyPacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.JoinParty:
                            packet.Forward = OnReceivedClientJoinPartyPacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.RevokeInvitation:
                            packet.Forward = OnReceivedClientRevokeInvitationPacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.PassLeadership:
                            packet.Forward = OnReceivedClientPassLeadershipPacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.LeaveParty:
                            packet.Forward = OnReceivedClientLeavePartyPacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.ShareExperience:
                            packet.Forward = OnReceivedClientShareExperiencePacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.DisbandParty:
                            packet.Forward = OnReceivedClientDisbandPartyPacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.OpenChannel:
                            packet.Forward = OnReceivedClientOpenChannelPacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.InviteToChannel:
                            packet.Forward = OnReceivedClientInviteToChannelPacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.ExcludeFromChannel:
                            packet.Forward = OnReceivedClientExcludeFromChannelPacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.CyclopediaHouseAction:
                            packet.Forward = OnReceivedClientCyclopediaHouseActionPacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.Highscores:
                            packet.Forward = OnReceivedClientHighscoresPacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.PreyHuntingTaskAction:
                            packet.Forward = OnReceivedClientPreyHuntingTaskActionPacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.Cancel:
                            packet.Forward = OnReceivedClientCancelPacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.ClaimTournamentReward:
                            packet.Forward = OnReceivedClientClaimTournamentRewardPacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.TournamentInformation:
                            packet.Forward = OnReceivedClientTournamentInformationPacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.SubscribeToUpdates:
                            packet.Forward = OnReceivedClientSubscribeToUpdatesPacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.TournamentLeaderboard:
                            packet.Forward = OnReceivedClientTournamentLeaderboardPacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.TournamentTicketAction:
                            packet.Forward = OnReceivedClientTournamentTicketActionPacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.GetTransactionDetails:
                            packet.Forward = OnReceivedClientGetTransactionDetailsPacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.UpdateExivaOptions:
                            packet.Forward = OnReceivedClientUpdateExivaOptionsPacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.BrowseField:
                            packet.Forward = OnReceivedClientBrowseFieldPacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.SeekInContainer:
                            packet.Forward = OnReceivedClientSeekInContainerPacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.InspectObject:
                            packet.Forward = OnReceivedClientInspectObjectPacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.InspectPlayer:
                            packet.Forward = OnReceivedClientInspectPlayerPacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.BlessingsDialog:
                            packet.Forward = OnReceivedClientBlessingsDialogPacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.TrackQuestflags:
                            packet.Forward = OnReceivedClientTrackQuestFlagsPacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.MarketStatistics:
                            packet.Forward = OnReceivedClientMarketStatisticsPacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.GetOutfit:
                            packet.Forward = OnReceivedClientGetOutfitPacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.SetOutfit:
                            packet.Forward = OnReceivedClientSetOutfitPacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.Mount:
                            packet.Forward = OnReceivedClientMountPacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.ApplyImbuement:
                            packet.Forward = OnReceivedClientApplyImbuementPacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.ApplyClearingCharm:
                            packet.Forward = OnReceivedClientApplyClearingCharmPacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.ClosedImbuingDialog:
                            packet.Forward = OnReceivedClientClosedImbuingDialogPacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.OpenRewardWall:
                            packet.Forward = OnReceivedClientOpenRewardWallPacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.DailyRewardHistory:
                            packet.Forward = OnReceivedClientDailyRewardHistoryPacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.CollectDailyReward:
                            packet.Forward = OnReceivedClientCollectDailyRewardPacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.CyclopediaMapAction:
                            packet.Forward = OnReceivedClientCyclopediaMapActionPacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.AddBuddy:
                            packet.Forward = OnReceivedClientAddBuddyPacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.RemoveBuddy:
                            packet.Forward = OnReceivedClientRemoveBuddyPacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.EditBuddy:
                            packet.Forward = OnReceivedClientEditBuddyPacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.BuddyGroup:
                            packet.Forward = OnReceivedClientBuddyGroupPacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.MarkGameNewsAsRead:
                            packet.Forward = OnReceivedClientMarkGameNewsAsReadPacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.OpenMonsterCyclopedia:
                            packet.Forward = OnReceivedClientOpenMonsterCyclopediaPacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.OpenMonsterCyclopediaMonsters:
                            packet.Forward = OnReceivedClientOpenMonsterCyclopediaMonstersPacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.OpenMonsterCyclopediaRace:
                            packet.Forward = OnReceivedClientOpenMonsterCyclopediaRacePacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.MonsterBonusEffectAction:
                            packet.Forward = OnReceivedClientMonsterBonusEffectActionPacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.OpenCyclopediaCharacterInfo:
                            packet.Forward = OnReceivedClientOpenCyclopediaCharacterInfoPacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.BugReport:
                            packet.Forward = OnReceivedClientBugReportPacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.ThankYou:
                            packet.Forward = OnReceivedClientThankYouPacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.GetOfferDescription:
                            packet.Forward = OnReceivedClientGetOfferDescription?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.StoreEvent:
                            packet.Forward = OnReceivedClientStoreEventPacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.FeatureEvent:
                            packet.Forward = OnReceivedClientFeatureEventPacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.PreyAction:
                            packet.Forward = OnReceivedClientPreyActionPacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.SetHirelingName:
                            packet.Forward = OnReceivedClientSetHirelingNamePacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.RequestResourceBalance:
                            packet.Forward = OnReceivedClientRequestResourceBalancePacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.Greet:
                            packet.Forward = OnReceivedClientGreetPacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.TransferCurrency:
                            packet.Forward = OnReceivedClientTransferCurrencyPacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.GetQuestLog:
                            packet.Forward = OnReceivedClientGetQuestLogPacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.GetQuestLine:
                            packet.Forward = OnReceivedClientGetQuestLinePacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.RuleViolationReport:
                            packet.Forward = OnReceivedClientRuleViolationReportPacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.GetObjectInfo:
                            packet.Forward = OnReceivedClientGetObjectInfoPacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.MarketLeave:
                            packet.Forward = OnReceivedClientMarketLeavePacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.MarketBrowse:
                            packet.Forward = OnReceivedClientMarketBrowsePacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.MarketCreate:
                            packet.Forward = OnReceivedClientMarketCreatePacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.MarketCancel:
                            packet.Forward = OnReceivedClientMarketCancelPacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.MarketAccept:
                            packet.Forward = OnReceivedClientMarketAcceptPacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.AnswerModalDialog:
                            packet.Forward = OnReceivedClientAnswerModalDialogPacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.OpenIngameShop:
                            packet.Forward = OnReceivedClientOpenIngameShopPacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.RequestShopOffers:
                            packet.Forward = OnReceivedClientRequestShopOffersPacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.BuyIngameShopOffer:
                            packet.Forward = OnReceivedClientBuyIngameShopOfferPacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.OpenTransactionHistory:
                            packet.Forward = OnReceivedClientOpenTransactionHistoryPacket?.Invoke(packet) ?? true;
                            break;
                        case ClientPacketType.GetTransactionHistory:
                            packet.Forward = OnReceivedClientGetTransactionHistoryPacket?.Invoke(packet) ?? true;
                            break;
                        default:
                            client.Logger.Error($"Unknown client packet: {((byte)currentPacket).ToString("X2")}, position: {packetPosition}");
                            client.Logger.Error($"Last known packets: {string.Join(" ", packets.Select(p => "[" + ((byte)p.PacketType).ToString("X2") + ":" + p.Position + "]" + p.PacketType).ToArray())}");
                            client.Logger.Error($"Data: {BitConverter.ToString(inMessage.GetData()).Replace('-', ' ')}");
                            return;
                    }

                    if (packet.Forward && client.Connection.IsClientPacketModificationEnabled)
                    {
                        packet.AppendToNetworkMessage(outMessage);
                    }

                    packets.Add((currentPacket, packetPosition));
                    lastKnownPacket = currentPacket;

                    if (client.Logger.Level == Logger.LogLevel.Debug && packetData != null)
                    {
                        var len = inMessage.Position - packetPosition;
                        var data = new byte[len];
                        Array.Copy(packetData, packetPosition, data, 0, len);
                        client.Logger.Debug($"{BitConverter.ToString(data).Replace('-', ' ')}");
                    }
                }
            }
            catch (Exception ex)
            {
                // Parsing failures are helpful when looking for packet changes or trying to understand
                // the structure of a new packet. Because of that, it's better to log as much data as
                // possible and continue.
                client.Logger.Error(ex.ToString());
                client.Logger.Error($"Current position: {inMessage.Position}");
                client.Logger.Error($"Current packet: [{((byte)currentPacket).ToString("X2")}:{packetPosition}]{currentPacket}");
                client.Logger.Error($"Last known packets: {string.Join(" ", packets.Select(p => "[" + ((byte)p.PacketType).ToString("X2") + ":" + p.Position + "]" + p.PacketType).ToArray())}");
                client.Logger.Error($"Data: {BitConverter.ToString(inMessage.GetData()).Replace('-', ' ')}");
            }
        }

        public void ParseServerMessage(Client client, NetworkMessage inMessage, NetworkMessage outMessage)
        {
            if (inMessage == null)
            {
                throw new ArgumentNullException(nameof(inMessage));
            }

            if (outMessage == null)
            {
                throw new ArgumentNullException(nameof(outMessage));
            }

            var packets = new List<(ServerPacketType PacketType, uint Position)>();
            var packetPosition = 0u;
            var currentPacket = ServerPacketType.Invalid;
            var lastKnownPacket = ServerPacketType.Invalid;
            byte[] packetData = null;

            try
            {
                if (client.Logger.Level == Logger.LogLevel.Debug)
                {
                    packetData = inMessage.GetData();
                }

                while (inMessage.Position < inMessage.Size)
                {
                    packetPosition = inMessage.Position;

                    var opcode = inMessage.ReadByte();
                    currentPacket = (ServerPacketType)opcode;

                    client.Logger.Debug($"[SERVER:{inMessage.SequenceNumber}] {opcode:X2} - {currentPacket}");

                    var packet = ServerPacket.CreateInstance(client, currentPacket);
                    packet.ParseFromNetworkMessage(inMessage);

                    switch (currentPacket)
                    {
                        case ServerPacketType.CreatureData:
                            packet.Forward = OnReceivedServerCreatureDataPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.SessionDumpStart:
                            packet.Forward = OnReceivedServerSessionDumpStartPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.PendingStateEntered:
                            packet.Forward = OnReceivedServerPendingStateEnteredPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.ReadyForSecondaryConnection:
                            packet.Forward = OnReceivedServerReadyForSecondaryConnectionPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.WorldEntered:
                            packet.Forward = OnReceivedServerWorldEnteredPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.LoginError:
                            packet.Forward = OnReceivedServerLoginErrorPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.LoginAdvice:
                            packet.Forward = OnReceivedServerLoginAdvicePacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.LoginWait:
                            packet.Forward = OnReceivedServerLoginWaitPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.LoginSuccess:
                            packet.Forward = OnReceivedServerLoginSuccessPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.StoreButtonIndicators:
                            packet.Forward = OnReceivedServerStoreButtonIndicatorsPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.Ping:
                            packet.Forward = OnReceivedServerPingPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.PingBack:
                            packet.Forward = OnReceivedServerPingBackPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.LoginChallenge:
                            packet.Forward = OnReceivedServerLoginChallengePacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.Dead:
                            packet.Forward = OnReceivedServerDeadPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.Stash:
                            packet.Forward = OnReceivedServerStashPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.SpecialContainersAvailable:
                            if (client.VersionNumber >= 12300000)
                            {
                                packet.Forward = OnReceivedServerSpecialContainersAvailablePacket?.Invoke(packet) ?? true;
                            }
                            else
                            {
                                packet.Forward = OnReceivedServerDepotTileStatePacket?.Invoke(packet) ?? true;
                            }
                            break;
                        case ServerPacketType.PartyHuntAnalyser:
                            packet.Forward = OnReceivedServerPartyHuntAnalyserPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.TeamFinderTeamLeader:
                            if (client.VersionNumber >= 12400000)
                            {
                                packet.Forward = OnReceivedServerTeamFinderTeamLeaderPacket?.Invoke(packet) ?? true;
                            }
                            else
                            {
                                packet.Forward = OnReceivedServerSpecialContainersAvailablePacket?.Invoke(packet) ?? true;
                            }
                            break;
                        case ServerPacketType.TeamFinderTeamMember:
                            packet.Forward = OnReceivedServerTeamFinderTeamMemberPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.ClientCheck:
                            packet.Forward = OnReceivedServerClientCheckPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.FullMap:
                            packet.Forward = OnReceivedServerFullMapPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.TopRow:
                            packet.Forward = OnReceivedServerTopRowPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.RightColumn:
                            packet.Forward = OnReceivedServerRightColumnPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.BottomRow:
                            packet.Forward = OnReceivedServerBottomRowPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.LeftColumn:
                            packet.Forward = OnReceivedServerLeftColumnPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.FieldData:
                            packet.Forward = OnReceivedServerFieldDataPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.CreateOnMap:
                            packet.Forward = OnReceivedServerCreateOnMapPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.ChangeOnMap:
                            packet.Forward = OnReceivedServerChangeOnMapPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.DeleteOnMap:
                            packet.Forward = OnReceivedServerDeleteOnMapPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.MoveCreature:
                            packet.Forward = OnReceivedServerMoveCreaturePacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.Container:
                            packet.Forward = OnReceivedServerContainerPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.CloseContainer:
                            packet.Forward = OnReceivedServerCloseContainerPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.CreateInContainer:
                            packet.Forward = OnReceivedServerCreateInContainerPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.ChangeInContainer:
                            packet.Forward = OnReceivedServerChangeInContainerPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.DeleteInContainer:
                            packet.Forward = OnReceivedServerDeleteInContainerPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.FriendSystemData:
                            packet.Forward = OnReceivedServerFriendSystemDataPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.ScreenshotEvent:
                            packet.Forward = OnReceivedServerScreenshotEventPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.InspectionList:
                            packet.Forward = OnReceivedServerInspectionListPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.InspectionState:
                            packet.Forward = OnReceivedServerInspectionStatePacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.SetInventory:
                            packet.Forward = OnReceivedServerSetInventoryPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.DeleteInventory:
                            packet.Forward = OnReceivedServerDeleteInventoryPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.NpcOffer:
                            packet.Forward = OnReceivedServerNpcOfferPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.PlayerGoods:
                            packet.Forward = OnReceivedServerPlayerGoodsPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.CloseNpcTrade:
                            packet.Forward = OnReceivedServerCloseNpcTradePacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.OwnOffer:
                            packet.Forward = OnReceivedServerOwnOfferPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.CounterOffer:
                            packet.Forward = OnReceivedServerCounterOfferPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.CloseTrade:
                            packet.Forward = OnReceivedServerCloseTradePacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.CharacterTradeConfiguration:
                            packet.Forward = OnReceivedServerCharacterTradeConfigurationPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.Ambiente:
                            packet.Forward = OnReceivedServerAmbientePacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.GraphicalEffects:
                            packet.Forward = OnReceivedServerGraphicalEffectsPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.RemoveGraphicalEffect:
                            packet.Forward = OnReceivedServerRemoveGraphicalEffectPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.MissileEffect:
                            packet.Forward = OnReceivedServerMissileEffectPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.Trappers:
                            packet.Forward = OnReceivedServerTrappersPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.CreatureUpdate:
                            packet.Forward = OnReceivedServerCreatureUpdatePacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.CreatureHealth:
                            packet.Forward = OnReceivedServerCreatureHealthPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.CreatureLight:
                            packet.Forward = OnReceivedServerCreatureLightPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.CreatureOutfit:
                            packet.Forward = OnReceivedServerCreatureOutfitPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.CreatureSpeed:
                            packet.Forward = OnReceivedServerCreatureSpeedPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.CreatureSkull:
                            packet.Forward = OnReceivedServerCreatureSkullPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.CreatureParty:
                            packet.Forward = OnReceivedServerCreaturePartyPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.CreatureUnpass:
                            packet.Forward = OnReceivedServerCreatureUnpassPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.CreatureMarks:
                            packet.Forward = OnReceivedServerCreatureMarksPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.DepotSearchResults:
                            if (client.VersionNumber >= 12200000)
                            {
                                packet.Forward = OnReceivedServerDepotSearchResultsPacket?.Invoke(packet) ?? true;
                            }
                            else
                            {
                                packet.Forward = OnReceivedServerCreaturePvpHelpersPacket?.Invoke(packet) ?? true;
                            }
                            break;
                        case ServerPacketType.CreatureType:
                            packet.Forward = OnReceivedServerCreatureTypePacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.EditText:
                            packet.Forward = OnReceivedServerEditTextPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.EditList:
                            packet.Forward = OnReceivedServerEditListPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.ShowGameNews:
                            packet.Forward = OnReceivedServerShowGameNewsPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.DepotSearchDetailList:
                            packet.Forward = OnReceivedServerDepotSearchDetailListPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.CloseDepotSearch:
                            packet.Forward = OnReceivedServerCloseDepotSearchPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.BlessingsDialog:
                            packet.Forward = OnReceivedServerBlessingsDialogPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.Blessings:
                            packet.Forward = OnReceivedServerBlessingsPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.SwitchPreset:
                            packet.Forward = OnReceivedServerSwitchPresetPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.PremiumTrigger:
                            packet.Forward = OnReceivedServerPremiumTriggerPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.PlayerDataBasic:
                            packet.Forward = OnReceivedServerPlayerDataBasicPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.PlayerDataCurrent:
                            packet.Forward = OnReceivedServerPlayerDataCurrentPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.PlayerSkills:
                            packet.Forward = OnReceivedServerPlayerSkillsPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.PlayerState:
                            packet.Forward = OnReceivedServerPlayerStatePacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.ClearTarget:
                            packet.Forward = OnReceivedServerClearTargetPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.SpellDelay:
                            packet.Forward = OnReceivedServerSpellDelayPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.SpellGroupDelay:
                            packet.Forward = OnReceivedServerSpellGroupDelayPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.MultiUseDelay:
                            packet.Forward = OnReceivedServerMultiUseDelayPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.SetTactics:
                            packet.Forward = OnReceivedServerSetTacticsPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.SetStoreButtonDeeplink:
                            packet.Forward = OnReceivedServerSetStoreDeepLinkPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.RestingAreaState:
                            packet.Forward = OnReceivedServerRestingAreaStatePacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.Talk:
                            packet.Forward = OnReceivedServerTalkPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.Channels:
                            packet.Forward = OnReceivedServerChannelsPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.OpenChannel:
                            packet.Forward = OnReceivedServerOpenChannelPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.PrivateChannel:
                            packet.Forward = OnReceivedServerPrivateChannelPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.EditGuildMessage:
                            packet.Forward = OnReceivedServerEditGuildMessagePacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.Highscores:
                            packet.Forward = OnReceivedServerHighscoresPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.OpenOwnChannel:
                            packet.Forward = OnReceivedServerOpenOwnChannelPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.CloseChannel:
                            packet.Forward = OnReceivedServerCloseChannelPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.Message:
                            packet.Forward = OnReceivedServerMessagePacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.SnapBack:
                            packet.Forward = OnReceivedServerSnapBackPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.Wait:
                            packet.Forward = OnReceivedServerWaitPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.UnjustifiedPoints:
                            packet.Forward = OnReceivedServerUnjustifiedPointsPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.PvpSituations:
                            packet.Forward = OnReceivedServerPvpSituationsPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.BestiaryTracker:
                            packet.Forward = OnReceivedServerBestiaryTrackerPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.PreyHuntingTaskBaseData:
                            packet.Forward = OnReceivedServerPreyHuntingTaskBaseDataPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.PreyHuntingTaskData:
                            packet.Forward = OnReceivedServerPreyHuntingTaskDataPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.TopFloor:
                            packet.Forward = OnReceivedServerTopFloorPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.BottomFloor:
                            packet.Forward = OnReceivedServerBottomFloorPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.UpdateLootContainers:
                            packet.Forward = OnReceivedServerUpdateLootContainersPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.PlayerDataTournament:
                            packet.Forward = OnReceivedServerPlayerDataTournamentPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.CyclopediaHouseActionResult:
                            packet.Forward = OnReceivedServerCyclopediaHouseActionResultPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.TournamentInformation:
                            packet.Forward = OnReceivedServerTournamentInformationPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.TournamentLeaderboard:
                            packet.Forward = OnReceivedServerTournamentLeaderboardPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.CyclopediaStaticHouseData:
                            packet.Forward = OnReceivedServerCyclopediaStaticHouseDataPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.CyclopediaCurrentHouseData:
                            packet.Forward = OnReceivedServerCyclopediaCurrentHouseDataPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.Outfit:
                            packet.Forward = OnReceivedServerOutfitPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.ExivaSuppressed:
                            packet.Forward = OnReceivedServerExivaSuppressedPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.UpdateExivaOptions:
                            packet.Forward = OnReceivedServerUpdateExivaOptionsPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.TransactionDetails:
                            packet.Forward = OnReceivedServerTransactionDetailsPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.ImpactTracking:
                            packet.Forward = OnReceivedServerImpactTrackingPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.MarketStatistics:
                            packet.Forward = OnReceivedServerMarketStatisticsPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.ItemWasted:
                            packet.Forward = OnReceivedServerItemWastedPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.ItemLooted:
                            packet.Forward = OnReceivedServerItemLootedPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.TrackQuestflags:
                            packet.Forward = OnReceivedServerTrackQuestFlagsPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.KillTracking:
                            packet.Forward = OnReceivedServerKillTrackingPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.BuddyData:
                            packet.Forward = OnReceivedServerBuddyDataPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.BuddyStatusChange:
                            packet.Forward = OnReceivedServerBuddyStatusChangePacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.BuddyGroupData:
                            packet.Forward = OnReceivedServerBuddyGroupDataPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.MonsterCyclopedia:
                            packet.Forward = OnReceivedServerMonsterCyclopediaPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.MonsterCyclopediaMonsters:
                            packet.Forward = OnReceivedServerMonsterCyclopediaMonstersPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.MonsterCyclopediaRace:
                            packet.Forward = OnReceivedServerMonsterCyclopediaRacePacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.MonsterCyclopediaBonusEffects:
                            packet.Forward = OnReceivedServerMonsterCyclopediaBonusEffectsPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.MonsterCyclopediaNewDetails:
                            packet.Forward = OnReceivedServerMonsterCyclopediaNewDetailsPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.CyclopediaCharacterInfo:
                            packet.Forward = OnReceivedServerCyclopediaCharacterInfoPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.HirelingNameChange:
                            packet.Forward = OnReceivedServerHirelingNameChangePacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.TutorialHint:
                            packet.Forward = OnReceivedServerTutorialHintPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.CyclopediaMapData:
                            {
                                if (client.VersionNumber >= 11800000)
                                {
                                    packet.Forward = OnReceivedServerCyclopediaMapDataPacket?.Invoke(packet) ?? true;
                                }
                                else
                                {
                                    packet.Forward = OnReceivedServerAutomapFlagPacket?.Invoke(packet) ?? true;
                                }
                            }
                            break;
                        case ServerPacketType.DailyRewardCollectionState:
                            packet.Forward = OnReceivedServerDailyRewardCollectionStatePacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.CreditBalance:
                            packet.Forward = OnReceivedServerCreditBalancePacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.IngameShopError:
                            packet.Forward = OnReceivedServerIngameShopErrorPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.RequestPurchaseData:
                            packet.Forward = OnReceivedServerRequestPurchaseDataPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.OpenRewardWall:
                            packet.Forward = OnReceivedServerOpenRewardWallPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.CloseRewardWall:
                            packet.Forward = OnReceivedServerCloseRewardWallPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.DailyRewardBasic:
                            packet.Forward = OnReceivedServerDailyRewardBasicPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.DailyRewardHistory:
                            packet.Forward = OnReceivedServerDailyRewardHistoryPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.PreyFreeListRerollAvailability:
                            packet.Forward = OnReceivedServerPreyFreeListRerollAvailabilityPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.PreyTimeLeft:
                            packet.Forward = OnReceivedServerPreyTimeLeftPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.PreyData:
                            packet.Forward = OnReceivedServerPreyDataPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.PreyPrices:
                            packet.Forward = OnReceivedServerPreyPricesPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.OfferDescription:
                            packet.Forward = OnReceivedServerOfferDescription?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.ImbuingDialogRefresh:
                            packet.Forward = OnReceivedServerImbuingDialogRefreshPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.CloseImbuingDialog:
                            packet.Forward = OnReceivedServerCloseImbuingDialogPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.ShowMessageDialog:
                            packet.Forward = OnReceivedServerShowMessageDialogPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.RequestResourceBalance:
                            packet.Forward = OnReceivedServerRequestResourceBalancePacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.TibiaTime:
                            packet.Forward = OnReceivedServerTibiaTimePacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.QuestLog:
                            packet.Forward = OnReceivedServerQuestLogPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.QuestLine:
                            packet.Forward = OnReceivedServerQuestLinePacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.UpdatingShopBalance:
                            packet.Forward = OnReceivedServerUpdatingShopBalancePacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.ChannelEvent:
                            packet.Forward = OnReceivedServerChannelEventPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.ObjectInfo:
                            packet.Forward = OnReceivedServerObjectInfoPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.PlayerInventory:
                            packet.Forward = OnReceivedServerPlayerInventoryPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.MarketEnter:
                            packet.Forward = OnReceivedServerMarketEnterPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.MarketLeave:
                            packet.Forward = OnReceivedServerMarketLeavePacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.MarketDetail:
                            packet.Forward = OnReceivedServerMarketDetailPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.MarketBrowse:
                            packet.Forward = OnReceivedServerMarketBrowsePacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.ShowModalDialog:
                            packet.Forward = OnReceivedServerShowModalDialogPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.StoreCategories:
                            {
                                if (client.VersionNumber >= 11600000)
                                {
                                    packet.Forward = OnReceivedServerStoreCategoriesPacket?.Invoke(packet) ?? true;
                                }
                                else
                                {
                                    packet.Forward = OnReceivedServerPremiumShopPacket?.Invoke(packet) ?? true;
                                }
                            }
                            break;
                        case ServerPacketType.StoreOffers:
                            packet.Forward = OnReceivedServerStoreOffersPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.TransactionHistory:
                            packet.Forward = OnReceivedServerTransactionHistoryPacket?.Invoke(packet) ?? true;
                            break;
                        case ServerPacketType.StoreSuccess:
                            packet.Forward = OnReceivedServerStoreSuccessPacket?.Invoke(packet) ?? true;
                            break;
                        default:
                            client.Logger.Error($"Unknown server packet: {((byte)currentPacket).ToString("X2")}, position: {packetPosition}");
                            client.Logger.Error($"Last known packets: {string.Join(" ", packets.Select(p => "[" + ((byte)p.PacketType).ToString("X2") + ":" + p.Position + "]" + p.PacketType).ToArray())}");
                            client.Logger.Error($"Data: {BitConverter.ToString(inMessage.GetData()).Replace('-', ' ')}");
                            return;
                    }

                    if (packet.Forward && client.Connection.IsServerPacketModificationEnabled)
                    {
                        packet.AppendToNetworkMessage(outMessage);
                    }

                    packets.Add((currentPacket, packetPosition));
                    lastKnownPacket = currentPacket;

                    if (client.Logger.Level == Logger.LogLevel.Debug && packetData != null)
                    {
                        var len = inMessage.Position - packetPosition;
                        var data = new byte[len];
                        Array.Copy(packetData, packetPosition, data, 0, len);
                        client.Logger.Debug($"{BitConverter.ToString(data).Replace('-', ' ')}");
                    }
                }
            }
            catch (Exception ex)
            {
                // Parsing failures are helpful when looking for packet changes or trying to understand
                // the structure of a new packet. Because of that, it's better to log as much data as
                // possible and continue.
                client.Logger.Error(ex.ToString());
                client.Logger.Error($"Current position: {inMessage.Position}");
                client.Logger.Error($"Current packet: [{(byte)currentPacket:X2}:{packetPosition}]{currentPacket}");
                client.Logger.Error($"Last known packets: {string.Join(" ", packets.Select(p => "[" + ((byte)p.PacketType).ToString("X2") + ":" + p.Position + "]" + p.PacketType).ToArray())}");
                client.Logger.Error($"Data: {BitConverter.ToString(inMessage.GetData()).Replace('-', ' ')}");
            }
        }
    }
}
