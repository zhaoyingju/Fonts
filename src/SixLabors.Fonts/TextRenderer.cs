﻿// Copyright (c) Six Labors and contributors.
// Licensed under the Apache License, Version 2.0.

using System.Collections.Immutable;
using System.Linq;
using System.Numerics;
using SixLabors.Primitives;

namespace SixLabors.Fonts
{
    /// <summary>
    /// Encapulated logic for laying out and then rendering text to a <see cref="IGlyphRenderer"/> surface.
    /// </summary>
    public class TextRenderer
    {
        private TextLayout layoutEngine;
        private IGlyphRenderer renderer;

        internal TextRenderer(IGlyphRenderer renderer, TextLayout layoutEngine)
        {
            this.layoutEngine = layoutEngine;
            this.renderer = renderer;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TextRenderer"/> class.
        /// </summary>
        /// <param name="renderer">The renderer.</param>
        public TextRenderer(IGlyphRenderer renderer)
            : this(renderer, TextLayout.Default)
        {
        }

        /// <summary>
        /// Renders the text to the <paramref name="renderer"/>.
        /// </summary>
        /// <param name="renderer">The target renderer.</param>
        /// <param name="text">The text.</param>
        /// <param name="options">The style.</param>
        public static void RenderTextTo(IGlyphRenderer renderer, string text, RendererOptions options)
        {
            new TextRenderer(renderer).RenderText(text, options);
        }

        /// <summary>
        /// Renders the text.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="options">The style.</param>
        public void RenderText(string text, RendererOptions options)
        {
            ImmutableArray<GlyphLayout> glyphsToRender = this.layoutEngine.GenerateLayout(text, options);

            var dpi = new Vector2(options.DpiX, options.DpiY);

            RectangleF rect = TextMeasurer.GetBounds(glyphsToRender, dpi);

            this.renderer.BeginText(rect);

            foreach (GlyphLayout g in glyphsToRender.Where(x => !x.IsWhiteSpace))
            {
                g.Glyph.RenderTo(this.renderer, g.Location, options.DpiX, options.DpiY, g.LineHeight);
            }

            this.renderer.EndText();
        }
    }
}
