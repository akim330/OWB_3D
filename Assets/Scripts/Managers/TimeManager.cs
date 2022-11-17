using UnityEngine;
using System.Text;
using System;

public class CalendarTime
{
    public int day;
    public ClockTime time;

    private StringBuilder sb = new StringBuilder();

    public override string ToString()
    {
        sb.Clear();
        sb.Append("Day ");
        sb.Append(day.ToString());
        sb.Append(",");
        sb.Append(time.ToString());
        return sb.ToString();
    }
}

public class ClockTime
{
    public int hour;
    public int minute;

    private StringBuilder sb = new StringBuilder();

    public override string ToString()
    {
        sb.Clear();
        sb.Append(hour.ToString());
        sb.Append(":");
        sb.Append(minute.ToString());
        return sb.ToString();
    }

    public bool Equals(ClockTime other)
    {
        if ((hour == other.hour) && (minute == other.minute))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

public class TimeManager : MonoBehaviour, IGameManager
{
    public ManagerStatus status { get; private set; }

    [SerializeField] private Clock clock;

    // Default is 0.75
    [SerializeField] public float minuteToRealTime = 0.75f;

    public CalendarTime currentCalendarTime;

    private float timer;

    public void Startup()
    {
        StartTimer();

        status = ManagerStatus.Started;
    }

    private void StartTimer()
    {
        currentCalendarTime = new CalendarTime()
        {
            day = 1,
            time = new ClockTime()
        };
        currentCalendarTime.time.minute = 0;
        currentCalendarTime.time.hour = 6;
        timer = minuteToRealTime;
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            timer = minuteToRealTime;

            currentCalendarTime.time.minute += 1;

            if (currentCalendarTime.time.minute >= 60)
            {
                currentCalendarTime.time.hour += 1;
                currentCalendarTime.time.minute = 0;
                //Actions.OnHourChanged(currentCalendarTime.time);
            }

            if (currentCalendarTime.time.hour >= 24)
            {
                currentCalendarTime.time.hour = 0;
                currentCalendarTime.day += 1;
            }

            clock.SetTime(currentCalendarTime);

            Actions.OnMinuteChanged(currentCalendarTime.time);

        }
    }
}
