using Oxide.Game.Rust.Cui;

namespace Oxide.Ext.RustyPipe.UI
{
    public class RustyPipeVerticalLayoutComponent : RustyPipeUIComponent
    {
        public int ChildWidth { get; set; } = 0;
        public int ChildHeight { get; set; } = 0;
        public int PaddingTop { get; set; } = 0;
        public int PaddingLeft { get; set; } = 0;


        public void Expand()
        {
            int height = (ChildHeight + PaddingTop) * Children.Count;
            Height= height;
            Width = ChildWidth;
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
            if (ChildWidth == 0)
                ChildWidth = Width;
            Build(container);
            int y = PaddingTop;
            foreach (var c in Children.ToArray())
            {
                c.Width = ChildWidth-3;
                c.Height = ChildHeight;
                c.X = 0;
                c.Y = y;
                y += PaddingTop + c.Height;
                c.InternalBuild(container);
            }
        }

    }
}