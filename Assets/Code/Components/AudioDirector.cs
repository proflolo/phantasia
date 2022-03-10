using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioDirector : MonoBehaviour
{
    [SerializeField] AudioSource m_musicAudioSource;
    [SerializeField] AudioSource m_ambientAudioSource;
    private void Awake()
    {
        Debug.Assert(m_musicAudioSource != null, "No tenemos fuente de Musica de fondo");
        Debug.Assert(m_ambientAudioSource != null, "No tenemos fuente de Ambiente de fondo");
    }
    public void PrepareAudio(Biome i_biome)
    {
        AudioClip background = i_biome.def.backgroundMusics[Random.Range(0, i_biome.def.backgroundMusics.Length)];
        m_musicAudioSource.clip = background;
        m_musicAudioSource.Play();

        AudioClip ambient = i_biome.def.backgroundSounds[Random.Range(0, i_biome.def.backgroundSounds.Length)];
        m_ambientAudioSource.clip = ambient;
        m_ambientAudioSource.Play();
    }

    private void OnEnable()
    {
        if(m_musicAudioSource.clip != null)
        {
            m_musicAudioSource.Play();
        }

        if(m_ambientAudioSource.clip != null)
        {
            m_ambientAudioSource.Play();
        }
    }
}
