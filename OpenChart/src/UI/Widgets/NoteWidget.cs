using OpenChart.UI.Assets;
using System;

namespace OpenChart.UI.Widgets
{
    public class NoteWidget : ImageWidget, IKeyWidget
    {
        int _key;
        public int Key
        {
            get => _key;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("Key cannot be negative.");
                }

                _key = value;
            }
        }

        public NoteWidget(int key, ImageAsset image) : base(image)
        {
            Image = image;
            Key = key;
        }
    }
}