using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oxide.Game.Rust;

namespace Oxide.Ext.RustyPipe.Plugin
{
    public class RustyPipePluginLoader:RustPluginLoader
    {
        public override Type[] CorePlugins => new[] {typeof(RustyPipePlugin),typeof(UModPluginDownloader) };
    }
}
