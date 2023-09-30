
using SB.StateMachine;

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
                default: return null;
            }
        }
    }
}
