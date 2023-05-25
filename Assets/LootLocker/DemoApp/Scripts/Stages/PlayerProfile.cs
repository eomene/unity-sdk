﻿using LootLockerRequests;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using Newtonsoft.Json;

public class PlayerProfile : MonoBehaviour
{
    public Text username;
    public Text playerId;
    public Text className;
    public Text credits;
    public Text level;
    public string creditsSprite = "Credits";
    public string xpSprite = "Xp";
    public Text message;

    public void UpdateScreen(SessionResponse sessionResponse)
    {
        if (sessionResponse == null) return;
        username.text = LootLockerConfig.current.playerName;
        playerId.text = sessionResponse.player_id.ToString();
        className.text = LootLockerConfig.current.playerClass;
        credits.text = sessionResponse.account_balance.ToString();
        level.text = sessionResponse.level.ToString();
        message.text = "";
        LootLockerSDKManager.GetMessages((response) =>
        {
            LoadingManager.HideLoadingScreen();
            if (response.success)
            {
                message.text = response.messages.Length > 0 ? response.messages.First().title : "";
            }
            else
            {
                Debug.LogError("failed to get all messages: " + response.Error);
            }
        });
    }

    public void Grant250XP()
    {
        List<RewardObject> rewardObjects = new List<RewardObject>();
        Dictionary<string, string> data = new Dictionary<string, string>();
        data.Add("XP", "250");
        PopupSystem.ShowPopup("XP Reward", data, "Continue", () =>
        {
            LoadingManager.ShowLoadingScreen();
            LootLockerSDKManager.TriggeringAnEvent("250 XP", (response) =>
            {
                Debug.Log("Response: " + response.message);
                if (response.success)
                {
                    //if (response.check_grant_notifications)
                    //{
                        LootLockerSDKManager.GetAssetNotification((res) =>
                        {
                            if (res.success)
                            {
                                for (int i = 0; i < res.objects.Length; i++)
                                {
                                    if (res.objects[i].acquisition_source == "reward_level_up")
                                    {
                                        rewardObjects.Add(res.objects[i]);
                                    }
                                }
                            }
                        });
                 //   }
                    SelectPlayer(Grant.XP, rewardObjects);
                }
                else
                {
                    Close();
                }
            });
        }, url: xpSprite);
    }

    private void SelectPlayer(Grant grant, List<RewardObject> rewardObjects = null)
    {
        string header = "";
        string normalTextMessage = "";
        Dictionary<string, string> data = new Dictionary<string, string>();
        string icon = grant == Grant.Credits ? creditsSprite : xpSprite;
        LootLockerSDKManager.StartSession(LootLockerConfig.current.deviceID, (response) =>
        {
            LoadingManager.HideLoadingScreen();
            if (response.success)
            {
                header = "Success";

                if (grant == Grant.Credits)
                {
                    normalTextMessage = "Successfully granted Credits.";
                }
                if (grant == Grant.XP)
                {
                    normalTextMessage = "Successfully granted XP.";
                }

                data.Clear();
                //Preparing data to display or error messages we have
                data.Add("1", normalTextMessage);
                StagesManager.instance.GoToStage(StagesManager.StageID.Home, response);
                if (rewardObjects != null && rewardObjects.Count > 0)
                {
                    for (int i = 0; i < rewardObjects.Count; i++)
                    {
                        PopupData PopupData = new PopupData();
                        PopupData.buttonText = "Ok";
                       // PopupData.url = rewardObjects[i].asset.links.thumbnail;
                        PopupData.withKey = true;
                        PopupData.normalText = new Dictionary<string, string>() { { "Reward", rewardObjects[i].asset.name } };
                        PopupData.header = "You got a reward";
                        PopupSystem.ShowScheduledPopup(PopupData);
                    }
                }
            }
            else
            {
                header = "Failed";

                string correctedResponse = response.Error.First() == '{' ? response.Error : response.Error.Substring(response.Error.IndexOf('{'));
                ResponseError errorMessage = new ResponseError();
                errorMessage = JsonConvert.DeserializeObject<ResponseError>(correctedResponse);

                normalTextMessage = errorMessage.messages[0];

                data.Clear();
                //Preparing data to display or error messages we have
                data.Add("1", normalTextMessage);
            }
            PopupSystem.ShowApprovalFailPopUp(header, data, icon);
        });
    }

    public void Close()
    {
        LoadingManager.HideLoadingScreen();
        PopupSystem.CloseNow();
    }

    public void Grant1000XP()
    {
        Dictionary<string, string> data = new Dictionary<string, string>();
        data.Add("Credits", "1000");
        PopupSystem.ShowPopup("Credits Reward", data, "Continue", () =>
        {
            LoadingManager.ShowLoadingScreen();
            LootLockerSDKManager.TriggeringAnEvent("1000 Credits", (response) =>
            {
                if (response.success)
                {
                    SelectPlayer(Grant.Credits);
                }
                else
                {
                    Close();
                }
            });
        }, url: creditsSprite);
    }

    public void OpenPlayerStorage()
    {
        LoadingManager.ShowLoadingScreen();
        StagesManager.instance.GoToStage(StagesManager.StageID.Storage, null);
    }

    private enum Grant
    {
        XP,
        Credits
    }
}
