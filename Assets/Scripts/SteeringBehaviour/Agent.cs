using SB.StateMachine;
using UnityEngine;
using Util.Define;

namespace SB
{
    /// <summary>
    /// 조종행동에서 조종을 담당하는 에이전트이다.
    /// </summary>
    public class Agent : MonoBehaviour
    {
        /// <summary> 외부데이터 </summary>
        [SerializeField] protected AgentBlackBoard blackBoard;

        public AgentBlackBoard BlackBoard => blackBoard;
        
        /// <summary> 에이전트가 향하고있는 방향 벡터 </summary>
        protected Vector2 dirHeading;

        /// <summary> 에이전트의 최대 속력 </summary>
        [SerializeField] float maxSpeed;

        /// <summary> 에이전트의 현재 속도 </summary>
        private Vector2 velocity;

        protected AgentStateMachine stateMachine;

        public Vector2 Position => new Vector2(transform.position.x, transform.position.y);

        protected virtual void Start()
        {
            stateMachine = new AgentStateMachine(this);
        }

        private void Update()
        {
            stateMachine.Update();
        }
        
        /// <summary>
        /// 찾기 행동
        /// </summary>
        private Vector2 Seek(Vector2 _target)
        {
            // 목표로 이동하는 속도를 계산한다.
            var desiredVelocity = (_target - Position).normalized * maxSpeed;
            return desiredVelocity - velocity;
        }

        /// <summary>
        /// 달아나기 행동
        /// </summary>
        private Vector2 Flee(Vector2 _target)
        {
            // 목표의 반대 방향으로 이동하는 속도를 계산한다. 
            var desiredVelocity = (Position - _target).normalized * maxSpeed;
            return desiredVelocity - velocity;
        }

        /// <summary>
        /// 도착하기 행동
        /// </summary>
        private Vector2 Arrive(Vector2 _target)
        {
            // 목표지점과 목표지점까지의 거리를 계산한다.
            var toTarget = _target - Position;
            var dirToTarget = toTarget.normalized;
            var dist = toTarget.magnitude;

            // 목적지에 도착했으면 힘이 발생하지 않는다.
            if (dist < float.Epsilon) return Vector2.zero;

            // 감속을 위한 상수.
            const float decelerationTweaker = 0.1f;
            
            // 목적지와 멀수록 속도가 크게 줄어들다가 가까워질수록 조금씩 줄어든다.
            var speed = Mathf.Min(dist / decelerationTweaker, maxSpeed);
            var desiredVelocity = dirToTarget * speed;

            return desiredVelocity - velocity;
        }

        /// <summary>
        /// 동작해야하는 행동의 플래그를 넘기면 그에 맞는 힘을 만들어서 동작한다. 
        /// </summary>
        public void Calculate(SteeringBehaviour.Behaviour _behaviour)
        {
            if (_behaviour.HasFlag(SteeringBehaviour.Behaviour.Forward))
            {
                velocity = dirHeading * maxSpeed;
            }
            else
            {
                velocity = Vector2.zero;
            }

            if (_behaviour.HasFlag(SteeringBehaviour.Behaviour.Seek))
            {
                velocity += Seek(blackBoard.TargetPosition);
            }

            if (_behaviour.HasFlag(SteeringBehaviour.Behaviour.Flee))
            {
                velocity += Flee(blackBoard.TargetPosition);
            }

            if (_behaviour.HasFlag(SteeringBehaviour.Behaviour.Arrive))
            {
                velocity += Arrive(blackBoard.TargetPosition);
            }
            
            dirHeading = velocity.normalized;
            if (velocity.magnitude > maxSpeed)
            {
                velocity = dirHeading * maxSpeed;
            }
            
            velocity *= Time.deltaTime;

            transform.position = Position + velocity;
        }
    }
}