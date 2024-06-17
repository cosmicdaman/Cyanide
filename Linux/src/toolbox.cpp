#include <toolbox.hpp>

namespace fs = std::filesystem;

std::string getHomeDir()
{
	const char* homeDir = std::getenv("HOME");
	return std::string(homeDir);
}

void toolbox::refresh()
{
			std::string Blocks = "";
		    std::string ToolBox = "";
		    std::string Impl = "";
		    std::unordered_map<std::string, std::vector<std::string>> CustomToolbox;
		
		    std::string dir = getHomeDir() + "/.cyanide";
			
			std::system("mkdir ~/.cyanide/CyanideRepo");
			
			std::system("git clone https://github.com/ThatGuyAstral/Cyanide.git ~/.cyanide/CyanideRepo");
			
			std::system("mkdir -p ~/.cyanide/CyanideDevelopmentEnvironment/");
			
			std::system("cp -r ~/.cyanide/CyanideRepo/CyanideDevelopmentEnvironment/* ~/.cyanide/CyanideDevelopmentEnvironment/");

			std::system("rm -rf ~/.cyanide/CyanideRepo");
			
		    for (const auto& block : fs::directory_iterator(dir + "/CyanideDevelopmentEnvironment/Blocks/Blocks")) {
		        if (block.is_regular_file()) {
		            std::ifstream blockFile(block.path());
		            std::string blockContent((std::istreambuf_iterator<char>(blockFile)), std::istreambuf_iterator<char>());
		            Blocks += "Blockly.defineBlocksWithJsonArray([" + blockContent + "]);\n";
		        }
		    }
		
		    for (const auto& category : fs::directory_iterator(dir + "/CyanideDevelopmentEnvironment/Blocks/Blocks")) {
		        if (category.is_directory()) {
		            std::string categoryName = category.path().filename().string();
		            CustomToolbox[categoryName] = std::vector<std::string>();
		
		            for (const auto& block : fs::directory_iterator(category)) {
		                if (block.is_regular_file()) {
		                    std::ifstream blockFile(block.path());
		                    std::string blockContent((std::istreambuf_iterator<char>(blockFile)), std::istreambuf_iterator<char>());
		                    Blocks += "Blockly.defineBlocksWithJsonArray([" + blockContent + "]);\n";
		
		                    CustomToolbox[categoryName].push_back(block.path().filename().stem().string());
		                }
		            }
		        }
		    }
		
		    for (const auto& impl : fs::directory_iterator(dir + "/CyanideDevelopmentEnvironment/Blocks/JS")) {
		        if (impl.is_regular_file()) {
		            std::ifstream implFile(impl.path());
		            std::string implContent((std::istreambuf_iterator<char>(implFile)), std::istreambuf_iterator<char>());
		            std::string implName = impl.path().filename().stem().string();
		            Impl += "Gen.forBlock['" + implName + "'] = " + implContent + "\n";
		        }
		    }
		
		    for (const auto& category : CustomToolbox) {
		        ToolBox += "<category name=\"" + category.first + "\" colour=\"#5b80a5\">\n";
		        for (const auto& block : category.second) {
		            ToolBox += "<block type=\"" + block + "\"></block>\n";
		        }
		        ToolBox += "</category>\n";
		    }

		    std::ifstream file(dir + "/CyanideDevelopmentEnvironment/UI.html");
		    if (!file.is_open()) {
		            std::cout << "\x1b[1;36mcyanide\x1b[0m: failed to refresh toolbox: UI.html failed to open";
		    }
		    std::string content((std::istreambuf_iterator<char>(file)),
		                            (std::istreambuf_iterator<char>()));
		    file.close();
		    
		    if (content.find("%ToolBoxLoader%") != std::string::npos) {
		        content.replace(content.find("%ToolBoxLoader%"), strlen("%ToolBoxLoader%"), ToolBox);
		   	}

		   	if (content.find("%BlocksLoader%") != std::string::npos) {
		   			        content.replace(content.find("%BlocksLoader%"), strlen("%BlocksLoader%"), Blocks);
		   	}

		   	if (content.find("%ImplLoader%") != std::string::npos) {
		   			   			        content.replace(content.find("%ImplLoader%"), strlen("%ImplLoader%"), Impl);
		  	}

		   	std::ofstream html;
		   	html.open(dir + "/CyanideDevelopmentEnvironment/UI.html");
			if (!html) std::cout << "\x1b[1;36mcyanide\x1b[0m: failed to refresh toolbox: couldn't write to UI.html";
			html << content;
			html.close();
}
