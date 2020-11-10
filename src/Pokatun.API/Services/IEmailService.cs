using System;
namespace Pokatun.API.Services
{
    public interface IEmailService
    {
        void Send(string to, string subject, string text, string from = null);
    }
}
