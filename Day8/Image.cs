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
        
        public string GetFinalLayer()
        {
            StringBuilder res = new StringBuilder();
            for (int i = 0; i < Width * Height; i++)
            {
                int layerToadd = 0;
                char charToAdd = Layers[layerToadd][i];
                while (charToAdd == '2')
                {
                    layerToadd++;
                    charToAdd = Layers[layerToadd][i];
                }
                res.Append(charToAdd);
            }

            return res.ToString();
        }

        public void PrintFinalLayer()
        {
            string output = GetFinalLayer();
            for (int i = 0; i < output.Length; i += Width)
            {
                Console.WriteLine(output.Substring(i, Width));
            }
        }
    }
}
