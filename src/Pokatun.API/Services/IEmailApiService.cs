using System;
namespace Pokatun.API.Services
{
    public interface IEmailApiService
    {
        void Send(string to, string subject, string text, string from = null);
    }
}
