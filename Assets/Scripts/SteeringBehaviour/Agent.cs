using SB.StateMachine;
using UnityEngine;
using UnityEngine.Serialization;
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
        public Vector2 DirHeading { get; protected set; }

        /// <summary> 에이전트의 현재 속도 </summary>
        private Vector2 Velocity { get; set; }

        public AgentStateMachine StateMachine { get; private set; }
        protected AgentInfo AgentInfo { get; set; }

        public Vector2 Position => new Vector2(transform.position.x, transform.position.y);

        protected virtual void Start()
        {
            blackBoard.Init(this);
            StateMachine = new AgentStateMachine(this);
        }

        private void Update()
        {
            StateMachine.Update();
        }
        
        /// <summary>
        /// 찾기 행동
        /// </summary>
        private Vector2 Seek(Vector2 _target)
        {
            // 목표로 이동하는 속도를 계산한다.
            var desiredVelocity = (_target - Position).normalized * AgentInfo.MaxSpeed;
            return desiredVelocity - Velocity;
        }

        /// <summary>
        /// 달아나기 행동
        /// </summary>
        private Vector2 Flee(Vector2 _target)
        {
            // 목표의 반대 방향으로 이동하는 속도를 계산한다. 
            var desiredVelocity = (Position - _target).normalized * AgentInfo.MaxSpeed;
            return desiredVelocity - Velocity;
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
            var speed = Mathf.Min(dist / decelerationTweaker, AgentInfo.MaxSpeed);
            var desiredVelocity = dirToTarget * speed;

            return desiredVelocity - Velocity;
        }

        /// <summary>
        /// 이동중인 적을 회피하는 행동
        /// </summary>
        private Vector2 Avoid(Agent _targetAgent)
        {
            // 타겟이 이동중이지 않다면 회피하지 않는다.
            if (_targetAgent.Velocity.magnitude <= float.Epsilon)
            {
                return Vector2.zero;
            }
            
            // 타겟 에이전트가 이동할것이라고 예측하는 목표 지점
            var targetMovePoint = _targetAgent.Position + _targetAgent.DirHeading * _targetAgent.AgentInfo.MaxSpeed;

            var fleeToTarget = Position - targetMovePoint;
            var distToTarget = fleeToTarget.magnitude;
            // 타겟 에이전트가 이동하려는 위치가 내 위치와 너무 멀다면 회피하지 않는다.
            if (distToTarget < AgentInfo.Radius + _targetAgent.AgentInfo.Radius)
            {
                return Vector2.zero;
            }

            return fleeToTarget.normalized * (distToTarget - AgentInfo.Radius - _targetAgent.AgentInfo.Radius);
        }

        /// <summary>
        /// 동작해야하는 행동의 플래그를 넘기면 그에 맞는 힘을 만들어서 동작한다. 
        /// </summary>
        public void Calculate(SteeringBehaviour.Behaviour _behaviour)
        {
            if (_behaviour.HasFlag(SteeringBehaviour.Behaviour.Forward))
            {
                Velocity = DirHeading * AgentInfo.MaxSpeed;
            }
            else
            {
                Velocity = Vector2.zero;
            }

            if (_behaviour.HasFlag(SteeringBehaviour.Behaviour.Seek))
            {
                Velocity += Seek(blackBoard.TargetPosition);
            }

            if (_behaviour.HasFlag(SteeringBehaviour.Behaviour.Flee))
            {
                Velocity += Flee(blackBoard.TargetPosition);
            }

            if (_behaviour.HasFlag(SteeringBehaviour.Behaviour.Arrive))
            {
                Velocity += Arrive(blackBoard.TargetPosition);
            }

            if (_behaviour.HasFlag(SteeringBehaviour.Behaviour.Avoid))
            {
                foreach (var agent in blackBoard.CollideAgentList)
                {
                    Velocity += Avoid(agent);
                }
            }
            
            DirHeading = Velocity.normalized;
            if (Velocity.magnitude > AgentInfo.MaxSpeed)
            {
                Velocity = DirHeading * AgentInfo.MaxSpeed;
            }
            
            Velocity *= Time.deltaTime;

            transform.position = Position + Velocity;
        }
    }
}