using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
//[RequireComponent(typeof(LineRendererSmoother))]
public class LineVisualizer : MonoBehaviour
{
    public AudioSource audio;
    public float width = 16f;
    public int sensitivity = 50;

    public float SmoothingLength = 2f;
    public int SmoothingSections = 10;

    public string style = "vc";

    LineRenderer lr;
    LineRendererSmoother lrs;
    float[] spectrumData = new float[128];
    float[] audioSpectrum = new float[7];
    int resolution;

    const int dblVertCurve = 15;
    const int vertCurve = 15;
    const int vol = 7;
    const int filledVol = 7;
    const int filledStacc = 15;
    const int dblVertRaw = 49;

    // Start is called before the first frame update
    void Awake()
    {
        lr = GetComponent<LineRenderer>();
        lrs = GetComponent<LineRendererSmoother>();
        SetResolution();
        InitPositions();
    }

    private void OnValidate()
    {
        lr = GetComponent<LineRenderer>();
        lrs = GetComponent<LineRendererSmoother>();
        SetResolution();
        InitPositions();
    }

    public void SetStyle(string styleName)
    {
        style = styleName;
        SetResolution();
        InitPositions();
    }

    void SetResolution()
    {
        if (style.Equals("dvc"))
        {
            LeanTween.moveLocalY(gameObject, 0f, 0.05f);
            resolution = dblVertCurve;
        }
        if (style.Equals("vc"))
        {
            LeanTween.moveLocalY(gameObject, -1f, 0.05f);
            resolution = vertCurve;
        }
        if (style.Equals("v"))
        {
            LeanTween.moveLocalY(gameObject, -1.5f, 0.05f);
            resolution = vol;
        }
        if (style.Equals("fv"))
        {
            LeanTween.moveLocalY(gameObject, 0f, 0.05f);
            resolution = filledVol;
        }
        if (style.Equals("fs"))
        {
            LeanTween.moveLocalY(gameObject, 0f, 0.05f);
            resolution = filledStacc;
        }
        if (style.Equals("dvr"))
        {
            LeanTween.moveLocalY(gameObject, 0f, 0.05f);
            resolution = dblVertRaw;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(style.Equals("vc"))
        {
            InitPositions();
            UpdateAudioSpectrum(60);
            UpdateCurve();
            lrs.SmoothPath();
        }
        if (style.Equals("dvc"))
        {
            InitPositions();
            UpdateAudioSpectrum(40);
            UpdateCurve();
            lrs.SmoothPath();
        }
        if (style.Equals("v"))
        {
            InitPositions();
            UpdateAudioSpectrum(80);
            UpdateCurve();
            lrs.SmoothPath();
        }
        if (style.Equals("fv"))
        {
            InitPositions();
            UpdateAudioSpectrum(80);
            UpdateCurve();
            lrs.SmoothPath();
            UpdateWidthCurve();
        }
        if (style.Equals("fs"))
        {
            InitPositions();
            UpdateAudioSpectrum(80);
            UpdateCurve();
            lrs.SmoothPath();
            UpdateWidthCurve();
        }
        if(style.Equals("dvr"))
        {
            InitPositions();
            UpdateAudioSpectrum(30);
            UpdateCurve();
        }
    }
     
    // Set Update the LineRenderer AnimationCurve with new positions
    private void InitPositions()
    {
        lr.positionCount = resolution + 2;
        float pct = 1.0f / (resolution + 1);
        float x = transform.position.x - (width / 2);
        for (int i = 0; i < lr.positionCount; i++)
        {
            lr.SetPosition(
                i,
                new Vector3(
                    x + (i * pct * width),
                    transform.position.y,
                    transform.position.z
                    )
                );
        }

        AnimationCurve curve = new AnimationCurve();

        for (int i = 0; i < lr.positionCount; i++)
        {
            if (i == 0 || i == lr.positionCount - 1) curve.AddKey(i * pct, 0.0f);
            curve.AddKey(i * pct, 0.2f);
        }
        lr.widthCurve = curve;
    }

    // Divide the spectrum data into octaves and put into AudioSpectrum[]
    void UpdateAudioSpectrum(int sensitivity)
    {
        AudioListener.GetSpectrumData(spectrumData, 0, FFTWindow.Hamming);
        for (int i = 0; i < 7; i++)
        {
            float newSpectrumValue = 0;
            for (int j = (int)Mathf.Pow(2, i - 1); j < (int)Mathf.Pow(2, i); j++)
            {
                newSpectrumValue += spectrumData[j] * sensitivity * (1 / audio.volume);
            }
            audioSpectrum[i] = newSpectrumValue;
        }
    }

    // Update the positions of the octave line positions
    private void UpdateCurve()
    {
        float increments = resolution / 8.0f;

        int[] peaks = new int[7];
        for(int i = 1; i <= 7; i++)
        {
            peaks[i - 1] = (int)(increments * i)+1;
        }

        //for (int i = 0; i < lr.positionCount; i++)
        //{
        //    curve.AddKey(i * pct, 0.2f);
        //}
        //curve = lr.widthCurve;
        for (int i = 0; i < audioSpectrum.Length; i++)
        {
            lr.SetPosition(
                peaks[i], 
                Vector3.Lerp(
                    lr.GetPosition(peaks[i]), 
                    new Vector3(lr.GetPosition(peaks[i]).x, 
                    transform.position.y + audioSpectrum[i], 
                    lr.GetPosition(peaks[i]).z),
                    sensitivity * Time.deltaTime)
                );
            if(style.Equals("dvr") || style.Equals("dvc")) 
                lr.SetPosition(
                    peaks[i]+1,
                    Vector3.Lerp(
                        lr.GetPosition(peaks[i]+1),
                        new Vector3(lr.GetPosition(peaks[i]+1).x,
                        transform.position.y - audioSpectrum[i],
                        lr.GetPosition(peaks[i]+1).z),
                        sensitivity * Time.deltaTime)
                    );
        }
    }

    void UpdateWidthCurve()
    {
        float pct = 1.0f / (lr.positionCount + 1);
        AnimationCurve curve = new AnimationCurve();

        if (style.Equals("fv") || style.Equals("fs"))
        {
            for (int i = 0; i < lr.positionCount; i++)
            {
                if(style.Equals("fs"))
                {
                    if (i * pct < 0.05) curve.AddKey(i * pct, (i * pct) / 0.3f);
                    if (i * pct > 0.95) curve.AddKey(i * pct, (1 - (i * pct)) / 0.3f);
                }
                else
                {
                    if (i == 0 || i == lr.positionCount - 1) curve.AddKey(i * pct, 0.0f);
                }

                Vector3 indexPos = lr.GetPosition(i);
                curve.AddKey(i * pct, indexPos.y - transform.position.y + 0.2f);
                lr.SetPosition(
                    i,
                    new Vector3(
                        indexPos.x,
                        transform.position.y,
                        indexPos.z
                        )
                    );
            }
        }
        lr.widthCurve = curve;
    }

    private float[] NormalizeArray(float[] arr)
    {
        float max = 0;
        for(int i = 0; i < arr.Length; i++)
        {
            if (arr[i] > max) max = arr[i];
        }
        for(int i = 0; i < arr.Length; i++)
        {
            arr[i] = arr[i] / max;
        }
        return arr;
    }
}
