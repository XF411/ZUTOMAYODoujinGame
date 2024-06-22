using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameLogic
{
    /// <summary>
    /// 玩家角色
    /// </summary>
    public class PlayerCharecter : CharecterEnity
    {
        public void Move(Vector2 moveV2) 
        {
            //TODO 其他处理,例如判断是否可以移动，是否被阻挡，是否被减速等等
            OnMove(moveV2);
        }

        public void OnMove(Vector2 moveV2) 
        {
            //移动的表现逻辑
            transform.DOMove(transform.position + new Vector3(moveV2.x, moveV2.y, 0), 0.5f);
        }
    }
}
