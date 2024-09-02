using System;
using System.IO;
using System.Net;
using UnityEngine;

//downloads the latest Backside Gekko Codes
public class DownloadBSCodes : MonoBehaviour
{
	//gets the BS codes
	public static void GetBSCodes(DirectoryInfo installDir)
	{
		string fileExt = ".ini";
		
		//attempt to load KWQI data, if not found use a baked in URL
        string KWQIFilePath = "KWQI/BSGekkoCodes.KWQI";
        KWQI content = new KWQI();
        if(!File.Exists(KWQIFilePath))
        {
            content.internalName = "KBSE01";
		    content.ContentDownloadURL_Windows = $"https://github.com/SeanMott/KARphin_Modern/releases/download/gekko/KBSE01{fileExt}";
		    KWQI.WriteKWQI(KWStructure.GenerateKWStructure_Directory_KWQI(installDir), content.internalName, content);
        }
        else
        {
            content = KWQI.LoadKWQI(KWQIFilePath);
        }

		//downloads the codes
		KWQIWebClient.Download_GekkoCodes_Windows(KWStructure.GenerateKWStructure_SubDirectory_Clients_User_GameSettings(installDir),
		content.ContentDownloadURL_Windows, "KBSE01");
	}

	//gets the latest content
	public void GetLatest()
	{
        MainUI.instance.headerText.text = "Downloading Backside Codes...";
		try
		{
#if UNITY_EDITOR
            DirectoryInfo installDir = new DirectoryInfo("C:/Users/rafal/Desktop/Boot test/KARNetplay"); //for editor
#else
        DirectoryInfo installDir = new DirectoryInfo(Environment.CurrentDirectory); //for standalone release

#endif
            Debug.Log(installDir.FullName);

            GetBSCodes(installDir);

            MainUI.instance.audioSource.PlayOneShot(MainUI.instance.menu[6]);
			MainUI.instance.audioSource.PlayOneShot(MainUI.instance.menu[2]);
			MainUI.instance.headerText.text = "<color=green>Download Complete!";
		}
		catch (Exception e)
		{
			MainUI.instance.audioSource.PlayOneShot(MainUI.instance.menu[4]);
			MainUI.instance.headerText.text = "<color=red>Download Failed!";
			Debug.LogError(e);
			MessageUI.MessageBox(IntPtr.Zero, e.ToString(), "Download Failed!", 0);
		}
	}
}