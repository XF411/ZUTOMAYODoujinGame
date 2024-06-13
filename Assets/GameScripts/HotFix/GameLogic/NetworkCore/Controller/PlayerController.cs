using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityGameFramework.Runtime;

namespace GameLogic.NetworkCore.Controller
{
    /// <summary>
    /// 玩家数据层
    /// </summary>
    public class PlayerController : ControllerBase
    {
        public void LoginSuccess(string json)
        {
            Log.Debug($"LoginSuccess: {json}");
            var login = GameModule.UI.GetUIForm("LoginWindowForm");
            GameModule.UI.CloseUIForm(login);
            GameModule.UI.AddUIGroup("Game");
            GameModule.UI.OpenUIForm("DialogWindowForm","Game");
        }
    }
}
