using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Oxide.Ext.RustyPipe.UI
{
    public interface IRustyPipeTheme
    {
        Color PanelColor { get; set; }
        Color Button { get; set; }
        Color Border { get; set; }
        Color FontColor { get; set; }
    }
}
