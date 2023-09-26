using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace SteeringBehaviour
{
    /// <summary>
    /// 조종행동에서 조종을 담당하는 에이전트이다.
    /// </summary>
    public class Agent : MonoBehaviour
    {
        /// <summary> 외부데이터 </summary>
        [SerializeField] AgentBlackBoard blackBoard;

        /// <summary> 에이전트가 향하고있는 방향 벡터 </summary>
        private Vector2 dirHeading;

        /// <summary> 에이전트의 최대 속력 </summary>
        [SerializeField] float maxSpeed;

        /// <summary> 에이전트의 현재 속도 </summary>
        private Vector2 velocity;

        private Vector2 Position => new Vector2(transform.position.x, transform.position.y);

        private void Update()
        {
            // 평소에 항상 이동중인 방향으로 이동한다.
            velocity = dirHeading * maxSpeed;

            var seekForce = Seek(blackBoard.TargetPosition);

            velocity += seekForce;

            velocity.Normalize();
            dirHeading = velocity;
            velocity *= maxSpeed * Time.deltaTime;

            transform.position = Position + velocity;
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
    }
}