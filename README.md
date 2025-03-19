# EONET Events Viewer

This is a demo project using [EONET API](https://eonet.gsfc.nasa.gov/docs/v3) to play with features of .NET, React, etc.

## How to run

⬇️ Get the repo locally.

🚀 Run [start.cmd](/start.cmd) on Windows.

🦄 It has the same effect as running [EonetViewer](backend\EonetViewer\EonetViewer.sln) .NET solution from Visual Studio.

⚠️ On the first run, restoring of **NuGet** and **npm** packages could take a few minutes.

🔗 A home page opens in browser automatically once things are ready.

## Next tasks

- Filtering

  - Magnitude filtering

    - magnitude unit (magnitude range should be loaded based on other current filters)
    - range picker

  - Map view bounding box filtering

    - on Map view, filter based on bounding box on zoom-in/zoom-out

  - Countries filtering

    - load Countries metadata from 3-rd party service
    - use 3rd-party service to attribute coordinates to Country
    - add filtering by countries on UI

- Align documentation between Eonet and events_service.proto
- Add maps on FE, display pins based on event coordinates
  - Choose maps to integrate with (Google Maps r smth else)
- Map geometry coordinates to countries (BE or FE?) and enables filtering by country on FE
- Add logging on backened
- Cover non-happy-path scenarious with unit tests on back end
- Return GrcException from EventsService on BE and connect it to notificatoins on FE
- Cache all events into local SQL db
  - ⚠️ SQL DB is not the best choice for this case, yet it's gonna be used here as I would like to play with SQL.
  - Setup process of caching all events on applicaiton start
- Add textbox search functionality on FE leveraging speed of local cache

## Done tasks

- 2025-03-19 Filtering. Basic Filtering
  - 'Categories' multi dropdown (all included by default) + reset button
  - 'Source' modal table multi select (all included by default) + reset button
  - 'Status' dropdown (Open by default)
  - 'Dates' date picker (last 10 days by default)
- 2025-03-11 Define SQL DB schema
- 2025-03-08 Load context to FE
