using Oxide.Game.Rust.Cui;
using UnityEngine;

namespace Oxide.Ext.RustyPipe.UI
{
    public class RustyPipeButton : RustyPipeUIComponent
    {
        public Color Color { get; set; } = Color.white;
        public string Text { get; set; } = "Hello World";
        public Color FontColor { get; set; } = Color.black;
        public int FontSize { get; set; } = 12;
        public TextAnchor TextAlign { get; set; } = TextAnchor.MiddleCenter;

        public string Command { get; set; }
        public string Close { get; set; }
        public bool HasBorder { get; set; } = false;
        public Color BorderColor { get; set; } = Color.red;
        public int BorderWidth = 6;

       
        CuiButton BuildElement(CuiElementContainer container)
        {
            var min = GetAnchorMin();
            var max = GetAnchorMax();
            string tmpName = Name;
            if (HasBorder)
            {
                min = GetAnchorMin(X + BorderWidth, Y + Height - BorderWidth);
                max = GetAnchorMax(X + Width - BorderWidth, Y + BorderWidth);
                tmpName = Name + "_Border";
                container.Add(new CuiPanel()
                {
                    RectTransform =
                    {
                        AnchorMin = GetAnchorMin(),
                        AnchorMax = GetAnchorMax(),
                    },
                    Image =
                    {
                        Color = $"{BorderColor.r} {BorderColor.g} {BorderColor.b} {BorderColor.a}",
                    }
                }, Parent.Name, tmpName);
            }

            var btn = new CuiButton()
            {
                RectTransform =
                {
                    AnchorMax = max,
                    AnchorMin = min
                },
                Button =
                {
                    Color = $"{Color.r} {Color.g} {Color.b} {Color.a}",
                    Command = Command,
                    Close = Close
                },
                Text =
                {
                    Text = Text,
                    Color = $"{FontColor.r} {FontColor.g} {FontColor.b} {FontColor.a}",
                    FontSize = FontSize,
                    Align = TextAlign
                }

            };
            container.Add(btn, Parent.Name, tmpName);
            return btn;
        }


        internal override void Initialize()
        {
            Color = Container.Theme.Button;
            BorderColor = Container.Theme.Border;
            FontColor = Container.Theme.FontColor;
        }

        public override void Build(CuiElementContainer container)
        {
            BuildElement(container);
        }

    }
}
    
