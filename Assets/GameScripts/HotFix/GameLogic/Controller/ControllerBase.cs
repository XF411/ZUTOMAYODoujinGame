using GameBase;
using UnityGameFramework.Runtime;

namespace GameLogic
{
    /// <summary>
    /// 控制器/数据层 基类
    /// </summary>
    internal interface IControllerBase
    {
        internal void Enter()
        {
            OnEnter();
            Log.Info(this.ToString() +　(" Enter"));
        }

        internal void Exit()
        {
            OnExit();
            Log.Info(this.ToString() + (" Enter"));
        }

        protected abstract void OnEnter();
        protected abstract void OnExit();
    }
}
