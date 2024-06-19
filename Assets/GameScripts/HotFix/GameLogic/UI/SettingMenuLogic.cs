using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace GameLogic
{
    public class SettingMenuLogic : UIFormLogic
    {
        SettingMenuBind view;

        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            view = GetComponent<SettingMenuBind>();
            view.BindUI();
        }
    }
}
