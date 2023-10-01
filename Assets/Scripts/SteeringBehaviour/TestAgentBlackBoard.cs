using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;
using Util.Define;
using Random = UnityEngine.Random;

namespace SB
{
    /// <summary>
    /// 에이전트에게 전달해줘야하는 외부 데이터의 집합이다.
    /// 모든 데이터를 다 주는게 아니라, 필요한 데이터만 전달한다.
    /// 데이터를 전달하는 입장이기 때문에, 에이전트는 오로직 데이터만 받는다.
    /// 데이터의 가공이나 특정 로직의 동작은 내부에서 스스로 수행하거나, 외부에서부터 호출된다.
    /// </summary>
    public class TestAgentBlackBoard : AgentBlackBoard
    {
        private void Awake()
        {
            CollideAgentList = new LinkedList<Agent>();
        }

        private void OnTriggerEnter2D(Collider2D _other)
        {
            var agent = _other.transform.GetComponent<Agent>();
            if (agent == null)
            {
                return;
            }

            CollideAgentList.AddLast(agent);
        }

        private void OnTriggerExit2D(Collider2D _other)
        {
            var agent = _other.transform.GetComponent<Agent>();
            if (agent == null)
            {
                return;
            }

            CollideAgentList.Remove(agent);
        }
    }
}