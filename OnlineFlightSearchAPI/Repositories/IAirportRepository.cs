namespace OnlineFlightSearchAPI.Repositories
{
    public interface IAirportRepository
    {
        bool IsAirportValid(string airportCode);
    }
}