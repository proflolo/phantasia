using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    [SerializeField] float m_delaySeconds = 2.0f;
    // Start is called before the first frame update
    public void OnAction()
    {
        StartCoroutine(WaitAndDestroy());


    }

    IEnumerator WaitAndDestroy()
    {
        yield return new WaitForSeconds(m_delaySeconds);
        Destroy(gameObject);
    }
}
