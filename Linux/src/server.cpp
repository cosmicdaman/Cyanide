#include <server.hpp>

string webServer::readHTMLtostring(const string& path)
{
	ifstream file(path);

	if (!file.is_open())
	{
		cerr << "\x1b[1;36mcyanide\x1b[0m: error reading HTML\n";
	}

	ostringstream ss;

	ss << file.rdbuf();

	file.close();

	return ss.str();
}

void webServer::createSocket(int port, string html)
{
	int servSocket = socket(AF_INET, SOCK_STREAM, 0);
	if (servSocket == -1) cerr << "\x1b[1;36mcyanide\x1b[0m: error creating socket";

	sockaddr_in servAddress{};
	servAddress.sin_family = AF_INET;
	servAddress.sin_port = htons(port);
	servAddress.sin_addr.s_addr = INADDR_ANY;
	
	if (bind(servSocket, (struct sockaddr*)&servAddress, sizeof(servAddress)) == -1)
	{
		cerr << "\x1b[1;36mcyanide\x1b[0m: error binding socket";
		close(servSocket);
	}

	if (listen(servSocket, 5) == -1) 
	{
		cerr << "\x1b[1;36mcyanide\x1b[0m: error listening on socket";
		close(servSocket);
	}
	
	for (;;)
	{
		sockaddr_in cliAddress{};
		socklen_t clientLen = sizeof(cliAddress);
		int cliSocket = accept(servSocket, (struct sockaddr*)&cliAddress, &clientLen);
		if (cliSocket == -1)
		{
			cerr << "\x1b[1;36mcyanide\x1b[0m: error accepting connection";
			close(cliSocket);
			break;
		}

		string httpResponse = "HTTP/1.1 200 OK\r\n";
		httpResponse += "Content-Type: text/html\r\n";
		httpResponse += "Content-Length: " + std::to_string(html.length()) + "\r\n";
		httpResponse += "\r\n";
		httpResponse += html;
		
		send(cliSocket, httpResponse.c_str(), httpResponse.size(), 0);
		close(cliSocket);
	}

	close(servSocket);
}
