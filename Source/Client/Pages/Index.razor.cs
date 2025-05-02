using System;
using Client;
using Microsoft.JSInterop;
using Microsoft.Xna.Framework;

namespace Client.Pages
{
    public partial class Index
    {
        protected override void OnAfterRender(bool firstRender)
        {
            OnAfterRender(firstRender);

            if (firstRender)
            {
                JSRuntime.InvokeAsync<object>("initRenderJS", DotNetObjectReference.Create(this));
            }
        }
        
        [JSInvokable]
        public void TickDotNet()
        {
            if (General.Client == null)
            {
                General.Client = new GameClient();
                General.Client.Run();
            }
        }

    }
}
