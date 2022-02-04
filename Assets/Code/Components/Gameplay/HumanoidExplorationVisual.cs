using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanoidExplorationVisual : HumanoidVisual
{
    MapDirector m_mapDirector;
    Biome m_biome;
    // Update is called once per frame
    void Update()
    {
        UpdateImpl();
    }

    public void Initialize(Biome i_biome, MapDirector i_mapDirector)
    {
        m_mapDirector = i_mapDirector;
        m_biome = i_biome;
    }

    protected override void PlayStep()
    {
        m_stepSource.pitch = Random.Range(0.85f, 1.15f);
        MapDirector.GroundType groundType = m_mapDirector.ComputeGroundType(transform.position);
        switch(groundType)
        {
            case MapDirector.GroundType.Low:
                m_stepSource.PlayOneShot(m_biome.def.lowStep);
                break;
            case MapDirector.GroundType.High:
                m_stepSource.PlayOneShot(m_biome.def.highStep);
                break;
        }
        
    }
}
