using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
    List<Node> path = new List<Node>();

    [SerializeField] [Range(0f, 5f)] float speed = 1f;

    Enemy enemy;
    GridManager gridManager;
    Pathfinder pathfinder;

    void OnEnable()
    {
        RecalculatePath();
        ReturnToStart();
        StartCoroutine(FollowPath());
    }

    void Awake()
    {
        enemy = GetComponent<Enemy>();
        gridManager = FindObjectOfType<GridManager>();
        pathfinder = FindObjectOfType<Pathfinder>();
    }

    void RecalculatePath()
    {
        path.Clear();
        path = pathfinder.GetNewPath();
    }

    void ReturnToStart()
    {
        transform.position = gridManager.GetPositionsFromCoordinates(pathfinder.StartCoordinates);
    }

    void FinishPath()
    {
        enemy.StealGold();
        gameObject.SetActive(false);
    }

    IEnumerator FollowPath()
    {
        for (int i = 0; i < path.Count; i++)
        {
            Vector3 startPosition = transform.position;
            Vector3 endPosition = gridManager.GetPositionsFromCoordinates(path[i].coordinates);
            float travelPercentage = 0f;

            transform.LookAt(endPosition);

            while (travelPercentage < 1f)
            {
                travelPercentage += Time.deltaTime * speed ;
                transform.position = Vector3.Lerp(startPosition, endPosition, travelPercentage);
                yield return new WaitForEndOfFrame();
            }
        }
        FinishPath();
    }
}