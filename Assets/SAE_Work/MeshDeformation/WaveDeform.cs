using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor;
using UnityEngine;

public class WaveDeform : MonoBehaviour
{
    public PlaneDeform pointsAccess;
    public float sineWaveSpeed;
    public float mag;
    public float timer = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    { 
        //0 2 //1 3
        //1 3 //2 4
        timer += Time.deltaTime;
        if (timer >= 0)
        {
            //pointsAccess.point1.transform.position = new Vector3(pointsAccess.point1.transform.position.x, Mathf.Sin(pointsAccess.point1.transform.position.y * sineWaveSpeed), pointsAccess.point1.transform.position.z);
            //pointsAccess.point3.transform.position = new Vector3(pointsAccess.point3.transform.position.x, Mathf.Sin(pointsAccess.point3.transform.position.y * sineWaveSpeed), pointsAccess.point3.transform.position.z);
            pointsAccess.point1.transform.position = pointsAccess.point1.transform.position + (-pointsAccess.point1.transform.forward * Mathf.Sin(Time.time * sineWaveSpeed) * mag);
            pointsAccess.point2.transform.position = pointsAccess.point2.transform.position + (-pointsAccess.point2.transform.forward * Mathf.Sin(Time.time * sineWaveSpeed) * mag);
        }
        if (timer > 2)
        {
            //pointsAccess.point2.transform.position = new Vector3(pointsAccess.point2.transform.position.x, Mathf.Sin(pointsAccess.point2.transform.position.y * sineWaveSpeed), pointsAccess.point2.transform.position.z);
            //pointsAccess.point4.transform.position = new Vector3(pointsAccess.point4.transform.position.x, Mathf.Sin(pointsAccess.point4.transform.position.y * sineWaveSpeed), pointsAccess.point4.transform.position.z);
            pointsAccess.point3.transform.position = pointsAccess.point3.transform.position + (-pointsAccess.point3.transform.forward * Mathf.Sin((Time.time + 2) * sineWaveSpeed) * mag);
            pointsAccess.point4.transform.position = pointsAccess.point4.transform.position + (-pointsAccess.point4.transform.forward * Mathf.Sin((Time.time + 2) * sineWaveSpeed) * mag);
        }
    }
}
