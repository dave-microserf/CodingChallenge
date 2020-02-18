using System;
using System.Net;
using System.Threading.Tasks;

namespace Paymentsense.Coding.Challenge.Api.Services
{
    public class WebClientWrapper : IWebClient
    {
        public async Task<string> DownloadStringTaskAsync(Uri address)
        {
            return await new WebClient().DownloadStringTaskAsync(address);
        }
    }
}