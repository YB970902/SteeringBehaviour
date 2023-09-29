using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace SB
{
    /// <summary>
    /// 에이전트에게 전달해줘야하는 외부 데이터의 집합이다.
    /// 모든 데이터를 다 주는게 아니라, 필요한 데이터만 전달한다.
    /// 데이터를 전달하는 입장이기 때문에, 에이전트는 오로직 데이터만 받는다.
    /// 데이터의 가공이나 특정 로직의 동작은 내부에서 스스로 수행하거나, 외부에서부터 호출된다.
    /// </summary>
    public class AgentBlackBoard : MonoBehaviour
    {
        /// <summary> 에이전트가 이동해야하는 타겟의 위치 </summary>
        public Vector2 TargetPosition { get; private set; }
        
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                TargetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
        }
    }
}