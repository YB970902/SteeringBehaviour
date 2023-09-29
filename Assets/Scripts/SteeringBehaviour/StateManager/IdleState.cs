using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util.Define;

namespace SB.StateManager
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
            if (ToTarget.magnitude <= StopDistance)
            {
                return;
            }
            
            stateManager.ChangeState(SteeringBehaviour.State.MoveToTarget);
        }
    }
}