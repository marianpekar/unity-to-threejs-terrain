class Socket {
    constructor(host, port, service) {
        this.socket = new WebSocket(`ws://${host}:${port}/${service}`);

        this.responseConsumer = null;

        this.socket.onmessage = (e) => {
            this.responseConsumer(e.data);
        }
    }

    OnOpen(action) {
        this.socket.onopen = action;
    }

    RequestElevationData(responseConsumer) {
        this.Send("elevationData", responseConsumer);
    }
    
    RequestWidth(responseConsumer) {
        this.Send("width", responseConsumer);
    }
    
    RequestHeight(responseConsumer) {
        this.Send("height", responseConsumer);
    }

    Send(message, responseConsumer) {
        this.socket.send(message);
        this.responseConsumer = responseConsumer;
    }
}

class TerrainData {
    constructor() {
        this.width = 0;
        this.height = 0;
        this.elevationData = [];
    }

    Load() {
        const socket = new Socket("localhost", 5963, "default");
    
        socket.OnOpen(() => {
            socket.RequestElevationData((data) => {
                let jsonData = JSON.parse(data);
    
                jsonData.forEach(element => { this.elevationData.push(element); })
    
                socket.RequestWidth((data) => { 
                    this.width = data;
    
                    socket.RequestHeight((data) => {
                        this.height = data;
                    })
                })
            })
        })
    }
}

terrainData = new TerrainData();
terrainData.Load();

