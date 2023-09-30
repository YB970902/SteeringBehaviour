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

            agentInfo = new TestAgentInfo();
            
            stateMachine.AddState(SteeringBehaviour.State.Idle);
            stateMachine.AddState(SteeringBehaviour.State.MoveToTarget);
            stateMachine.ChangeState(SteeringBehaviour.State.Idle);
        }
    }
}