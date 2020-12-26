using Cairo;
using System.Collections.Generic;

namespace OpenChart.UI
{
    public class Container : Component
    {
        public List<Component> Children { get; private set; }

        public Container()
        {
            Children = new List<Component>();
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

        public override void ReceiveEvent(InputEvent e)
        {
            if (Children.Count == 0)
                return;

            for (var i = Children.Count - 1; i > 0; i--)
            {
                Children[i].ReceiveEvent(e);

                if (e.Consumed)
                    break;
            }
        }
    }
}
