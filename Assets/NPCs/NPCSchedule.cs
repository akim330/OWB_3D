using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using System.Text;
using System;

public class NPCSchedule : MonoBehaviour
{
    private Dictionary<Tuple<int, int>, NPCLandmark> schedule;
    private NPCMovement movement;

    private void Start()
    {
        movement = GetComponent<NPCMovement>();
        schedule = new Dictionary<Tuple<int, int>, NPCLandmark>();

        //ClockTime test1 = new ClockTime() { hour = 6, minute = 1 };
        //ClockTime test2 = new ClockTime() { hour = 6, minute = 1 };

        //Debug.Log($"TEST: {test1 == test2}");

        RandomizeSchedule(10);
    }

    private ClockTime GetRandomTime()
    {
        return new ClockTime()
        {
            hour = Random.Range(0, 24),
            minute = Random.Range(0, 60)
        };
    }

    private void RandomizeSchedule(int nEntries)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append($"Schedule for {gameObject.name}:\n");
        for (int i = 0; i < nEntries; i++)
        {
            ClockTime time = GetRandomTime();
            NPCLandmark landmark = Managers.Town.GetRandomLandmark();

            try
            {
                schedule.Add(new Tuple<int, int>(time.hour, time.minute), landmark);
                sb.Append($"{time.ToString()}: {landmark.ToString()}\n");
            }
            catch (ArgumentException)
            {

            }

        }
        //Debug.Log(sb.ToString());


    }

    private void OnEnable()
    {
        Actions.OnMinuteChanged += CheckTime;
    }

    private void OnDisable()
    {
        Actions.OnMinuteChanged -= CheckTime;
    }

    private void CheckTime(ClockTime time)
    {
        //Debug.Log($"Checking {time.ToString()}");

        foreach (KeyValuePair<Tuple<int, int>, NPCLandmark> pair in schedule)
        {
            if (pair.Key.Item1 == time.hour && pair.Key.Item2 == time.minute)
            {
                //Debug.Log($"{gameObject.name}'s schedule contains {time.ToString()}");
                NPCLandmark landmark = schedule[pair.Key];

                movement.SetLandmarkDestination(landmark);
            }
        }
    }
}
