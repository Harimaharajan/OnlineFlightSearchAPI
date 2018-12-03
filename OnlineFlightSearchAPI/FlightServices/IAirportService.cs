namespace OnlineFlightSearchAPI.FlightServices
{
    public interface IAirportService
    {
        bool CheckIfAirportIsValid(string airportCode);
    }
}