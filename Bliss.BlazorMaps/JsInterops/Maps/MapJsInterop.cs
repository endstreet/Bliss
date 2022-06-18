using Bliss.BlazorMaps.JsInterops.Base;
using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace Bliss.BlazorMaps.JsInterops.Maps
{
    internal class MapJsInterop : BaseJsInterop, IMapJsInterop
    {
        private static readonly string jsFilePath = $"{JsInteropConfig.BaseJsFolder}{JsInteropConfig.MapFile}";
        private const string initialize = "initialize";
        

        public MapJsInterop(IJSRuntime jsRuntime) : base(jsRuntime, jsFilePath)
        {

        }

        public async ValueTask<IJSObjectReference> Initialize(MapOptions mapOptions)
        {
            IJSObjectReference module = await moduleTask.Value;
            return await module.InvokeAsync<IJSObjectReference>(initialize, mapOptions);
        }
    }
}
