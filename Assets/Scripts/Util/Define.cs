using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Util.Define
{
    public static class SteeringBehaviour
    {
        public enum State
        {
            Idle = 0,
            MoveToTarget,
            End, // 가장 마지막에 위치해야 한다.
        }
    }
}