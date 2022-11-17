using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkySprite : MonoBehaviour
{
    [SerializeField] SpriteRenderer currentLandscape;
    [SerializeField] SpriteRenderer nextLandscape;

    private Sprite day;
    private Sprite dusk;
    private Sprite night;

    [SerializeField] private Sprite forestDay;
    [SerializeField] private Sprite forestNight;

    [SerializeField] private Sprite cityDay;
    [SerializeField] private Sprite cityDusk;
    [SerializeField] private Sprite cityNight;

    private void OnEnable()
    {
        Actions.OnMinuteChanged += UpdateSpriteMinute;

        Actions.OnBiomeChanged += UpdateSpriteBiome;
    }

    private void OnDisable()
    {
        Actions.OnMinuteChanged -= UpdateSpriteMinute;

        Actions.OnBiomeChanged -= UpdateSpriteBiome;

    }

    private void Start()
    {
        UpdateSpriteMinute(Managers.Time.currentCalendarTime.time);
    }

    private void UpdateSpriteBiome(Biome biome)
    {
        if (biome == Biome.City)
        {
            day = cityDay;
            dusk = cityDusk;
            night = cityNight;
        }
        else if (biome == Biome.Forest)
        {
            day = forestDay;
            dusk = forestDay;
            night = forestNight;
        }
        else
        {
            Debug.LogError("Something went wrong!");
        }
    }

    private void UpdateSpriteMinute(ClockTime time)
    {
        float currentAlpha;

        float startDayTransition = 5f;
        float startDay = 8f;
        float startDuskTransition = 16f;
        float startNightTransition = 18f;
        float startNight = 20f;

        float currentHour = time.hour + time.minute / 60f;

        if (time.hour >= startDayTransition && time.hour < startDay)
        {

            currentAlpha = ((startDay - currentHour) / (startDay - startDayTransition));
            currentLandscape.sprite = night;
            nextLandscape.sprite = day;

            Debug.Log($"1: {time.hour}, {currentAlpha}");

        }
        else if (time.hour >= startDay && time.hour < startDuskTransition)
        {
            Debug.Log("2");

            currentAlpha = 1;
            currentLandscape.sprite = day;
            nextLandscape.sprite = null;
        }
        else if (time.hour >= startDuskTransition && time.hour < startNightTransition)
        {
            Debug.Log("3");

            currentAlpha = ((startNightTransition - currentHour) / (startNightTransition - startDuskTransition));
            currentLandscape.sprite = day;
            nextLandscape.sprite = dusk;
        }
        else if (time.hour >= startNightTransition && time.hour < startNight)
        {
            Debug.Log("4");

            currentAlpha = ((startNight - currentHour) / (startNight - startNightTransition));
            currentLandscape.sprite = dusk;
            nextLandscape.sprite = night;
        }
        else if (time.hour >= startNight || time.hour < startDayTransition)
        {
            Debug.Log("5");

            currentAlpha = 1;
            currentLandscape.sprite = night;
            nextLandscape.sprite = null;
        }
        else
        {
            Debug.LogError($"Hour {time.hour} didn't fall into a bucket");
            currentAlpha = 1;
            currentLandscape.sprite = day;
            nextLandscape.sprite = null;
        }

        currentLandscape.color = new Color(1, 1, 1, currentAlpha);
        nextLandscape.color = new Color(1, 1, 1, 1 - currentAlpha);
    }

    //private void UpdateSpriteHour(ClockTime time)
    //{
    //    float currentAlpha;

    //    float startDayTransition = 5f;
    //    float startDay = 8f;
    //    float startDuskTransition = 16f;
    //    float startNightTransition = 18f;
    //    float startNight = 20f;

    //    if (time.hour >= startDayTransition && time.hour < startDay)
    //    {

    //        currentAlpha = ((startDay - (float) time.hour) / (startDay - startDayTransition));
    //        currentLandscape.sprite = night;
    //        nextLandscape.sprite = day;

    //        Debug.Log($"1: {time.hour}, {currentAlpha}");

    //    }
    //    else if (time.hour >= startDay && time.hour < startDuskTransition)
    //    {
    //        Debug.Log("2");

    //        currentAlpha = 1;
    //        currentLandscape.sprite = day;
    //        nextLandscape.sprite = null;
    //    }
    //    else if (time.hour >= startDuskTransition && time.hour < startNightTransition)
    //    {
    //        Debug.Log("3");

    //        currentAlpha = ((startNightTransition - (float) time.hour) / (startNightTransition - startDuskTransition));
    //        currentLandscape.sprite = day;
    //        nextLandscape.sprite = dusk;
    //    }
    //    else if (time.hour >= startNightTransition && time.hour < startNight)
    //    {
    //        Debug.Log("4");

    //        currentAlpha = ((startNight - (float) time.hour) / (startNight - startNightTransition));
    //        currentLandscape.sprite = dusk;
    //        nextLandscape.sprite = night;
    //    }
    //    else if (time.hour >= startNight || time.hour < startDayTransition)
    //    {
    //        Debug.Log("5");

    //        currentAlpha = 1;
    //        currentLandscape.sprite = night;
    //        nextLandscape.sprite = null;
    //    }
    //    else
    //    {
    //        Debug.LogError($"Hour {time.hour} didn't fall into a bucket");
    //        currentAlpha = 1;
    //        currentLandscape.sprite = day;
    //        nextLandscape.sprite = null;
    //    }

    //    currentLandscape.color = new Color(1, 1, 1, currentAlpha);
    //    nextLandscape.color = new Color(1, 1, 1, 1 - currentAlpha);
    //}
}
