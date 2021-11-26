using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBehaviourTree : AIBase
{
    bt.IAINode m_root;
    public void Configure(bt.IAINode i_root)
    {
        bt.AILoopNode loop = new bt.AILoopNode(i_root);
        m_root = loop;
        m_root.Init();
    }

    void Update()
    {
        m_root.Update();
    }
}
