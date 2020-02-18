using System;
using System.Threading.Tasks;

namespace Paymentsense.Coding.Challenge.Api.Services
{
    public interface IWebClient
    {
        public Task<string> DownloadStringTaskAsync(Uri address);
    }
}