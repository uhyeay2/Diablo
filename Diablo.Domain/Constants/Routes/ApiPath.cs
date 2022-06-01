using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Diablo.Domain.Constants.Routes
{
    public static class ApiPath
    {

        private static readonly string _apiPath;
        public static string Path => _apiPath;

        //TODO: Get this from launcher settings?
        private static readonly string _baseUrl = "https://localhost:5001";

        public static string GetUrl(string route) => _baseUrl + route;

        static ApiPath()
        {
                _apiPath = new Regex(@"(^.+Diablo\\Diablo\.).+").Match(Directory.GetCurrentDirectory())
                      .Groups[1].Value + "API/bin/Debug/net6.0/Diablo.API";
        }        
    }
}
