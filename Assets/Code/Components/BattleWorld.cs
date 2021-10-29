using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleWorld : MonoBehaviour
{
    Spell m_trainingSpell;
    // Start is called before the first frame update
    public void Initialize(Spell i_trainingSpell)
    {
        m_trainingSpell = i_trainingSpell;
    }

    public Spell GetTrainingSpell()
    {
        return m_trainingSpell;
    }
}
