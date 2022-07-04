using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetSpectrum : MonoBehaviour
{
    public static float spectrumValue { get; private set; }
    private float[] m_audioSpectrum;

    void Start()
    {
        m_audioSpectrum = new float[128];
    }

    // Update is called once per frame
    public void GetData()
    {
        AudioListener.GetSpectrumData(m_audioSpectrum, 0, FFTWindow.Hamming);

        if (m_audioSpectrum != null && m_audioSpectrum.Length > 0)
        {
            foreach(float f in m_audioSpectrum)
            {
                float fl = f * 10000;
                Debug.Log(fl.ToString());
            }
        }
    }
}
