using bt;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyController))]
public class AIMeleeMonster : AIBehaviourTree
{
    EnemyController m_enemyController;


    
    BattleWorld m_world;

    private void Awake()
    {
        m_enemyController = GetComponent<EnemyController>();
        Debug.Assert(m_enemyController != null, "No hay enemy controller!");
        m_world = GetComponentInParent<BattleWorld>();
        Debug.Assert(m_world != null, "No hay battle World");

    }

    class AIMoveInDirection : IAINode
    {
        public AIMoveInDirection(AIBase.BattleAIInfo i_battleAiInfo, EnemyController i_pawn, float i_deltaX, float i_speed)
        {
            m_battleInfo = i_battleAiInfo;
            m_pawn = i_pawn;
            m_deltaX = i_deltaX;
            m_speed = i_speed;
        }
        public void Init()
        {
            m_initialPosition = m_pawn.transform.position;
        }

        public NodeUpdateResult Update()
        {
            //Estoy cerca? Si? -> Success No? -> intento acercarme
            Vector3 delta = m_pawn.transform.position - m_initialPosition;
            if(m_deltaX < 0.0f)
            {
                if(delta.x < m_deltaX)
                {
                    return NodeUpdateResult.Success;
                }
                else
                {
                    Vector3 speed = Vector3.zero;
                    speed.x = Mathf.Sign(m_deltaX) * m_speed;
                    m_pawn.SetVelocity(speed);
                    return NodeUpdateResult.Running;
                }
            }
            else
            {
                if (delta.x > m_deltaX)
                {
                    return NodeUpdateResult.Success;
                }
                else
                {
                    Vector3 speed = Vector3.zero;
                    speed.x = Mathf.Sign(m_deltaX) * m_speed;
                    m_pawn.SetVelocity(speed);
                    return NodeUpdateResult.Running;
                }
            }
        }

        AIBase.BattleAIInfo m_battleInfo;
        EnemyController m_pawn;
        float m_deltaX;
        Vector3 m_initialPosition;
        float m_speed;
    }

    class AIBeFarWay : bt.IAINode
    {
        public AIBeFarWay(AIBase.BattleAIInfo i_battleAiInfo, EnemyController i_pawn, float i_distance)
        {
            m_battleAiInfo = i_battleAiInfo;
            m_enemyController = i_pawn;
            m_distance = i_distance;
        }

        public void Init()
        {
            
        }

        public NodeUpdateResult Update()
        {
            float distance = Vector3.Distance(m_enemyController.transform.position, m_battleAiInfo.player.transform.position);
            if(distance > m_distance)
            {
                return NodeUpdateResult.Success;
            }
            else
            {
                return NodeUpdateResult.Failure;
            }
        }

        AIBase.BattleAIInfo m_battleAiInfo;
        EnemyController m_enemyController;
        float m_distance;
    }

    class AIAttackNode : bt.IAINode
    {
        public AIAttackNode(EnemyController i_enemyController, Transform i_target)
        {
            m_enemyController = i_enemyController;
            m_target = i_target;
        }

        public void Init()
        {
            m_attackLaunched = false;
        }

        public NodeUpdateResult Update()
        {
            m_enemyController.SetVelocity(Vector3.zero);
            if(m_enemyController.IsAttacking())
            {
                return NodeUpdateResult.Running;
            }
            else
            {
                if(!m_attackLaunched)
                {
                    m_enemyController.PerformAttack();
                    m_attackLaunched = true;
                    return NodeUpdateResult.Running;
                }
                else
                {
                    return NodeUpdateResult.Success;
                }
            }
        }

        EnemyController m_enemyController;
        Transform m_target;
        bool m_attackLaunched = false;
    }


