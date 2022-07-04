using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexMap : MonoBehaviour
{
    public GameObject hexPrefab;
    List<Hex> hexes = new List<Hex>();

    public int mapSize = 200;
    public int maxHeight = 20;
    public int minHeight = 4;
    public int baseHeight = -2;

    public float hexSize = 1;
    public float hexOffset = 0.2f;

    public float syncMaxThreshold = 25.0f;
    public float syncTimeStep = 0.15f;
    public float syncTimeUntilbeat = 0.05f;
    public float syncRestSmoothTime = 5.0f;
    public float syncBeatOffset = 2.0f;
    public bool beatBased = true;

    int layers = 0;
    int largestStudio = 0;

    private void Start()
    {
        if (Application.isPlaying)
        {
            foreach (Hex hex in hexes)
            {
                Destroy(hex.thisHex);
            }
            hexes = new List<Hex>();
            largestStudio = 0;

            for (int i = 0; i < mapSize; i++)
            {
                AddHex(i.ToString(), (int)(mapSize * Mathf.Pow(0.99f, i)));
            }

            foreach (Hex hex in hexes)
            {
                SetSyncScalerByIndexAndLayer(hex);
            }
        }
    }

    public void UpdateSettings()
    {
        if (Application.isPlaying)
        {
            foreach (Hex hex in hexes)
            {
                Destroy(hex.thisHex);
            }
            hexes = new List<Hex>();
            largestStudio = 0;

            for (int i = 1; i < mapSize; i++)
            {
                AddHex(i.ToString(), (int)(mapSize * Mathf.Pow(0.99f, i)));
            }

            foreach (Hex hex in hexes)
            {
                SetSyncScalerByIndexAndLayer(hex);
            }
        }
    }

    public void SwapStyle(string type)
    {
        if (type.Equals("raw")) beatBased = false;
        else if(type.Equals("beat")) beatBased = true;

        foreach (Hex hex in hexes)
        {
            SetSyncScalerByIndexAndLayer(hex);
        }
    }

    public void OnValidate()
    {
        if (Application.isPlaying)
        {
            foreach(Hex hex in hexes)
            {
                Destroy(hex.thisHex);
            }
            hexes = new List<Hex>();
            largestStudio = 0;

            for (int i = 1; i < mapSize; i++)
            {
                AddHex(i.ToString(), (int)(mapSize * Mathf.Pow(0.99f, i)));
            }

            foreach(Hex hex in hexes)
            {
                SetSyncScalerByIndexAndLayer(hex);
            }
        }
    }

    public void AddHex(string studioName, int numMembers)
    {
        if(numMembers > largestStudio) { 
            largestStudio = numMembers;
            foreach(Hex hex in hexes)
            {
                SetSize(hex);
            }
        }
        int index = hexes.Count;
        GameObject go = Instantiate(hexPrefab, CalculatePosition(index), Quaternion.identity);
        go.transform.parent = this.gameObject.transform;
        go.name = index.ToString();

        int layer = CalculateLayer(index);
        if (layer + 1 > layers)
        {
            layers = layer + 1;
        }

        Hex newHex = new Hex(
            studioName, 
            numMembers,
            go,
            layer,
            index
            );
        SetSize(newHex);
        hexes.Add(newHex);
    }

    private void SetSize(Hex h)
    {
        GameObject go = h.thisHex;
        go.GetComponent<HexRenderer>().outerSize = hexSize;
        go.GetComponent<HexRenderer>().height = ((((float)h.Subscribers / (float)largestStudio) * maxHeight) + minHeight);
        go.GetComponent<HexRenderer>().DrawMesh();
    }

    private void SetSyncScalerByIndexAndLayer(Hex h)
    {
        GameObject go = h.thisHex;
        AudioSyncScale sync = go.GetComponent<AudioSyncScale>();

        float layerMax = syncMaxThreshold / (layers - h.layer);
        float layerMin = syncMaxThreshold / (layers + 1 - h.layer);
        int lastIndexOfLastLayer = CalculateLastIndexOfLastLayer(h.layer);
        int layerIndex = h.index - lastIndexOfLastLayer - 1;
        int sizeOfLayer = CalculateLastIndexOfLastLayer(h.layer + 1) - lastIndexOfLastLayer+1;
        float layerThresholdSpan = layerMax - layerMin;
        float layerThresholdIncrement = layerThresholdSpan / sizeOfLayer;
        sync.threshold = layerMin + (layerThresholdIncrement*layerIndex);

        sync.timeStep = syncTimeStep;
        sync.timeUntilBeat = syncTimeUntilbeat;
        sync.restSmoothTime = syncRestSmoothTime;// / (h.layer + 1);

        float beatScale = ((maxHeight + syncBeatOffset + minHeight) / go.GetComponent<HexRenderer>().height);
        sync.beatScale = new Vector3(1, beatScale, 1);

        sync.restScale = new Vector3(1, 1, 1);

        sync.section = (int)((h.index / (hexes.Count / 8.0f))-0.0001f);
        sync.beatBased = beatBased;
    }



    private int CalculateLayer(int i)
    {
        // formula to figure out which layer of the hexagonal grid the hex should be on
        
        int layer = Mathf.CeilToInt(
            (Mathf.Sqrt(4.0f * i + 3.0f) / (2.0f * Mathf.Sqrt(3.0f)))-0.5001f
            );
        return layer;
    }

    private int CalculateLastIndexOfLastLayer(int layer)
    {
        if (layer <= 0) return 0;

        int firstIndex = (int)(3 * (Mathf.Pow(layer-1, 2)) + 3 * (layer-1));
        return firstIndex;
    }

    private Vector3 CalculatePosition(int index)
    {
        int layer = CalculateLayer(index);
        if (layer == 0)
        {
            return new Vector3(0, baseHeight, 0);
        }

        // Index within the list minus num of indices of all the previous layers minus 1 so the first index is 0
        int layerIndex = index - CalculateLastIndexOfLastLayer(layer) - 1;

        // Figure out the positioning around the hex map within the layer
        float xPos = 0.0f;
        float zPos = 0.0f;
        int side = Mathf.FloorToInt(layerIndex / layer);
        int sideIndex = layerIndex % layer;

        /*Debug.Log(
            "INDEX: " + index +
            "\nLAYER: " + layer + 
            "\nLAYERINDEX: " + layerIndex + 
            "\nSIDE: " + side + 
            "\nSIDEINDEX: " + sideIndex
            );*/

        switch (side)
        {
            case 0:
                // Defines the first hex of the side
                xPos = (layer * (hexSize * (1 + Mathf.Abs(hexOffset))) * (2.0f * Mathf.Cos((1.0f * Mathf.PI) / 6.0f)));
                zPos = 0.0f;
                if (sideIndex == 0) break;

                // LEFT
                xPos += (
                    sideIndex * (hexSize * (1 + Mathf.Abs(hexOffset))) * Mathf.Cos((7.0f * Mathf.PI) / 6.0f)
                    );
                // DOWN
                zPos += (
                    sideIndex * (hexSize * (1 + Mathf.Abs(hexOffset))) * (3.0f * Mathf.Sin((7.0f * Mathf.PI) / 6.0f))
                    );
                break;

            case 1:
                // Defines the first hex of the side
                xPos = (layer * (hexSize * (1 + Mathf.Abs(hexOffset))) * Mathf.Cos((11.0f * Mathf.PI) / 6.0f));
                zPos = (layer * (hexSize * (1 + Mathf.Abs(hexOffset))) * (3.0f * Mathf.Sin((11.0f * Mathf.PI) / 6.0f)));
                if (sideIndex == 0) break;

                // To the left
                xPos += (
                    sideIndex * (hexSize * (1 + Mathf.Abs(hexOffset))) * (2.0f * Mathf.Cos((7.0f * Mathf.PI) / 6.0f))
                    );
                break;

            case 2:
                // Defines the first hex of the side
                xPos = (layer * (hexSize * (1 + Mathf.Abs(hexOffset))) * Mathf.Cos((7.0f * Mathf.PI) / 6.0f));
                zPos = (layer * (hexSize * (1 + Mathf.Abs(hexOffset))) * (3.0f * Mathf.Sin((7.0f * Mathf.PI) / 6.0f)));
                if (sideIndex == 0) break;

                // Up and to the left
                xPos += (
                    sideIndex * (hexSize * (1 + Mathf.Abs(hexOffset))) * Mathf.Cos((5.0f * Mathf.PI) / 6.0f)
                    );
                zPos += (
                    sideIndex * (hexSize * (1 + Mathf.Abs(hexOffset))) * (3.0f * Mathf.Sin((5.0f * Mathf.PI) / 6.0f))
                    );
                break;

            case 3:
                // Defines the first hex of the side
                xPos = (layer * (hexSize * (1 + Mathf.Abs(hexOffset))) * (2.0f * Mathf.Cos((7.0f * Mathf.PI) / 6.0f)));
                zPos = 0.0f;
                if (sideIndex == 0) break;

                // Up and to the right
                xPos += (
                    sideIndex * (hexSize * (1 + Mathf.Abs(hexOffset))) * Mathf.Cos((1.0f * Mathf.PI) / 6.0f)
                    );
                zPos += (
                    sideIndex * (hexSize * (1 + Mathf.Abs(hexOffset))) * (3.0f * Mathf.Sin((1.0f * Mathf.PI) / 6.0f))
                    );
                break;

            case 4:
                // Defines the first hex of the side
                xPos = (layer * (hexSize * (1 + Mathf.Abs(hexOffset))) * Mathf.Cos((5.0f * Mathf.PI) / 6.0f));
                zPos = (layer * (hexSize * (1 + Mathf.Abs(hexOffset))) * (3.0f * Mathf.Sin((5.0f * Mathf.PI) / 6.0f)));
                if (sideIndex == 0) break;

                // To the right
                xPos += (
                    sideIndex * (hexSize * (1 + Mathf.Abs(hexOffset))) * (2.0f * Mathf.Cos((1.0f * Mathf.PI) / 6.0f))
                    );
                break;

            case 5:
                // Defines the first hex of the side
                xPos = (layer * (hexSize * (1 + Mathf.Abs(hexOffset))) * Mathf.Cos((1.0f * Mathf.PI) / 6.0f));
                zPos = (layer * (hexSize * (1 + Mathf.Abs(hexOffset))) * (3.0f * Mathf.Sin((1.0f * Mathf.PI) / 6.0f)));
                if (sideIndex == 0) break;

                // Down and to the right
                xPos += (
                    sideIndex * (hexSize * (1 + Mathf.Abs(hexOffset))) * Mathf.Cos((11.0f * Mathf.PI) / 6.0f)
                    );
                zPos += (
                    sideIndex * (hexSize * (1 + Mathf.Abs(hexOffset))) * (3.0f * Mathf.Sin((11.0f * Mathf.PI) / 6.0f))
                    );
                break;

        }
        return new Vector3(xPos, baseHeight, zPos);
    }
}

public class Hex
{
    public string Name;
    public int Subscribers;

    public GameObject thisHex;
    public int layer;
    public int index;

    public Hex(string n, int s, GameObject o, int l, int i)
    {
        Name = n;
        Subscribers = s;
        thisHex = o;
        layer = l;
        index = i;
    }
}
