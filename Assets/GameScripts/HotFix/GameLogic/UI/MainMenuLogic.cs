using GameFramework.Localization;
using GameLogic.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace GameLogic
{
    public class MainMenuLogic : UIFormLogic
    {
        MainMenuBind view;

        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            view = GetComponent<MainMenuBind>();
            view.BindUI();
            InitTMPLocal();
            AddBtnListener();
        }

        private void InitTMPLocal()
        {
            view.tmpStart.text = LocalizationManager.GetTranslation("GameStart");
            view.tmpSetting.text = LocalizationManager.GetTranslation("GameSetting");
            view.tmpOut.text = LocalizationManager.GetTranslation("OutGame");
        }

        private void AddBtnListener() 
        {
            view.btnStart.onClick.AddListener(OnStartClick);
            view.btnSetting.onClick.AddListener(OnSettingClick);
            view.btnOut.onClick.AddListener(OnQuitClick);
            
        }

        private void OnQuitClick()
        {
            Log.Debug("Quit Game");
#if UNITY_EDITOR
            if (EditorApplication.isPlaying)
            {
                EditorApplication.isPlaying = false;
            }
#endif
            Application.Quit();
        }

        private void OnSettingClick()
        {
            Log.Debug("Setting");
            GameModule.UI.OpenUIForm("SettingMenu", UIGroupEnum.Popups.ToString());
        }

        private void OnStartClick()
        {
            Log.Debug("Start Game");
            RPGSytem.Instance.EnterGame();
            Close();
        }
    }
}
