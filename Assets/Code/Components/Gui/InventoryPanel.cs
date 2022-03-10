using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryPanel : MonoBehaviour
{
    public InventoryEntry m_entryTemplate;

    private void OnItemAmountChanged(ItemDef i_item, uint i_oldamount, uint i_newAmount)
    {
        bool found = false;
        for(int i = 1; i < transform.childCount; ++i)
        {
            InventoryEntry itemEntry = transform.GetChild(i).GetComponent<InventoryEntry>();
            if(itemEntry)
            {
                if (itemEntry.GetItem() == i_item && itemEntry.CanAmountBeUpdated())
                {
                    itemEntry.UpdateAmount();
                    found = true;
                }
            }
        }

        if (!found)
        {
            GameObject newEntry = Instantiate(m_entryTemplate.gameObject, transform);
            newEntry.SetActive(true);
            InventoryEntry entry = newEntry.GetComponent<InventoryEntry>();
            entry.DisplayItem(i_item);
        }
    }

    private void Awake()
    {
        Debug.Assert(m_entryTemplate, "No se ha definido una entrada para el item");
        m_entryTemplate.gameObject.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        Game.instance.inventory.sig_itemAmountChanged += OnItemAmountChanged;
        //foreach (ItemDef manaItem in Game.instance.database.ListAllManaItems())
        //{
        //    GameObject newEntry = Instantiate(m_entryTemplate.gameObject, transform);
        //    newEntry.SetActive(true);
        //    InventoryEntry entry = newEntry.GetComponent<InventoryEntry>();
        //    entry.DisplayItem(manaItem);
        //}
    }

    // Update is called once per frame
    void Update()
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(transform as RectTransform);
    }
}
