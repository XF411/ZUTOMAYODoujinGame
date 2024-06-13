using GameLogic.NetworkCore.Controller;
using GameLogic.NetworkCore.Proto;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityGameFramework.Runtime;

namespace GameLogic.NetworkCore
{
    public class Client
    {
        private static readonly Lazy<Client> instance = new Lazy<Client>(() => new Client());

        public static Client Instance => instance.Value;

        public Dictionary<Type, ControllerBase> CTLes { get; set; }

        private const string serverIP = "https://hh.cenkai123.com/play_story_test";
        private const string loginPort = "/user/login/";

        private Client()
        {
            var controllerList = BindAll();
            CTLes = new Dictionary<Type, ControllerBase>();
            foreach (Type type in controllerList)
            {
                ControllerBase controller = Activator.CreateInstance(type) as ControllerBase;
                CTLes.Add(type, controller);
            }
        }

        public static List<Type> BindAll()
        {
            List<Type> types = new List<Type>();
            types.Add(typeof(PlayerController));
            return types;
        }

        public IEnumerator Request(LoginProto.LoginRequest loginRequest)
        {
            string url = serverIP + loginPort;
            //JsonData requestJson = JsonMapper.ToJson(loginRequest);
            var requestJson = JsonUtility.ToJson(loginRequest);  
            Log.Debug($"Request: {requestJson}");
            Log.Debug($"url: {url}");
            using (UnityWebRequest req = new UnityWebRequest(url, "POST"))
            {
                // 将JSON数据转换为byte数组
                byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(requestJson);
                req.uploadHandler = new UploadHandlerRaw(jsonToSend);
                req.downloadHandler = new DownloadHandlerBuffer();

                // 设置请求头
                req.SetRequestHeader("Content-Type", "application/json");

                // 发送请求并等待返回
                req.timeout = 10;
                yield return req.SendWebRequest();

                if (req.result != UnityWebRequest.Result.Success)
                {
                    Log.Error("Connection failed: " + req.error);
                }
                else
                {
                    Log.Debug("Connection ok");
                    // 处理服务器返回的结果
                    HandleAuthorResult(req.downloadHandler.data);
                }
            }
        }


        private void HandleAuthorResult(byte[] bytes)
        {
            string json = System.Text.Encoding.Default.GetString(bytes);
            (CTLes[typeof(PlayerController)] as PlayerController).LoginSuccess(json);
        }
    }

}
