using System;
using System.Collections.Generic;
using System.Linq;

using OXGaming.TibiaAPI.Constants;

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
        public event ReceivedPacketEventHandler OnReceivedClientCancelPacket;
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
        public event ReceivedPacketEventHandler OnReceivedClientBugReportPacket;
        public event ReceivedPacketEventHandler OnReceivedClientThankYouPacket;
        public event ReceivedPacketEventHandler OnReceivedClientGetOfferDescription;
        public event ReceivedPacketEventHandler OnReceivedClientStoreEventPacket;
        public event ReceivedPacketEventHandler OnReceivedClientFeatureEventPacket;
        public event ReceivedPacketEventHandler OnReceivedClientPreyActionPacket;
        public event ReceivedPacketEventHandler OnReceivedClientRequestResourceBalancePacket;
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
        public event ReceivedPacketEventHandler OnReceivedServerAmbientePacket;
        public event ReceivedPacketEventHandler OnReceivedServerGraphicalEffectPacket;
        public event ReceivedPacketEventHandler OnReceivedServerRemoveGraphicalEffectPacket;
        public event ReceivedPacketEventHandler OnReceivedServerMissileEffectPacket;
        public event ReceivedPacketEventHandler OnReceivedServerTrappersPacket;
        public event ReceivedPacketEventHandler OnReceivedServerCreatureHealthPacket;
        public event ReceivedPacketEventHandler OnReceivedServerCreatureLightPacket;
        public event ReceivedPacketEventHandler OnReceivedServerCreatureOutfitPacket;
        public event ReceivedPacketEventHandler OnReceivedServerCreatureSpeedPacket;
        public event ReceivedPacketEventHandler OnReceivedServerCreatureSkullPacket;
        public event ReceivedPacketEventHandler OnReceivedServerCreaturePartyPacket;
        public event ReceivedPacketEventHandler OnReceivedServerCreatureUnpassPacket;
        public event ReceivedPacketEventHandler OnReceivedServerCreatureMarksPacket;
        public event ReceivedPacketEventHandler OnReceivedServerCreaturePvpHelpersPacket;
        public event ReceivedPacketEventHandler OnReceivedServerCreatureTypePacket;
        public event ReceivedPacketEventHandler OnReceivedServerEditTextPacket;
        public event ReceivedPacketEventHandler OnReceivedServerEditListPacket;
        public event ReceivedPacketEventHandler OnReceivedServerShowGameNewsPacket;
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
        public event ReceivedPacketEventHandler OnReceivedServerOpenOwnChannelPacket;
        public event ReceivedPacketEventHandler OnReceivedServerCloseChannelPacket;
        public event ReceivedPacketEventHandler OnReceivedServerMessagePacket;
        public event ReceivedPacketEventHandler OnReceivedServerSnapBackPacket;
        public event ReceivedPacketEventHandler OnReceivedServerWaitPacket;
        public event ReceivedPacketEventHandler OnReceivedServerUnjustifiedPointsPacket;
        public event ReceivedPacketEventHandler OnReceivedServerPvpSituationsPacket;
        public event ReceivedPacketEventHandler OnReceivedServerTopFloorPacket;
        public event ReceivedPacketEventHandler OnReceivedServerBottomFloorPacket;
        public event ReceivedPacketEventHandler OnReceivedServerUpdateLootContainersPacket;
        public event ReceivedPacketEventHandler OnReceivedServerOutfitPacket;
        public event ReceivedPacketEventHandler OnReceivedServerExivaSuppressedPacket;
        public event ReceivedPacketEventHandler OnReceivedServerUpdateExivaOptionsPacket;
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
        public event ReceivedPacketEventHandler OnReceivedServerTutorialHintPacket;
        public event ReceivedPacketEventHandler OnReceivedServerCyclopediaMapDataPacket;
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
        public event ReceivedPacketEventHandler OnReceivedServerPreyRerollPricePacket;
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
        public event ReceivedPacketEventHandler OnReceivedServerStoreOffersPacket;
        public event ReceivedPacketEventHandler OnReceivedServerTransactionHistoryPacket;
        public event ReceivedPacketEventHandler OnReceivedServerStoreSuccessPacket;

        internal void ParseClientMessage(NetworkMessage inMessage, NetworkMessage outMessage)
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

            try
            {
                while (inMessage.Position < inMessage.Size)
                {
                    packetPosition = inMessage.Position;
                    currentPacket = (ClientPacketType)inMessage.PeekByte();
                    switch (currentPacket)
                    {
                        case ClientPacketType.Login:
                            {
                                var packet = new ClientPackets.Login();
                                if (packet.ParseFromNetworkMessage(inMessage))
                                {
                                    packet.Forward = OnReceivedClientLoginPacket?.Invoke(packet) ?? true;
                                    if (packet.Forward)
                                    {
                                        packet.AppendToNetworkMessage(outMessage);
                                    }
                                }
                            }
                            break;
                        case ClientPacketType.SecondaryLogin:
                            {
                                var packet = new ClientPackets.SecondaryLogin();
                                if (packet.ParseFromNetworkMessage(inMessage))
                                {
                                    packet.Forward = OnReceivedClientSecondaryLoginPacket?.Invoke(packet) ?? true;
                                    if (packet.Forward)
                                    {
                                        packet.AppendToNetworkMessage(outMessage);
                                    }
                                }
                            }
                            break;
                        case ClientPacketType.EnterWorld:
                            {
                                var packet = new ClientPackets.EnterWorld();
                                if (packet.ParseFromNetworkMessage(inMessage))
                                {
                                    packet.Forward = OnReceivedClientEnterWorldPacket?.Invoke(packet) ?? true;
                                    if (packet.Forward)
                                    {
                                        packet.AppendToNetworkMessage(outMessage);
                                    }
                                }
                            }
                            break;
                        case ClientPacketType.QuitGame:
                            {
                                var packet = new ClientPackets.QuitGame();
                                if (packet.ParseFromNetworkMessage(inMessage))
                                {
                                    packet.Forward = OnReceivedClientQuitGamePacket?.Invoke(packet) ?? true;
                                    if (packet.Forward)
                                    {
                                        packet.AppendToNetworkMessage(outMessage);
                                    }
                                }
                            }
                            break;
                        case ClientPacketType.ConnectionPingBack:
                            {
                                var packet = new ClientPackets.ConnectionPingBack();
                                if (packet.ParseFromNetworkMessage(inMessage))
                                {
                                    packet.Forward = OnReceivedClientConnectionPingBack?.Invoke(packet) ?? true;
                                    if (packet.Forward)
                                    {
                                        packet.AppendToNetworkMessage(outMessage);
                                    }
                                }
                            }
                            break;
                        case ClientPacketType.Ping:
                            {
                                var packet = new ClientPackets.Ping();
                                if (packet.ParseFromNetworkMessage(inMessage))
                                {
                                    packet.Forward = OnReceivedClientPingPacket?.Invoke(packet) ?? true;
                                    if (packet.Forward)
                                    {
                                        packet.AppendToNetworkMessage(outMessage);
                                    }
                                }
                            }
                            break;
                        case ClientPacketType.PingBack:
                            {
                                var packet = new ClientPackets.PingBack();
                                if (packet.ParseFromNetworkMessage(inMessage))
                                {
                                    packet.Forward = OnReceivedClientPingBackPacket?.Invoke(packet) ?? true;
                                    if (packet.Forward)
                                    {
                                        packet.AppendToNetworkMessage(outMessage);
                                    }
                                }
                            }
                            break;
                        case ClientPacketType.PerformanceMetrics:
                            {
                                var packet = new ClientPackets.PerformanceMetrics();
                                if (packet.ParseFromNetworkMessage(inMessage))
                                {
                                    packet.Forward = OnReceivedClientPerformanceMetricsPacket?.Invoke(packet) ?? true;
                                    if (packet.Forward)
                                    {
                                        packet.AppendToNetworkMessage(outMessage);
                                    }
                                }
                            }
                            break;
                        case ClientPacketType.StashAction:
                            {
                                var packet = new ClientPackets.StashAction();
                                if (packet.ParseFromNetworkMessage(inMessage))
                                {
                                    packet.Forward = OnReceivedClientStashActionPacket?.Invoke(packet) ?? true;
                                    if (packet.Forward)
                                    {
                                        packet.AppendToNetworkMessage(outMessage);
                                    }
                                }
                            }
                            break;
                        case ClientPacketType.ClientCheck:
                            {
                                var packet = new ClientPackets.ClientCheck();
                                if (packet.ParseFromNetworkMessage(inMessage))
                                {
                                    packet.Forward = OnReceivedClientClientCheckPacket?.Invoke(packet) ?? true;
                                    if (packet.Forward)
                                    {
                                        packet.AppendToNetworkMessage(outMessage);
                                    }
                                }
                            }
                            break;
                        case ClientPacketType.GoPath:
                            {
                                var packet = new ClientPackets.GoPath();
                                if (packet.ParseFromNetworkMessage(inMessage))
                                {
                                    packet.Forward = OnReceivedClientGoPathPacket?.Invoke(packet) ?? true;
                                    if (packet.Forward)
                                    {
                                        packet.AppendToNetworkMessage(outMessage);
                                    }
                                }
                            }
                            break;
                        case ClientPacketType.GoNorth:
                            {
                                var packet = new ClientPackets.GoNorth();
                                if (packet.ParseFromNetworkMessage(inMessage))
                                {
                                    packet.Forward = OnReceivedClientGoNorthPacket?.Invoke(packet) ?? true;
                                    if (packet.Forward)
                                    {
                                        packet.AppendToNetworkMessage(outMessage);
                                    }
                                }
                            }
                            break;
                        case ClientPacketType.GoEast:
                            {
                                var packet = new ClientPackets.GoEast();
                                if (packet.ParseFromNetworkMessage(inMessage))
                                {
                                    packet.Forward = OnReceivedClientGoEastPacket?.Invoke(packet) ?? true;
                                    if (packet.Forward)
                                    {
                                        packet.AppendToNetworkMessage(outMessage);
                                    }
                                }
                            }
                            break;
                        case ClientPacketType.GoSouth:
                            {
                                var packet = new ClientPackets.GoSouth();
                                if (packet.ParseFromNetworkMessage(inMessage))
                                {
                                    packet.Forward = OnReceivedClientGoSouthPacket?.Invoke(packet) ?? true;
                                    if (packet.Forward)
                                    {
                                        packet.AppendToNetworkMessage(outMessage);
                                    }
                                }
                            }
                            break;
                        case ClientPacketType.GoWest:
                            {
                                var packet = new ClientPackets.GoWest();
                                if (packet.ParseFromNetworkMessage(inMessage))
                                {
                                    packet.Forward = OnReceivedClientGoWestPacket?.Invoke(packet) ?? true;
                                    if (packet.Forward)
                                    {
                                        packet.AppendToNetworkMessage(outMessage);
                                    }
                                }
                            }
                            break;
                        case ClientPacketType.Stop:
                            {
                                var packet = new ClientPackets.Stop();
                                if (packet.ParseFromNetworkMessage(inMessage))
                                {
                                    packet.Forward = OnReceivedClientStopPacket?.Invoke(packet) ?? true;
                                    if (packet.Forward)
                                    {
                                        packet.AppendToNetworkMessage(outMessage);
                                    }
                                }
                            }
                            break;
                        case ClientPacketType.GoNorthEast:
                            {
                                var packet = new ClientPackets.GoNorthEast();
                                if (packet.ParseFromNetworkMessage(inMessage))
                                {
                                    packet.Forward = OnReceivedClientGoNorthEastPacket?.Invoke(packet) ?? true;
                                    if (packet.Forward)
                                    {
                                        packet.AppendToNetworkMessage(outMessage);
                                    }
                                }
                            }
                            break;
                        case ClientPacketType.GoSouthEast:
                            {
                                var packet = new ClientPackets.GoSouthEast();
                                if (packet.ParseFromNetworkMessage(inMessage))
                                {
                                    packet.Forward = OnReceivedClientGoSouthEastPacket?.Invoke(packet) ?? true;
                                    if (packet.Forward)
                                    {
                                        packet.AppendToNetworkMessage(outMessage);
                                    }
                                }
                            }
                            break;
                        case ClientPacketType.GoSouthWest:
                            {
                                var packet = new ClientPackets.GoSouthWest();
                                if (packet.ParseFromNetworkMessage(inMessage))
                                {
                                    packet.Forward = OnReceivedClientGoSouthWestPacket?.Invoke(packet) ?? true;
                                    if (packet.Forward)
                                    {
                                        packet.AppendToNetworkMessage(outMessage);
                                    }
                                }
                            }
                            break;
                        case ClientPacketType.GoNorthWest:
                            {
                                var packet = new ClientPackets.GoNorthWest();
                                if (packet.ParseFromNetworkMessage(inMessage))
                                {
                                    packet.Forward = OnReceivedClientGoNorthWestPacket?.Invoke(packet) ?? true;
                                    if (packet.Forward)
                                    {
                                        packet.AppendToNetworkMessage(outMessage);
                                    }
                                }
                            }
                            break;
                        case ClientPacketType.RotateNorth:
                            {
                                var packet = new ClientPackets.RotateNorth();
                                if (packet.ParseFromNetworkMessage(inMessage))
                                {
                                    packet.Forward = OnReceivedClientRotateNorthPacket?.Invoke(packet) ?? true;
                                    if (packet.Forward)
                                    {
                                        packet.AppendToNetworkMessage(outMessage);
                                    }
                                }
                            }
                            break;
                        case ClientPacketType.RotateEast:
                            {
                                var packet = new ClientPackets.RotateEast();
                                if (packet.ParseFromNetworkMessage(inMessage))
                                {
                                    packet.Forward = OnReceivedClientRotateEastPacket?.Invoke(packet) ?? true;
                                    if (packet.Forward)
                                    {
                                        packet.AppendToNetworkMessage(outMessage);
                                    }
                                }
                            }
                            break;
                        case ClientPacketType.RotateSouth:
                            {
                                var packet = new ClientPackets.RotateSouth();
                                if (packet.ParseFromNetworkMessage(inMessage))
                                {
                                    packet.Forward = OnReceivedClientRotateSouthPacket?.Invoke(packet) ?? true;
                                    if (packet.Forward)
                                    {
                                        packet.AppendToNetworkMessage(outMessage);
                                    }
                                }
                            }
                            break;
                        case ClientPacketType.RotateWest:
                            {
                                var packet = new ClientPackets.RotateWest();
                                if (packet.ParseFromNetworkMessage(inMessage))
                                {
                                    packet.Forward = OnReceivedClientRotateWestPacket?.Invoke(packet) ?? true;
                                    if (packet.Forward)
                                    {
                                        packet.AppendToNetworkMessage(outMessage);
                                    }
                                }
                            }
                            break;
                        case ClientPacketType.Teleport:
                            {
                                var packet = new ClientPackets.Teleport();
                                if (packet.ParseFromNetworkMessage(inMessage))
                                {
                                    packet.Forward = OnReceivedClientTeleportPacket?.Invoke(packet) ?? true;
                                    if (packet.Forward)
                                    {
                                        packet.AppendToNetworkMessage(outMessage);
                                    }
                                }
                            }
                            break;
                        case ClientPacketType.EquipObject:
                            {
                                var packet = new ClientPackets.EquipObject();
                                if (packet.ParseFromNetworkMessage(inMessage))
                                {
                                    packet.Forward = OnReceivedClientEquipObjectPacket?.Invoke(packet) ?? true;
                                    if (packet.Forward)
                                    {
                                        packet.AppendToNetworkMessage(outMessage);
                                    }
                                }
                            }
                            break;
                        case ClientPacketType.MoveObject:
                            {
                                var packet = new ClientPackets.MoveObject();
                                if (packet.ParseFromNetworkMessage(inMessage))
                                {
                                    packet.Forward = OnReceivedClientMoveObjectPacket?.Invoke(packet) ?? true;
                                    if (packet.Forward)
                                    {
                                        packet.AppendToNetworkMessage(outMessage);
                                    }
                                }
                            }
                            break;
                        case ClientPacketType.LookNpcTrade:
                            {
                                var packet = new ClientPackets.LookNpcTrade();
                                if (packet.ParseFromNetworkMessage(inMessage))
                                {
                                    packet.Forward = OnReceivedClientLookNpcTradePacket?.Invoke(packet) ?? true;
                                    if (packet.Forward)
                                    {
                                        packet.AppendToNetworkMessage(outMessage);
                                    }
                                }
                            }
                            break;
                        case ClientPacketType.BuyObject:
                            {
                                var packet = new ClientPackets.BuyObject();
                                if (packet.ParseFromNetworkMessage(inMessage))
                                {
                                    packet.Forward = OnReceivedClientBuyObjectPacket?.Invoke(packet) ?? true;
                                    if (packet.Forward)
                                    {
                                        packet.AppendToNetworkMessage(outMessage);
                                    }
                                }
                            }
                            break;
                        case ClientPacketType.SellObject:
                            {
                                var packet = new ClientPackets.SellObject();
                                if (packet.ParseFromNetworkMessage(inMessage))
                                {
                                    packet.Forward = OnReceivedClientSellObjectPacket?.Invoke(packet) ?? true;
                                    if (packet.Forward)
                                    {
                                        packet.AppendToNetworkMessage(outMessage);
                                    }
                                }
                            }
                            break;
                        case ClientPacketType.CloseNpcTrade:
                            {
                                var packet = new ClientPackets.CloseNpcTrade();
                                if (packet.ParseFromNetworkMessage(inMessage))
                                {
                                    packet.Forward = OnReceivedClientCloseNpcTradePacket?.Invoke(packet) ?? true;
                                    if (packet.Forward)
                                    {
                                        packet.AppendToNetworkMessage(outMessage);
                                    }
                                }
                            }
                            break;
                        case ClientPacketType.TradeObject:
                            {
                                var packet = new ClientPackets.TradeObject();
                                if (packet.ParseFromNetworkMessage(inMessage))
                                {
                                    packet.Forward = OnReceivedClientTradeObjectPacket?.Invoke(packet) ?? true;
                                    if (packet.Forward)
                                    {
                                        packet.AppendToNetworkMessage(outMessage);
                                    }
                                }
                            }
                            break;
                        case ClientPacketType.LookTrade:
                            {
                                var packet = new ClientPackets.LookTrade();
                                if (packet.ParseFromNetworkMessage(inMessage))
                                {
                                    packet.Forward = OnReceivedClientLookTradePacket?.Invoke(packet) ?? true;
                                    if (packet.Forward)
                                    {
                                        packet.AppendToNetworkMessage(outMessage);
                                    }
                                }
                            }
                            break;
                        case ClientPacketType.AcceptTrade:
                            {
                                var packet = new ClientPackets.AcceptTrade();
                                if (packet.ParseFromNetworkMessage(inMessage))
                                {
                                    packet.Forward = OnReceivedClientAcceptTradePacket?.Invoke(packet) ?? true;
                                    if (packet.Forward)
                                    {
                                        packet.AppendToNetworkMessage(outMessage);
                                    }
                                }
                            }
                            break;
                        case ClientPacketType.RejectTrade:
                            {
                                var packet = new ClientPackets.RejectTrade();
                                if (packet.ParseFromNetworkMessage(inMessage))
                                {
                                    packet.Forward = OnReceivedClientRejectTradePacket?.Invoke(packet) ?? true;
                                    if (packet.Forward)
                                    {
                                        packet.AppendToNetworkMessage(outMessage);
                                    }
                                }
                            }
                            break;
                        case ClientPacketType.UseObject:
                            {
                                var packet = new ClientPackets.UseObject();
                                if (packet.ParseFromNetworkMessage(inMessage))
                                {
                                    packet.Forward = OnReceivedClientUseObjectPacket?.Invoke(packet) ?? true;
                                    if (packet.Forward)
                                    {
                                        packet.AppendToNetworkMessage(outMessage);
                                    }
                                }
                            }
                            break;
                        case ClientPacketType.UseTwoObjects:
                            {
                                var packet = new ClientPackets.UseTwoObjects();
                                if (packet.ParseFromNetworkMessage(inMessage))
                                {
                                    packet.Forward = OnReceivedClientUseTwoObjectsPacket?.Invoke(packet) ?? true;
                                    if (packet.Forward)
                                    {
                                        packet.AppendToNetworkMessage(outMessage);
                                    }
                                }
                            }
                            break;
                        case ClientPacketType.UseOnCreature:
                            {
                                var packet = new ClientPackets.UseOnCreature();
                                if (packet.ParseFromNetworkMessage(inMessage))
                                {
                                    packet.Forward = OnReceivedClientUseOnCreaturePacket?.Invoke(packet) ?? true;
                                    if (packet.Forward)
                                    {
                                        packet.AppendToNetworkMessage(outMessage);
                                    }
                                }
                            }
                            break;
                        case ClientPacketType.TurnObject:
                            {
                                var packet = new ClientPackets.TurnObject();
                                if (packet.ParseFromNetworkMessage(inMessage))
                                {
                                    packet.Forward = OnReceivedClientTurnObjectPacket?.Invoke(packet) ?? true;
                                    if (packet.Forward)
                                    {
                                        packet.AppendToNetworkMessage(outMessage);
                                    }
                                }
                            }
                            break;
                        case ClientPacketType.CloseContainer:
                            {
                                var packet = new ClientPackets.CloseContainer();
                                if (packet.ParseFromNetworkMessage(inMessage))
                                {
                                    packet.Forward = OnReceivedClientCloseContainerPacket?.Invoke(packet) ?? true;
                                    if (packet.Forward)
                                    {
                                        packet.AppendToNetworkMessage(outMessage);
                                    }
                                }
                            }
                            break;
                        case ClientPacketType.UpContainer:
                            {
                                var packet = new ClientPackets.UpContainer();
                                if (packet.ParseFromNetworkMessage(inMessage))
                                {
                                    packet.Forward = OnReceivedClientUpContainerPacket?.Invoke(packet) ?? true;
                                    if (packet.Forward)
                                    {
                                        packet.AppendToNetworkMessage(outMessage);
                                    }
                                }
                            }
                            break;
                        case ClientPacketType.EditText:
                            {
                                var packet = new ClientPackets.EditText();
                                if (packet.ParseFromNetworkMessage(inMessage))
                                {
                                    packet.Forward = OnReceivedClientEditTextPacket?.Invoke(packet) ?? true;
                                    if (packet.Forward)
                                    {
                                        packet.AppendToNetworkMessage(outMessage);
                                    }
                                }
                            }
                            break;
                        case ClientPacketType.EditList:
                            {
                                var packet = new ClientPackets.EditList();
                                if (packet.ParseFromNetworkMessage(inMessage))
                                {
                                    packet.Forward = OnReceivedClientEditListPacket?.Invoke(packet) ?? true;
                                    if (packet.Forward)
                                    {
                                        packet.AppendToNetworkMessage(outMessage);
                                    }
                                }
                            }
                            break;
                        case ClientPacketType.ToggleWrapState:
                            {
                                var packet = new ClientPackets.ToggleWrapState();
                                if (packet.ParseFromNetworkMessage(inMessage))
                                {
                                    packet.Forward = OnReceivedClientToggleWarpStatePacket?.Invoke(packet) ?? true;
                                    if (packet.Forward)
                                    {
                                        packet.AppendToNetworkMessage(outMessage);
                                    }
                                }
                            }
                            break;
                        case ClientPacketType.Look:
                            {
                                var packet = new ClientPackets.Look();
                                if (packet.ParseFromNetworkMessage(inMessage))
                                {
                                    packet.Forward = OnReceivedClientLookPacket?.Invoke(packet) ?? true;
                                    if (packet.Forward)
                                    {
                                        packet.AppendToNetworkMessage(outMessage);
                                    }
                                }
                            }
                            break;
                        case ClientPacketType.LookAtCreature:
                            {
                                var packet = new ClientPackets.LookAtCreature();
                                if (packet.ParseFromNetworkMessage(inMessage))
                                {
                                    packet.Forward = OnReceivedClientLookAtCreaturePacket?.Invoke(packet) ?? true;
                                    if (packet.Forward)
                                    {
                                        packet.AppendToNetworkMessage(outMessage);
                                    }
                                }
                            }
                            break;
                        case ClientPacketType.JoinAggression:
                            {
                                var packet = new ClientPackets.JoinAggression();
                                if (packet.ParseFromNetworkMessage(inMessage))
                                {
                                    packet.Forward = OnReceivedClientJoinAggressionPacket?.Invoke(packet) ?? true;
                                    if (packet.Forward)
                                    {
                                        packet.AppendToNetworkMessage(outMessage);
                                    }
                                }
                            }
                            break;
                        case ClientPacketType.QuickLoot:
                            {
                                var packet = new ClientPackets.QuickLoot();
                                if (packet.ParseFromNetworkMessage(inMessage))
                                {
                                    packet.Forward = OnReceivedClientQuickLootPacket?.Invoke(packet) ?? true;
                                    if (packet.Forward)
                                    {
                                        packet.AppendToNetworkMessage(outMessage);
                                    }
                                }
                            }
                            break;
                        case ClientPacketType.LootContainer:
                            {
                                var packet = new ClientPackets.LootContainer();
                                if (packet.ParseFromNetworkMessage(inMessage))
                                {
                                    packet.Forward = OnReceivedClientLootContainerPacket?.Invoke(packet) ?? true;
                                    if (packet.Forward)
                                    {
                                        packet.AppendToNetworkMessage(outMessage);
                                    }
                                }
                            }
                            break;
                        case ClientPacketType.QuickLootBlackWhitelist:
                            {
                                var packet = new ClientPackets.QuickLootBlackWhitelist();
                                if (packet.ParseFromNetworkMessage(inMessage))
                                {
                                    packet.Forward = OnReceivedClientQuickLootBlackWhitelistPacket?.Invoke(packet) ?? true;
                                    if (packet.Forward)
                                    {
                                        packet.AppendToNetworkMessage(outMessage);
                                    }
                                }
                            }
                            break;
                        case ClientPacketType.Talk:
                            {
                                var packet = new ClientPackets.Talk();
                                if (packet.ParseFromNetworkMessage(inMessage))
                                {
                                    packet.Forward = OnReceivedClientTalkPacket?.Invoke(packet) ?? true;
                                    if (packet.Forward)
                                    {
                                        packet.AppendToNetworkMessage(outMessage);
                                    }
                                }
                            }
                            break;
                        case ClientPacketType.GetChannels:
                            {
                                var packet = new ClientPackets.GetChannels();
                                if (packet.ParseFromNetworkMessage(inMessage))
                                {
                                    packet.Forward = OnReceivedClientGetChannelsPacket?.Invoke(packet) ?? true;
                                    if (packet.Forward)
                                    {
                                        packet.AppendToNetworkMessage(outMessage);
                                    }
                                }
                            }
                            break;
                        case ClientPacketType.JoinChannel:
                            {
                                var packet = new ClientPackets.JoinChannel();
                                if (packet.ParseFromNetworkMessage(inMessage))
                                {
                                    packet.Forward = OnReceivedClientJoinChannelPacket?.Invoke(packet) ?? true;
                                    if (packet.Forward)
                                    {
                                        packet.AppendToNetworkMessage(outMessage);
                                    }
                                }
                            }
                            break;
                        case ClientPacketType.LeaveChannel:
                            {
                                var packet = new ClientPackets.LeaveChannel();
                                if (packet.ParseFromNetworkMessage(inMessage))
                                {
                                    packet.Forward = OnReceivedClientLeaveChannelPacket?.Invoke(packet) ?? true;
                                    if (packet.Forward)
                                    {
                                        packet.AppendToNetworkMessage(outMessage);
                                    }
                                }
                            }
                            break;
                        case ClientPacketType.PrivateChannel:
                            {
                                var packet = new ClientPackets.PrivateChannel();
                                if (packet.ParseFromNetworkMessage(inMessage))
                                {
                                    packet.Forward = OnReceivedClientPrivateChannelPacket?.Invoke(packet) ?? true;
                                    if (packet.Forward)
                                    {
                                        packet.AppendToNetworkMessage(outMessage);
                                    }
                                }
                            }
                            break;
                        case ClientPacketType.GuildMessage:
                            {
                                var packet = new ClientPackets.GuildMessage();
                                if (packet.ParseFromNetworkMessage(inMessage))
                                {
                                    packet.Forward = OnReceivedClientGuildMessagePacket?.Invoke(packet) ?? true;
                                    if (packet.Forward)
                                    {
                                        packet.AppendToNetworkMessage(outMessage);
                                    }
                                }
                            }
                            break;
                        case ClientPacketType.EditGuildMessage:
                            {
                                var packet = new ClientPackets.EditGuildMessage();
                                if (packet.ParseFromNetworkMessage(inMessage))
                                {
                                    packet.Forward = OnReceivedClientEditGuildMessagePacket?.Invoke(packet) ?? true;
                                    if (packet.Forward)
                                    {
                                        packet.AppendToNetworkMessage(outMessage);
                                    }
                                }
                            }
                            break;
                        case ClientPacketType.CloseNpcChannel:
                            {
                                var packet = new ClientPackets.CloseNpcChannel();
                                if (packet.ParseFromNetworkMessage(inMessage))
                                {
                                    packet.Forward = OnReceivedClientCloseNpcChannelPacket?.Invoke(packet) ?? true;
                                    if (packet.Forward)
                                    {
                                        packet.AppendToNetworkMessage(outMessage);
                                    }
                                }
                            }
                            break;
                        case ClientPacketType.SetTactics:
                            {
                                var packet = new ClientPackets.SetTactics();
                                if (packet.ParseFromNetworkMessage(inMessage))
                                {
                                    packet.Forward = OnReceivedClientSetTacticsPacket?.Invoke(packet) ?? true;
                                    if (packet.Forward)
                                    {
                                        packet.AppendToNetworkMessage(outMessage);
                                    }
                                }
                            }
                            break;
                        case ClientPacketType.Attack:
                            {
                                var packet = new ClientPackets.Attack();
                                if (packet.ParseFromNetworkMessage(inMessage))
                                {
                                    packet.Forward = OnReceivedClientAttackPacket?.Invoke(packet) ?? true;
                                    if (packet.Forward)
                                    {
                                        packet.AppendToNetworkMessage(outMessage);
                                    }
                                }
                            }
                            break;
                        case ClientPacketType.Follow:
                            {
                                var packet = new ClientPackets.Follow();
                                if (packet.ParseFromNetworkMessage(inMessage))
                                {
                                    packet.Forward = OnReceivedClientFollowPacket?.Invoke(packet) ?? true;
                                    if (packet.Forward)
                                    {
                                        packet.AppendToNetworkMessage(outMessage);
                                    }
                                }
                            }
                            break;
                        case ClientPacketType.InviteToParty:
                            {
                                var packet = new ClientPackets.InviteToParty();
                                if (packet.ParseFromNetworkMessage(inMessage))
                                {
                                    packet.Forward = OnReceivedClientInviteToPartyPacket?.Invoke(packet) ?? true;
                                    if (packet.Forward)
                                    {
                                        packet.AppendToNetworkMessage(outMessage);
                                    }
                                }
                            }
                            break;
                        case ClientPacketType.JoinParty:
                            {
                                var packet = new ClientPackets.JoinParty();
                                if (packet.ParseFromNetworkMessage(inMessage))
                                {
                                    packet.Forward = OnReceivedClientJoinPartyPacket?.Invoke(packet) ?? true;
                                    if (packet.Forward)
                                    {
                                        packet.AppendToNetworkMessage(outMessage);
                                    }
                                }
                            }
                            break;
                        case ClientPacketType.RevokeInvitation:
                            {
                                var packet = new ClientPackets.RevokeInvitation();
                                if (packet.ParseFromNetworkMessage(inMessage))
                                {
                                    packet.Forward = OnReceivedClientRevokeInvitationPacket?.Invoke(packet) ?? true;
                                    if (packet.Forward)
                                    {
                                        packet.AppendToNetworkMessage(outMessage);
                                    }
                                }
                            }
                            break;
                        case ClientPacketType.PassLeadership:
                            {
                                var packet = new ClientPackets.PassLeadership();
                                if (packet.ParseFromNetworkMessage(inMessage))
                                {
                                    packet.Forward = OnReceivedClientPassLeadershipPacket?.Invoke(packet) ?? true;
                                    if (packet.Forward)
                                    {
                                        packet.AppendToNetworkMessage(outMessage);
                                    }
                                }
                            }
                            break;
                        case ClientPacketType.LeaveParty:
                            {
                                var packet = new ClientPackets.LeaveParty();
                                if (packet.ParseFromNetworkMessage(inMessage))
                                {
                                    packet.Forward = OnReceivedClientLeavePartyPacket?.Invoke(packet) ?? true;
                                    if (packet.Forward)
                                    {
                                        packet.AppendToNetworkMessage(outMessage);
                                    }
                                }
                            }
                            break;
                        case ClientPacketType.ShareExperience:
                            {
                                var packet = new ClientPackets.ShareExperience();
                                if (packet.ParseFromNetworkMessage(inMessage))
                                {
                                    packet.Forward = OnReceivedClientShareExperiencePacket?.Invoke(packet) ?? true;
                                    if (packet.Forward)
                                    {
                                        packet.AppendToNetworkMessage(outMessage);
                                    }
                                }
                            }
                            break;
                        case ClientPacketType.DisbandParty:
                            {
                                var packet = new ClientPackets.DisbandParty();
                                if (packet.ParseFromNetworkMessage(inMessage))
                                {
                                    packet.Forward = OnReceivedClientDisbandPartyPacket?.Invoke(packet) ?? true;
                                    if (packet.Forward)
                                    {
                                        packet.AppendToNetworkMessage(outMessage);
                                    }
                                }
                            }
                            break;
                        case ClientPacketType.OpenChannel:
                            {
                                var packet = new ClientPackets.OpenChannel();
                                if (packet.ParseFromNetworkMessage(inMessage))
                                {
                                    packet.Forward = OnReceivedClientOpenChannelPacket?.Invoke(packet) ?? true;
                                    if (packet.Forward)
                                    {
                                        packet.AppendToNetworkMessage(outMessage);
                                    }
                                }
                            }
                            break;
                        case ClientPacketType.InviteToChannel:
                            {
                                var packet = new ClientPackets.InviteToChannel();
                                if (packet.ParseFromNetworkMessage(inMessage))
                                {
                                    packet.Forward = OnReceivedClientInviteToChannelPacket?.Invoke(packet) ?? true;
                                    if (packet.Forward)
                                    {
                                        packet.AppendToNetworkMessage(outMessage);
                                    }
                                }
                            }
                            break;
                        case ClientPacketType.ExcludeFromChannel:
                            {
                                var packet = new ClientPackets.ExcludeFromChannel();
                                if (packet.ParseFromNetworkMessage(inMessage))
                                {
                                    packet.Forward = OnReceivedClientExcludeFromChannelPacket?.Invoke(packet) ?? true;
                                    if (packet.Forward)
                                    {
                                        packet.AppendToNetworkMessage(outMessage);
                                    }
                                }
                            }
                            break;
                        case ClientPacketType.Cancel:
                            {
                                var packet = new ClientPackets.Cancel();
                                if (packet.ParseFromNetworkMessage(inMessage))
                                {
                                    packet.Forward = OnReceivedClientCancelPacket?.Invoke(packet) ?? true;
                                    if (packet.Forward)
                                    {
                                        packet.AppendToNetworkMessage(outMessage);
                                    }
                                }
                            }
                            break;
                        case ClientPacketType.UpdateExivaOptions:
                            {
                                var packet = new ClientPackets.UpdateExivaOptions();
                                if (packet.ParseFromNetworkMessage(inMessage))
                                {
                                    packet.Forward = OnReceivedClientUpdateExivaOptionsPacket?.Invoke(packet) ?? true;
                                    if (packet.Forward)
                                    {
                                        packet.AppendToNetworkMessage(outMessage);
                                    }
                                }
                            }
                            break;
                        case ClientPacketType.BrowseField:
                            {
                                var packet = new ClientPackets.BrowseField();
                                if (packet.ParseFromNetworkMessage(inMessage))
                                {
                                    packet.Forward = OnReceivedClientBrowseFieldPacket?.Invoke(packet) ?? true;
                                    if (packet.Forward)
                                    {
                                        packet.AppendToNetworkMessage(outMessage);
                                    }
                                }
                            }
                            break;
                        case ClientPacketType.SeekInContainer:
                            {
                                var packet = new ClientPackets.SeekInContainer();
                                if (packet.ParseFromNetworkMessage(inMessage))
                                {
                                    packet.Forward = OnReceivedClientSeekInContainerPacket?.Invoke(packet) ?? true;
                                    if (packet.Forward)
                                    {
                                        packet.AppendToNetworkMessage(outMessage);
                                    }
                                }
                            }
                            break;
                        case ClientPacketType.InspectObject:
                            {
                                var packet = new ClientPackets.InspectObject();
                                if (packet.ParseFromNetworkMessage(inMessage))
                                {
                                    packet.Forward = OnReceivedClientInspectObjectPacket?.Invoke(packet) ?? true;
                                    if (packet.Forward)
                                    {
                                        packet.AppendToNetworkMessage(outMessage);
                                    }
                                }
                            }
                            break;
                        case ClientPacketType.InspectPlayer:
                            {
                                var packet = new ClientPackets.InspectPlayer();
                                if (packet.ParseFromNetworkMessage(inMessage))
                                {
                                    packet.Forward = OnReceivedClientInspectPlayerPacket?.Invoke(packet) ?? true;
                                    if (packet.Forward)
                                    {
                                        packet.AppendToNetworkMessage(outMessage);
                                    }
                                }
                            }
                            break;
                        case ClientPacketType.BlessingsDialog:
                            {
                                var packet = new ClientPackets.BlessingsDialog();
                                if (packet.ParseFromNetworkMessage(inMessage))
                                {
                                    packet.Forward = OnReceivedClientBlessingsDialogPacket?.Invoke(packet) ?? true;
                                    if (packet.Forward)
                                    {
                                        packet.AppendToNetworkMessage(outMessage);
                                    }
                                }
                            }
                            break;
                        case ClientPacketType.TrackQuestFlags:
                            {
                                var packet = new ClientPackets.TrackQuestFlags();
                                if (packet.ParseFromNetworkMessage(inMessage))
                                {
                                    packet.Forward = OnReceivedClientTrackQuestFlagsPacket?.Invoke(packet) ?? true;
                                    if (packet.Forward)
                                    {
                                        packet.AppendToNetworkMessage(outMessage);
                                    }
                                }
                            }
                            break;
                        case ClientPacketType.MarketStatistics:
                            {
                                var packet = new ClientPackets.MarketStatistics();
                                if (packet.ParseFromNetworkMessage(inMessage))
                                {
                                    packet.Forward = OnReceivedClientMarketStatisticsPacket?.Invoke(packet) ?? true;
                                    if (packet.Forward)
                                    {
                                        packet.AppendToNetworkMessage(outMessage);
                                    }
                                }
                            }
                            break;
                        case ClientPacketType.GetOutfit:
                            {
                                var packet = new ClientPackets.GetOutfit();
                                if (packet.ParseFromNetworkMessage(inMessage))
                                {
                                    packet.Forward = OnReceivedClientGetOutfitPacket?.Invoke(packet) ?? true;
                                    if (packet.Forward)
                                    {
                                        packet.AppendToNetworkMessage(outMessage);
                                    }
                                }
                            }
                            break;
                        case ClientPacketType.SetOutfit:
                            {
                                var packet = new ClientPackets.SetOutfit();
                                if (packet.ParseFromNetworkMessage(inMessage))
                                {
                                    packet.Forward = OnReceivedClientSetOutfitPacket?.Invoke(packet) ?? true;
                                    if (packet.Forward)
                                    {
                                        packet.AppendToNetworkMessage(outMessage);
                                    }
                                }
                            }
                            break;
                        case ClientPacketType.Mount:
                            {
                                var packet = new ClientPackets.Mount();
                                if (packet.ParseFromNetworkMessage(inMessage))
                                {
                                    packet.Forward = OnReceivedClientMountPacket?.Invoke(packet) ?? true;
                                    if (packet.Forward)
                                    {
                                        packet.AppendToNetworkMessage(outMessage);
                                    }
                                }
                            }
                            break;
                        case ClientPacketType.ApplyImbuement:
                            {
                                var packet = new ClientPackets.ApplyImbuement();
                                if (packet.ParseFromNetworkMessage(inMessage))
                                {
                                    packet.Forward = OnReceivedClientApplyImbuementPacket?.Invoke(packet) ?? true;
                                    if (packet.Forward)
                                    {
                                        packet.AppendToNetworkMessage(outMessage);
                                    }
                                }
                            }
                            break;
                        case ClientPacketType.ApplyClearingCharm:
                            {
                                var packet = new ClientPackets.ApplyClearingCharm();
                                if (packet.ParseFromNetworkMessage(inMessage))
                                {
                                    packet.Forward = OnReceivedClientApplyClearingCharmPacket?.Invoke(packet) ?? true;
                                    if (packet.Forward)
                                    {
                                        packet.AppendToNetworkMessage(outMessage);
                                    }
                                }
                            }
                            break;
                        case ClientPacketType.ClosedImbuingDialog:
                            {
                                var packet = new ClientPackets.ClosedImbuingDialog();
                                if (packet.ParseFromNetworkMessage(inMessage))
                                {
                                    packet.Forward = OnReceivedClientClosedImbuingDialogPacket?.Invoke(packet) ?? true;
                                    if (packet.Forward)
                                    {
                                        packet.AppendToNetworkMessage(outMessage);
                                    }
                                }
                            }
                            break;
                        case ClientPacketType.OpenRewardWall:
                            {
                                var packet = new ClientPackets.OpenRewardWall();
                                if (packet.ParseFromNetworkMessage(inMessage))
                                {
                                    packet.Forward = OnReceivedClientOpenRewardWallPacket?.Invoke(packet) ?? true;
                                    if (packet.Forward)
                                    {
                                        packet.AppendToNetworkMessage(outMessage);
                                    }
                                }
                            }
                            break;
                        case ClientPacketType.DailyRewardHistory:
                            {
                                var packet = new ClientPackets.DailyRewardHistory();
                                if (packet.ParseFromNetworkMessage(inMessage))
                                {
                                    packet.Forward = OnReceivedClientDailyRewardHistoryPacket?.Invoke(packet) ?? true;
                                    if (packet.Forward)
                                    {
                                        packet.AppendToNetworkMessage(outMessage);
                                    }
                                }
                            }
                            break;
                        case ClientPacketType.CollectDailyReward:
                            {
                                var packet = new ClientPackets.CollectDailyReward();
                                if (packet.ParseFromNetworkMessage(inMessage))
                                {
                                    packet.Forward = OnReceivedClientCollectDailyRewardPacket?.Invoke(packet) ?? true;
                                    if (packet.Forward)
                                    {
                                        packet.AppendToNetworkMessage(outMessage);
                                    }
                                }
                            }
                            break;
                        case ClientPacketType.CyclopediaMapAction:
                            {
                                var packet = new ClientPackets.CyclopediaMapAction();
                                if (packet.ParseFromNetworkMessage(inMessage))
                                {
                                    packet.Forward = OnReceivedClientCyclopediaMapActionPacket?.Invoke(packet) ?? true;
                                    if (packet.Forward)
                                    {
                                        packet.AppendToNetworkMessage(outMessage);
                                    }
                                }
                            }
                            break;
                        case ClientPacketType.AddBuddy:
                            {
                                var packet = new ClientPackets.AddBuddy();
                                if (packet.ParseFromNetworkMessage(inMessage))
                                {
                                    packet.Forward = OnReceivedClientAddBuddyPacket?.Invoke(packet) ?? true;
                                    if (packet.Forward)
                                    {
                                        packet.AppendToNetworkMessage(outMessage);
                                    }
                                }
                            }
                            break;
                        case ClientPacketType.RemoveBuddy:
                            {
                                var packet = new ClientPackets.RemoveBuddy();
                                if (packet.ParseFromNetworkMessage(inMessage))
                                {
                                    packet.Forward = OnReceivedClientRemoveBuddyPacket?.Invoke(packet) ?? true;
                                    if (packet.Forward)
                                    {
                                        packet.AppendToNetworkMessage(outMessage);
                                    }
                                }
                            }
                            break;
                        case ClientPacketType.EditBuddy:
                            {
                                var packet = new ClientPackets.EditBuddy();
                                if (packet.ParseFromNetworkMessage(inMessage))
                                {
                                    packet.Forward = OnReceivedClientEditBuddyPacket?.Invoke(packet) ?? true;
                                    if (packet.Forward)
                                    {
                                        packet.AppendToNetworkMessage(outMessage);
                                    }
                                }
                            }
                            break;
                        case ClientPacketType.BuddyGroup:
                            {
                                var packet = new ClientPackets.BuddyGroup();
                                if (packet.ParseFromNetworkMessage(inMessage))
                                {
                                    packet.Forward = OnReceivedClientBuddyGroupPacket?.Invoke(packet) ?? true;
                                    if (packet.Forward)
                                    {
                                        packet.AppendToNetworkMessage(outMessage);
                                    }
                                }
                            }
                            break;
                        case ClientPacketType.MarkGameNewsAsRead:
                            {
                                var packet = new ClientPackets.MarkGameNewsAsRead();
                                if (packet.ParseFromNetworkMessage(inMessage))
                                {
                                    packet.Forward = OnReceivedClientMarkGameNewsAsReadPacket?.Invoke(packet) ?? true;
                                    if (packet.Forward)
                                    {
                                        packet.AppendToNetworkMessage(outMessage);
                                    }
                                }
                            }
                            break;
                        case ClientPacketType.OpenMonsterCyclopedia:
                            {
                                var packet = new ClientPackets.OpenMonsterCyclopedia();
                                if (packet.ParseFromNetworkMessage(inMessage))
                                {
                                    packet.Forward = OnReceivedClientOpenMonsterCyclopediaPacket?.Invoke(packet) ?? true;
                                    if (packet.Forward)
                                    {
                                        packet.AppendToNetworkMessage(outMessage);
                                    }
                                }
                            }
                            break;
                        case ClientPacketType.OpenMonsterCyclopediaMonsters:
                            {
                                var packet = new ClientPackets.OpenMonsterCyclopediaMonsters();
                                if (packet.ParseFromNetworkMessage(inMessage))
                                {
                                    packet.Forward = OnReceivedClientOpenMonsterCyclopediaMonstersPacket?.Invoke(packet) ?? true;
                                    if (packet.Forward)
                                    {
                                        packet.AppendToNetworkMessage(outMessage);
                                    }
                                }
                            }
                            break;
                        case ClientPacketType.OpenMonsterCyclopediaRace:
                            {
                                var packet = new ClientPackets.OpenMonsterCyclopediaRace();
                                if (packet.ParseFromNetworkMessage(inMessage))
                                {
                                    packet.Forward = OnReceivedClientOpenMonsterCyclopediaRacePacket?.Invoke(packet) ?? true;
                                    if (packet.Forward)
                                    {
                                        packet.AppendToNetworkMessage(outMessage);
                                    }
                                }
                            }
                            break;
                        case ClientPacketType.MonsterBonusEffectAction:
                            {
                                var packet = new ClientPackets.MonsterBonusEffectAction();
                                if (packet.ParseFromNetworkMessage(inMessage))
                                {
                                    packet.Forward = OnReceivedClientMonsterBonusEffectActionPacket?.Invoke(packet) ?? true;
                                    if (packet.Forward)
                                    {
                                        packet.AppendToNetworkMessage(outMessage);
                                    }
                                }
                            }
                            break;
                        case ClientPacketType.BugReport:
                            {
                                var packet = new ClientPackets.BugReport();
                                if (packet.ParseFromNetworkMessage(inMessage))
                                {
                                    packet.Forward = OnReceivedClientBugReportPacket?.Invoke(packet) ?? true;
                                    if (packet.Forward)
                                    {
                                        packet.AppendToNetworkMessage(outMessage);
                                    }
                                }
                            }
                            break;
                        case ClientPacketType.ThankYou:
                            {
                                var packet = new ClientPackets.ThankYou();
                                if (packet.ParseFromNetworkMessage(inMessage))
                                {
                                    packet.Forward = OnReceivedClientThankYouPacket?.Invoke(packet) ?? true;
                                    if (packet.Forward)
                                    {
                                        packet.AppendToNetworkMessage(outMessage);
                                    }
                                }
                            }
                            break;
                        case ClientPacketType.GetOfferDescription:
                            {
                                var packet = new ClientPackets.GetOfferDescription();
                                if (packet.ParseFromNetworkMessage(inMessage))
                                {
                                    packet.Forward = OnReceivedClientGetOfferDescription?.Invoke(packet) ?? true;
                                    if (packet.Forward)
                                    {
                                        packet.AppendToNetworkMessage(outMessage);
                                    }
                                }
                            }
                            break;
                        case ClientPacketType.StoreEvent:
                            {
                                var packet = new ClientPackets.StoreEvent();
                                if (packet.ParseFromNetworkMessage(inMessage))
                                {
                                    packet.Forward = OnReceivedClientStoreEventPacket?.Invoke(packet) ?? true;
                                    if (packet.Forward)
                                    {
                                        packet.AppendToNetworkMessage(outMessage);
                                    }
                                }
                            }
                            break;
                        case ClientPacketType.FeatureEvent:
                            {
                                var packet = new ClientPackets.FeatureEvent();
                                if (packet.ParseFromNetworkMessage(inMessage))
                                {
                                    packet.Forward = OnReceivedClientFeatureEventPacket?.Invoke(packet) ?? true;
                                    if (packet.Forward)
                                    {
                                        packet.AppendToNetworkMessage(outMessage);
                                    }
                                }
                            }
                            break;
                        case ClientPacketType.PreyAction:
                            {
                                var packet = new ClientPackets.PreyAction();
                                if (packet.ParseFromNetworkMessage(inMessage))
                                {
                                    packet.Forward = OnReceivedClientPreyActionPacket?.Invoke(packet) ?? true;
                                    if (packet.Forward)
                                    {
                                        packet.AppendToNetworkMessage(outMessage);
                                    }
                                }
                            }
                            break;
                        case ClientPacketType.RequestResourceBalance:
                            {
                                var packet = new ClientPackets.RequestResourceBalance();
                                if (packet.ParseFromNetworkMessage(inMessage))
                                {
                                    packet.Forward = OnReceivedClientRequestResourceBalancePacket?.Invoke(packet) ?? true;
                                    if (packet.Forward)
                                    {
                                        packet.AppendToNetworkMessage(outMessage);
                                    }
                                }
                            }
                            break;
                        case ClientPacketType.TransferCurrency:
                            {
                                var packet = new ClientPackets.TransferCurrency();
                                if (packet.ParseFromNetworkMessage(inMessage))
                                {
                                    packet.Forward = OnReceivedClientTransferCurrencyPacket?.Invoke(packet) ?? true;
                                    if (packet.Forward)
                                    {
                                        packet.AppendToNetworkMessage(outMessage);
                                    }
                                }
                            }
                            break;
                        case ClientPacketType.GetQuestLog:
                            {
                                var packet = new ClientPackets.GetQuestLog();
                                if (packet.ParseFromNetworkMessage(inMessage))
                                {
                                    packet.Forward = OnReceivedClientGetQuestLogPacket?.Invoke(packet) ?? true;
                                    if (packet.Forward)
                                    {
                                        packet.AppendToNetworkMessage(outMessage);
                                    }
                                }
                            }
                            break;
                        case ClientPacketType.GetQuestLine:
                            {
                                var packet = new ClientPackets.GetQuestLine();
                                if (packet.ParseFromNetworkMessage(inMessage))
                                {
                                    packet.Forward = OnReceivedClientGetQuestLinePacket?.Invoke(packet) ?? true;
                                    if (packet.Forward)
                                    {
                                        packet.AppendToNetworkMessage(outMessage);
                                    }
                                }
                            }
                            break;
                        case ClientPacketType.RuleViolationReport:
                            {
                                var packet = new ClientPackets.RuleViolationReport();
                                if (packet.ParseFromNetworkMessage(inMessage))
                                {
                                    packet.Forward = OnReceivedClientRuleViolationReportPacket?.Invoke(packet) ?? true;
                                    if (packet.Forward)
                                    {
                                        packet.AppendToNetworkMessage(outMessage);
                                    }
                                }
                            }
                            break;
                        case ClientPacketType.GetObjectInfo:
                            {
                                var packet = new ClientPackets.GetObjectInfo();
                                if (packet.ParseFromNetworkMessage(inMessage))
                                {
                                    packet.Forward = OnReceivedClientGetObjectInfoPacket?.Invoke(packet) ?? true;
                                    if (packet.Forward)
                                    {
                                        packet.AppendToNetworkMessage(outMessage);
                                    }
                                }
                            }
                            break;
                        case ClientPacketType.MarketLeave:
                            {
                                var packet = new ClientPackets.MarketLeave();
                                if (packet.ParseFromNetworkMessage(inMessage))
                                {
                                    packet.Forward = OnReceivedClientMarketLeavePacket?.Invoke(packet) ?? true;
                                    if (packet.Forward)
                                    {
                                        packet.AppendToNetworkMessage(outMessage);
                                    }
                                }
                            }
                            break;
                        case ClientPacketType.MarketBrowse:
                            {
                                var packet = new ClientPackets.MarketBrowse();
                                if (packet.ParseFromNetworkMessage(inMessage))
                                {
                                    packet.Forward = OnReceivedClientMarketBrowsePacket?.Invoke(packet) ?? true;
                                    if (packet.Forward)
                                    {
                                        packet.AppendToNetworkMessage(outMessage);
                                    }
                                }
                            }
                            break;
                        case ClientPacketType.MarketCreate:
                            {
                                var packet = new ClientPackets.MarketCreate();
                                if (packet.ParseFromNetworkMessage(inMessage))
                                {
                                    packet.Forward = OnReceivedClientMarketCreatePacket?.Invoke(packet) ?? true;
                                    if (packet.Forward)
                                    {
                                        packet.AppendToNetworkMessage(outMessage);
                                    }
                                }
                            }
                            break;
                        case ClientPacketType.MarketCancel:
                            {
                                var packet = new ClientPackets.MarketCancel();
                                if (packet.ParseFromNetworkMessage(inMessage))
                                {
                                    packet.Forward = OnReceivedClientMarketCancelPacket?.Invoke(packet) ?? true;
                                    if (packet.Forward)
                                    {
                                        packet.AppendToNetworkMessage(outMessage);
                                    }
                                }
                            }
                            break;
                        case ClientPacketType.MarketAccept:
                            {
                                var packet = new ClientPackets.MarketAccept();
                                if (packet.ParseFromNetworkMessage(inMessage))
                                {
                                    packet.Forward = OnReceivedClientMarketAcceptPacket?.Invoke(packet) ?? true;
                                    if (packet.Forward)
                                    {
                                        packet.AppendToNetworkMessage(outMessage);
                                    }
                                }
                            }
                            break;
                        case ClientPacketType.AnswerModalDialog:
                            {
                                var packet = new ClientPackets.AnswerModalDialog();
                                if (packet.ParseFromNetworkMessage(inMessage))
                                {
                                    packet.Forward = OnReceivedClientAnswerModalDialogPacket?.Invoke(packet) ?? true;
                                    if (packet.Forward)
                                    {
                                        packet.AppendToNetworkMessage(outMessage);
                                    }
                                }
                            }
                            break;
                        case ClientPacketType.OpenIngameShop:
                            {
                                var packet = new ClientPackets.OpenIngameShop();
                                if (packet.ParseFromNetworkMessage(inMessage))
                                {
                                    packet.Forward = OnReceivedClientOpenIngameShopPacket?.Invoke(packet) ?? true;
                                    if (packet.Forward)
                                    {
                                        packet.AppendToNetworkMessage(outMessage);
                                    }
                                }
                            }
                            break;
                        case ClientPacketType.RequestShopOffers:
                            {
                                var packet = new ClientPackets.RequestShopOffers();
                                if (packet.ParseFromNetworkMessage(inMessage))
                                {
                                    packet.Forward = OnReceivedClientRequestShopOffersPacket?.Invoke(packet) ?? true;
                                    if (packet.Forward)
                                    {
                                        packet.AppendToNetworkMessage(outMessage);
                                    }
                                }
                            }
                            break;
                        case ClientPacketType.BuyIngameShopOffer:
                            {
                                var packet = new ClientPackets.BuyIngameShopOffer();
                                if (packet.ParseFromNetworkMessage(inMessage))
                                {
                                    packet.Forward = OnReceivedClientBuyIngameShopOfferPacket?.Invoke(packet) ?? true;
                                    if (packet.Forward)
                                    {
                                        packet.AppendToNetworkMessage(outMessage);
                                    }
                                }
                            }
                            break;
                        case ClientPacketType.OpenTransactionHistory:
                            {
                                var packet = new ClientPackets.OpenTransactionHistory();
                                if (packet.ParseFromNetworkMessage(inMessage))
                                {
                                    packet.Forward = OnReceivedClientOpenTransactionHistoryPacket?.Invoke(packet) ?? true;
                                    if (packet.Forward)
                                    {
                                        packet.AppendToNetworkMessage(outMessage);
                                    }
                                }
                            }
                            break;
                        case ClientPacketType.GetTransactionHistory:
                            {
                                var packet = new ClientPackets.GetTransactionHistory();
                                if (packet.ParseFromNetworkMessage(inMessage))
                                {
                                    packet.Forward = OnReceivedClientGetTransactionHistoryPacket?.Invoke(packet) ?? true;
                                    if (packet.Forward)
                                    {
                                        packet.AppendToNetworkMessage(outMessage);
                                    }
                                }
                            }
                            break;
                        default:
                            // TODO: Log unknown packet.
                            Console.WriteLine($"Unknown client packet: {currentPacket.ToString("X")}, position: {packetPosition}");
                            Console.WriteLine($"Data: {BitConverter.ToString(inMessage.GetData()).Replace('-', ' ')}");
                            return;
                    }

                    packets.Add((currentPacket, packetPosition));
                    lastKnownPacket = currentPacket;
                }
            }
            catch (Exception ex)
            {
                // TODO: Log exception.
                // Parsing failures are helpful when looking for packet changes or trying to understand
                // the structure of a new packet. Because of that, it's better to log as much data as
                // possible and continue.
                Console.WriteLine(ex.ToString());
                Console.WriteLine($"Current packet: [{((byte)currentPacket).ToString("X2")}]{currentPacket}, " +
                    $"Last known packets: {string.Join(" ", packets.Select(p => "[" + ((byte)p.PacketType).ToString("X2") + ":" + p.Position + "]" + p.PacketType).ToArray())}");
                Console.WriteLine($"Data: {BitConverter.ToString(inMessage.GetData()).Replace('-', ' ')}");
            }
        }

        internal void ParseServerMessage(NetworkMessage inMessage, NetworkMessage outMessage)
        {
            if (inMessage == null)
            {
                throw new ArgumentNullException(nameof(inMessage));
            }

            while (inMessage.Position < inMessage.Size)
            {
                var type = (ServerPacketType)inMessage.PeekByte();
                switch (type)
                {
                    case ServerPacketType.CreatureData:
                        {
                            var packet = new ServerPackets.CreatureData();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerCreatureDataPacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.PendingStateEntered:
                        {
                            var packet = new ServerPackets.PendingStateEntered();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerPendingStateEnteredPacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.ReadyForSecondaryConnection:
                        {
                            var packet = new ServerPackets.ReadyForSecondaryConnection();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerReadyForSecondaryConnectionPacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.WorldEntered:
                        {
                            var packet = new ServerPackets.WorldEntered();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerWorldEnteredPacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.LoginError:
                        {
                            var packet = new ServerPackets.LoginError();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerLoginErrorPacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.LoginAdvice:
                        {
                            var packet = new ServerPackets.LoginAdvice();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerLoginAdvicePacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.LoginWait:
                        {
                            var packet = new ServerPackets.LoginWait();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerLoginWaitPacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.LoginSuccess:
                        {
                            var packet = new ServerPackets.LoginSuccess();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerLoginSuccessPacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.StoreButtonIndicators:
                        {
                            var packet = new ServerPackets.StoreButtonIndicators();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerStoreButtonIndicatorsPacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.Ping:
                        {
                            var packet = new ServerPackets.Ping();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerPingPacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.PingBack:
                        {
                            var packet = new ServerPackets.PingBack();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerPingBackPacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.LoginChallenge:
                        {
                            var packet = new ServerPackets.LoginChallenge();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerLoginChallengePacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.Dead:
                        {
                            var packet = new ServerPackets.Dead();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerDeadPacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.Stash:
                        {
                            var packet = new ServerPackets.Stash();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerStashPacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.DepotTileState:
                        {
                            var packet = new ServerPackets.DepotTileState();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerDepotTileStatePacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.ClientCheck:
                        {
                            var packet = new ServerPackets.ClientCheck();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerClientCheckPacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.FullMap:
                        {
                            var packet = new ServerPackets.FullMap();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerFullMapPacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.TopRow:
                        {
                            var packet = new ServerPackets.TopRow();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerTopRowPacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.RightColumn:
                        {
                            var packet = new ServerPackets.RightColumn();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerRightColumnPacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.BottomRow:
                        {
                            var packet = new ServerPackets.BottomRow();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerBottomRowPacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.LeftColumn:
                        {
                            var packet = new ServerPackets.LeftColumn();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerLeftColumnPacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.FieldData:
                        {
                            var packet = new ServerPackets.FieldData();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerFieldDataPacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.CreateOnMap:
                        {
                            var packet = new ServerPackets.CreateOnMap();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerCreateOnMapPacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.ChangeOnMap:
                        {
                            var packet = new ServerPackets.ChangeOnMap();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerChangeOnMapPacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.DeleteOnMap:
                        {
                            var packet = new ServerPackets.DeleteOnMap();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerDeleteOnMapPacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.MoveCreature:
                        {
                            var packet = new ServerPackets.MoveCreature();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerMoveCreaturePacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.Container:
                        {
                            var packet = new ServerPackets.Container();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerContainerPacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.CloseContainer:
                        {
                            var packet = new ServerPackets.CloseContainer();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerCloseContainerPacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.CreateInContainer:
                        {
                            var packet = new ServerPackets.CreateInContainer();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerCreateInContainerPacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.ChangeInContainer:
                        {
                            var packet = new ServerPackets.ChangeInContainer();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerChangeInContainerPacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.DeleteInContainer:
                        {
                            var packet = new ServerPackets.DeleteInContainer();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerDeleteInContainerPacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.ScreenshotEvent:
                        {
                            var packet = new ServerPackets.ScreenshotEvent();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerScreenshotEventPacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.InspectionList:
                        {
                            var packet = new ServerPackets.InspectionList();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerInspectionListPacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.InspectionState:
                        {
                            var packet = new ServerPackets.InspectionState();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerInspectionStatePacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.SetInventory:
                        {
                            var packet = new ServerPackets.SetInventory();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerSetInventoryPacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.DeleteInventory:
                        {
                            var packet = new ServerPackets.DeleteInventory();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerDeleteInventoryPacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.NpcOffer:
                        {
                            var packet = new ServerPackets.NpcOffer();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerNpcOfferPacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.PlayerGoods:
                        {
                            var packet = new ServerPackets.PlayerGoods();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerPlayerGoodsPacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.CloseNpcTrade:
                        {
                            var packet = new ServerPackets.CloseNpcTrade();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerCloseNpcTradePacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.OwnOffer:
                        {
                            var packet = new ServerPackets.OwnOffer();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerOwnOfferPacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.CounterOffer:
                        {
                            var packet = new ServerPackets.CounterOffer();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerCounterOfferPacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.CloseTrade:
                        {
                            var packet = new ServerPackets.CloseTrade();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerCloseTradePacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.Ambiente:
                        {
                            var packet = new ServerPackets.Ambiente();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerAmbientePacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.GraphicalEffect:
                        {
                            var packet = new ServerPackets.GraphicalEffect();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerGraphicalEffectPacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.RemoveGraphicalEffect:
                        {
                            var packet = new ServerPackets.RemoveGraphicalEffect();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerRemoveGraphicalEffectPacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.MissileEffect:
                        {
                            var packet = new ServerPackets.MissileEffect();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerMissileEffectPacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.Trappers:
                        {
                            var packet = new ServerPackets.Trappers();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerTrappersPacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.CreatureHealth:
                        {
                            var packet = new ServerPackets.CreatureHealth();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerCreatureHealthPacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.CreatureLight:
                        {
                            var packet = new ServerPackets.CreatureLight();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerCreatureLightPacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.CreatureOutfit:
                        {
                            var packet = new ServerPackets.CreatureOutfit();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerCreatureOutfitPacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.CreatureSpeed:
                        {
                            var packet = new ServerPackets.CreatureSpeed();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerCreatureSpeedPacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.CreatureSkull:
                        {
                            var packet = new ServerPackets.CreatureSkull();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerCreatureSkullPacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.CreatureParty:
                        {
                            var packet = new ServerPackets.CreatureParty();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerCreaturePartyPacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.CreatureUnpass:
                        {
                            var packet = new ServerPackets.CreatureUnpass();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerCreatureUnpassPacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.CreatureMarks:
                        {
                            var packet = new ServerPackets.CreatureMarks();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerCreatureMarksPacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.CreaturePvpHelpers:
                        {
                            var packet = new ServerPackets.CreaturePvpHelpers();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerCreaturePvpHelpersPacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.CreatureType:
                        {
                            var packet = new ServerPackets.CreatureType();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerCreatureTypePacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.EditText:
                        {
                            var packet = new ServerPackets.EditText();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerEditTextPacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.EditList:
                        {
                            var packet = new ServerPackets.EditList();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerEditListPacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.ShowGameNews:
                        {
                            var packet = new ServerPackets.ShowGameNews();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerShowGameNewsPacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.BlessingsDialog:
                        {
                            var packet = new ServerPackets.BlessingsDialog();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerBlessingsDialogPacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.Blessings:
                        {
                            var packet = new ServerPackets.Blessings();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerBlessingsPacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.SwitchPreset:
                        {
                            var packet = new ServerPackets.SwitchPreset();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerSwitchPresetPacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.PremiumTrigger:
                        {
                            var packet = new ServerPackets.PremiumTrigger();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerPremiumTriggerPacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.PlayerDataBasic:
                        {
                            var packet = new ServerPackets.PlayerDataBasic();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerPlayerDataBasicPacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.PlayerDataCurrent:
                        {
                            var packet = new ServerPackets.PlayerDataCurrent();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerPlayerDataCurrentPacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.PlayerSkills:
                        {
                            var packet = new ServerPackets.PlayerSkills();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerPlayerSkillsPacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.PlayerState:
                        {
                            var packet = new ServerPackets.PlayerState();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerPlayerStatePacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.ClearTarget:
                        {
                            var packet = new ServerPackets.ClearTarget();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerClearTargetPacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.SpellDelay:
                        {
                            var packet = new ServerPackets.SpellDelay();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerSpellDelayPacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.SpellGroupDelay:
                        {
                            var packet = new ServerPackets.SpellGroupDelay();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerSpellGroupDelayPacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.MultiUseDelay:
                        {
                            var packet = new ServerPackets.MultiUseDelay();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerMultiUseDelayPacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.SetTactics:
                        {
                            var packet = new ServerPackets.SetTactics();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerSetTacticsPacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.SetStoreDeepLink:
                        {
                            var packet = new ServerPackets.SetStoreDeepLink();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerSetStoreDeepLinkPacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.RestingAreaState:
                        {
                            var packet = new ServerPackets.RestingAreaState();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerRestingAreaStatePacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.Talk:
                        {
                            var packet = new ServerPackets.Talk();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerTalkPacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.Channels:
                        {
                            var packet = new ServerPackets.Channels();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerChannelsPacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.OpenChannel:
                        {
                            var packet = new ServerPackets.OpenChannel();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerOpenChannelPacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.PrivateChannel:
                        {
                            var packet = new ServerPackets.PrivateChannel();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerPrivateChannelPacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.EditGuildMessage:
                        {
                            var packet = new ServerPackets.EditGuildMessage();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerEditGuildMessagePacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.OpenOwnChannel:
                        {
                            var packet = new ServerPackets.OpenOwnChannel();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerOpenOwnChannelPacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.CloseChannel:
                        {
                            var packet = new ServerPackets.CloseChannel();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerCloseChannelPacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.Message:
                        {
                            var packet = new ServerPackets.Message();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerMessagePacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.SnapBack:
                        {
                            var packet = new ServerPackets.SnapBack();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerSnapBackPacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.Wait:
                        {
                            var packet = new ServerPackets.Wait();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerWaitPacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.UnjustifiedPoints:
                        {
                            var packet = new ServerPackets.UnjustifiedPoints();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerUnjustifiedPointsPacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.PvpSituations:
                        {
                            var packet = new ServerPackets.PvpSituations();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerPvpSituationsPacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.TopFloor:
                        {
                            var packet = new ServerPackets.TopFloor();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerTopFloorPacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.BottomFloor:
                        {
                            var packet = new ServerPackets.BottomFloor();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerBottomFloorPacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.UpdateLootContainers:
                        {
                            var packet = new ServerPackets.UpdateLootContainers();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerUpdateLootContainersPacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.Outfit:
                        {
                            var packet = new ServerPackets.Outfit();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerOutfitPacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.ExivaSuppressed:
                        {
                            var packet = new ServerPackets.ExivaSuppressed();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerExivaSuppressedPacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.UpdateExivaOptions:
                        {
                            var packet = new ServerPackets.UpdateExivaOptions();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerUpdateExivaOptionsPacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.ImpactTracking:
                        {
                            var packet = new ServerPackets.ImpactTracking();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerImpactTrackingPacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.MarketStatistics:
                        {
                            var packet = new ServerPackets.MarketStatistics();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerMarketStatisticsPacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.ItemWasted:
                        {
                            var packet = new ServerPackets.ItemWasted();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerItemWastedPacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.ItemLooted:
                        {
                            var packet = new ServerPackets.ItemLooted();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerItemLootedPacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.TrackQuestFlags:
                        {
                            var packet = new ServerPackets.TrackQuestFlags();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerTrackQuestFlagsPacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.KillTracking:
                        {
                            var packet = new ServerPackets.KillTracking();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerKillTrackingPacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.BuddyData:
                        {
                            var packet = new ServerPackets.BuddyData();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerBuddyDataPacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.BuddyStatusChange:
                        {
                            var packet = new ServerPackets.BuddyStatusChange();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerBuddyStatusChangePacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.BuddyGroupData:
                        {
                            var packet = new ServerPackets.BuddyGroupData();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerBuddyGroupDataPacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.MonsterCyclopedia:
                        {
                            var packet = new ServerPackets.MonsterCyclopedia();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerMonsterCyclopediaPacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.MonsterCyclopediaMonsters:
                        {
                            var packet = new ServerPackets.MonsterCyclopediaMonsters();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerMonsterCyclopediaMonstersPacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.MonsterCyclopediaRace:
                        {
                            var packet = new ServerPackets.MonsterCyclopediaRace();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerMonsterCyclopediaRacePacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.MonsterCyclopediaBonusEffects:
                        {
                            var packet = new ServerPackets.MonsterCyclopediaBonusEffects();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerMonsterCyclopediaBonusEffectsPacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.MonsterCyclopediaNewDetails:
                        {
                            var packet = new ServerPackets.MonsterCyclopediaNewDetails();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerMonsterCyclopediaNewDetailsPacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.TutorialHint:
                        {
                            var packet = new ServerPackets.TutorialHint();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerTutorialHintPacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.CyclopediaMapData:
                        {
                            var packet = new ServerPackets.CyclopediaMapData();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerCyclopediaMapDataPacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.DailyRewardCollectionState:
                        {
                            var packet = new ServerPackets.DailyRewardCollectionState();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerDailyRewardCollectionStatePacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.CreditBalance:
                        {
                            var packet = new ServerPackets.CreditBalance();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerCreditBalancePacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.IngameShopError:
                        {
                            var packet = new ServerPackets.IngameShopError();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerIngameShopErrorPacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.RequestPurchaseData:
                        {
                            var packet = new ServerPackets.RequestPurchaseData();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerRequestPurchaseDataPacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.OpenRewardWall:
                        {
                            var packet = new ServerPackets.OpenRewardWall();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerOpenRewardWallPacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.CloseRewardWall:
                        {
                            var packet = new ServerPackets.CloseRewardWall();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerCloseRewardWallPacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.DailyRewardBasic:
                        {
                            var packet = new ServerPackets.DailyRewardBasic();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerDailyRewardBasicPacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.DailyRewardHistory:
                        {
                            var packet = new ServerPackets.DailyRewardHistory();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerDailyRewardHistoryPacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.PreyFreeListRerollAvailability:
                        {
                            var packet = new ServerPackets.PreyFreeListRerollAvailability();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerPreyFreeListRerollAvailabilityPacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.PreyTimeLeft:
                        {
                            var packet = new ServerPackets.PreyTimeLeft();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerPreyTimeLeftPacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.PreyData:
                        {
                            var packet = new ServerPackets.PreyData();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerPreyDataPacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.PreyRerollPrice:
                        {
                            var packet = new ServerPackets.PreyRerollPrice();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerPreyRerollPricePacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.OfferDescription:
                        {
                            var packet = new ServerPackets.OfferDescription();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerOfferDescription?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.ImbuingDialogRefresh:
                        {
                            var packet = new ServerPackets.ImbuingDialogRefresh();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerImbuingDialogRefreshPacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.CloseImbuingDialog:
                        {
                            var packet = new ServerPackets.CloseImbuingDialog();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerCloseImbuingDialogPacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.ShowMessageDialog:
                        {
                            var packet = new ServerPackets.ShowMessageDialog();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerShowMessageDialogPacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.RequestResourceBalance:
                        {
                            var packet = new ServerPackets.RequestResourceBalance();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerRequestResourceBalancePacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.TibiaTime:
                        {
                            var packet = new ServerPackets.TibiaTime();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerTibiaTimePacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.QuestLog:
                        {
                            var packet = new ServerPackets.QuestLog();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerQuestLogPacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.QuestLine:
                        {
                            var packet = new ServerPackets.QuestLine();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerQuestLinePacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.UpdatingShopBalance:
                        {
                            var packet = new ServerPackets.UpdatingShopBalance();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerUpdatingShopBalancePacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.ChannelEvent:
                        {
                            var packet = new ServerPackets.ChannelEvent();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerChannelEventPacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.ObjectInfo:
                        {
                            var packet = new ServerPackets.ObjectInfo();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerObjectInfoPacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.PlayerInventory:
                        {
                            var packet = new ServerPackets.PlayerInventory();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerPlayerInventoryPacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.MarketEnter:
                        {
                            var packet = new ServerPackets.MarketEnter();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerMarketEnterPacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.MarketLeave:
                        {
                            var packet = new ServerPackets.MarketLeave();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerMarketLeavePacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.MarketDetail:
                        {
                            var packet = new ServerPackets.MarketDetail();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerMarketDetailPacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.MarketBrowse:
                        {
                            var packet = new ServerPackets.MarketBrowse();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerMarketBrowsePacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.ShowModalDialog:
                        {
                            var packet = new ServerPackets.ShowModalDialog();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerShowModalDialogPacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.StoreCategories:
                        {
                            var packet = new ServerPackets.StoreCategories();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerStoreCategoriesPacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.StoreOffers:
                        {
                            var packet = new ServerPackets.StoreOffers();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerStoreOffersPacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.TransactionHistory:
                        {
                            var packet = new ServerPackets.TransactionHistory();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerTransactionHistoryPacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    case ServerPacketType.StoreSuccess:
                        {
                            var packet = new ServerPackets.StoreSuccess();
                            if (packet.ParseFromNetworkMessage(inMessage))
                            {
                                packet.Forward = OnReceivedServerStoreSuccessPacket?.Invoke(packet) ?? true;
                                if (packet.Forward)
                                {
                                    packet.AppendToNetworkMessage(outMessage);
                                }
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
        }
    }
}