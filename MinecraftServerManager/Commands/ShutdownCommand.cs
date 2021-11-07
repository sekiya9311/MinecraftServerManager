using System;

namespace MinecraftServerManager.Commands
{
    // OSシャットダウン時: bedrock 終了
    public class ShutdownCommand
    {
        public void Run()
        {
            Console.WriteLine("Call ShutdownCommand");
        }
    }
}