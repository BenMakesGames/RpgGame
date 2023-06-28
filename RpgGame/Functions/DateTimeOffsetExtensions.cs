namespace RpgGame.Functions;

public static class DateTimeOffsetExtensions
{
    private const double MoonCycleLength = 29.53058868;

    public static MoonPhase ComputeMoonPhase(this DateTimeOffset dt)
    {
        return dt.GetMoonAge() switch
        {
            < 1.84566 => MoonPhase.NewMoon,
            < 5.53699 => MoonPhase.WaxingCrescent,
            < 9.22831 => MoonPhase.FirstQuarter,
            < 12.91963 => MoonPhase.WaxingGibbous,
            < 16.61096 => MoonPhase.FullMoon,
            < 20.30228 => MoonPhase.WaningGibbous,
            < 23.99361 => MoonPhase.ThirdQuarter,
            < 27.68493 => MoonPhase.WaningCrescent,
            _ => MoonPhase.NewMoon
        };
    }

    public static long GetJulianDay(this DateTimeOffset dt)
    {
        var year = dt.Year;
        var month = dt.Month;
        var day = dt.Day;

        if (month < 3)
        {
            month += 12;
            year -= 1;
        }

        return day + (153 * month - 457) / 5 + 365 * year + (year / 4) - (year / 100) + (year / 400) + 1721119;
    }

    /// <summary>
    /// Returns the age of the moon in days. (0 = new moon, 14ish = full moon)
    /// </summary>
    public static double GetMoonAge(this DateTimeOffset dt)
    {
        var julianDay = dt.GetJulianDay();

        var interpolatedPhase = (julianDay - 2451550.1) / MoonCycleLength;

        // normalize interpolated phase to be between 0 and 1:
        interpolatedPhase -= Math.Floor(interpolatedPhase);

        if(interpolatedPhase < 0)
            interpolatedPhase++;

        return interpolatedPhase * MoonCycleLength;
    }
}

public enum MoonPhase
{
    NewMoon,
    WaxingCrescent,
    FirstQuarter,
    WaxingGibbous,
    FullMoon,
    WaningGibbous,
    ThirdQuarter,
    WaningCrescent
}

public static class MoonPhaseExtensions
{
    public static string ToEmoji(this MoonPhase phase) => phase switch
    {
        MoonPhase.NewMoon => "🌑",
        MoonPhase.WaxingCrescent => "🌒",
        MoonPhase.FirstQuarter => "🌓",
        MoonPhase.WaxingGibbous => "🌔",
        MoonPhase.FullMoon => "🌕",
        MoonPhase.WaningGibbous => "🌖",
        MoonPhase.ThirdQuarter => "🌗",
        MoonPhase.WaningCrescent => "🌘",
        _ => throw new ArgumentOutOfRangeException(nameof(phase), phase, null)
    };
}
