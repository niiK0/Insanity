using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class HeadAim : MonoBehaviour
{
    // Start is called before the first frame update
    public RigBuilder rig;
    void Start()
    {
        MultiAimConstraint aim = GetComponent<MultiAimConstraint>();
        var data = aim.data.sourceObjects;
        data.SetTransform(0, GameObject.FindGameObjectWithTag("Player").transform);
        aim.data.sourceObjects = data;
        rig.Build();
    }
}
