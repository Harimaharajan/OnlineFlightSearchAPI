namespace OnlineFlightSearchAPI.FlightServices
{
    public static class ValidationMessages
    {
        public const string StartLocationCannotBeEmpty = "Start Location cannot be empty";
        public const string DestinationCannotBeEmpty = "Destination cannot be empty";
        public const string InvalidStartLocation = "Invalid Start Location";
        public const string InvalidDestination = "Invalid Destination";
        public const string InvalidDepartureDate = "Invalid Departure Date";

        public const string NoFlightsAvailable = "No Flights Available";
        public const string StartandEndLocationCannotBeSame = "Start and End Location cannot be same";
    }
}
