namespace MoviesAnalysisApp.Services
{
    public class TMDBService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public TMDBService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _apiKey = config["TMDB:ApiKey"];
        }

        public async Task<string> GetMovieRawAsync(int movieId)
        {
            var url = $"https://api.themoviedb.org/3/movie/{movieId}?api_key={_apiKey}&append_to_response=credits";
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        // You can add mapping logic to DTOs here
    }

}
