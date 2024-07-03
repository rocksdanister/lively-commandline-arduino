using Google.Protobuf.WellKnownTypes;
using GrpcDotNetNamedPipes;
using Lively.Grpc.Common.Proto.Commands;
using Lively.IPC.Errors;
using Lively.IPC.Helpers;
using System.Threading.Tasks;

namespace Lively.IPC.Client
{
    public class CommandsClient : ICommandsClient
    {
        private readonly CommandsService.CommandsServiceClient client;

        public CommandsClient()
        {
            if (!SingleInstanceUtil.IsAppMutexRunning(Constants.AppInstance.UniqueAppName))
                throw new ApplicationNotFoundException("Lively Wallpaper is not running.");

            client = new CommandsService.CommandsServiceClient(new NamedPipeChannel(".", Constants.AppInstance.GrpcPipeServerName));
        }

        public async Task ShowUI()
        {
            await client.ShowUIAsync(new Empty());
        }

        public async Task CloseUI()
        {
            await client.CloseUIAsync(new Empty());
        }

        public async Task RestartUI()
        {
            await client.RestartUIAsync(new Empty());
        }

        public async Task RestartUI(string startArgs)
        {
            await client.RestartUIWithArgsAsync(new RestartRequest() { StartArgs = startArgs });
        }

        public async Task ShowDebugger()
        {
            await client.ShowDebuggerAsync(new Empty());
        }

        public async Task ScreensaverShow(bool show)
        {
            await client.ScreensaverAsync(new ScreensaverRequest()
            {
                State = show ? ScreensaverState.Start : ScreensaverState.Stop,
            });
        }

        public async Task ScreensaverConfigure()
        {
            await client.ScreensaverAsync(new ScreensaverRequest()
            {
                State = ScreensaverState.Configure,
            });
        }

        public async Task ScreensaverPreview(int previewHandle)
        {
            await client.ScreensaverAsync(new ScreensaverRequest()
            {
                State = ScreensaverState.Preview,
                PreviewHwnd = previewHandle,
            });
        }

        public async Task ShutDown()
        {
            await client.ShutDownAsync(new Empty());
        }

        public async Task AutomationCommandAsync(string[] args)
        {
            var request = new AutomationCommandRequest();
            request.Args.AddRange(args);
            await client.AutomationCommandAsync(request);
        }

        public void AutomationCommand(string[] args)
        {
            var request = new AutomationCommandRequest();
            request.Args.AddRange(args);
            _ = client.AutomationCommand(request);
        }

        public void SaveRectUI()
        {
            client.SaveRectUI(new Empty());
        }

        public async Task SaveRectUIAsync()
        {
            await client.SaveRectUIAsync(new Empty());
        }
    }
}
