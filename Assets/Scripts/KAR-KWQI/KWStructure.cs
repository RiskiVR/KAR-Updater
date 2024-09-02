/*
0.3.0 implementation of how the KAR Workshop Quick Install format.

The spec for the project and latest version can be found at the Github.
https://github.com/SeanMott/KAR-KWQI

*/

using System.IO;

//defines a global for handling the KWStructure
public class KWStructure
{
	//------HIGH LEVEL DIRECTORIES------//

	//gets a string name for netplay client directory
	public static string GetStringDirectoryName_NetplayClients() {return "Clients";}

	//gets a string name for accounts directory
	public static string GetStringDirectoryName_Accounts() {return "Accounts";}

	//gets a string name for mods directory
	public static string GetStringDirectoryName_Mods() {return "Mods";}

	//gets a string name for tools directory
	public static string GetStringDirectoryName_Tools() {return "Tools";}

	//gets a string name for ROMs directory
	public static string GetStringDirectoryName_ROMs() {return "ROMs";}

	//gets a string name for KWQI file directory
	public static string GetStringDirectoryName_KWQI() {return "KWQI";}

	//gets a string name for Replays directory
	public static string GetStringDirectoryName_Replays() {return "Replays";}

	//------SUB DIRECTORIES-------//

	 //gets a string name for mods sub-directory for Skin Packs
	public static string GetStringDirectoryName_Mods_SkinPacks() {return "SkinPacks";}

	//gets a string name for mods sub-directory for Homebrew
	public static string GetStringDirectoryName_Mods_Homebrew() {return "Homebrew";}

	//gets a string name for clients sub-directory for User
	public static string GetStringDirectoryName_Clients_User() {return "User";}

	//gets a string name for clients sub-directory User sub directory for Game Settings
	public static string GetStringDirectoryName_Clients_User_GameSettings() {return "GameSettings";}

	//generates a netplay client directory
	public static DirectoryInfo GenerateKWStructure_Directory_NetplayClients(DirectoryInfo rootDir)
	{
		DirectoryInfo dir = new DirectoryInfo(rootDir.FullName + "/" + GetStringDirectoryName_NetplayClients());

		if (!dir.Exists)
			dir.Create();

		return dir;
	}

	//generates a accounts directory
	public static DirectoryInfo GenerateKWStructure_Directory_Accounts(DirectoryInfo rootDir)
	{
		DirectoryInfo dir = new DirectoryInfo(rootDir.FullName + "/" + GetStringDirectoryName_Accounts());

		if (!dir.Exists)
			dir.Create();

		return dir;
	}

	//generates a mods directory
	public static DirectoryInfo GenerateKWStructure_Directory_Mods(DirectoryInfo rootDir)
	{
		DirectoryInfo dir = new DirectoryInfo(rootDir.FullName + "/" + GetStringDirectoryName_Mods());

		if (!dir.Exists)
			dir.Create();

		return dir;
	}

	//generates a tools directory
	public static DirectoryInfo GenerateKWStructure_Directory_Tools(DirectoryInfo rootDir)
	{
		DirectoryInfo dir = new DirectoryInfo(rootDir + "/" + GetStringDirectoryName_Tools());

		if (!dir.Exists)
			dir.Create();

		return dir;
	}

	//generates a ROMs directory
	public static DirectoryInfo GenerateKWStructure_Directory_ROMs(DirectoryInfo rootDir)
	{
		DirectoryInfo dir = new DirectoryInfo(rootDir.FullName + "/" + GetStringDirectoryName_ROMs());

		if (!dir.Exists)
			dir.Create();

		return dir;
	}

	//generates KWQI file directory
	public static DirectoryInfo GenerateKWStructure_Directory_KWQI(DirectoryInfo rootDir)
	{
		DirectoryInfo dir = new DirectoryInfo(rootDir.FullName + "/" + GetStringDirectoryName_KWQI());

		if (!dir.Exists)
			dir.Create();

		return dir;
	}

	//generates Replays directory
	public static DirectoryInfo GenerateKWStructure_Directory_Replays(DirectoryInfo rootDir)
	{
		DirectoryInfo dir = new DirectoryInfo(rootDir.FullName + "/" + GetStringDirectoryName_Replays());

		if (!dir.Exists)
			dir.Create();

		return dir;
	}

	//generates Clients sub directory for User
	public static DirectoryInfo GenerateKWStructure_SubDirectory_Clients_User(DirectoryInfo rootDir)
	{
		DirectoryInfo dir = new DirectoryInfo(GenerateKWStructure_Directory_NetplayClients(rootDir) + "/" + GetStringDirectoryName_Clients_User());

		if (!dir.Exists)
			dir.Create();

		return dir;
	}

	//generates Clients sub directory User sub directory Game Settings
	public static DirectoryInfo GenerateKWStructure_SubDirectory_Clients_User_GameSettings(DirectoryInfo rootDir)
	{
		DirectoryInfo dir = new DirectoryInfo(GenerateKWStructure_SubDirectory_Clients_User(rootDir) + "/" + GetStringDirectoryName_Clients_User_GameSettings());

		if (!dir.Exists)
			dir.Create();

		return dir;
	}

	//generates mod sub directory for Skin Packs
	public static DirectoryInfo GenerateKWStructure_SubDirectory_Mod_SkinPacks(DirectoryInfo rootDir)
	{
		DirectoryInfo dir = new DirectoryInfo(GenerateKWStructure_Directory_Mods(rootDir) + "/" + GetStringDirectoryName_Mods_SkinPacks());

		if (!dir.Exists)
			dir.Create();

		return dir;
	}

	//generates mod sub directory for Homebrew
	public static DirectoryInfo GenerateKWStructure_SubDirectory_Mod_Hombrew(DirectoryInfo rootDir)
	{
		DirectoryInfo dir = new DirectoryInfo(GenerateKWStructure_Directory_Mods(rootDir) + "/" + GetStringDirectoryName_Mods_Homebrew());

		if (!dir.Exists)
			dir.Create();

		return dir;
	}

	//gets the brotli tool used on windows
	public static FileInfo GetSupportTool_Brotli_Windows(DirectoryInfo rootDir)
	{
		return new FileInfo(GenerateKWStructure_Directory_Tools(rootDir) + "/Windows/brotli.exe");
	}
}
