using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Pokatun.Core.Enums;
using Pokatun.Core.Factories;
using Pokatun.Data;
using RestSharp;

namespace Pokatun.Core.Services
{
    public sealed class GeocodingService : IGeocodingService
    {
        private readonly IRestClient _restClient;

        public GeocodingService(IRestClientFactory restClientFactory)
        {
            _restClient = restClientFactory.GetRestClient(Api.Visicom);
        }

        public async Task<LocationDto[]> GetLocationsAsync(string stringForSearch)
        {
            RestRequest request = new RestRequest(
                "https://api.visicom.ua/data-api/5.0/ru/geocode.json?categories=adr_street,adr_address&text=" + stringForSearch + "&limit=10&country=ua&key=" + Constants.VisicomKey,
            Method.GET);

            request.AddHeader("Content-Type", "application/json");

            IRestResponse<GeocodingResponce> response = await _restClient.ExecuteAsync<GeocodingResponce>(request);

            if (response.Data.Features == null || !response.Data.Features.Any())
            {
                return Array.Empty<LocationDto>();
            }

            List<LocationDto> locations = new List<LocationDto>(response.Data.Features.Count);

            foreach (FeatureDto featureDto in response.Data.Features)
            {
                string address;

                if (featureDto.Properties.Categories == "adr_address")
                {
                    address = featureDto.Properties.Settlement + ", " + featureDto.Properties.street_type + " " + featureDto.Properties.Street + ", " + featureDto.Properties.Name;
                }
                else
                {
                    address = featureDto.Properties.Settlement + ", " + featureDto.Properties.Type + " " + featureDto.Properties.Name;
                }

                locations.Add(new LocationDto
                {
                    Addres = address,
                    Longtitude = featureDto.geo_centroid.Coordinates[0],
                    Latitude = featureDto.geo_centroid.Coordinates[1],
                });
            }

            return locations.ToArray();

        }

        private sealed class GeocodingResponce
        {
            public List<FeatureDto> Features { get; set; }
        }

        private sealed class FeatureDto
        {
            public PropertiesDto Properties { get; set; }

            public GeoDto geo_centroid { get; set; }
        }

        private sealed class PropertiesDto
        {
            public string Categories { get; set; }

            public string Name { get; set; }

            public string Street { get; set; }

            public string street_type { get; set; }

            public string Type { get; set; }

            public string Settlement { get; set; }

        }

        private sealed class GeoDto
        {
            public List<double> Coordinates { get; set; }
        }
    }
}
