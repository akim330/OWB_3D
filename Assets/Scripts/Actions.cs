using System;

public static class Actions
{
    // Time
    public static Action<ClockTime> OnMinuteChanged;
    public static Action<ClockTime> OnHourChanged;
    public static Action<CalendarTime> OnDayChanged;

    // Biome
    public static Action<Biome> OnBiomeChanged;
}
