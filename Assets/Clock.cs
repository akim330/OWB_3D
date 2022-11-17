using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Clock : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timeText;

    public void SetTime(CalendarTime calendarTime)
    {
        string am_pm;
        string hour;
        string minute = (Mathf.Floor(calendarTime.time.minute / 10) * 10).ToString();

        if (minute == "0")
        {
            minute = "00";
        }

        if (calendarTime.time.hour == 0)
        {
            am_pm = "AM";
            hour = "12";
        }
        else if (calendarTime.time.hour >= 1 && calendarTime.time.hour <= 11)
        {
            am_pm = "AM";
            hour = calendarTime.time.hour.ToString();
        }
        else if (calendarTime.time.hour == 12)
        {
            am_pm = "PM";
            hour = calendarTime.time.hour.ToString();
        }
        else if (calendarTime.time.hour > 12)
        {
            am_pm = "PM";
            hour = (calendarTime.time.hour - 12).ToString();
        }
        else
        {
            Debug.LogError("Hour is messed up!");
            hour = null;
            am_pm = null;
        }

        //Debug.Log($"Setting time: {clockTime.ToString()}");
        _timeText.text = $"Day {calendarTime.day}\n{hour}:{minute} {am_pm}";
    }
}
