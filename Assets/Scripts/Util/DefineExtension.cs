
using SB.StateMachine;
using UnityEngine;

namespace Util.Define
{
    public static class DefineExtension
    {
        // SteeringBehaviour
        public static AgentStateBase ToState(this Define.SteeringBehaviour.State _state)
        {
            switch (_state)
            {
                case SteeringBehaviour.State.Idle: return new IdleState();
                case SteeringBehaviour.State.MoveToTarget: return new MoveState();
                case SteeringBehaviour.State.MoveForward: return new MoveForwardState();
                case SteeringBehaviour.State.MoveRandom: return new MoveRandomState();
                default:
                    Debug.LogError($"상태 {_state.ToString()}의 반환 클래스가 정해지지 않았습니다.");
                    return null;
            }
        }
    }
}
