using System;
using System.Collections.Generic;
using Oxide.Game.Rust.Cui;
using UnityEngine;

namespace Oxide.Ext.RustyPipe.UI
{
    public abstract class RustyPipeUIComponent
    {
        
        protected RustyPipeUIComponent()
        {
        }
        private RustyPipeUIComponent _parent;
        public T FindComponent<T>(string name) where T : RustyPipeUIComponent
        {
            if (Name == name && this is T)
                return (T) this;
            foreach (var c in Children)
            {
                var res = c.FindComponent<T>(name);
                if (res != null)
                    return res;
            }

            return default(T);
        }

        public List<RustyPipeUIComponent> Children { get; set; }=new List<RustyPipeUIComponent>();
       
        public RustyPipeContainer Container { get; set; }
        public virtual int X { get; set; }
        public virtual int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public virtual string Name { get; set; }

        internal abstract void Initialize();

        public void RefreshComponent()
        {
            Initialize();
            foreach (var c in Children.ToArray())
            {
                c.RefreshComponent();
            }
        }
     
        public void FillParent()
        {
            X = 0;
            Y = 0;
            Width = Parent.Width;
            Height = Parent.Height;
        }
        public void SetPosition(TextAnchor anchor)
        {
            int halfWidth = Width / 2;
            int halfHeight = Height / 2;
            int parentHalfWidth = Parent.Width / 2;
            int parentHalfHeight = Parent.Height / 2;
            switch (anchor)
            {
                case TextAnchor.UpperLeft:
                {
                    X = 0;
                    Y = 0;
                    break;
                }
                case TextAnchor.UpperCenter:
                {

                    X = (parentHalfWidth) - (halfWidth);;
                    Y = 0;
                    break;
                }
                case TextAnchor.UpperRight:
                {

                    X = Parent.Width - (Width);
                    Y = 0;
                    break;
                }
                case TextAnchor.MiddleLeft:
                {
                    X = 0;
                    Y = (parentHalfHeight) - (halfHeight);
                    break;
                }
                case TextAnchor.MiddleCenter:
                {
                    X = (parentHalfWidth) - (halfWidth);
                    Y = (parentHalfHeight) - (halfHeight);
                    break;
                }
                case TextAnchor.MiddleRight:
                {
                    X = Parent.Width - (Width);
                    Y = (parentHalfHeight) - (halfHeight);
                    break;
                }
                case TextAnchor.LowerLeft:
                {
                    X = 0;
                    Y = Parent.Height - Height;
                    break;
                }
                case TextAnchor.LowerCenter:
                {
                    X = (parentHalfWidth) - (halfWidth);
                    Y = Parent.Height - Height;
                    break;
                }
                case TextAnchor.LowerRight:
                {
                    X = Parent.Width - (Width);
                    Y = Parent.Height - Height;
                    break;
                }
            }
        }
        public virtual RustyPipeUIComponent Parent
        {
            get
            {
                if (_parent == null)
                    return Container;
                return _parent;
            }
            set { _parent = value; }
        }

        internal string GetAnchorMin()
        {
            float x1 = 1f / Parent.Width;
            x1 *= X;
            float y1 = 1f / Parent.Height;
            y1 *= (Y + Height);
            y1 = 1f - y1;

            return $"{x1} {y1}";
        }
        internal string GetAnchorMin(int x,int y)
        {
            float x1 = 1f / Parent.Width;
            x1 *= x;
            float y1 = 1f / Parent.Height;
            y1 *= (y);
            y1 = 1f - y1;

            return $"{x1} {y1}";
        }
         internal string GetAnchorMax(int x,int y)
        {
            float x1 = 1f / Parent.Width;
            x1 *= x;
            float y1 = 1f / Parent.Height;
            y1 *= (y);
            y1 = 1f - y1;
            return $"{x1} {y1}";
        }
        internal string GetAnchorMax()
        {
            float x1 = 1f / Parent.Width;
            x1 *= X + Width;
            float y1 = 1f / Parent.Height;
            y1 *= (Y);
            y1 = 1f - y1;
            return $"{x1} {y1}";
        }
        internal virtual void InternalBuild(CuiElementContainer container)
        {
            Build(container);
            foreach (var c in Children)
                c.InternalBuild(container);
        }
        public abstract void Build(CuiElementContainer container);

        public T AddComponent<T>(string name = null) where T : RustyPipeUIComponent, new()
        {
            if (string.IsNullOrEmpty(name))
                name = Guid.NewGuid().ToString();
            var r = new T() { Parent = this, Name = name, Container = Container };
            r.Initialize();
            Children.Add(r);
            return r;
        }

        public void RemoveComponent(RustyPipeUIComponent component)
        {
            if (Children.Contains(component))
                Children.Remove(component);
        }

    }
}
