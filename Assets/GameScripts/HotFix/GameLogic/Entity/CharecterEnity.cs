using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameLogic
{
    /// <summary>
    /// 角色实体基类,所有角色实体都继承这个类并实现对应的行为接口
    /// 包括玩家，NPC，怪物等等
    /// </summary>
    public class CharecterEnity : EntityBase,IMoveBehavier
    {

    }
}
