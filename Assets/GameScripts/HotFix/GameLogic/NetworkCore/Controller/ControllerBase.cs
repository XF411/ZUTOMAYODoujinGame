using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLogic.NetworkCore 
{
    /// <summary>
    /// 数据层基类
    /// </summary>
    public abstract class ControllerBase
    {
        protected ControllerBase() { }

        internal void Enter()
        {
            OnEnter();
        }

        internal void Exit()
        {
            OnExit();
        }

        protected virtual void OnEnter() { }

        protected virtual void OnExit() { }
    }
}
