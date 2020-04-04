using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oxide.Plugins;

namespace Oxide.Ext.RustyPipe.Plugin
{
    [Info("UMod Plugin Downloader", "Team Yakuza (Ultimation)", "0.1.0")]
    [Description("Easilly download and install plugins")]
    public class UModPluginDownloader : RustPlugin
    {
        void Unload()
        {

        }

        [ConsoleCommand("rustypipe.install")]
        void InstallPlugin(ConsoleSystem.Arg ar)
        {
            if (ar.IsRcon || ar.IsServerside)
            {
                var plugin = ar.GetString(0).ToLower().Replace(" ", "-");
                if (!string.IsNullOrEmpty(plugin))
                {
                    var path = $"https://umod.org/plugins/{plugin}/latest.json";
                    var pluginData = RustyPipe.Downloader.HttpGetJson<UModPluginData>(path);
                    if (pluginData != null)
                    {
                        var localFilename = Path.GetFileName(pluginData.download_url);
                        var downloaded = RustyPipe.Downloader.DownloadFile(pluginData.download_url, "oxide/plugins/" + localFilename);
                        if (downloaded)
                        {
                            Puts($"[RustyPipe]: Installed Plugin -> {Path.GetFileNameWithoutExtension(localFilename)}");
                        }
                    }
                }
            }
        }
    }
}
