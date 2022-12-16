// See https://gist.github.com/define-private-public/d05bc52dd0bed1c4699d49e2737e80e7
// See https://learn.microsoft.com/en-us/dotnet/api/system.net.httplistenerrequest?view=net-6.0

using System.Net;


if (!HttpListener.IsSupported)
{
	Console.WriteLine ("Windows XP SP2 or Server 2003 is required to use the HttpListener class.");
	return;
}

// Create a listener.
HttpListener listener = new HttpListener();

// Add the prefixes.
listener.Prefixes.Add("http://localhost:5555/");

listener.Start();
Console.WriteLine("Listening...");
// Note: The GetContext method blocks while waiting for a request.
HttpListenerContext context = listener.GetContext();
HttpListenerRequest request = context.Request;
// Obtain a response object.
HttpListenerResponse response = context.Response;
// Construct a response.
string responseString = "<HTML><BODY> Hello world!</BODY></HTML>";
byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
// Get a response stream and write the response to it.
response.ContentLength64 = buffer.Length;
using (System.IO.Stream output = response.OutputStream)
{
	output.Write(buffer,0,buffer.Length);
	// You must close the output stream.
	output.Close();
}

listener.Stop();
