using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityGameFramework.Runtime;
using UnityEngine;
namespace GameLogic 
{ 
    /// <summary>
    /// 玩家控制层
    /// </summary>
    public class PlayerController : Singleton<PlayerController>, IControllerBase
    {
        PlayerCharecter playerCharecter;
        public void Move(Vector2 moveV2)
        {
            //TODO 
            if (playerCharecter != null)
            {
                playerCharecter.Move(moveV2);
            }
        }

        public void LoadPlayerObj() 
        {
            //TODO 加载玩家到地图中
            var obj = GameModule.Resource.LoadGameObject("PlayerCharecter");
            obj.transform.position = Vector3.zero;//TODO 这里需要根据存档数据来设置位置
            playerCharecter = obj.GetComponent<PlayerCharecter>();
        }

        void IControllerBase.OnEnter()
        {
            
        }

        void IControllerBase.OnExit() 
        {

        }

    }
}
