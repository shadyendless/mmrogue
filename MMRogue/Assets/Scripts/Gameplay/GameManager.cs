using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public Transform spawnPoint = null;
    public Transform killBoundary = null;
    public Transform playerPrefab = null;

    public GameObject player;

    void Start()
    {
        player = GameObject.Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation) as GameObject;
    }
	
	// Update is called once per frame
	void Update()
	{
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            return;
        }

        if (player.transform.position.y < killBoundary.position.y)
            player.transform.position = spawnPoint.position;
	}
}