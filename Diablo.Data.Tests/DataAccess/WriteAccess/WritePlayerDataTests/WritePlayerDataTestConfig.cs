using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diablo.Data.Tests.DataAccess.WriteAccess.WritePlayerDataTests
{
    internal static class WritePlayerDataTestConfig
    {
        internal static string NameAlreadyTaken = "PlayerNameTaken";

        internal static string TestPlayerName = "TestPlayerName";

        internal static string TestPlayerDataPath => Paths.PlayerData + TestPlayerName;

        internal static IReadPlayerData GetMockedPlayerDataReaderObject()
        {
            var mockedPlayerDataReader = new Mock<IReadPlayerData>();

            mockedPlayerDataReader.Setup(x =>
                x.IsNameTakenAsync(TestPlayerName)).Returns(Task.FromResult(false));

            mockedPlayerDataReader.Setup(x =>
                x.IsNameTakenAsync(NameAlreadyTaken)).Returns(Task.FromResult(true));

            return mockedPlayerDataReader.Object;
        }
    }
}
