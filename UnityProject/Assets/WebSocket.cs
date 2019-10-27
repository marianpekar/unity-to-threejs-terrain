using UnityEngine;
using WebSocketSharp.Server;

public class WebSocket : MonoBehaviour
{
    [SerializeField]
    int wsServerPort = 5963;

    [SerializeField]
    string servicePath = "/default";

    WebSocketServer wsServer;

    void Start()
    {
        wsServer = new WebSocketServer(wsServerPort);
        wsServer.AddWebSocketService<WebSocketService>(servicePath);
        wsServer.Start();

#if UNITY_EDITOR
        if (wsServer.IsListening)
        {
            Debug.Log(string.Format("Listening on port {0}, and providing WebSocket services:", wsServer.Port));
            foreach (var path in wsServer.WebSocketServices.Paths)
                Debug.Log(string.Format("- {0}", path));
        }
#endif
    }
}
