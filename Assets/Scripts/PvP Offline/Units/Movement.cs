using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private float movementSpeed;

    private IEnumerator currentMovementCoroutine;

    public float MovementSpeed
    {
        get => movementSpeed;
        set => movementSpeed = value;
    }

    public void Start()
    {
        movementSpeed = gameObject.GetComponent<InplayUnit>().Unit.UnitAppearance.MovementSpeed;
    }

    public void Move(GameObject boardSquare)
    {
        if (currentMovementCoroutine != null)
            StopCoroutine(currentMovementCoroutine);

        currentMovementCoroutine = MoveCoroutine(boardSquare);
        StartCoroutine(currentMovementCoroutine);
    }

    IEnumerator MoveCoroutine(GameObject boardSquare)
    {
        while (gameObject.transform.position != boardSquare.transform.position)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, boardSquare.transform.position, movementSpeed * Time.deltaTime);
            yield return null;
        }
        gameObject.transform.SetParent(boardSquare.transform, true);
    }
}
