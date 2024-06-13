using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;
using System;
using GameFramework.Event;
using GameFramework;
using TMPro;
using UnityEngine.UI;
using GameFramework.Network;
using GameLogic.NetworkCore;
using GameLogic.NetworkCore.Proto;
using GameProto;

namespace GameLogic
{
    public class LoginWindowForm : UIFormLogic
    {
        public TMP_InputField inputField;
        public TextMeshProUGUI loginBtnTMP;
        public Button BtnLogin;
        protected override void OnInit(object userData)
        {
            //base.OnInit(userData);
            //inputField.placeholder.GetComponent<TextMeshProUGUI>().text = "GoodLuck";
            loginBtnTMP.text = "开始游戏";
            TMP_Text placeholderText = inputField.placeholder as TMP_Text;
            if (placeholderText != null)
            {
                placeholderText.text = "输入账号";  // 修改Placeholder的文本内容
            }
            BtnLogin.onClick.AddListener(OnLoginClick);
        }

        private void OnLoginClick()
        {
            LoginProto.LoginRequest loginRequest = new LoginProto.LoginRequest();
            loginRequest.device_id = inputField.text;
            loginRequest.mode = "device_id";
            StartCoroutine(Client.Instance.Request(loginRequest));
            ConfigSystem.Instance.Load();
        }
    }
}
