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

        /// <summary>
        /// 찾기 행동
        /// </summary>
        private Vector2 Seek(Vector2 _target)
        {
            // 목표로 하는 속도를 계산한다.
            var desiredVelocity = (_target - Position).normalized * maxSpeed;
            return desiredVelocity - velocity;
        }

        private void Update()
        {
            // 현재 속도 측정
            velocity = dirHeading * maxSpeed;

            var seekForce = Seek(blackBoard.TargetPosition);

            velocity += seekForce;

            velocity.Normalize();
            dirHeading = velocity;
            velocity *= maxSpeed * Time.deltaTime;

            transform.position = Position + velocity;
        }
    }
}