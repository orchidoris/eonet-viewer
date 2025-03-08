# EONET Web Client

Earth Observatory Natural Event Tracker (EONET) is a repository of metadata about natural events. EONET is accessible via web services. EONET will drive your natural event application.

https://eonet.gsfc.nasa.gov/

This client is based on Version 3.0 of the EONET API.

Disclaimer from [the official website](https://eonet.gsfc.nasa.gov/):

>> EONET metadata is for visualization and general information purposes only and should not be construed as "official" with regards to spatial or temporal extent.

## Details of implementation

- This client library is based on [EONET API documentation](https://eonet.gsfc.nasa.gov/docs/v3) and analysis of actual JSON responses in v3 API.
- An automatic type-safe REST client is built with [Refit](https://github.com/reactiveui/refit).
  - While Refit may cause a small overhead, it increases readability of the code.
- [GeoJSON.Text](https://github.com/GeoJSON-Net/GeoJSON.Text) used for GeoJSON coordinates, specifically `Position` and `LineString` models with their JSON converters.
- The abundance of .NET documentation in public APIs is here due to this library potential to be published to NuGet.