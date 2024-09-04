using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class SnakeManager : MonoBehaviour
{
    public Camera cam;
    public GameObject segmentPFab;


    public Vector3 target;
    public float speed;
    public float[] radii = new float[15];
    public GameObject[] segments = new GameObject[15];

    // colors
    public Color skin;

    public Vector3 center;

    // Start is called before the first frame update
    void Start()
    {
        target = new Vector3(Random.Range(-10, 10), Random.Range(-7.5f, 7.5f), 0);

        // initialize segments
        for (int i = 0; i < segments.Length; i++)
        {
            GameObject seg = Instantiate(segmentPFab);
            seg.GetComponentInChildren<SegmentScript>().gameObject.transform.localScale = new Vector3(radii[i], radii[i], radii[i]);
            seg.GetComponentInChildren<SegmentScript>().radius = radii[i];
            seg.GetComponentInChildren<SpriteRenderer>().color = skin;
            segments[i] = seg;
        }
    }

    // Update is called once per frame
    void Update()
    {
        /*
        Vector3 mousepos = Input.mousePosition;
        mousepos = cam.ScreenToWorldPoint(mousepos);
        mousepos = new Vector3 (mousepos.x, mousepos.y, 0);

        segments[0].transform.position = Vector3.MoveTowards(segments[0].transform.position, mousepos, speed * Time.deltaTime);
        */

        /*
        if (Vector3.Distance(segments[0].transform.position, target) < 0.1)
        {
            float x = Random.Range(-10, 10);
            float y = Random.Range(-7.5f, 7.5f);

            target = new Vector3(x, y, 0);
        }
        */

        //segments[0].transform.position = Vector3.MoveTowards(segments[0].transform.position, target, speed * Time.deltaTime);
        segments[0].transform.position = segments[0].transform.forward * Time.deltaTime;


        for (int i = 1; i < segments.Length; i++)
        {
            // get vector from current to next segment
            Vector3 vec = segments[i].transform.position - segments[i - 1].transform.position;

            // clamp to radius
            vec = Vector3.ClampMagnitude(vec, segments[i - 1].GetComponentInChildren<SegmentScript>().radius/2);

            // new vector
            segments[i].transform.position = vec + segments[i-1].transform.position;
        }

        /*
        for (int i = 1; i < segments.Length - 1; i++)
        {
            // check if angle is too tight
            Vector3 vec1 = segments[i].transform.position - segments[i-1].transform.position;
            Vector3 vec2 = segments[i+1].transform.position - segments[i].transform.position;

            float currAngle = Mathf.Rad2Deg * Mathf.Acos(Vector3.Dot(vec1, vec2)/(vec1.magnitude * vec2.magnitude));
            //Debug.Log(currAngle);

            if (currAngle > tightestAngle)
            {
                // if its too tight, adjust it???????
                // calculate the tightest angle of the next segment
                // I REMEMBER THIS FROM DYNAMICS >:DDDDDDDDDD

                // tight angle in radians
                float ang = Mathf.Deg2Rad * tightestAngle;

                Vector3 vec = new Vector3(vec2.x * Mathf.Cos(ang) - vec2.y * Mathf.Sin(ang), vec2.x * Mathf.Sin(ang) + vec2.y * Mathf.Cos(ang), 0);

                segments[i+1].transform.position += vec;

            }
        }
        */
    }
}
