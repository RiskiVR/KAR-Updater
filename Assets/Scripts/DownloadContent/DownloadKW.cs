using System;
using System.IO;
using System.Net;
using UnityEngine;

//downloads the latest skin packs
public class DownloadKW : MonoBehaviour
{
	//gets the latest content
	public void GetLatest()
	{
		MainUI.instance.headerText.text = "Downloading KAR Workshop...";
		try
		{
#if UNITY_EDITOR
            DirectoryInfo installDir = new DirectoryInfo("C:/Users/rafal/Desktop/Boot test/KARNetplay"); //for editor
#else
        DirectoryInfo installDir = new DirectoryInfo(Environment.CurrentDirectory); //for standalone release

#endif
			//attempt to load KWQI data, if not found use a baked in URL
			string KWQIFilePath = "KWQI/KARWorkshop.KWQI";
			KWQI content = new KWQI();
			if(!File.Exists(KWQIFilePath))
			{
				content.internalName = "KARWorkshop";
				content.ContentDownloadURL_Windows = $"https://github.com/SeanMott/KARphin_Modern/releases/download/kw/KARWorkshop.br";
				KWQI.WriteKWQI(KWStructure.GenerateKWStructure_Directory_KWQI(installDir), content.internalName, content);
			}
			else
			{
				content = KWQI.LoadKWQI(KWQIFilePath);
			}

            //downloads || returns the file it downloaded
            FileInfo archive = KWQIWebClient.Download_Archive_Windows(installDir, content.ContentDownloadURL_Windows, "KARWorkshop");

            //extracts || returns the final extracted folder
            DirectoryInfo uncompressedData = KWQIArchive.Unpack_Windows(KWStructure.GetSupportTool_Brotli_Windows(installDir), archive, installDir);

            //installs the content
            KWInstaller.Root_AllContent(new DirectoryInfo(uncompressedData.FullName + "/UncompressedPackages/" + "KARWorkshop"), installDir);

            //deletes the uncompressed and packaged data
            uncompressedData.Delete(true);
            archive.Delete();

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