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
    public class BattleReadyForm : UIFormLogic
    {
        public Button StartBattle;
        protected override void OnInit(object userData)
        {
            StartBattle.onClick.AddListener(OnLoginStartBattle);
        }

        private void OnLoginStartBattle()
        {

        }
    }
}
