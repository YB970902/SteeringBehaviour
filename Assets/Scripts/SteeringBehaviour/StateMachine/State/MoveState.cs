using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util.Define;

namespace SB.StateMachine
{
    public class MoveState : AgentStateBase
    {
        /// <summary> Arrive 행동을 시작할 거리 </summary>
        private const float ArriveDistance = 5.0f;

        public override void OnEnter()
        {
            
        }

        public override void OnExit()
        {
            
        }

        public override void OnUpdate()
        {
            var behaviour = SteeringBehaviour.Behaviour.Forward | SteeringBehaviour.Behaviour.AvoidAndMove;
            if (ToTarget.magnitude < ArriveDistance)
            {
                behaviour |= SteeringBehaviour.Behaviour.Arrive;
                if (ToTarget.magnitude <= StopDistance)
                {
                    stateMachine.ChangeState(SteeringBehaviour.State.Idle);
                    return;
                }
            }
            else
            {
                behaviour |= SteeringBehaviour.Behaviour.Seek;
            }
            
            agent.Calculate(behaviour);
        }
    }
}