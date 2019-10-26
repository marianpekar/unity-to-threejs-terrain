class Socket {
    constructor(host, port, service) {
        this.socket = new WebSocket(`ws://${host}:${port}/${service}`);

        this.socket.onmessage = (e) => {
            this.buffer = e.data;
            this.RunConsumers();
        }

        this.buffer = "";
        this.consumers = [];
    }

    RunConsumers() {
        for(let i = 0; i < this.consumers.length; i++)
            this.consumers[i](this.buffer);
    } 

    AddConsumer(func) {
        this.consumers.push(func);
    }

    RequestElevationData() {
        this.socket.send("elevationData");
    }
    
    RequestWidth() {
        this.socket.send("width");
    }
    
    RequestHeight() {
        this.socket.send("height");
    }
}

const socket = new Socket("localhost", 5963, "default");

socket.AddConsumer(LogToConsole);

function LogToConsole(data) {
    console.log(data);
}


