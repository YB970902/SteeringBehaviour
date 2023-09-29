
using SteeringBehaviour.StateManager;

namespace Util.Define.Extension
{
    public static class SteeringBehaviour
    {
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
