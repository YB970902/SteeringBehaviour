using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util.Define;

namespace SB.StateMachine
{
    public class IdleState : AgentStateBase
    {
        public override void OnEnter()
        {
            
        }

        public override void OnExit()
        {
            
        }

        public override void OnUpdate()
        {
            agent.Calculate(SteeringBehaviour.Behaviour.Avoid);
        }
    }
}