    bt.IAINode CreateWatchNode()
    {
        //Estar Lejos
        bt.IAINode beFarWay = new AIBeFarWay(m_world.GetBattleAIInfo(), m_enemyController, 10.0f);
        
        //Ronda
        bt.IAINode moveLeft = new AIMoveInDirection(m_world.GetBattleAIInfo(), m_enemyController, -3.0f, m_enemyController.walkSpeed);
        bt.IAINode rest1 = new bt.AIWaitNode(1.0f, m_enemyController); //esperate
        bt.IAINode moveRight = new AIMoveInDirection(m_world.GetBattleAIInfo(), m_enemyController, 3.0f, m_enemyController.walkSpeed);
        bt.IAINode rest2 = new bt.AIWaitNode(1.0f, m_enemyController); //esperate

        List<bt.IAINode> rondaNodes = new List<bt.IAINode>();
        rondaNodes.Add(moveLeft);
        rondaNodes.Add(rest1);
        rondaNodes.Add(moveRight);
        rondaNodes.Add(rest2);

        bt.IAINode rondaSequence = new bt.AILoopNode(new bt.AISequenceNode(rondaNodes));

        //Final...
        List<bt.IAINode> parallelNodes = new List<IAINode>();
        parallelNodes.Add(rondaSequence);
        parallelNodes.Add(beFarWay);
        bt.IAINode parallel = new bt.AIParallelNode(parallelNodes);

        return parallel;
    }

   

    bt.IAINode CreateAttackNode()
    {
        List<bt.IAINode> approaxList = new List<bt.IAINode>();
        approaxList.Add(new bt.AINodeBeAtDistance(m_world.GetBattleAIInfo(), m_enemyController, 1.5f));
        approaxList.Add(new bt.AIMoveTodistanceNode(m_world.GetBattleAIInfo(), m_enemyController, 1.5f, m_enemyController.runSpeed));
        bt.IAINode approax = new bt.AISelectorNode(approaxList);

        //bt.IAINode rest = new bt.AIWaitNode(2.0f, m_enemyController); //esperate
        bt.IAINode attack = new AIAttackNode(m_enemyController, m_world.GetBattleAIInfo().player.transform);

        List<bt.IAINode> escapeList = new List<bt.IAINode>();
        escapeList.Add(new bt.AINodeBeAtDistance(m_world.GetBattleAIInfo(), m_enemyController, 15.0f));
        escapeList.Add(new bt.AIMoveTodistanceNode(m_world.GetBattleAIInfo(), m_enemyController, 15.0f, m_enemyController.runSpeed));
        bt.IAINode escape = new bt.AISelectorNode(escapeList); //alejate


        List<bt.IAINode> sequenceList = new List<bt.IAINode>();
        sequenceList.Add(approax);
        sequenceList.Add(attack);
        sequenceList.Add(escape);
        //
        return new bt.AISequenceNode(sequenceList);
    }

    // Start is called before the first frame update
    void Start()
    {
        //Rama de Vigilancia

        bt.IAINode vigilar = CreateWatchNode();

        //Rama de ataque
        bt.IAINode atacar = CreateAttackNode();

        List<bt.IAINode> selectorNodes = new List<bt.IAINode>();
        selectorNodes.Add(vigilar);
        selectorNodes.Add(atacar);
        bt.AISelectorNode selector = new bt.AISelectorNode(selectorNodes);

        Configure(selector);


        //List<bt.IAINode> approaxList = new List<bt.IAINode>();
        //approaxList.Add(new bt.AINodeBeAtDistance(m_world.GetBattleAIInfo(), m_enemyController, 3.0f));
        //approaxList.Add(new bt.AIMoveNode(m_world.GetBattleAIInfo(), m_enemyController, 3.0f));
        //bt.IAINode approax = new bt.AISelectorNode(approaxList);
        //bt.IAINode rest = new bt.AIWaitNode(3.0f, m_enemyController); //esperate
        //List<bt.IAINode> escapeList = new List<bt.IAINode>();
        //escapeList.Add(new bt.AINodeBeAtDistance(m_world.GetBattleAIInfo(), m_enemyController, 6.0f));
        //escapeList.Add(new bt.AIMoveNode(m_world.GetBattleAIInfo(), m_enemyController, 6.0f));
        //bt.IAINode escape = new bt.AISelectorNode(escapeList); //alejate
        //bt.IAINode rest2 = new bt.AIWaitNode(3.0f, m_enemyController); //esperate2
        //
        //List<bt.IAINode> sequenceList = new List<bt.IAINode>();
        //sequenceList.Add(approax);
        //sequenceList.Add(rest);
        //sequenceList.Add(escape);
        //sequenceList.Add(rest2);
        //
        //bt.IAINode sequence = new bt.AISequenceNode(sequenceList);

    }

    // Update is called once per frame

}
