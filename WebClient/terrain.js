class Socket {
    constructor(host, port, service) {
        this.socket = new WebSocket(`ws://${host}:${port}/${service}`);

        this.responseConsumer = null;

        this.socket.onmessage = (e) => {
            this.responseConsumer(e.data);
        }
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

const socket = new Socket("localhost", 5963, "default");
