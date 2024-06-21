using System.Collections.Generic;
using UnityGameFramework.Runtime;

namespace GameLogic
{
    /// <summary>
    /// RPG游戏部分的总系统管理器,设计上其他所有Controller在这里进行生命周期控制
    /// </summary>
    public class RPGSytem : Singleton<RPGSytem>
    {
        private List<IControllerBase> allControllers = new List<IControllerBase>();

        private void InitControllers() 
        {
            allControllers.Add(PlayerController.Instance);
            allControllers.Add(CameraControl.Instance);
        }

        private void Enter() 
        {
            foreach (var controller in allControllers)
            {
                controller.Enter();
            }
        }

        private void Exit()
        {
            foreach (var controller in allControllers)
            {
                controller.Exit();
            }
        }

        public void EnterGame() 
        {
            InitControllers();
            Enter();
        }

        public void ExitGame() 
        {
            Exit();
        }
    }
}