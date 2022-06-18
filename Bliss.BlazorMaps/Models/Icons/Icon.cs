using Microsoft.JSInterop;

namespace Bliss.BlazorMaps
{
    /// <summary>
    /// Can be a graphical representation of an object on a Map.
    /// </summary>
    public class Icon : JsReferenceBase
    {
        internal Icon(IJSObjectReference jsReference)
        {
            JsReference = jsReference;
        }
    }
}
