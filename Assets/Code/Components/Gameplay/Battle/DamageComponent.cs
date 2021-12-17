using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DamageComponent : MonoBehaviour
{
    [SerializeField] uint m_damage = 0;
    public bool m_damageEnabled = true;
    List<LifeComponent> m_pendingToDamage;
    [SerializeField] float m_impactForce = 0;


    private void Awake()
    {
        m_pendingToDamage = new List<LifeComponent>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (m_damageEnabled)
        {
            float sourcePosition = transform.position.x;
            foreach (LifeComponent lifeComponent in m_pendingToDamage)
            {
                lifeComponent.ApplyDamage(m_damage);
                CharacterController characterController = lifeComponent.GetComponent<CharacterController>();
                if (characterController != null)
                {
                    float targetPosition = lifeComponent.transform.position.x;
                    characterController.ApplyImpact(-Vector3.right * Mathf.Sign(targetPosition - sourcePosition), m_impactForce);
                }
            }
            m_pendingToDamage.Clear();
        }
    }

    private void OnTriggerEnter(Collider i_other)
    {
        
        LifeComponent lifeComponent = i_other.gameObject.GetComponent<LifeComponent>();
        if (lifeComponent)
        {
            m_pendingToDamage.Add(lifeComponent);
        }
        
    }

    private void OnTriggerExit(Collider i_other)
    {
        LifeComponent lifeComponent = i_other.gameObject.GetComponent<LifeComponent>();
        if (lifeComponent)
        {
            m_pendingToDamage.Remove(lifeComponent);
        }
    }
}
