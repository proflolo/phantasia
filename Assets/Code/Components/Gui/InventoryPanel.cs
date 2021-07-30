using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPanel : MonoBehaviour
{
    public InventoryEntry m_entryTemplate;
    private void Awake()
    {
        Debug.Assert(m_entryTemplate, "No se ha definido una entrada para el item");
        m_entryTemplate.gameObject.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        foreach(ItemDef manaItem in Game.instance.database.ListAllManaItems())
        {
            GameObject newEntry = Instantiate(m_entryTemplate.gameObject, transform);
            newEntry.SetActive(true);
            InventoryEntry entry = newEntry.GetComponent<InventoryEntry>();
            entry.DisplayItem(manaItem);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
