using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class MCController : MonoBehaviour
{
    Rigidbody m_rigidBody;
    Vector2 m_rawAxis;
    Vector3 m_velocity;
    Vector3 m_lookAt;
    bool m_actionRequested = false;
    World m_world;

    private void Awake()
    {
        m_rigidBody = GetComponent<Rigidbody>();
        m_world = GetComponentInParent<World>();
        
        Debug.Assert(m_world != null, "World es nulo en el MCController!");
        
    }
    // Start is called before the first frame update
    void Start()
    {
        m_velocity = Vector3.zero;
        m_lookAt = Vector3.forward;
    }

    // Update is called once per frame
    void Update()
    {
        
        m_rigidBody.velocity = m_velocity;
        m_rigidBody.rotation = Quaternion.LookRotation(m_lookAt);

        if(m_actionRequested)
        {
            m_actionRequested = false;
            //Ejecutar accion
            Vector3 start = transform.position + new Vector3(0.0f, 0.5f, 0.0f);
            Vector3 direction = m_lookAt.normalized;
            float maxDistance = 1.5f;
            LayerMask mask = LayerMask.GetMask(GameplayConstants.PhysicLayer.Interactible);
            QueryTriggerInteraction queryTrigger = QueryTriggerInteraction.Ignore;
            RaycastHit hit;
            if (Physics.Raycast(start, direction, out hit, maxDistance, mask, queryTrigger))
            {
                //Ha habido colision
                Interactible interactible = hit.collider.gameObject.GetComponent<Interactible>();
                interactible.OnInteracted();
            }
        }
    }

    void OnMove(InputValue i_value)
    {
        m_rawAxis = i_value.Get<Vector2>();
        m_velocity = new Vector3(m_rawAxis.x * GameplayConstants.humanSpeed, 0.0f, m_rawAxis.y * GameplayConstants.humanSpeed);
        if (m_velocity.magnitude > 0.0f)
        {
            m_lookAt = m_velocity.normalized;
        }
    }

    void OnPause(InputValue i_value)
    {
        if (i_value.isPressed)
        {
            m_world.RequestPause();
        }
    }

    void OnAction(InputValue i_value)
    {
        if (i_value.isPressed)
        {
            m_actionRequested = true;
        }
    }

    private void OnDisable()
    {
        m_velocity = Vector3.zero;
    }

    private void OnEnable()
    {
        
    }
}
