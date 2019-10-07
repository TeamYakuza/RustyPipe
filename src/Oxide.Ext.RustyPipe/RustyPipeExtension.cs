using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oxide.Core;
using Oxide.Core.Extensions;

namespace Oxide.Ext.RustyPipe
{
    public class RustyPipeExtension:Extension
    {
        public RustyPipeExtension(ExtensionManager manager) : base(manager)
        {
        }

        public override string Name => "RustyPipe";
        public override string Author => "Team Yakuza";
        public override VersionNumber Version => new VersionNumber(1,0,0);
    }
}
