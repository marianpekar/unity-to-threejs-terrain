using UnityEngine;
using WebSocketSharp.Server;

public class WebSocket : MonoBehaviour
{
    [SerializeField]
    int wsServerPort = 5963;

    WebSocketService webSocketService = new WebSocketService();
    WebSocketServer wsServer;
    void Start()
    {
        wsServer = new WebSocketServer(wsServerPort);
        wsServer.AddWebSocketService<WebSocketService>("/terrain");
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
