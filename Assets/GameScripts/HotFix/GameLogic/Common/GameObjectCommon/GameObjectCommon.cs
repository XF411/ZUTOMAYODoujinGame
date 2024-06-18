using System;
using System.Collections;
using UnityEngine;

namespace GameLogic.Common
{
    public static class GameObjectCommon 
    {
        /// <summary>
        /// 根据名称查找对象
        /// </summary>
        /// <param name="name"></param>
        /// <param name="root"></param>
        /// <returns></returns>
        public static GameObject FindObjectByName(string name, GameObject root = null)
        {
            if (null == root)
                return GameObject.Find(name);

            GameObject thisObject = null;
            foreach (Transform child in root.transform)
            {
                thisObject = child.gameObject;
                if (name.Equals(thisObject.name, StringComparison.Ordinal))
                    return thisObject;
            }

            foreach (Transform child in root.transform)
            {
                if (null != (thisObject = FindObjectByName(name, child.gameObject)))
                    return thisObject;
            }

            return null;
        }

        /// <summary>
        /// 约束类型，并根据名称和类型获取到指定物体上的对应组件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <param name="root"></param>
        /// <returns></returns>
        public static T FindComponentWithName<T>(string name, GameObject root = null) where T : Component
        {
            GameObject go = FindObjectByName(name, root);
            if (null == go)
            {
                return null;
            }

            return go.GetComponent<T>();
        }
    }
}