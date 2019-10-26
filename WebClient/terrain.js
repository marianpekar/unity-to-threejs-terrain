let socket;

function Init() {
    socket = new WebSocket("ws://localhost:5963/terrain");

    socket.onmessage = (e) => {
        console.log(e.data);
    }
}

function RequestElevationData() {
    socket.send("elevationData");
}

function RequestWidth() {
    socket.send("width");
}

function RequestHeight() {
    socket.send("height");
}




