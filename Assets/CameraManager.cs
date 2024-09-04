using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Camera cam;
    public float movespeed;
    public float zoomspeed;

    public PlayerScript player;
    public SnakeManager boss;

    public Vector3 target;
    public float xdistance;
    public float ydistance;
    public float xmin = 0;
    public float xmax = 0;
    public float ymin = 0;
    public float ymax = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Determine center point between player and boss

        // Calculate the outer edges of the focus by looking through all the segments and stuff.
        
        for (int i = 0; i < boss.segments.Length; i++)
        {
            Vector3 temp = boss.segments[i].transform.position;

            CheckRange(temp);
        }
        CheckRange(player.mace.transform.position);
        CheckRange(player.transform.position);

        // Center of focus
        target = new Vector3((xmin + xmax) / 2, (ymin + ymax) / 2, -10);

        // Move towards
        cam.transform.position = Vector3.MoveTowards(cam.transform.position, target, movespeed * Time.deltaTime);

        // Adjust zoom to fit both things
        xdistance = Mathf.Abs(xmax - xmin);
        ydistance = Mathf.Abs(ymax - ymin);

        float xdratio = xdistance / 16;
        float ydratio = ydistance / 9;

        
        if (xdratio > ydratio)
        {
            float newSize = 5f / 12f * xdistance;
            cam.orthographicSize = Mathf.MoveTowards(cam.orthographicSize, newSize, zoomspeed);
        }
        else
        {   
            float newSize = 5f / 6.75f * ydistance;
            cam.orthographicSize = Mathf.MoveTowards(cam.orthographicSize, newSize, zoomspeed);
        }

        BoundsReset();
    }

    public void CheckRange(Vector3 temp)
    {
        // check if x is max or min.
        if (temp.x < xmin) xmin = temp.x;
        if (temp.x > xmax) xmax = temp.x;

        // check if y is max or min
        if (temp.y < ymin) ymin = temp.y;
        if (temp.y > ymax) ymax = temp.y;
    }

    public void BoundsReset()
    {
        xmin = 0; xmax = 0; ymin = 0; ymax = 0;
    }
}
