using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Util.Define;

namespace SB.StateMachine
{
    /// <summary>
    /// 에이전트 상태의 베이스
    /// 에이전트에 접근할 수 있으며, 특정 상황일때 
    /// </summary>
    public abstract class AgentStateBase
    {
        protected Agent agent;
        protected AgentStateMachine stateMachine;

        protected Vector2 ToTarget => agent.BlackBoard.TargetPosition - agent.Position;

        protected const float StopDistance = 0.05f; 
        
        /// <summary>
        /// 상태 생성직후 1회 호출
        /// </summary>
        public void Init(Agent _agent, AgentStateMachine _stateMachine)
        {
            agent = _agent;
            stateMachine = _stateMachine;
        }

        /// <summary>
        /// 상태 진입시 호출
        /// </summary>
        public abstract void OnEnter();
        /// <summary>
        /// 상태 종료시 호출
        /// </summary>
        public abstract void OnExit();
        /// <summary>
        /// 상태 갱신시 호출
        /// </summary>
        public abstract void OnUpdate();

        /// <summary>
        /// 센서에 다른 오브젝트가 닿으면 호출
        /// </summary>
        public virtual void OnSencerEnter(Transform _transform)
        {
            
        }

        /// <summary>
        /// 센서에 다른 오브젝트가 닿으면 호출
        /// </summary>
        public virtual void OnSencerExit(Transform _transform)
        {
            
        }
    }

    /// <summary>
    /// 에이전트의 행동을 관리해주는 관리자.
    /// 외부에서 현재 상태를 선택하고 주기적으로 업데이트를 호출해주면, 그 상태에 정의되어있는 행동들을 조합하여 에이전트를 동작시킨다.
    /// 하나의 에이전트가 하나의 매니저 인스턴스를 가지고 있다.
    /// </summary>
    public class AgentStateMachine
    {
        private readonly Dictionary<SteeringBehaviour.State, AgentStateBase> stateList;
        private AgentStateBase currentState;

        private Agent agent;
        
        public AgentStateMachine(Agent _agent)
        {
            agent = _agent;
            stateList = new Dictionary<SteeringBehaviour.State, AgentStateBase>();
            currentState = null;
        }

        public void AddState(SteeringBehaviour.State _state)
        {
            if (stateList.ContainsKey(_state))
            {
                return;
            }

            var state = _state.ToState();
            state.Init(agent, this);
            stateList[_state] = state;
        }

        public void ChangeState(SteeringBehaviour.State _state)
        {
            if (stateList.ContainsKey(_state) == false)
            {
                return;
            }
            
            currentState?.OnExit();
            currentState = stateList[_state];
            currentState.OnEnter();
        }

        /// <summary>
        /// 행동 업데이트
        /// </summary>
        public void Update()
        {
            currentState?.OnUpdate();
        }
    }
}