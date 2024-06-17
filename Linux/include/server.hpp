#ifndef SERVER_HPP
#define SERVER_HPP
#include <cstring>
#include <unistd.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <iostream>
#include <fstream>
#include <sstream>

using namespace std;

class webServer
{
	public:
		void createSocket(int port, string html);
		string readHTMLtostring(const string& path);
};

#endif
