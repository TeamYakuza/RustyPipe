using Oxide.Game.Rust.Cui;

namespace Oxide.Ext.RustyPipe.UI
{
    public class RustyPipeGridComponent:RustyPipeUIComponent
    {
        public int ChildWidth { get; set; } = 10;
        public int ChildHeight { get; set; } = 10;
        public int PaddingTop { get; set; } = 1;
        public int PaddingRight { get; set; } = 1;
        public int PaddingLeft { get; set; } = 1;
        public int PaddingBottom { get; set; } = 1;
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
            int y = PaddingTop;

            foreach (var c in Children.ToArray())
            {
                c.Width = ChildWidth;
                c.Height = ChildHeight;
                c.X = x;
                c.Y = y;
                x += PaddingLeft+PaddingRight+c.Width;
                c.InternalBuild(container);
                if (x >=Width)
                {
                    x = PaddingLeft;
                    y += PaddingTop;
                    y += PaddingBottom;
                    y += c.Height;
                }

                if (y >= Height)
                {
                    return;
                }
               
            }
        }

    }
}
