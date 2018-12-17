namespace OnlineFlightSearchAPI.UnitTests
{
    public static class TestConstants
    {
        public const string ValidFlightSearchRequest = "/api/Flight/Search?startLocation={0}&endDestination={1}&departureDate={2}";
        public const string InvalidFlightSearchRequest = "/api/Flights/Search?startLocation={0}&endDestination={1}&departureDate={2}";
    }
}
