using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLineRenderer : MonoBehaviour
{
    private Camera camera;
    [SerializeField]
    private SpawnHandler spawnHandler;
    [SerializeField]
    private LineRenderer lineRenderer;
    private Vector3 endPosition;

    private Vector3 camOffset = new Vector3(0, 0, 10);

    public void Start()
    {
        camera = Camera.main;

        lineRenderer.positionCount = 2;
        lineRenderer.enabled = false;
    }

    public void StartLineRendererFromUnitTray()
    {
        Debug.Log("Start line render");
        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, (Input.mousePosition) + camOffset);
    }

    public void Update()
    {
        if (lineRenderer.enabled == false)
            return;

        endPosition = (Input.mousePosition) + camOffset;
        lineRenderer.SetPosition(1, endPosition);
    }
}
