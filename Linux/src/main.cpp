#include <iostream>
#include <server.hpp>
#include <toolbox.hpp>

using namespace std;

int main(int argc, char** argv)
{
	if (argc < 2) cout << "\x1b[1;36mcyanide\x1b[0m: no parameters specified\nfor a list of parameters, use `cyanide -lp`\n";
	else
	{
		if (string(argv[1]) == "-lp")
		{
			cout << "-lp        :: shows a list of parameters\n";
			cout << "-ws <path> :: starts a local webserver with the Editor\n";
			cout << "-b         :: builds a cyanide project\n";
			cout << "-r         :: refreshes the toolbox (use if it is your first time!)\n";
		}
		else if (string(argv[1]) == "-ws")
		{
			if (argc < 3) cout << "\x1b[1;36mcyanide\x1b[0m: no path specified\n";
			else
			{
				cout << "\x1b[1;36mcyanide\x1b[0m: please head to http://localhost:8081 on a web browser\n";

				webServer ws;				

				const char* homeDir = getenv("HOME");
				const char* htmlPath = strcat(strcpy(new char[strlen(homeDir) + strlen("/.cyanide/CyanideDevelopmentEnvironment/UI.html") + 1], homeDir), "/.cyanide/CyanideDevelopmentEnvironment/UI.html");
				
				ws.createSocket(8081, ws.readHTMLtostring(htmlPath));
			}
		}
		else if (string(argv[1]) == "-r")
		{
			toolbox tb;
			tb.refresh();
		}
	}
}
