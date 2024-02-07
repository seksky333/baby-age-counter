namespace BabyAgeCounter.Server.utilities;

public static class DateTimeConverter
{
    public static long ToUtcMillis(DateTime dateTime)
    {
        try
        {
            var offset = new DateTimeOffset(dateTime);
            return offset.ToUnixTimeMilliseconds();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}