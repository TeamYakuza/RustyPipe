using Oxide.Game.Rust.Cui;
using UnityEngine;

namespace Oxide.Ext.RustyPipe.UI
{
    public class RustyPipePanel : RustyPipeUIComponent
    {
        public Color Color { get; set; }
     
        internal override void Initialize()
        {
            Color = Container.Theme.PanelColor;
        }

        public override void Build(CuiElementContainer container)
        {
            container.Add(new CuiPanel()
            {
                RectTransform =
                {
                    AnchorMax = GetAnchorMax(),
                    AnchorMin = GetAnchorMin()
                },
                Image =
                {
                    Color = $"{Color.r} {Color.g} {Color.b} {Color.a}"
                }
            }, Parent.Name, Name);
        }
    }
}