﻿using Bliss.BlazorMaps.JsInterops.Events;
using Microsoft.JSInterop;

namespace Bliss.BlazorMaps
{
    /// <summary>
    /// A class for drawing polygon overlays on a Map.
    /// </summary>
    public class Polygon : Polyline
    {
        internal Polygon(IJSObjectReference jsReference, IEventedJsInterop eventedJsInterop)
            : base(jsReference, eventedJsInterop)
        {
        }
    }
}
