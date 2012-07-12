using System;

public static class DateTimeExtensions
{
    private static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

    public static DateTime ConvertUnixTicksToDateTime(this long ticks)
    {
        return UnixEpoch.AddTicks(ticks);
    }

    // returns the number of milliseconds since Jan 1, 1970 (useful for converting C# dates to JS dates)
    public static double ToUnixDateTime(this DateTime dt)
    {
        return (int)(dt - UnixEpoch).TotalMilliseconds;
    }
}