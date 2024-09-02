/*
0.3.0 implementation of how the KAR Workshop Quick Install format.

The spec for the project and latest version can be found at the Github.
https://github.com/SeanMott/KAR-KWQI

*/

//defines a class for common downloads of the core packages
using System.IO;
using System.Linq.Expressions;

class KWQICommonInstalls
{
    //installs the latest KARphin
    public static bool GetLatest_KARphin(FileInfo brotliEXE, DirectoryInfo installTarget)
    {
        //downloads the latest KARphin
        FileInfo archive = KWQIWebClient.Download_Archive_Windows(installTarget, 
        "https://github.com/SeanMott/KARphin_Modern/releases/download/latest/KARphin.tar.gz.br",
        "KARphin");

        //unpacks it
        FileInfo tar = KWQIArchive.BrotliPackage_UnpackToTar_Windows(brotliEXE, archive, installTarget);
        
        //uncompresses it
        DirectoryInfo uncompressed = KWQIArchive.TarPackage_Unpack_Windows(tar, installTarget);
        uncompressed = new DirectoryInfo(uncompressed.FullName + "/KARphin");

        //moves the contents into the target directory
        KWInstaller.CopyAllDirContents(uncompressed, installTarget);

        //clean up
        archive.Delete();
        tar.Delete();
        uncompressed.Delete(true);

        return true;
    }

    //installs the latest KARphin Dev
    public static bool GetLatest_KARphinDev(FileInfo brotliEXE, DirectoryInfo installTarget)
    {
        //downloads the latest KARphin
        FileInfo archive = KWQIWebClient.Download_Archive_Windows(installTarget, 
        "https://github.com/SeanMott/KARphin_Modern/releases/download/latest-dev/KARphinDev.tar.gz.br",
        "KARphinDev");

        //unpacks it
        FileInfo tar = KWQIArchive.BrotliPackage_UnpackToTar_Windows(brotliEXE, archive, installTarget);
        
        //uncompresses it
        DirectoryInfo uncompressed = KWQIArchive.TarPackage_Unpack_Windows(tar, installTarget);
        uncompressed = new DirectoryInfo(uncompressed.FullName + "/KARphinDev");

        //moves the contents into the target directory
        KWInstaller.CopyAllDirContents(uncompressed, installTarget);

        //clean up
        archive.Delete();
        tar.Delete();
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
    public static bool GetLatest_SkinPacks(FileInfo brotliEXE, DirectoryInfo installTarget)
    {
        //downloads the latest KARphin
        FileInfo archive = KWQIWebClient.Download_Archive_Windows(installTarget,
            "https://github.com/SeanMott/KAR-Workshop/releases/download/KWQI-Data-Dev/SkinPacks.tar.gz.br",
            "SkinPacks");

        //unpacks it
        FileInfo tar = KWQIArchive.BrotliPackage_UnpackToTar_Windows(brotliEXE, archive, installTarget);

        //uncompresses it
        DirectoryInfo uncompressed = KWQIArchive.TarPackage_Unpack_Windows(tar, installTarget);
        uncompressed = new DirectoryInfo(uncompressed.FullName + "/SkinPacks");

        //moves the contents into the target directory
        KWInstaller.CopyAllDirContents(uncompressed, installTarget);

        //clean up
        archive.Delete();
        tar.Delete();
        uncompressed.Delete(true);

        return true;
    }

    //installs the latest KARDont
    public static bool GetLatest_KARDont(FileInfo brotliEXE, DirectoryInfo installTarget)
    {
        //downloads the latest KARphin
        FileInfo archive = KWQIWebClient.Download_Archive_Windows(installTarget,
            "https://github.com/SeanMott/KARDont/releases/download/latest/KARDont.tar.gz.br",
            "KARDont");

        //unpacks it
        FileInfo tar = KWQIArchive.BrotliPackage_UnpackToTar_Windows(brotliEXE, archive, installTarget);

        //uncompresses it
        DirectoryInfo uncompressed = KWQIArchive.TarPackage_Unpack_Windows(tar, installTarget);
        uncompressed = new DirectoryInfo(uncompressed.FullName + "/KARDont");

        //moves the contents into the target directory
        KWInstaller.CopyAllDirContents(uncompressed, new DirectoryInfo(installTarget + "/KARDont"));

        //clean up
        archive.Delete();
        tar.Delete();
        uncompressed.Delete(true);

        return true;
    }

    //installs the latest Client Deps
    public static bool GetLatest_ClientDeps(FileInfo brotliEXE, DirectoryInfo installTarget)
    {
        //downloads the latest KARphin
        FileInfo archive = KWQIWebClient.Download_Archive_Windows(installTarget,
			"https://github.com/SeanMott/KAR-Workshop/releases/download/KWQI-Data-Dev/ClientDeps.tar.gz.br",
			"ClientDeps");

        //unpacks it
        FileInfo tar = KWQIArchive.BrotliPackage_UnpackToTar_Windows(brotliEXE, archive, installTarget);
        
        //uncompresses it
        DirectoryInfo uncompressed = KWQIArchive.TarPackage_Unpack_Windows(tar, installTarget);
        uncompressed = new DirectoryInfo(uncompressed.FullName + "/ClientDeps");

        //moves the contents into the target directory
        KWInstaller.CopyAllDirContents(uncompressed, installTarget);

        //clean up
        archive.Delete();
        tar.Delete();
        uncompressed.Delete(true);

        return true;
    }

    //installs the latest KAR Updater
    /*public static bool GetLatest_KARUpdater(FileInfo brotliEXE, DirectoryInfo installTarget)
    {
        //downloads the latest KARphin
        FileInfo archive = KWQIWebClient.Download_Archive_Windows(installTarget,
			"https://github.com/SeanMott/KAR-Workshop/releases/download/KWQI-Data-Dev/ClientDeps.tar.gz.br",
			"KARUpdater");

        //unpacks it
        FileInfo tar = KWQIArchive.BrotliPackage_UnpackToTar_Windows(brotliEXE, archive, installTarget);
        
        //uncompresses it
        DirectoryInfo uncompressed = KWQIArchive.TarPackage_Unpack_Windows(tar, installTarget);
        uncompressed = new DirectoryInfo(uncompressed.FullName + "/KARUpdater");

        //moves the contents into the target directory
        KWInstaller.CopyAllDirContents(uncompressed, installTarget);

        //clean up
        archive.Delete();
        tar.Delete();
        uncompressed.Delete(true);

        return true;
    }*/

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

    //installs the latest Tools
    public static bool GetLatest_Tools(FileInfo brotliEXE, DirectoryInfo installTarget)
    {
        //downloads the latest KARphin
        FileInfo archive = KWQIWebClient.Download_Archive_Windows(installTarget,
			"https://github.com/SeanMott/KAR-Workshop/releases/download/KWQI-Data-Dev/Tools.tar.gz.br",
			"Tools");

        //unpacks it
        FileInfo tar = KWQIArchive.BrotliPackage_UnpackToTar_Windows(brotliEXE, archive, installTarget);
        
        //uncompresses it
        DirectoryInfo uncompressed = KWQIArchive.TarPackage_Unpack_Windows(tar, installTarget);
        uncompressed = new DirectoryInfo(uncompressed.FullName + "/Tools");

        //moves the contents into the target directory
        KWInstaller.CopyAllDirContents(uncompressed, installTarget);

        //clean up
        archive.Delete();
        tar.Delete();
        uncompressed.Delete(true);

        return true;
    }
}