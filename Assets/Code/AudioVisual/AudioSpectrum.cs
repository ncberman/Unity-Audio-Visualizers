using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSpectrum : MonoBehaviour
{
    public static float spectrumValue { get; private set; }
    private float[] m_audioSpectrum;

    void Start()
    {
        m_audioSpectrum = new float[128];
    }

    // Update is called once per frame
    void Update()
    {
        AudioListener.GetSpectrumData(m_audioSpectrum, 0, FFTWindow.Hamming);

        if(m_audioSpectrum != null && m_audioSpectrum.Length > 0)
        {
            spectrumValue = m_audioSpectrum[0] * 100;
        }
    }

    public static float GetSumOfValues(int section, AudioSource audio)
    {
        section %= 8;
        float[] audioSpectrum = new float[128];
        AudioListener.GetSpectrumData(audioSpectrum, 0, FFTWindow.Hamming);
        if (audioSpectrum != null && audioSpectrum.Length > 0)
        {
            float newSpectrumValue = 0;
            for (int i = (int)Mathf.Pow(2, section - 1); i < (int)Mathf.Pow(2, section); i++)
            {
                newSpectrumValue += audioSpectrum[i] * 30 * (1/audio.volume);
            }
            return newSpectrumValue;
        }
        return 0.0f;
    }
}
