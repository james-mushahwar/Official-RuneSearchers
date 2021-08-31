using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderHandler : MonoBehaviour
{
    void Start()
    {
        gameObject.AddComponent<PolygonCollider2D>();
    }
}
