using WebSocketSharp;
using WebSocketSharp.Server;

public class WebSocketService : WebSocketBehavior
{
    protected override void OnMessage(MessageEventArgs e)
    {
#if UNITY_EDITOR
        UnityEngine.Debug.Log(string.Format("WebSocketService received: {0}", e.Data));
#endif
        if (e.Data == "elevationData")
            Send(DataProviders.TerrainDataProvider.ElevationData);
        else if (e.Data == "width")
            Send(DataProviders.TerrainDataProvider.Width.ToString());
        else if (e.Data == "height")
            Send(DataProviders.TerrainDataProvider.Height.ToString());
        else
            Send("Unknown request.");
    }
}
