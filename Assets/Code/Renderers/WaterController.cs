using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LowPolyWater
{
    public class WaterController : MonoBehaviour
    {
        public float waveHeight = 0.5f;
        public float waveFrequency = 0.5f;
        public float waveLength = 0.75f;

        // Start is called before the first frame update
        void Start()
        {
            LowPolyWater[] childControllers = transform.GetComponentsInChildren<LowPolyWater>();
            foreach(LowPolyWater l in childControllers)
            {
                l.waveHeight = waveHeight;
                l.waveFrequency = waveFrequency;
                l.waveLength = waveLength;
            }
        }

        // Update is called once per frame
        private void OnValidate()
        {
            UpdateChildren(waveHeight, waveFrequency, waveLength);
        }

        void UpdateChildren(float wH, float wF, float wL)
        {
            LowPolyWater[] childControllers = transform.GetComponentsInChildren<LowPolyWater>();
            foreach (LowPolyWater l in childControllers)
            {
                l.waveHeight = wH;
                l.waveFrequency = wF;
                l.waveLength = wL;
            }
        }

        public void SetWaveHeight(float h)
        {
            UpdateChildren(h, waveFrequency, waveLength);
        }
    }
}
