using System;
using System.Collections.Generic;
using Oxide.Game.Rust.Cui;

namespace Oxide.Ext.RustyPipe.UI
{
    public class RustyPipeContainer:RustyPipeUIComponent
    {
        public List<RustyPipeUIComponent> Components { get; set; }=new List<RustyPipeUIComponent>();
        public IRustyPipeTheme Theme { get; set; } = new RustyPipeDefaultTheme();
        public RustyPipeHud Hud { get; internal set; }
        internal override void Initialize()
        {
            
        }

        public new BasePlayer Player { get; set; }
        public bool ShowCursor { get; set; }

        public new T FindComponent<T>(string name) where T:RustyPipeUIComponent
        {
            foreach (var c in Components)
            {
                if (c is T && c.Name == Name)
                    return (T) c;
                var res = c.FindComponent<T>(name);
                if (res != null)
                    return res;
            }

            return default(T);
        }
        
        public new T AddComponent<T>(string name = null) where T : RustyPipeUIComponent, new()
        {
            if (string.IsNullOrEmpty(name))
                name = Guid.NewGuid().ToString();
            var r = new T { Parent = this, Name = name, Container = this };
            r.Initialize();
            Console.WriteLine(r.Name);
            Components.Add(r);
            return r;
        }

        public new void RemoveComponent(RustyPipeUIComponent component)
        {
            if (Children.Contains(component))
                Children.Remove(component);
        }
        public void Hide()
        {
            CuiHelper.DestroyUi(Hud.Player, Name);
        }
        public void Show()
        {
            var elements= Build();
            CuiHelper.DestroyUi(Player, Name);
            CuiHelper.AddUi(Player, elements);
        }
       

        public override void Build(CuiElementContainer container)
        {
            throw new Exception("Not to be used.");
        }
        internal new string GetAnchorMin()
        {
            float x1 = 1f / Hud.Width;
            x1 *= X;
            float y1 = 1f / Hud.Height;
            y1 *= (Y + Height);
            y1 = 1f - y1;

            return $"{x1} {y1}";
        }
        internal new string GetAnchorMax()
        {
            float x1 = 1f / Hud.Width;
            x1 *= X + Width;
            float y1 = 1f / Hud.Height;
            y1 *= (Y);
            y1 = 1f - y1;
            return $"{x1} {y1}";
        }
        public CuiElementContainer Build()
        {
            var container = new CuiElementContainer();
            var p = new CuiPanel()
            {
                RectTransform =
                {
                    AnchorMin = GetAnchorMin(),
                    AnchorMax = GetAnchorMax()
                },
                CursorEnabled = ShowCursor,
                Image =
                {
                    Color = "1 1 1 0"
                },
            };
            container.Add(p, "Hud", Name);
            foreach (var c in Components.ToArray())
            {
                c.InternalBuild(container);
            }

            return container;
        }


    }
}
