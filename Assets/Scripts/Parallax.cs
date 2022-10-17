using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float length, height, skyLength, skyHeight, undergroundLength, undergroundHeight, startxpos, startypos;
    public GameObject cam;
    public float parallaxEffect;
    public bool yParallax;

    [SerializeField] private SpriteRenderer landscapeRenderer;
    [SerializeField] private Transform skyTransform;
    private SpriteRenderer skyRenderer;
    [SerializeField] private Transform undergroundTransform;
    private SpriteRenderer undergroundRenderer;
    
    [SerializeField] private int nHorizontalCycles;
    [SerializeField] private int skyCycles;
    [SerializeField] private int undergroundCycles;
    private float horizontalCycleLength;
    private float skyCycleLength;
    private float undergroundCycleLength;

    // Start is called before the first frame update
    void Start()
    {
        skyRenderer = skyTransform.GetComponent<SpriteRenderer>();
        undergroundRenderer = undergroundTransform.GetComponent<SpriteRenderer>();
        
        // Original position of Background
        startxpos = transform.position.x;
        startypos = transform.position.y;

        // Get length and height of landscape strip
        length = landscapeRenderer.bounds.size.x;
        height = landscapeRenderer.bounds.size.y;

        // Get length and height + sky strip
        skyLength = skyRenderer.bounds.size.x;
        skyHeight = skyRenderer.bounds.size.y;

        undergroundLength = undergroundRenderer.bounds.size.x;
        undergroundHeight = undergroundRenderer.bounds.size.y;        horizontalCycleLength = (float) length / nHorizontalCycles;
        //Debug.Log($"Horizontal cycle length: {horizontalCycleLength}");
        skyCycleLength = skyHeight / skyCycles;
        undergroundCycleLength = undergroundHeight / undergroundCycles;

    }

    // Update is called once per frame
    void Update()
    {

        // xdist, ydist: amount that the background moves (a proportion of the camera's movement)
        float xdist = (cam.transform.position.x * parallaxEffect);

        float ydist;
        if (yParallax)
        {
            ydist = 0;
        }
        else
        {
            ydist = (cam.transform.position.y * parallaxEffect);
        }

        // Set background location 
        transform.position = new Vector3(startxpos + xdist, startypos + ydist, transform.position.z);

        float endpoint = transform.position.x + length / 2;
        float startpoint = endpoint - length;

        // Sky 
        if (cam.transform.position.y > height)
        {
            float toppoint = skyTransform.position.y + skyHeight / 2;
            float bottompoint = skyTransform.position.y - skyHeight / 2;

            if (cam.transform.position.y > toppoint - skyCycleLength)
            {
                skyTransform.position = new Vector3(skyTransform.position.x, skyTransform.position.y + 2 * skyCycleLength, skyTransform.position.z);
            }
            else if (cam.transform.position.y < bottompoint + skyCycleLength)
            {
                skyTransform.position = new Vector3(skyTransform.position.x, skyTransform.position.y - 2 * skyCycleLength, skyTransform.position.z);

            }
        }
        // Underground
        else if (cam.transform.position.y < 0)
        {
            float toppoint = undergroundTransform.position.y + undergroundHeight / 2;
            float bottompoint = undergroundTransform.position.y - undergroundHeight / 2;

            if (cam.transform.position.y > toppoint - undergroundCycleLength)
            {
                undergroundTransform.position = new Vector3(undergroundTransform.position.x, undergroundTransform.position.y + 2 * undergroundCycleLength, undergroundTransform.position.z);
            }
            else if (cam.transform.position.y < bottompoint + undergroundCycleLength)
            {
                undergroundTransform.position = new Vector3(undergroundTransform.position.x, undergroundTransform.position.y - 2 * undergroundCycleLength, undergroundTransform.position.z);

            }
        }

        // 
        float xtemp = (cam.transform.position.x * (1 - parallaxEffect));
        float ytemp = (cam.transform.position.y * (1 - parallaxEffect));

        //Debug.Log($"cam: {cam.transform.position.x}, endpoint: {endpoint}, startpoint: {startpoint}\n cycleLength: {horizontalCycleLength}, startxpos: {startxpos}, length: {length}");

        if (cam.transform.position.x > endpoint - horizontalCycleLength)
        {
            startxpos += horizontalCycleLength;
        }
        else if (cam.transform.position.x < startpoint + horizontalCycleLength)
        {
            startxpos -= horizontalCycleLength;
        }

        //if (xtemp > startxpos + length) startxpos += length;
        //else if (xtemp < startxpos - length) startxpos -= length;

        //if (ytemp > startypos + height) startypos += height;
        //else if (ytemp < startypos - height) startypos -= height;

    }
}
