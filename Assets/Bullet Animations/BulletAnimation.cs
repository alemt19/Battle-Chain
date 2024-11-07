using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAnimation : MonoBehaviour
{
    Animator anim;
    int setAnimValue1;
    int setAnimValue2;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        if (gameObject.CompareTag("BulletCollision"))
        {
            setAnimValue1 = 1;
            setAnimValue2 = 0;
        }
        else if (gameObject.CompareTag("BlueLaserCollision"))
        {
            setAnimValue1 = -1;
            setAnimValue2 = 0;
        }
        else if (gameObject.CompareTag("RedLaserCollision"))
        {
            setAnimValue1 = 0;
            setAnimValue2 = -1;
        }
        else if (gameObject.CompareTag("GreenLaserCollision"))
        {
            setAnimValue1 = 0;
            setAnimValue2 = 1;
        }
        anim.SetFloat("SetAnim1", setAnimValue1);
        anim.SetFloat("SetAnim2", setAnimValue2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
