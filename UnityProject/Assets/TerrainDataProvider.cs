using UnityEngine;

[RequireComponent(typeof(Terrain))]
public class TerrainDataProvider : MonoBehaviour
{
    TerrainData terrainData;
    public int Width { get; private set; }
    public int Height { get; private set; }
    public string ElevationData { get; private set; }

    enum Compression { 
        High, Medium, Low, Off
    }

    [SerializeField]
    Compression dataCompression = Compression.Medium;

    private float Steps { get
        {
            if (dataCompression == Compression.High) return 0.1f;
            else if (dataCompression == Compression.Medium) return 0.2f;
            else if (dataCompression == Compression.Low) return 0.5f;

            return 1f;
        }
    }

    private float[,] GetElevationData()
    {
        return terrainData.GetHeights(0, 0, terrainData.heightmapWidth, terrainData.heightmapHeight);
    }

    void Awake()
    {
        terrainData = GetComponent<Terrain>().terrainData;

        float[,] elevationData = GetElevationData();
        int density = (int)Mathf.RoundToInt(1 / Steps);

        Width = elevationData.GetLength(0) / density;
        Height = elevationData.GetLength(1) / density;

#if UNITY_EDITOR
        Debug.Log(string.Format("Serialize elevation data - Steps: {0} => Density: {1}", Steps, density));
#endif
        ElevationData = Serializer.ToJSONArray(elevationData, density);

        DataProviders.TerrainDataProvider = this;
    } 
}
