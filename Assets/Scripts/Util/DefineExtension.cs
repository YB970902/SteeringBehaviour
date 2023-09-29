
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
                case Define.SteeringBehaviour.State.Idle: return new IdleState();
                case Define.SteeringBehaviour.State.MoveToTarget: return new MoveState();
                default: return null;
            }
        }
    }
}
