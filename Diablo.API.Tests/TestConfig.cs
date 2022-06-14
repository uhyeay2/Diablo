using Diablo.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diablo.API.Tests
{
    internal static class TestConfig
    {

        private static readonly WebApplicationFactory<Program> factory = new();

        public static HttpClient ApiClient { get; } = factory.CreateClient();

    }
}
