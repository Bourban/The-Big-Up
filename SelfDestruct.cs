﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    // Start is called before the first frame update
    public float lifeTime;
    void Awake()
    {
        Invoke("DestroyMe", lifeTime);
    }

    void DestroyMe()
    {
        Destroy(this.gameObject);
    }
}
