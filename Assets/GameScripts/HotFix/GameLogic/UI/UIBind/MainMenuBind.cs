using UnityEngine;
using UnityEngine.UI;
using TMPro;
using GameLogic.Common;

namespace GameLogic
{
    public class MainMenuBind : UIBindBase
    {
        public Image imgBg;
        public RectTransform transButtonList;
        public Button btnStart;
        public TextMeshProUGUI tmpStart;
        public Button btnSetting;
        public TextMeshProUGUI tmpSetting;
        public Button btnOut;
        public TextMeshProUGUI tmpOut;

        public override void BindUI()
        {
            imgBg = GameObjectCommon.FindComponentWithName<Image>("imgBg", gameObject);
            transButtonList = GameObjectCommon.FindComponentWithName<RectTransform>("transButtonList", imgBg.gameObject);
            btnStart = GameObjectCommon.FindComponentWithName<Button>("btnStart", transButtonList.gameObject);
            tmpStart = GameObjectCommon.FindComponentWithName<TextMeshProUGUI>("tmpStart", btnStart.gameObject);
            btnSetting = GameObjectCommon.FindComponentWithName<Button>("btnSetting", transButtonList.gameObject);
            tmpSetting = GameObjectCommon.FindComponentWithName<TextMeshProUGUI>("tmpSetting", btnSetting.gameObject);
            btnOut = GameObjectCommon.FindComponentWithName<Button>("btnOut", transButtonList.gameObject);
            tmpOut = GameObjectCommon.FindComponentWithName<TextMeshProUGUI>("tmpOut", btnOut.gameObject);
        }
    }
}
