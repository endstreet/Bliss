using Bliss.BlazorMaps.JsInterops.Events;
using Microsoft.JSInterop;

namespace Bliss.BlazorMaps
{
    internal class MapEvented : Evented
    {
        public MapEvented(IJSObjectReference jsReference, IEventedJsInterop eventedJsInterop)
        {
            this.JsReference = jsReference;
            this.EventedJsInterop = eventedJsInterop;
        }
    }
}
