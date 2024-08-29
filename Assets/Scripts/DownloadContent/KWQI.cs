/*
0.2.0 implementation of the KAR Workshop Quick Install format.

KWQI is a JSON based text/binary format used for downloading and managing content.
From custom ROMs to audio files and tools.

The spec for the project and latest version can be found at the Github.
https://github.com/SeanMott/KAR-KWQI

*/
using Newtonsoft.Json;

//defines a stage type
public enum SoftwareStage
{
	Stable = 0, //stable release
	Unstable, //unstable

	Beta,
	Alpha,
	PreRelease,

	Deprecated, //deprecated

	Count
}

//defines a content type
public enum ContentType
{
	Game = 0, //defines a game

	GekkoCode, //defines a gekko code

	ModTool,
	ModTool_Graphics,

	Texture,
	Audio,
	Model,
	Animation,

	AltSkin, //a alt skin for Kirby or the Star

	Rider, //defines a Rider like Kirby or Deedee

	Star, //defines a Star for riding

	City_Map, //a city map

	City_Event, //defines a event in the City

	Statium, //defines a Stadium

	Item, //defines a item pick up

	Patch, //defines a Patch for stats

	PowerUp, //defines a power up

	Count
}

//defines the KWQI struct || depends on Newtonsoft.Json
public class KWQI
{
	public ContentType type = ContentType.Game;
	public SoftwareStage stage = SoftwareStage.Stable;

	public bool doesItOnlyWorkOnASpecificOS = false; //does the content only work on specific platform?
	public bool doesItOnlyWorkOnASpecificOS_Windows = false; //does the content work on Windows? (only true if it only runs on specific OSes)
	public bool doesItOnlyWorkOnASpecificOS_Mac_Intel = false; //does the content work on Mac (Intel)? (only true if it only runs on specific OSes)
	public bool doesItOnlyWorkOnASpecificOS_Mac_MSeries = false; //does the content work on Mac (M series)? (only true if it only runs on specific OSes)
	public bool doesItOnlyWorkOnASpecificOS_Linux_Desktop = false; //does the content work on Linux Desktop? (only true if it only runs on specific OSes)
	
	public bool doesItOnlyWorkOnASpecificOS_Android = false; //does the content work on Android? (only true if it only runs on specific OSes)
	public bool doesItOnlyWorkOnASpecificOS_IOS = false; //does the content work on IOS? (only true if it only runs on specific OSes)
	
	public bool doesItOnlyWorkOnASpecificOS_GameCube = false; //does the content work on Game Cube? (only true if it only runs on specific OSes)
	public bool doesItOnlyWorkOnASpecificOS_Wii = false; //does the content work on Wii? (only true if it only runs on specific OSes)
	public bool doesItOnlyWorkOnASpecificOS_WiiU = false; //does the content work on Wii U? (only true if it only runs on specific OSes)
	public bool doesItOnlyWorkOnASpecificOS_Switch = false; //does the content work on Switch? (only true if it only runs on specific OSes)
	public bool doesItOnlyWorkOnASpecificOS_Linux_Switch_Sheild = false; //does the content work on Switch Linux (Sheild)? (only true if it only runs on specific OSes)
	public bool doesItOnlyWorkOnASpecificOS_Linux_Switch_Tablet = false; //does the content work on Switch Linux (Tablet)? (only true if it only runs on specific OSes)

	public string displayName = "KAR Content"; //the display name for showing in GUI
	public string shortDesc = "Short tooltip/quick description of content"; //the short description used for tool tips
	public string longDescription = "Much longer description of your content. \n\n\n\n\nCan be paragraphs and link to whatever Github or what have you."; //the long description

	public string internalName = "Content_Internal"; //the internal package name
	public string author = "Author Here"; //the author's name

	public string date = "MM/DD/YYYY"; //the date this version was released
	public string contentVersion = "0.0.0"; //the version of the content

	public string KWQIFormatVersion = "0.1.0"; //the version of the KWQI spec

	public string ContentDownloadURL_Windows = ""; //the URL for downloading the item on Windows || this is the default URL if it's not platform dependent
	public string ContentDownloadURL_Mac_Intel = "";  //the URL for downloading the item on Mac (Intel)
	public string ContentDownloadURL_Mac_MSeries = ""; //the URL for downloading the item on Mac (M series)
	public string ContentDownloadURL_Linux_Desktop = ""; //the URL for downloading the item on Linux Desktop

	public string ContentDownloadURL_Android = ""; //the URL for downloading the item on Android
	public string ContentDownloadURL_IOS = ""; //the URL for downloading the item on ISO

	public string ContentDownloadURL_GameCube = ""; //the URL for downloading the item on Game Cube
	public string ContentDownloadURL_Wii = ""; //the URL for downloading the item on Wii
	public string ContentDownloadURL_WiiU = ""; //the URL for downloading the item on Wii U
	public string ContentDownloadURL_Switch = ""; //the URL for downloading the item on Switch
	public string ContentDownloadURL_Linux_Switch_Sheild = ""; //the URL for downloading the item on Switch Linux (Sheild)?
	public string ContentDownloadURL_Linux_Switch_Tablet = ""; //the URL for downloading the item on Switch Linux (Tablet)?

	public KWQI() {}

	//writes a KWQI file
	public static bool WriteKWQI(string dir, string filename, KWQI data)
	{
		System.IO.StreamWriter file = new System.IO.StreamWriter(dir + "/" + filename + ".KWQI");
		file.Write(JsonConvert.SerializeObject(data));
		file.Close();

		return true;
	}

	//loads a KWQI file
	public static KWQI LoadKWQI(string path)
	{
		System.IO.StreamReader file = new System.IO.StreamReader(path);
		return JsonConvert.DeserializeObject<KWQI>(file.ReadToEnd());
	}
}