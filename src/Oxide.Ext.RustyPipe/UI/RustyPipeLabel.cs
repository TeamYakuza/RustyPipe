using Oxide.Game.Rust.Cui;
using UnityEngine;

namespace Oxide.Ext.RustyPipe.UI
{
    public class RustyPipeLabel : RustyPipeUIComponent
    {
        public string Text { get; set; } = "Hello World";
        public Color FontColor { get; set; } = Color.black;
        public int FontSize { get; set; } = 12;
        public TextAnchor TextAlign { get; set; } = TextAnchor.MiddleCenter;

        internal override void Initialize()
        {
            FontColor = Container.Theme.FontColor;
        }

        public override void Build(CuiElementContainer container)
        {
            container.Add(new CuiLabel()
            {
                RectTransform =
                {
                    AnchorMin = GetAnchorMin(),
                    AnchorMax = GetAnchorMax(),
                },
                Text =
                {
                    Text = Text,
                    Color = $"{FontColor.r} {FontColor.g} {FontColor.b} {FontColor.a}",
                    FontSize = FontSize,
                    Align = TextAlign
                }
            }, Parent.Name, Name);
        }

    }
}