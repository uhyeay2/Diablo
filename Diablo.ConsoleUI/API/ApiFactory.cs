using Diablo.Domain.Constants.Routes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diablo.ConsoleUI.API
{
    internal static class ApiFactory
    {
        internal static DisposableApi StartDisposableApi(string apiExePath) => new(apiExePath);
        internal static DisposableApi StartDisposableApi() => new(ApiPath.Path);
    }

    internal class DisposableApi : IDisposable
    {
        private readonly Process _apiProcess;

        public DisposableApi(string apiExePath)
        {
            _apiProcess = Process.Start(apiExePath);
        }

        public void Dispose()
        {
            _apiProcess.Close();
        }
    }
}
