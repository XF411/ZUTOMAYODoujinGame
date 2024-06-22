using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityGameFramework.Runtime;
using UnityEngine;
using UnityEngine.EventSystems;
namespace GameLogic 
{ 
    /// <summary>
    /// 玩家控制层
    /// </summary>
    public class PlayerController : Singleton<PlayerController>, IControllerBase
    {
        /// <summary>
        /// 当前控制的角色
        /// </summary>
        public PlayerCharecter PlayerCharecter { get; private set; }
         
        /// <summary>
        /// 当前角色的坐标位置
        /// </summary>

        public void Move(Vector2 moveV2)
        {
            if (PlayerCharecter != null)
            {
                Vector2 targetGridPosition = PlayerCharecter.CurrentGridPosition + moveV2;
                if (PlayerCharecter.CurrentGridPosition != targetGridPosition 
                    && PlayerCharecter.TargetGridPosition != targetGridPosition)
                {
                    PlayerCharecter.Move(targetGridPosition);
                }
            }
        }

        public void LoadPlayerObj() 
        {
            //TODO 加载玩家到地图中
            var obj = GameModule.Resource.LoadGameObject("Nira1");//TODO 修改为根据配置和存档来确定加载什么资源
            PlayerCharecter = obj.GetComponent<PlayerCharecter>();
            PlayerCharecter.Init();
        }

        void IControllerBase.OnEnter()
        {
            
        }

        void IControllerBase.OnExit() 
        {

        }

    }
}
