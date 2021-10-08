using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class World : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] GameDirector m_gameDirector;

    private void Awake()
    {
        Debug.Assert(m_gameDirector, "No has seteado Game Director en el World");
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RequestAddXP(int i_xp)
    {
        Game.instance.AddXP(10);   
    }

    public void RequestAddItem(ItemDef i_item, uint i_itemAmount)
    {
        Debug.Assert(i_item != null, "No puedes añadir un item nulo");
        Debug.Assert(i_itemAmount > 0, "No puedes dar 0 de un item");
        Game.instance.AddItem(i_item, i_itemAmount);
    }

    public void RequestBattle(BattleDef i_battleDef)
    {
        m_gameDirector.RequestBattleStart(i_battleDef);
    }

    public void RequestPause()
    {
        m_gameDirector.UserRequestedPause();
    }
}
