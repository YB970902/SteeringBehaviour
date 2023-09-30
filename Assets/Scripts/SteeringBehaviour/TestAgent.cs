using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util.Define;

namespace SB
{
    /// <summary>
    /// 행동을 테스트 하기 위한 에이전트
    /// </summary>
    public class TestAgent : Agent
    {
        protected override void Start()
        {
            base.Start();

            AgentInfo = new TestAgentInfo();
            
            StateMachine.AddState(SteeringBehaviour.State.Idle);
            StateMachine.AddState(SteeringBehaviour.State.MoveToTarget);
            StateMachine.ChangeState(SteeringBehaviour.State.Idle);
        }
    }
}