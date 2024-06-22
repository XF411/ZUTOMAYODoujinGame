using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace GameLogic
{
    /// <summary>
    /// RPG游戏部分的总系统管理器,设计上其他所有Controller在这里进行生命周期控制
    /// </summary>
    public class RPGSytem : Singleton<RPGSytem>
    {
        //TODO 要做一个有限状态机,用于处理游戏不同状态时，控制的焦点对象

        private List<IControllerBase> allControllers = new List<IControllerBase>();

        private void InitControllers() 
        {
            allControllers.Add(PlayerController.Instance);
            allControllers.Add(CameraControl.Instance);
            allControllers.Add(InputController.Instance);
            allControllers.Add(MapController.Instance);
            
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

        public void InitRPG() 
        {
            InitControllers();
            Enter();
        }

        /// <summary>
        /// 按下方向键的时候
        /// </summary>
        /// <param name="direction"></param>
        public void OnInputDirection(Vector2 direction) 
        {
            //TODO 根据状态机控制焦点对象，现在先只控制角色移动
            PlayerController.Instance.Move(direction);
        }

        public void EnterGame() 
        {
            //TODO 制作加载界面
            //TODO 读取玩家存档数据
            //TODO 根据数据加载地图
            //TODO 根据数据加载玩家角色
            MapController.Instance.LoadMap();
            PlayerController.Instance.LoadPlayerObj();
            CameraControl.Instance.LookAt(PlayerController.Instance.PlayerCharecter.transform);
            CameraControl.Instance.Follow(PlayerController.Instance.PlayerCharecter.transform);
        }

        public void ExitGame() 
        {
            //Exit();
        }
    }
}