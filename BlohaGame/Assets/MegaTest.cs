using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class MegaTest : MonoBehaviour
{
    [SerializeField] Transform tr_cam;
    Transform tr;
    float dist;
    
    void Start()
    {
        tr = transform;
        
    }

    // Update is called once per frame
    void Update()
    {
        dist = tr.position.y - tr_cam.position.y + 2;
        tr.position = new Vector3(tr.position.x, tr.position.y,  dist);
    }
}
