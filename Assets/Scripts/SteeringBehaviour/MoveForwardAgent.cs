using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util.Define;

namespace SB
{
    /// <summary>
    /// 직진으로 움직이는 에이전트
    /// </summary>
    public class MoveForwardAgent : Agent
    {
        protected override void Start()
        {
            base.Start();

            AgentInfo = new MoveForwardAgentInfo();
            
            DirHeading = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
            blackBoard.ForwardDirection = DirHeading;
            StateMachine.AddState(SteeringBehaviour.State.MoveRandom);
            StateMachine.ChangeState(SteeringBehaviour.State.MoveRandom);
        }
    }
}