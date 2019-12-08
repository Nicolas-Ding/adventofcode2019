using System;
using System.Collections.Generic;
using System.Text;

namespace Day8
{
    public class Image
    {
        int Width { get; }

        int Height { get; }

        public List<string> Layers { get; } = new List<string>();

        public Image(int width, int height, string image)
        {
            Width = width;
            Height = height;

            for (int i = 0; i < image.Length; i += width * height)
            {
                Layers.Add(image.Substring(i, Width * Height));
            }
        }
    }
}
