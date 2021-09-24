using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellBehaviour : MonoBehaviour
{
    ParticleSystem[] m_particlesystems;
    Collider m_collider;
    Renderer[] m_renderers;
    protected void _Awake()
    {
        m_particlesystems = GetComponentsInChildren<ParticleSystem>();
        m_collider = GetComponent<Collider>();
        m_renderers = GetComponentsInChildren<Renderer>();
        
    }

    protected void ScheduleDestruction()
    {
        this.enabled = false;
        if(m_collider)
        {
            m_collider.enabled = false;
        }

        foreach (ParticleSystem psystem in m_particlesystems)
        {
            psystem.Stop(false, ParticleSystemStopBehavior.StopEmitting);
        }

        foreach(Renderer renderer in m_renderers)
        {
            if(renderer is ParticleSystemRenderer)
            {
                //No hagas nada
            }
            else
            {
                renderer.enabled = false;
            }
        }

        StartCoroutine(DestroyWhenNoParticles());
    }

    IEnumerator DestroyWhenNoParticles()
    {
        bool particles = true;
        while (particles)
        {
            particles = false;
            foreach (ParticleSystem psystem in m_particlesystems)
            {
                particles = particles || psystem.particleCount > 0;
            }

            yield return new WaitForEndOfFrame();
        }

        Destroy(gameObject);
    }


}
