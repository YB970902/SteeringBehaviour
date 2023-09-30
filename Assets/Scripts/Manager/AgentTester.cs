using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using SB;
using Random = UnityEngine.Random;

namespace Manager
{
    /// <summary>
    /// 에이전트가 실제로 어떻게 동작하는지 테스트하기 위한 매니저
    /// </summary>
    public class AgentTester : MonoBehaviour
    {
        [SerializeField] Agent prefabMoveForwardAgent;
        [SerializeField] Agent prefabTestAgent;

        private List<Agent> agents;

        private Rect cameraArea;

        /// <summary> 직선 방향으로 움직이기만 하는 에이전트의 수 </summary>
        [SerializeField] int moveForwardAgentCount = 10;
        /// <summary> 테스트 하려는 에이전트의 수 </summary>
        private const int TestAgentCount = 1;

        private void Start()
        {
            agents = new List<Agent>(moveForwardAgentCount + TestAgentCount);

            var cameraOffset = 1.0f;

            var screenLB = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
            var screenRT = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
            cameraArea.x = screenLB.x - cameraOffset;
            cameraArea.y = screenLB.y - cameraOffset;
            cameraArea.width = screenRT.x - screenLB.x + cameraOffset * 2;
            cameraArea.height = screenRT.y - screenLB.y + cameraOffset * 2;


            for (int i = 0; i < moveForwardAgentCount; ++i)
            {
                var agent = Instantiate(prefabMoveForwardAgent);
                var position = new Vector3();
                position.x = Random.Range(cameraArea.x, cameraArea.x + cameraArea.width);
                position.y = Random.Range(cameraArea.y, cameraArea.y + cameraArea.height);

                agent.transform.position = position;
                agents.Add(agent);
            }
            
            for (int i = 0; i < TestAgentCount; ++i)
            {
                var agent = Instantiate(prefabTestAgent);
                var position = new Vector3();
                position.x = Random.Range(cameraArea.x, cameraArea.x + cameraArea.width);
                position.y = Random.Range(cameraArea.y, cameraArea.y + cameraArea.height);

                agent.transform.position = position;
                agents.Add(agent);
            }
        }

        private void Update()
        {
            foreach (var agent in agents)
            {
                if (agent.Position.x < cameraArea.x)
                {
                    agent.transform.position += Vector3.right * cameraArea.width;
                }
                
                if (agent.Position.x > cameraArea.x + cameraArea.width)
                {
                    agent.transform.position += Vector3.left * cameraArea.width;
                }
                
                if (agent.Position.y < cameraArea.y)
                {
                    agent.transform.position += Vector3.up * cameraArea.height;
                }
                
                if (agent.Position.y > cameraArea.y + cameraArea.height)
                {
                    agent.transform.position += Vector3.down * cameraArea.height;
                }
            }
        }
    }
}