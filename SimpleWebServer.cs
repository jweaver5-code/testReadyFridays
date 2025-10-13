using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;

class SimpleWebServer
{
    private HttpListener listener;
    private string rootDirectory;
    private int port;

    public SimpleWebServer(string rootDir, int port = 8000)
    {
        this.rootDirectory = rootDir;
        this.port = port;
        this.listener = new HttpListener();
    }

    public void Start()
    {
        listener.Prefixes.Add($"http://localhost:{port}/");
        listener.Start();
        Console.WriteLine($"Web server started at http://localhost:{port}/");
        Console.WriteLine($"Serving files from: {rootDirectory}");
        Console.WriteLine("Press Ctrl+C to stop the server...");

        ThreadPool.QueueUserWorkItem(ProcessRequests);
    }

    private void ProcessRequests(object state)
    {
        while (listener.IsListening)
        {
            try
            {
                var context = listener.GetContext();
                ThreadPool.QueueUserWorkItem(ProcessRequest, context);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }

    private void ProcessRequest(object state)
    {
        var context = (HttpListenerContext)state;
        var request = context.Request;
        var response = context.Response;

        try
        {
            string path = request.Url.AbsolutePath;
            if (path == "/")
                path = "/index.html";

            string filePath = Path.Combine(rootDirectory, path.TrimStart('/'));
            
            if (File.Exists(filePath))
            {
                string content = File.ReadAllText(filePath);
                byte[] buffer = Encoding.UTF8.GetBytes(content);
                
                // Set content type based on file extension
                string contentType = GetContentType(filePath);
                response.ContentType = contentType;
                response.ContentLength64 = buffer.Length;
                
                // Add CORS headers
                response.Headers.Add("Access-Control-Allow-Origin", "*");
                response.Headers.Add("Access-Control-Allow-Methods", "GET, POST, OPTIONS");
                response.Headers.Add("Access-Control-Allow-Headers", "Content-Type");
                
                response.OutputStream.Write(buffer, 0, buffer.Length);
                response.StatusCode = 200;
            }
            else
            {
                // Try to serve binary files (images, etc.)
                if (File.Exists(filePath))
                {
                    byte[] fileBytes = File.ReadAllBytes(filePath);
                    string contentType = GetContentType(filePath);
                    response.ContentType = contentType;
                    response.ContentLength64 = fileBytes.Length;
                    
                    // Add CORS headers
                    response.Headers.Add("Access-Control-Allow-Origin", "*");
                    response.Headers.Add("Access-Control-Allow-Methods", "GET, POST, OPTIONS");
                    response.Headers.Add("Access-Control-Allow-Headers", "Content-Type");
                    
                    response.OutputStream.Write(fileBytes, 0, fileBytes.Length);
                    response.StatusCode = 200;
                }
                else
                {
                    response.StatusCode = 404;
                    string notFound = "404 - File not found";
                    byte[] buffer = Encoding.UTF8.GetBytes(notFound);
                    response.OutputStream.Write(buffer, 0, buffer.Length);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error processing request: {ex.Message}");
            response.StatusCode = 500;
        }
        finally
        {
            response.OutputStream.Close();
        }
    }

    private string GetContentType(string filePath)
    {
        string extension = Path.GetExtension(filePath).ToLower();
        switch (extension)
        {
            case ".html":
            case ".htm":
                return "text/html";
            case ".css":
                return "text/css";
            case ".js":
                return "application/javascript";
            case ".json":
                return "application/json";
            case ".png":
                return "image/png";
            case ".jpg":
            case ".jpeg":
                return "image/jpeg";
            case ".gif":
                return "image/gif";
            case ".mp3":
                return "audio/mpeg";
            case ".wav":
                return "audio/wav";
            default:
                return "text/plain";
        }
    }

    public void Stop()
    {
        listener.Stop();
        listener.Close();
    }
}

class SimpleWebServerProgram
{
    static void Main(string[] args)
    {
        string currentDirectory = Directory.GetCurrentDirectory();
        var server = new SimpleWebServer(currentDirectory, 8000);
        
        try
        {
            server.Start();
            Console.WriteLine("Web server is running...");
            Console.WriteLine("Open your browser and go to: http://localhost:8000");
            Console.WriteLine("Press any key to stop the server...");
            Console.ReadKey();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error starting server: {ex.Message}");
        }
        finally
        {
            server.Stop();
        }
    }
}

