using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class PostManager : Singleton<PostManager>
{
    private CinemachineImpulseSource impulseSource;
    // Start is called before the first frame update
    void Start()
    {
        impulseSource=GetComponent<CinemachineImpulseSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShakeCamera()
    {
        impulseSource.GenerateImpulse();
    }
}
