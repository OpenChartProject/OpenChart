using Cairo;
using System.Collections.Generic;

namespace OpenChart.UI
{
    public class Container : Component
    {
        public Stack<Component> Children { get; private set; }

        public Container()
        {
            Children = new Stack<Component>();
        }

        public override void Draw(Context ctx)
        {
            foreach (var c in Children)
            {
                ctx.Save();
                ctx.Translate(c.Rect.X, c.Rect.Y);
                c.Draw(ctx);
                ctx.Restore();
            }
        }

        public override void ReceiveEvent()
        {
            if (Children.Count == 0)
                return;

            var childList = Children.ToArray();

            for (var i = childList.Length - 1; i > 0; i--)
            {
                childList[i].ReceiveEvent();
            }
        }
    }
}
