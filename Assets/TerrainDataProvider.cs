using UnityEngine;

[RequireComponent(typeof(Terrain))]
public class TerrainDataProvider : MonoBehaviour
{
    TerrainData terrainData;

    [SerializeField]
    [Range(0.1f, 1.0f)]
    float quality = 0.5f;

    public int Width { get; private set; }
    public int Height { get; private set; }
    public string ElevationData { get; private set; }

    int density;

    private float[,] GetElevationData()
    {
        return terrainData.GetHeights(0, 0, terrainData.heightmapWidth, terrainData.heightmapHeight);
    }

    void Awake()
    {
        terrainData = GetComponent<Terrain>().terrainData;

        float[,] elevationData = GetElevationData();
        density = (int)Mathf.RoundToInt(1 / quality);

        Width = elevationData.GetLength(0) / density;
        Height = elevationData.GetLength(1) / density;
        ElevationData = Serializer.ToJSONArray(elevationData, density);

        DataProviders.TerrainDataProvider = this;
    } 
}
