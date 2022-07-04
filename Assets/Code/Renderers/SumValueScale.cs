using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SumValueScale : MonoBehaviour
{
    public int section = 0;
    public static float spectrumValue { get; private set; }
    float[] m_audioSpectrum;

    void Start()
    {
        m_audioSpectrum = new float[128];
    }

    // Update is called once per frame
    void Update()
    {
        AudioListener.GetSpectrumData(m_audioSpectrum, 0, FFTWindow.Hamming);

        if (m_audioSpectrum != null && m_audioSpectrum.Length > 0)
        {
            spectrumValue = 0;
            for(int i = (int)Mathf.Pow(2,section-1); i < (int)Mathf.Pow(2,section); i++)
            spectrumValue += m_audioSpectrum[i]*40;
            transform.localScale = new Vector3(1, 1+spectrumValue, 1);
        }
    }
}
