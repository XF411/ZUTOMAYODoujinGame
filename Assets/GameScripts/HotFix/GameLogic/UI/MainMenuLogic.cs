using GameFramework.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
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
            Debug.Log("Quit Game");
        }

        private void OnSettingClick()
        {
           Debug.Log("Setting");
        }

        private void OnStartClick()
        {
            Debug.Log("Start Game");
        }
    }
}
