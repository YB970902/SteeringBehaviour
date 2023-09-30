using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Util.Define
{
    public static class SteeringBehaviour
    {
        /// <summary>
        /// 에이전트의 행동 타입 모음
        /// </summary>
        public enum State
        {
            Idle = 0,
            MoveToTarget,
            MoveForward,
            End, // 가장 마지막에 위치해야 한다.
        }

        /// <summary>
        /// 에이전트의 상태 타입 모음
        /// </summary>
        [Flags]
        public enum Behaviour
        {
            Forward = 1,
            Seek = 1 << 1,
            Flee = 1 << 2,
            Arrive = 1 << 3,
        }
    }
}