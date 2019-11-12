using Oxide.Game.Rust.Cui;
using UnityEngine;

namespace Oxide.Ext.RustyPipe.UI
{
    public class RustyPipeImage : RustyPipeUIComponent
    {
        private CuiRawImageComponent _imageComponent =new CuiRawImageComponent();
        private string _imageUrl;
        private string _streamedImage;
        public Color Color { get; set; } = Color.white;
        public string ImageUrl
        {
            get { return _imageUrl; }
            set
            {
                _imageUrl = value;
                if (string.IsNullOrEmpty(value))
                {
                    return;
                }

                _imageComponent.Png = null;
                _imageComponent.Url = _imageUrl;
            }
        }

        public string StreamedImage
        {
            get { return _streamedImage; }
            set
            {
                _streamedImage = value;
                if (string.IsNullOrEmpty(value))
                {
                    return;
                }
                _imageComponent.Png = RustyPipe.ImageLibrary.GetImage(value).ToString();
                _imageComponent.Url = null;
            } 
        }

        public RustyPipeImage()
        {
            _imageComponent.Color = "1 1 1 1";
        }


        internal override void Initialize()
        {
            
        }

        public override void Build(CuiElementContainer container)
        {
            var element = new CuiElement()
            {
                Name = Name,
                Parent = Parent.Name
            };
            element.Components.Add(_imageComponent);
            _imageComponent.Color = $"{Color.r} {Color.g} {Color.b} {Color.a}";
            element.Components.Add(new CuiRectTransformComponent()
            {
                AnchorMax = GetAnchorMax(),
                AnchorMin = GetAnchorMin()
            });
            container.Add(element);
        }
    }
}