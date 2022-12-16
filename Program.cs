// See https://gist.github.com/define-private-public/d05bc52dd0bed1c4699d49e2737e80e7
// See https://learn.microsoft.com/en-us/dotnet/api/system.net.httplistenerrequest?view=net-6.0

using System.Net;

async Task ListenAsync(HttpListener listener)
{
	while(true)
	{
		var context = await listener.GetContextAsync();
		var request = context.Request;

		// Log Request
		Console.WriteLine(request.HttpMethod);
#if true
		foreach (var key in request.Headers.Keys)
		{
			Console.WriteLine("{0}", key);
			Console.WriteLine("\t{0}", request.Headers[key.ToString()]);
		}
#endif

		// Send response
		var response = context.Response;
		// Construct a response.
		string responseTemplate = "<html><body>Hello at<br/>{0}!</body></html>";
		string responseString = string.Format(responseTemplate, DateTime.Now);
		byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
		// Get a response stream and write the response to it.
		response.ContentLength64 = buffer.Length;
		using (var output = response.OutputStream)
		{
			await output.WriteAsync(buffer,0,buffer.Length);
		}
	}
}

using(var listener = new HttpListener())
{
	// Add the prefixes.
	listener.Prefixes.Add("http://localhost:5555/");
	listener.Start();

	Console.WriteLine("Listening...");

	ListenAsync(listener).GetAwaiter().GetResult();
}
