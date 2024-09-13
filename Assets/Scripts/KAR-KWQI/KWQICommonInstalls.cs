/*
0.3.0 implementation of how the KAR Workshop Quick Install format.

The spec for the project and latest version can be found at the Github.
https://github.com/SeanMott/KAR-KWQI

*/

//defines a class for common downloads of the core packages

using System;
using System.IO;
using System.IO.Compression;
using System.Net;

class KWQICommonInstalls
{
    //installs the latest KARphin
    public static bool GetLatest_KARphin(DirectoryInfo installTarget)
    {
        //downloads the latest KARphin
        FileInfo archive = KWQIWebClient.Download_Archive_Windows(installTarget,
        "https://github.com/SeanMott/KARphin_Modern/releases/download/latest/KARphin.zip",
        "KARphin");

        //unpacks it
        DirectoryInfo uncompressed = new DirectoryInfo(installTarget.FullName + "/KARphin");
        ZipFile.ExtractToDirectory(archive.FullName, uncompressed.FullName, true);

        //moves the contents into the target directory
        KWInstaller.CopyAllDirContents(uncompressed, installTarget);
        KWInstaller.CopyAllDirContents(uncompressed, installTarget);

        //clean up
        archive.Delete();
        uncompressed.Delete(true);

        return true;
    }

    //installs the latest KARphin Dev
    public static bool GetLatest_KARphinDev(DirectoryInfo installTarget)
    {
        //downloads the latest KARphin
        FileInfo archive = KWQIWebClient.Download_Archive_Windows(installTarget,
        "https://github.com/SeanMott/KARphin_Modern/releases/download/latest-dev/KARphinDev.zip",
        "KARphinDev");

        //unpacks it
        DirectoryInfo uncompressed = new DirectoryInfo(installTarget.FullName + "/KARphinDev");
        ZipFile.ExtractToDirectory(archive.FullName, uncompressed.FullName, true);
        
        //moves the contents into the target directory
        KWInstaller.CopyAllDirContents(uncompressed, installTarget);
        KWInstaller.CopyAllDirContents(uncompressed, installTarget);

        //clean up
        archive.Delete();
        uncompressed.Delete(true);

        return true;
    }

    //installs the latest Hack Pack ROM

    //installs the latest Backside ROM

    //installs the latest Hack Pack Gekko Codes
    public static bool GetLatest_GekkoCodes_HackPack(DirectoryInfo installTarget)
    {
        //downloads the latest KARphin
        KWQIWebClient.Download_GekkoCodes_Windows(installTarget,
			"https://github.com/SeanMott/KARphin_Modern/releases/download/gekko/KHPE01.ini",
			"KHPE01");
        return true;
    }

    //installs the latest Backside Gekko Codes
    public static bool GetLatest_GekkoCodes_Backside(DirectoryInfo installTarget)
    {
        //downloads the latest KARphin
        KWQIWebClient.Download_GekkoCodes_Windows(installTarget,
			"https://github.com/SeanMott/KARphin_Modern/releases/download/gekko/KBSE01.ini",
			"KBSE01");
        return true;
    }

    //installs the latest JP KAR Gekko Codes
    public static bool GetLatest_GekkoCodes_JP(DirectoryInfo installTarget)
    {
        //downloads the latest KARphin
        KWQIWebClient.Download_GekkoCodes_Windows(installTarget,
			"https://github.com/SeanMott/KARphin_Modern/releases/download/gekko/GKYP01.ini",
			"GKYP01");
        return true;
    }

    //installs the latest NA KAR Gekko Codes
    public static bool GetLatest_GekkoCodes_NA(DirectoryInfo installTarget)
    {
        //downloads the latest KARphin
        KWQIWebClient.Download_GekkoCodes_Windows(installTarget,
			"https://github.com/SeanMott/KARphin_Modern/releases/download/gekko/GKYE01.ini",
			"GKYE01");
        return true;
    }

    //installs the latest Skin Packs
    public static bool GetLatest_SkinPacks(DirectoryInfo installTarget)
    {
        //downloads the latest KARphin
        FileInfo archive = KWQIWebClient.Download_Archive_Windows(installTarget,
            "https://github.com/SeanMott/KAR-Workshop/releases/download/KWQI-Data-Dev/SkinPacks.zip",
            "SkinPacks");

        //unpacks it
        DirectoryInfo uncompressed = new DirectoryInfo(installTarget.FullName + "/SkinPacks");
        ZipFile.ExtractToDirectory(archive.FullName, uncompressed.FullName, true);

        //moves the contents into the target directory
        KWInstaller.CopyAllDirContents(uncompressed, installTarget);
        KWInstaller.CopyAllDirContents(uncompressed, installTarget);

        //clean up
        archive.Delete();
        uncompressed.Delete(true);

        return true;
    }

