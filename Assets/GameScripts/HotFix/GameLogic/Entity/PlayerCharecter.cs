using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameLogic
{
    /// <summary>
    /// ��ҽ�ɫ
    /// </summary>
    public class PlayerCharecter : CharecterEnity
    {
        public void Move(Vector2 moveV2) 
        {
            //TODO ��������,�����ж��Ƿ�����ƶ����Ƿ��赲���Ƿ񱻼��ٵȵ�
            OnMove(moveV2);
        }

        public void OnMove(Vector2 moveV2) 
        {
            //�ƶ��ı����߼�
            transform.DOMove(transform.position + new Vector3(moveV2.x, moveV2.y, 0), 0.5f);
        }
    }
}
