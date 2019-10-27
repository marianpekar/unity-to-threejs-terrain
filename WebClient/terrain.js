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

                        Start();
                    })
                })
            })
        })
    }
}

terrainData = new TerrainData();
terrainData.Load();

function Start() {
    // options
    const DEPTH = 600;
    const PLANE_WIDTH = 1024;
    const PLANE_HEIGHT = 1024;

    const ROTATE = false;
    const ROTATION_SPEED = 0.001;

    const CAMERA_FOV = 75;
    const CAMERA_NEAR_PLANE = 0.1;
    const CAMERA_FAR_PLANE = 2000;

    // objects
    let scene = new THREE.Scene();
    scene.background = new THREE.Color( 0x29434e );
    let camera = new THREE.PerspectiveCamera( CAMERA_FOV, window.innerWidth/window.innerHeight, CAMERA_NEAR_PLANE, CAMERA_FAR_PLANE );
    let renderer = new THREE.WebGLRenderer();

    new THREE.OrbitControls( camera, renderer.domElement );

    // event listeners
    window.addEventListener( 'resize', onWindowResize, false );

    function onWindowResize(){
        camera.aspect = window.innerWidth / window.innerHeight;
        camera.updateProjectionMatrix();

        renderer.setSize( window.innerWidth, window.innerHeight );
    }

    renderer.setSize( window.innerWidth, window.innerHeight );
    document.body.appendChild( renderer.domElement );

    // construct and add terrain
    let geometry = new THREE.PlaneGeometry(PLANE_WIDTH, PLANE_HEIGHT, terrainData.width - 1, terrainData.height - 1);

    for (let i = 0, l = geometry.vertices.length; i < l; i++) {
        geometry.vertices[i].z = terrainData.elevationData[i] * DEPTH;
      }

    let material = new THREE.MeshBasicMaterial( { color: 0x00e676, wireframe: true } );
    let plane = new THREE.Mesh( geometry, material );
    plane.rotation.x = -Math.PI / 2;
    scene.add( plane );

    // position camera
    camera.position.z = PLANE_WIDTH * 0.9;
    camera.position.y = PLANE_WIDTH * 0.2;
    camera.lookAt(plane.position);

    // render loop
    let animate = function () {
        requestAnimationFrame( animate );

        if(ROTATE)
            plane.rotation.z += ROTATION_SPEED;

        renderer.render( scene, camera );
    };

    animate();
}

