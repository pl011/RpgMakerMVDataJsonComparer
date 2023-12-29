using System.Diagnostics;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

const string NameOfFileToCompare = "System.json";

const string ProjectDataFilePath = "..\\..\\..\\..\\..\\..\\demon fire team\\projectdemonfire\\Crystal Travel V0.01\\data";
string JsonFileToComparePath = ProjectDataFilePath + Path.DirectorySeparatorChar + NameOfFileToCompare;

string myContent = "";
string theirContent = "";

const string myContentTempFileName = "myTemp.json";
const string theirContentTempFileName = "theirTemp.json";

const string WinMergePath = "C:\\Program Files (x86)\\WinMerge\\WinMergeU.exe";

// Find the conflicted system.json file
// Extract the lines for 'theirs' and 'mine'
int counter = 0;
foreach (string line in File.ReadLines(JsonFileToComparePath))
{
    if (counter == 1)
    {
        myContent = line;
    }
    else if (counter == 3)
    {
        theirContent = line;
    }

    counter++;
}

// Format the JSON
myContent = JToken.Parse(myContent).ToString();
theirContent = JToken.Parse(theirContent).ToString();

// Write into temporary text files
File.WriteAllText(myContentTempFileName, myContent);
File.WriteAllText(theirContentTempFileName, theirContent);

// Open into WinMerge
string myContentTempFilePath = Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + myContentTempFileName;
string theirContentTempFilePath = Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + theirContentTempFileName;
ProcessStartInfo info = new ProcessStartInfo(WinMergePath, "\"" + myContentTempFilePath + "\" \"" + theirContentTempFilePath +"\"");
var process = Process.Start(info);
if (process != null)
{
    process.WaitForExit();
}

// Delete temporary text files
File.Delete(myContentTempFileName);
File.Delete(theirContentTempFileName);
