using System.Threading.Tasks;

namespace Bliss.BlazorMaps.JsInterops.Base
{
    internal interface IBaseJsInterop
    {
        ValueTask DisposeAsync();
    }
}
