const fs = require('fs');
const http = require('http');
const url = require('url');

const index = fs.readFileSync(`${__dirname}/index.html`, 'utf-8');
const threejs = fs.readFileSync(`${__dirname}/three-js/three.min.js`, 'utf-8');
const orbitControls = fs.readFileSync(`${__dirname}/three-js/controls/OrbitControls.js`, 'utf-8');
const terrain = fs.readFileSync(`${__dirname}/terrain.js`, 'utf-8');
const socket = fs.readFileSync(`${__dirname}/socket.js`, 'utf-8');

const server = http.createServer((req, res) => {
  const { query, pathname } = url.parse(req.url, true);

  if (pathname === '/') {
    res.writeHead(200, { 'Content-type': 'text/html', });
    res.end(index);
  } 
  else if (pathname === '/three-js/three.min.js') 
  {
    res.writeHead(200, { 'Content-type': 'application/javascript', });
    res.end(threejs);
  }
  else if (pathname === '/three-js/controls/OrbitControls.js') 
  {
    res.writeHead(200, { 'Content-type': 'application/javascript', });
    res.end(orbitControls);
  }
  else if (pathname === '/socket.js') 
  {
    res.writeHead(200, { 'Content-type': 'application/javascript', });
    res.end(socket);
  }
  else if (pathname === '/terrain.js') 
  {
    res.writeHead(200, { 'Content-type': 'application/javascript', });
    res.end(terrain);
  }
  else 
  {
    res.writeHead(404, {'Content-type': 'text/html', });
    res.end('<h1>.:.. .... .:..</h1>');
  }
});

server.listen(80, '127.0.0.1', () => {
  console.log('Listening to requests on port 80');
});
