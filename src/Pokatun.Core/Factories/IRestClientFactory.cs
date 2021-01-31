using System;
using Pokatun.Core.Enums;
using RestSharp;

namespace Pokatun.Core.Factories
{
    public interface IRestClientFactory
    {
        IRestClient GetRestClient(Api api);
    }
}
