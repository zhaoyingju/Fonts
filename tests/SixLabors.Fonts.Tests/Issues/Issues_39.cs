﻿using SixLabors.Fonts.Tests.Fakes;
using Xunit;

namespace SixLabors.Fonts.Tests.Issues
{
    public class Issues_39
    {
        [Fact]
        public void RenderingEmptyString_DoesNotThrow()
        {
            var font = CreateFont("\t x");
         
            GlyphRenderer r = new GlyphRenderer();

            new TextRenderer(r).RenderText("", new RendererOptions(new Font(font, 30), 72));
        }

        public static Font CreateFont(string text)
        {
            FontCollection fc = new FontCollection();
            Font d = fc.Install(new FakeFontInstance(text)).CreateFont(12);
            return new Font(d, 1);
        }
    }
}
