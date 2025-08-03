# Movie Price App

This is a full-stack web application that compares movie prices from two different providers — CinemaWorld and FilmWorld — and displays the best available price to the user. It is designed for performance and resiliency using .NET 8 on the backend and React on the frontend.

## Assumptions

- The backend fetches movie data from two provider APIs using a shared access token via HTTP headers.
- The movie lists and price details are fetched concurrently to reduce response time.
- Polly is used to apply retry logic for resilience in case any provider API is temporarily unavailable.
- Configuration such as API tokens and provider URLs are kept out of source code and placed in `appsettings.json` or environment variables.

## How to Run

1. **Backend** (located in `movie-price-app/backend`)
   - Prerequisites: .NET 8 SDK
   - Create a file named `appsettings.json` in the `backend` directory with the following content:
     ```json
     {
       "MovieApi": {
         "BaseUrl": "https://webjetapitest.azurewebsites.net/api",
         "AccessToken": "<your-access-token>",
         "Providers": {
           "CinemaWorld": "cinemaworld",
           "FilmWorld": "filmworld"
         }
       }
     }
     ```
   - Commands:
     ```bash
     cd backend
     dotnet restore
     dotnet run
     ```
   - The API will run at `http://localhost:5000`

2. **Frontend** (located in `movie-price-app/frontend`)
   - Prerequisites: Node.js
   - Commands:
     ```bash
     cd frontend
     npm install
     npm start
     ```
   - The React app will run at `http://localhost:3000`

 Ensure that CORS is enabled on the backend so the frontend can call the API successfully.
