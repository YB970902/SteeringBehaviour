
using SB.StateManager;

namespace Util.Define
{
    public static class DefineExtension
    {
        // SteeringBehaviour
        public static AgentStateBase ToState(this Define.SteeringBehaviour.State _state)
        {
            switch (_state)
            {
                case Define.SteeringBehaviour.State.Idle: return null;
                case Define.SteeringBehaviour.State.MoveToTarget: return null;
                default: return null;
            }
        }
    }
}
