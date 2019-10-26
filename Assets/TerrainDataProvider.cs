using UnityEngine;

[RequireComponent(typeof(Terrain))]
public class TerrainDataProvider : MonoBehaviour
{
    TerrainData terrainData;

    [SerializeField]
    [Range(0.01f, 1.0f)]
    float quality = 0.1f;

    public int Width { get; private set; }
    public int Height { get; private set; }
    public string ElevationData { get; private set; }

    private float[,] GetElevationData()
    {
        return terrainData.GetHeights(0, 0, (int)(terrainData.heightmapWidth * quality), (int)(terrainData.heightmapHeight * quality));
    }

    void Awake()
    {
        terrainData = GetComponent<Terrain>().terrainData;

        float[,] elevationData = GetElevationData();

        Width = elevationData.GetLength(0);
        Height = elevationData.GetLength(1);
        ElevationData = Serializer.ToJSONArray(elevationData);

        DataProviders.TerrainDataProvider = this;
    } 
}
