using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace Bliss.BlazorMaps.JsInterops.IconFactories
{
    internal interface IIconFactoryJsInterop
    {
        ValueTask<IJSObjectReference> CreateDefaultIcon();
    }
}
