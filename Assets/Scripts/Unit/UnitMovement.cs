using UnityEngine;
using System.Collections;

public class UnitMovement : MonoBehaviour
{
	public Transform target;
	public float speed = 10;
	Vector3 destination;
	Vector3[] path;
	int targetIndex;

    void Start()
    {
		target.parent = null;
    }

    public void SetDestination(Vector3 position)
    {
		target.position = position;
		destination = position;
		PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
	}

	public void OnPathFound(Vector3[] newPath, bool pathSuccessful)
	{
		if (pathSuccessful)
		{
			path = new Vector3[newPath.Length + 1];
            for (int i = 0; i < newPath.Length; i++)
            {
				path[i] = newPath[i];
            }
			path[path.Length - 1] = destination;
			targetIndex = 0;
			StopCoroutine("FollowPath");
			StartCoroutine("FollowPath");
		}
	}

	IEnumerator FollowPath()
	{
		if (path.Length > 0)
        {
			Vector3 currentWaypoint = path[0];

			while (true)
			{
				if (transform.position == currentWaypoint)
				{
					targetIndex++;
					if (targetIndex >= path.Length)
					{
						//transform.position = target.position; // added bc unit stops right before destination for some reason?
						yield break;
					}
					currentWaypoint = path[targetIndex];
				}

				transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);
				yield return null;
			}
		}
	}

	public void OnDrawGizmos()
	{
		if (path != null)
		{
			for (int i = targetIndex; i < path.Length; i++)
			{
				Gizmos.color = Color.black;
				Gizmos.DrawCube(path[i], Vector3.one);

				if (i == targetIndex)
				{
					Gizmos.DrawLine(transform.position, path[i]);
				}
				else
				{
					Gizmos.DrawLine(path[i - 1], path[i]);
				}
			}
		}
	}
}