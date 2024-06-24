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
         

        public void PlayerStartMove() 
        {
            PlayerCharecter.StartMove();
        }

        public void PlayerStopMove() 
        {
            PlayerCharecter.StopMove();
        }

        public void OnInputMove(Vector2 moveV2)
        {
            if (PlayerCharecter != null)
            {
                //判断到底是前后移动还是左右移动
                //禁止斜着移动
                if (Mathf.Abs(moveV2.x) > Mathf.Abs(moveV2.y))
                {
                    moveV2.y = 0;
                }
                else if (Mathf.Abs(moveV2.x) < Mathf.Abs(moveV2.y))
                {
                    moveV2.x = 0;
                }
                else
                {
                    moveV2.x = 0;
                    moveV2.y = 0;
                }
                Vector2 targetGridPosition = PlayerCharecter.CurrentGridPosition + moveV2;
                PlayerCharecter.Move2TargetPosition(targetGridPosition);
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