    //installs the latest KARDont
    public static bool GetLatest_KARDont(DirectoryInfo installTarget)
    {
        //downloads the latest KARphin
        FileInfo archive = KWQIWebClient.Download_Archive_Windows(installTarget,
            "https://github.com/SeanMott/KARDont/releases/download/latest/KARDont.zip",
            "KARDont");

        //unpacks it
        DirectoryInfo uncompressed = new DirectoryInfo(installTarget.FullName);
        ZipFile.ExtractToDirectory(archive.FullName, uncompressed.FullName, true);

        //moves the contents into the target directory
       // KWInstaller.CopyAllDirContents(uncompressed, installTarget);
      //  KWInstaller.CopyAllDirContents(uncompressed, installTarget);

        //clean up
        archive.Delete();
       // uncompressed.Delete(true);

        return true;
    }

    //installs the latest Client Deps
    public static bool GetLatest_ClientDeps(DirectoryInfo installTarget)
    {
        //downloads the latest KARphin
        FileInfo archive = KWQIWebClient.Download_Archive_Windows(installTarget,
            "https://github.com/SeanMott/KAR-Workshop/releases/download/KWQI-Data-Dev/ClientDeps.zip",
			"ClientDeps");

        //unpacks it
        DirectoryInfo uncompressed = new DirectoryInfo(installTarget.FullName + "/ClientDeps");
        ZipFile.ExtractToDirectory(archive.FullName, uncompressed.FullName, true);

        //moves the contents into the target directory
        KWInstaller.CopyAllDirContents(uncompressed, installTarget);
        KWInstaller.CopyAllDirContents(uncompressed, installTarget);

        //clean up
        archive.Delete();
        uncompressed.Delete(true);

        return true;
    }

    //installs the latest KAR Updater
    public static void GetLatest_KARUpdater()
    {
        using (WebClient client = new WebClient())
        {
            try
            {
                client.DownloadFile("https://github.com/RiskiVR/KAR-Updater/releases/latest/download/UpdateFiles.zip", "KARUpdater");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                UnityEngine.Debug.LogError(ex);
                MainUI.instance.audioSource.PlayOneShot(MainUI.instance.menu[4]);
                MessageUI.MessageBox(IntPtr.Zero, ex.ToString(), "Download Failed!", 0);
            }
        }
        
        //uncompresses it
        ZipFile.ExtractToDirectory("KARUpdater", Directory.GetCurrentDirectory());

        //clean up
        File.Delete("KARUpdater");
    }

    //installs the latest KAR Workshop
    public static bool GetLatest_KARWorkshop(FileInfo brotliEXE, DirectoryInfo installTarget)
    {
        //downloads the latest KARphin
        FileInfo archive = KWQIWebClient.Download_Archive_Windows(installTarget,
			"https://github.com/SeanMott/KAR-Workshop/releases/download/KWQI-Data-Dev/KARWorkshop.tar.gz.br",
			"KARWorkshop");

        //unpacks it
        FileInfo tar = KWQIArchive.BrotliPackage_UnpackToTar_Windows(brotliEXE, archive, installTarget);
        
        //uncompresses it
        DirectoryInfo uncompressed = KWQIArchive.TarPackage_Unpack_Windows(tar, installTarget);
        uncompressed = new DirectoryInfo(uncompressed.FullName + "/KARWorkshop");

        //moves the contents into the target directory
        KWInstaller.CopyAllDirContents(uncompressed, installTarget);

        //clean up
        archive.Delete();
        tar.Delete();
        uncompressed.Delete(true);

        return true;
    }

    //installs the latest KAR Launcher
    public static void GetLatest_KARLauncher()
    {
        using (WebClient client = new WebClient())
        {
            try
            {
                client.DownloadFile("https://github.com/RiskiVR/KAR-Launcher/releases/latest/download/UpdateFiles.zip", "KARLauncher");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                UnityEngine.Debug.LogError(ex);
                MainUI.instance.audioSource.PlayOneShot(MainUI.instance.menu[4]);
                MessageUI.MessageBox(IntPtr.Zero, ex.ToString(), "Download Failed!", 0);
            }
            
            //uncompresses it
            ZipFile.ExtractToDirectory("KARLauncher", Directory.GetCurrentDirectory());

            //clean up
            File.Delete("KARLauncher");
        }
    }

    //installs the latest Tools
    public static bool GetLatest_Tools(DirectoryInfo installTarget)
    {
        //downloads the latest KARphin
        FileInfo archive = KWQIWebClient.Download_Archive_Windows(installTarget,
            "https://github.com/SeanMott/KAR-Workshop/releases/download/KWQI-Data-Dev/Tools.zip",
			"Tools");

        //unpacks it
        DirectoryInfo uncompressed = new DirectoryInfo(installTarget.FullName + "/Tools");
        ZipFile.ExtractToDirectory(archive.FullName, uncompressed.FullName, true);

        //moves the contents into the target directory
        KWInstaller.CopyAllDirContents(uncompressed, installTarget);
        KWInstaller.CopyAllDirContents(uncompressed, installTarget);

        //clean up
        archive.Delete();
        uncompressed.Delete(true);

        return true;
    }
}