const http = require('http');
const fs = require('fs');
const path = require('path');

const port = 3000;

// MIME types for different file extensions
const mimeTypes = {
    '.html': 'text/html',
    '.css': 'text/css',
    '.js': 'application/javascript',
    '.json': 'application/json',
    '.png': 'image/png',
    '.jpg': 'image/jpeg',
    '.jpeg': 'image/jpeg',
    '.gif': 'image/gif',
    '.mp3': 'audio/mpeg',
    '.wav': 'audio/wav',
    '.ico': 'image/x-icon'
};

const server = http.createServer((req, res) => {
    // Add CORS headers
    res.setHeader('Access-Control-Allow-Origin', '*');
    res.setHeader('Access-Control-Allow-Methods', 'GET, POST, OPTIONS');
    res.setHeader('Access-Control-Allow-Headers', 'Content-Type');
    
    // Handle preflight requests
    if (req.method === 'OPTIONS') {
        res.writeHead(200);
        res.end();
        return;
    }
    
    let filePath = req.url;
    if (filePath === '/') {
        filePath = '/index.html';
    }
    
    // Remove leading slash and resolve path
    filePath = filePath.substring(1);
    const fullPath = path.join(__dirname, filePath);
    
    // Security check - prevent directory traversal
    if (!fullPath.startsWith(__dirname)) {
        res.writeHead(403);
        res.end('Forbidden');
        return;
    }
    
    // Check if file exists
    fs.access(fullPath, fs.constants.F_OK, (err) => {
        if (err) {
            res.writeHead(404);
            res.end('File not found');
            return;
        }
        
        // Get file extension and set content type
        const ext = path.extname(fullPath).toLowerCase();
        const contentType = mimeTypes[ext] || 'text/plain';
        
        // Read and serve the file
        fs.readFile(fullPath, (err, data) => {
            if (err) {
                res.writeHead(500);
                res.end('Error reading file');
                return;
            }
            
            res.writeHead(200, { 'Content-Type': contentType });
            res.end(data);
        });
    });
});

server.listen(port, () => {
    console.log(`Web server running at http://localhost:${port}/`);
    console.log('Serving files from:', __dirname);
    console.log('Press Ctrl+C to stop the server');
    console.log('');
    console.log('Available URLs:');
    console.log(`  Main App: http://localhost:${port}/index.html`);
    console.log(`  Subway Game: http://localhost:${port}/subway/Subway-Surfers/game.html`);
    console.log(`  Texture Test: http://localhost:${port}/test-texture.html`);
    console.log(`  Subway Test: http://localhost:${port}/test-subway.html`);
});

