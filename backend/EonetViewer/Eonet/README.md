# EONET Web Client

Earth Observatory Natural Event Tracker (EONET) is a repository of metadata about natural events. EONET is accessible via web services. EONET will drive your natural event application.

https://eonet.gsfc.nasa.gov/

This client is based on Version 3.0 of the EONET API.

EONET metadata is for visualization and general information purposes only and should not be construed as "official" with regards to spatial or temporal extent

## Details of implementation

- Models are built based on [EONET API documentation](https://eonet.gsfc.nasa.gov/docs/v3) and actual JSON responses. Human reviewed and tested.
- [Refit](https://github.com/reactiveui/refit) is used for an automatic type-safe REST client.
  - It's not the most performant way to query an API, but it increases the readability of the code.
- [GeoJSON.Text](https://github.com/GeoJSON-Net/GeoJSON.Text) is used for parsing GeoJSON coordinates.