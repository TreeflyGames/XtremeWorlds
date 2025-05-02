using System;
using Client;
using Microsoft.JSInterop;
using Microsoft.Xna.Framework;

namespace WebGLxna.Pages
{
    public partial class Index
    {
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
