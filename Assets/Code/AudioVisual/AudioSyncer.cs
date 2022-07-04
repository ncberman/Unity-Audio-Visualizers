using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSyncer : MonoBehaviour
{
    public float threshold;
    public float timeStep;
    public float timeUntilBeat;
    public float restSmoothTime;
    public int section = 0;
    public bool beatBased = true;

    float m_previousAudioValue;
    protected float m_audioValue;
    float m_timer;
    AudioSource audio;

    protected bool m_isBeat;

    private void Awake()
    {
        audio = GameObject.Find("Audio").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        OnUpdate();
    }

    public virtual void OnUpdate()
    {
        if(beatBased)
        {
            m_previousAudioValue = m_audioValue;
            //m_audioValue = AudioSpectrum.spectrumValue;

            //
            m_audioValue = AudioSpectrum.GetSumOfValues(section, audio);
            //
            if (m_previousAudioValue > threshold && m_audioValue <= threshold)
            {
                if (m_timer > timeStep)
                {
                    OnBeat();
                }
            }

            if (m_previousAudioValue <= threshold && m_audioValue > threshold)
            {
                if (m_timer > timeStep)
                {
                    OnBeat();
                }
            }

            m_timer += Time.deltaTime;
        }
        else
        {
            if(m_timer > timeStep)
            {
                m_audioValue = AudioSpectrum.GetSumOfValues(section, audio);
                OnBeat();  
            }
            m_timer += Time.deltaTime;
        }
    }

    public virtual void OnBeat()
    {
        m_timer = 0;
        m_isBeat = true;
    }
}
