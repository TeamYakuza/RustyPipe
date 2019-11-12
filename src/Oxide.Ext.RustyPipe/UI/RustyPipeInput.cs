using Oxide.Game.Rust.Cui;
using UnityEngine;

namespace Oxide.Ext.RustyPipe.UI
{
    public class RustyPipeInput:RustyPipeUIComponent
    {
      

        public int CharLimit { get; set; }
        
        public int FontSize { get; set; }

        public bool IsPassword { get; set; }

        public Color FontColor { get; set; }
        public Color Color { get; set; }

        public string Text { get; set; }

        public TextAnchor TextAlign { get; set; }

        public string Command { get; set; }


        internal override void Initialize()
        {
            Color = Container.Theme.FontColor;
        }

        public override void Build(CuiElementContainer container)
        {
            var input = new CuiInputFieldComponent
            {
                Align = TextAlign,
                CharsLimit = CharLimit,
                Command = Command,
                IsPassword = IsPassword,
                FontSize = FontSize,
                Color = $"{FontColor.r} {FontColor.g} {FontColor.b} {FontColor.a}"
            };
            var rect = new CuiRectTransformComponent()
            {
                AnchorMin = GetAnchorMin(),
                AnchorMax = GetAnchorMax()
            };
            var element = new CuiElement()
            {
                Parent = Parent.Name,
                Name = Name,
            };
          
            element.Components.Add(input);
            element.Components.Add(rect);
            container.Add(element);
        }

    }
}
