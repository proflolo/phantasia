using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    [SerializeField] float m_delaySeconds = 2.0f;
    bool m_active = false;
    // Start is called before the first frame update
    public void OnAction()
    {
        m_active = true;
    }

    private void Update()
    {
        if(m_active)
        {
            m_delaySeconds -= Time.deltaTime;
            if(m_delaySeconds <= 0.0f)
            {
                Destroy(gameObject);
            }
        }
    }
}
