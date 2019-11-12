using UnityEngine;

namespace Oxide.Ext.RustyPipe.UI
{
   
    public class RustyPipeDefaultTheme:IRustyPipeTheme
    {
        public Color PanelColor { get; set; } = new Color(Color.black.r, Color.black.g, Color.black.b, 0.8f);
        public Color Button { get; set; } = new Color(Color.black.r, Color.black.g, Color.black.b, 0.9f);
        public Color Border { get; set; } = new Color(Color.black.r, Color.black.g, Color.black.b, 1f);
        public Color FontColor { get; set; } = Color.white;
       
    }
}
