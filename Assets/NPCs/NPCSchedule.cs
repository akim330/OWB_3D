using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using System.Text;
using System;

public class NPCSchedule : MonoBehaviour
{
    private Dictionary<Tuple<int, int>, NPCLandmark> schedule;
    private NPCNavigation navigation;

    private void Start()
    {
        navigation = GetComponent<NPCNavigation>();
        schedule = new Dictionary<Tuple<int, int>, NPCLandmark>();

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
                sb.Append($"{time.ToString()}: {landmark.ToString()} at {landmark.transform.position}\n");
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
                NPCLandmark landmark = schedule[pair.Key];

                //Debug.Log($"{gameObject.name} {time.ToString()}: going to {landmark.ToString()} which is at {landmark.transform.position}");

                navigation.SetLandmarkDestination(landmark);
            }
        }
    }


}
