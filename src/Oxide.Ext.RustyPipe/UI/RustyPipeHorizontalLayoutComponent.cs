using Oxide.Game.Rust.Cui;

namespace Oxide.Ext.RustyPipe.UI
{
    public class RustyPipeHorizontalLayoutComponent : RustyPipeUIComponent
    {
        public int ChildWidth { get; set; } = 10;
        public int ChildHeight { get; set; } = 10;
        public int PaddingTop { get; set; } = 0;
        public int PaddingLeft { get; set; } = 0;

        public bool RightToLeft { get; set; } = false;
        public void Expand()
        {
            int width = (ChildWidth + PaddingLeft) * Children.Count;
            Width = width;
            Height = ChildHeight;
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
                    Color = "0 0 0 0",
                }
            }, Parent.Name, Name);
        }


        internal override void Initialize()
        {
            
        }

        internal override void InternalBuild(CuiElementContainer container)
        {
         
            Build(container);
            int x = PaddingLeft;
            int paddingLeft = this.PaddingLeft;
            if (this.RightToLeft)
            {
                paddingLeft = base.Width - this.PaddingLeft - this.ChildWidth;
            }
            foreach (var c in Children.ToArray())
            {
                c.Width = ChildWidth;
                c.Height = ChildHeight;
                c.X = paddingLeft;
                c.Y = PaddingTop;
                paddingLeft = (!this.RightToLeft ? paddingLeft + this.PaddingLeft + c.Width : paddingLeft - (this.PaddingLeft + c.Width));
                x += PaddingLeft + c.Width;
                c.InternalBuild(container);
            }
        }

    }
}