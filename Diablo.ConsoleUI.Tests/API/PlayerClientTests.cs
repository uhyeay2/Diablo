using Diablo.ConsoleUI.API;
using Diablo.Domain.Constants.Routes;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diablo.ConsoleUI.Tests.API
{
    [TestFixture]
    public class PlayerClientTests
    {
        private PlayerClient _playerClient = new PlayerClient();
        private Process _apiProcess = null!;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _apiProcess = Process.Start(ApiPath.Path);
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _apiProcess.Close();
        }

        [Test]
        public async Task DoesAnyPlayerExist_DoesNotReturnNull()
        {
            var result = await _playerClient.DoesAnyPlayerExist();

            result.ShouldBeTrue();
        }

    }
}
