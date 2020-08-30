using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    private void Start()
    {
        transform.LookAt(PlayManager.instance.pc.transform);
    }

    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * 10;
        
    }
}
