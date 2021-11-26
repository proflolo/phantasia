using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace bt
{
    public enum NodeUpdateResult
    {
        Running,
        Success,
        Failure
    }

    public interface IAINode
    {
        void Init();
        NodeUpdateResult Update();
    }



    struct AIBlackboard
    {

    }

    class AISequenceNode : IAINode
    {
        public AISequenceNode(IList<IAINode> i_nodes)
        {
            m_subNodes = i_nodes;
        }

        public void Init()
        {
            m_currentNode = 0;
            foreach (IAINode node in m_subNodes)
            {
                node.Init();
            }
        }

        public NodeUpdateResult Update()
        {
            if (m_currentNode >= m_subNodes.Count)
            {
                return NodeUpdateResult.Success;
            }
            else
            {
                IAINode node = m_subNodes[m_currentNode];
                NodeUpdateResult subNodeResult = node.Update();
                if (subNodeResult == NodeUpdateResult.Running)
                {
                    return NodeUpdateResult.Running;
                }
                else if (subNodeResult == NodeUpdateResult.Failure)
                {
                    return NodeUpdateResult.Failure;
                }
                else
                {
                    m_currentNode++;
                    if (m_currentNode < m_subNodes.Count)
                    {
                        m_subNodes[m_currentNode].Init();
                    }
                    return NodeUpdateResult.Running;
                }
            }
        }
        IList<IAINode> m_subNodes;
        int m_currentNode = 0;

    }

    class AISelectorNode : IAINode
    {
        public AISelectorNode(IList<IAINode> i_nodes)
        {
            m_subNodes = i_nodes;
        }

        public void Init()
        {
            m_currentNode = 0;
            foreach (IAINode node in m_subNodes)
            {
                node.Init();
            }
        }

        public NodeUpdateResult Update()
        {
            if (m_currentNode >= m_subNodes.Count)
            {
                return NodeUpdateResult.Failure;
            }
            else
            {
                IAINode node = m_subNodes[m_currentNode];
                NodeUpdateResult subNodeResult = node.Update();
                if (subNodeResult == NodeUpdateResult.Running)
                {
                    return NodeUpdateResult.Running;
                }
                else if (subNodeResult == NodeUpdateResult.Success)
                {
                    return NodeUpdateResult.Success;
                }
                else
                {
                    m_currentNode++;
                    if (m_currentNode < m_subNodes.Count)
                    {
                        m_subNodes[m_currentNode].Init();
                    }
                    return NodeUpdateResult.Running;
                }
            }
        }
        IList<IAINode> m_subNodes;
        int m_currentNode = 0;
    }

    class AINodeBeAtDistance : IAINode
    {
        public AINodeBeAtDistance(AIBase.BattleAIInfo i_battleAiInfo, EnemyController i_pawn, float i_distance)
        {
            m_battleInfo = i_battleAiInfo;
            m_pawn = i_pawn;
            m_targetdistance = i_distance;
        }

        public void Init()
        {

        }

        public NodeUpdateResult Update()
        {
            Vector3 delta = m_battleInfo.player.transform.position - m_pawn.transform.position;

            if (Mathf.Abs(delta.magnitude - m_targetdistance) < 0.1f)
            {
                return NodeUpdateResult.Success;
            }
            else
            {
                return NodeUpdateResult.Failure;
            }
        }

        AIBase.BattleAIInfo m_battleInfo;
        EnemyController m_pawn;
        float m_targetdistance;
    }


    class AIMoveTodistanceNode : IAINode
    {
        public AIMoveTodistanceNode(AIBase.BattleAIInfo i_battleAiInfo, EnemyController i_pawn, float i_distance, float i_speed)
        {
            m_battleInfo = i_battleAiInfo;
            m_pawn = i_pawn;
            m_targetdistance = i_distance;
            m_speed = i_speed;
        }
        public void Init()
        {

        }

        public NodeUpdateResult Update()
        {
            //Estoy cerca? Si? -> Success No? -> intento acercarme
            Vector3 delta = m_battleInfo.player.transform.position - m_pawn.transform.position;
            if (Mathf.Abs(delta.magnitude - m_targetdistance) < 0.3f)
            {
                return NodeUpdateResult.Success;
            }
            else if (Mathf.Abs(delta.x) < m_targetdistance)
            {
                Vector3 speed = Vector3.zero;
                speed.x = -Mathf.Sign(delta.x) * m_speed;
                m_pawn.SetVelocity(speed);
                return NodeUpdateResult.Running;
            }
            else if (Mathf.Abs(delta.x) > m_targetdistance)
            {
                Vector3 speed = Vector3.zero;
                speed.x = Mathf.Sign(delta.x) * m_speed;
                m_pawn.SetVelocity(speed);
                return NodeUpdateResult.Running;
            }
            else
            {
                return NodeUpdateResult.Failure;
            }
        }

        AIBase.BattleAIInfo m_battleInfo;
        EnemyController m_pawn;
        float m_targetdistance;
        float m_speed;
    }

    class AIWaitNode : IAINode
    {
        public AIWaitNode(float i_waitseconds, EnemyController i_pawn)
        {
            m_waitSeconds = i_waitseconds;
            m_secondsWaited = 0.0f;
            m_pawn = i_pawn;
        }

        public void Init()
        {
            m_secondsWaited = 0.0f;
        }

        public NodeUpdateResult Update()
        {
            if (m_secondsWaited > m_waitSeconds)
            {
                return NodeUpdateResult.Success;
            }
            else
            {
                m_pawn.SetVelocity(Vector3.zero);
                m_secondsWaited += Time.deltaTime;
                return NodeUpdateResult.Running;
            }
        }

        float m_waitSeconds;
        float m_secondsWaited;
        EnemyController m_pawn;
    }

    class AILoopNode : IAINode
    {
        public AILoopNode(IAINode i_subNode)
        {
            m_subNode = i_subNode;
        }
        public void Init()
        {
            m_subNode.Init();
        }

        public NodeUpdateResult Update()
        {
            NodeUpdateResult subResult = m_subNode.Update();
            if (subResult == NodeUpdateResult.Failure)
            {
                return NodeUpdateResult.Failure;
            }
            else if (subResult == NodeUpdateResult.Running)
            {
                return NodeUpdateResult.Running;
            }
            else
            {
                m_subNode.Init();
                return NodeUpdateResult.Running;
            }
        }

        IAINode m_subNode;
    }

    public class AIParallelNode : IAINode
    {
        public AIParallelNode(IList<IAINode> i_parallelNodes)
        {
            m_parallelNodes = i_parallelNodes;
        }
        public void Init()
        {
            foreach(IAINode node in m_parallelNodes)
            {
                node.Init();
            }
        }

        public NodeUpdateResult Update()
        {
            NodeUpdateResult finalResult = NodeUpdateResult.Success;
            foreach(IAINode node in m_parallelNodes)
            {
                NodeUpdateResult result = node.Update();
                if(result == NodeUpdateResult.Failure)
                {
                    return NodeUpdateResult.Failure;
                }
                else if (result == NodeUpdateResult.Running)
                {
                    finalResult = NodeUpdateResult.Running;
                }
            }
            return finalResult;
        }

        IList<IAINode> m_parallelNodes;
    }
}
