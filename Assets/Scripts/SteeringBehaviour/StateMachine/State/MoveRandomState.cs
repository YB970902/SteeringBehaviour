using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util.Define;

namespace SB.StateMachine
{
    public class MoveRandomState : AgentStateBase
    {
        public override void OnEnter()
        {
            agent.BlackBoard.SetRandomTargetPosition();
        }

        public override void OnExit()
        {
            
        }

        public override void OnUpdate()
        {
            var behaviour = SteeringBehaviour.Behaviour.Forward | SteeringBehaviour.Behaviour.AvoidAndMove | SteeringBehaviour.Behaviour.Evade;
            if (ToTarget.magnitude < agent.AgentInfo.MaxSpeed)
            {
                behaviour |= SteeringBehaviour.Behaviour.Arrive;
                if (ToTarget.magnitude <= StopDistance)
                {
                    stateMachine.ChangeState(SteeringBehaviour.State.MoveRandom);
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