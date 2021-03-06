﻿using System;
using System.Collections.Generic;

namespace Oxide.Ext.RustyPipe.UI
{
    public class RustyPipeHud
    {
        public int Width { get; set; } = 1920;
        public int Height { get; set; } = 1080;
        public BasePlayer Player { get; set; }
        public Dictionary<string, RustyPipeContainer> Containers { get; set; } = new Dictionary<string, RustyPipeContainer>();
        public IRustyPipeTheme Theme { get; set; }
        public RustyPipeHud(BasePlayer player, int width, int height,IRustyPipeTheme theme=null)
        {
            Width = width;
            Height = height;
            Player = player;
            Theme = theme;
            if (theme == null)
                Theme = new RustyPipeDefaultTheme();

        }

        public RustyPipeContainer GetContainer(string name)
        {
            if (Containers.TryGetValue(name, out var container))
                return container;
            return null;
        }

        public void SwitchTheme(IRustyPipeTheme theme)
        {
            if (theme == null)
                theme = new RustyPipeDefaultTheme();
            Theme = theme;
            foreach (var c in Containers)
            {
                c.Value.Theme = theme;
                c.Value.RefreshComponents();
            }
        }
        public RustyPipeContainer CreateContainer(string name,int width=100,int height=100)
        {
            if (string.IsNullOrEmpty(name))
                name = Guid.NewGuid().ToString();
            if (!Containers.ContainsKey(name))
            {
                var container = new RustyPipeContainer()
                {
                    Hud = this,
                    Name = name,
                    Player = Player,
                    Width = width,
                    Height = height,
                    Theme = Theme
                };
                Containers.Add(name, container);
                container.Container = container;
                return container;
            }
            return null;
        }
    }
}
