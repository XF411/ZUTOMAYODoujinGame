using GameLogic.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;
using GameFramework.Localization;

namespace GameLogic
{
    public class SettingMenuLogic : UIFormLogic
    {
        SettingMenuBind view;
        int CurLanguageIndex = 0;
        int NewLanguageIndex = 0;
        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            view = GetComponent<SettingMenuBind>();
            view.BindUI();
            AddBtnListener();
            var count = GameModule.Setting.Count;
            Log.Debug("Setting count = " + count);
            InitLanguageSetting();
        }

        private void AddBtnListener()
        {
            view.btnCancel.onClick.AddListener(OnCancelClick);
            view.btnConfirm.onClick.AddListener(OnConfirmClick);

            view.btnLastLanguage.onClick.AddListener(() => OnLanguageClick(-1));
            view.btnNextLanguage.onClick.AddListener(() => OnLanguageClick(1));
        }

        private void OnLanguageClick(int offset)
        {
            int max = (int)UsedLanguageEnum.Max;
            NewLanguageIndex += offset;
            if (NewLanguageIndex < 0)
            {
                NewLanguageIndex = max - 1;
            }
            else if (NewLanguageIndex >= max)
            {
                NewLanguageIndex = 0;
            }
            var language = ((UsedLanguageEnum)NewLanguageIndex).ToString();
            view.tmpCurLanguage.text = language;
            //GameModule.Setting.SetString("Setting.Language", language);
        }

        private void OnCancelClick() 
        {
            GameModule.UI.CloseUIForm(this.UIForm);
        }
        private void OnConfirmClick()
        {
            GameModule.UI.CloseUIForm(this.UIForm);
            if (NewLanguageIndex != CurLanguageIndex)
            {
                GameModule.Setting.SetString("Setting.Language", ((UsedLanguageEnum)NewLanguageIndex).ToString());
                GameModule.Localization.SetLanguage(((UsedLanguageEnum)NewLanguageIndex).ToString());
            }
        }

        private void InitLanguageSetting()
        {
            var language = GameModule.Setting.GetString("Setting.Language");
            Log.Debug("Language = " + language);
            view.tmpCurLanguage.text = language;
            CurLanguageIndex = GetLanguageIndex(language);
            NewLanguageIndex = CurLanguageIndex;
        }

        private int GetLanguageIndex(string language) 
        {
            switch (language)
            {
                case "ChineseSimplified":
                    return 0;
                case "Japanese":
                    return 1;
                case "English":
                    return 2;
            }
            return 0;
        }
    }
}
