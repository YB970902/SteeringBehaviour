using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SB
{
    /// <summary>
    /// 에이전트의 정보가 들어있는 클래스이다.
    /// 원래라면 데이터 테이블을 파싱하는 방법으로 사용했겠지만, 지금은 임의로 만드는 프로젝트이므로 상속받아서 데이터를 넣는다.
    /// </summary>
    public class TestAgentInfo : AgentInfo
    {
        public TestAgentInfo()
        {
            Radius = 0.5f;
            MaxSpeed = 2f;
        }
    }
}