using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oxide.Core;
using Oxide.Core.Extensions;
using Oxide.Ext.RustyPipe.Image;
using Oxide.Ext.RustyPipe.Plugin;

namespace Oxide.Ext.RustyPipe
{
    public class RustyPipeExtension:Extension
    {
        public RustyPipeExtension(ExtensionManager manager) : base(manager)
        {
            manager.RegisterPluginLoader(new RustyPipePluginLoader());
        }

        public override void OnModLoad()
        {
            base.OnModLoad();
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            RustyPipe.Init();
            
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            
        }

        public override string Name => "RustyPipe";
        public override string Author => "Team Yakuza";
        public override VersionNumber Version => new VersionNumber(1,0,0);
    }
}
