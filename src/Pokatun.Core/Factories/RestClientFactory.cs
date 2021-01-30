using System;
using Pokatun.Core.Enums;
using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;
using Xamarin.Essentials;
using Xamarin.Essentials.Interfaces;

namespace Pokatun.Core.Factories
{
    public class RestClientFactory : IRestClientFactory
    {
        private readonly IDeviceInfo _deviceInfo;

        private RestClient _pokatunRestClient;
        private RestClient _visicomRestClient;

        public RestClientFactory(IDeviceInfo deviceInfo)
        {
            _deviceInfo = deviceInfo;
        }

        public IRestClient VisicomRestClient
        {
            get
            {
                if (_visicomRestClient == null)
                {
                    _visicomRestClient = new RestClient();
                }

                return _visicomRestClient;
            }
        }
        private IRestClient PokatunRestClient
        {
            get
            {
                if (_pokatunRestClient == null)
                {
                    #if DEBUG

                    _pokatunRestClient = new RestClient(string.Format(Constants.BaseUrl,
                        _deviceInfo.Platform == DevicePlatform.iOS ? Constants.iOSDebugIP : Constants.AndroidDebugIP
                    ));

                    #else

                    _pokatunRestClient = new RestClient(Constants.BaseUrl);

                    #endif

                    _pokatunRestClient.UseNewtonsoftJson();
                }

                return _pokatunRestClient;
            }
        }

        public IRestClient GetRestClient(Api api)
        {
            if (api == Api.Pokatun)
            {
                return PokatunRestClient;
            }
            else
            {
                return VisicomRestClient;
            }
        }
    }
}
