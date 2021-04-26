﻿using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker;
using LootLocker.Requests;


namespace LootLocker.Requests
{
    public class LootLockerVerifyRequest
    {
        public string key => LootLockerConfig.current.apiKey.ToString();
        public string platform => LootLockerConfig.current.platform.ToString();
        public string token { get; set; }

        public LootLockerVerifyRequest(string token)
        {
            this.token = token;
        }
    }

    public class LootLockerVerifyResponse : LootLockerResponse
    {
        public bool success { set; get; }
    }
}

namespace LootLocker
{

    public partial class LootLockerAPIManager
    {
        public static void Verify(LootLockerVerifyRequest data, Action<LootLockerVerifyResponse> onComplete)
        {
            string json = "";
            if (data == null) return;
            else json = JsonConvert.SerializeObject(data);

            EndPointClass endPoint = LootLockerEndPoints.current.playerVerification;

            LootLockerServerRequest.CallAPI(endPoint.endPoint, endPoint.httpMethod, json, (serverResponse) =>
            {
                LootLockerVerifyResponse response = new LootLockerVerifyResponse();
                if (string.IsNullOrEmpty(serverResponse.Error))
                    response = JsonConvert.DeserializeObject<LootLockerVerifyResponse>(serverResponse.text);

                //LootLockerSDKManager.DebugMessage(serverResponse.text, !string.IsNullOrEmpty(serverResponse.Error));
                response.text = serverResponse.text;
                response.status = serverResponse.status;
                response.Error = serverResponse.Error; response.statusCode = serverResponse.statusCode;
                onComplete?.Invoke(response);
            }, false);
        }

    }
}
