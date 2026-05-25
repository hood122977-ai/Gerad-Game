using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    GameObject player;
    public GameObject subScreen;

    public float leftLimit = 0.0f;
    public float rightLimit = 0.0f;
    public float topLimit = 0.0f;
    public float bottomLimit = 0.0f;

    public bool isForceScrollX = false;
    public float forceScrollSpeedx = 0.5f;
    public bool isForceScrollY = false;
    public float forceScrollSpeedY = 0.5f;

    void Start()
    {
        this.player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (this.player != null)
        {
            Vector3 playerPos = new Vector3
                (player.transform.position.x, player.transform.position.y, transform.position.z);

            if (isForceScrollX)
            {
                playerPos.x = transform.position.x + (forceScrollSpeedx * Time.deltaTime);
            }

            if (playerPos.x < leftLimit)
            {
                playerPos.x = leftLimit;
            }
            else if (playerPos.x > rightLimit)
            {
                playerPos.x = rightLimit;
            }

            if (isForceScrollY)
            {
                playerPos.y = transform.position.y + (forceScrollSpeedY * Time.deltaTime);
            }

            if (playerPos.y < bottomLimit)
            {
                playerPos.y = bottomLimit;
            }
            else if (playerPos.y > topLimit)
            {
                playerPos.y = topLimit;
            }

            Vector3 CameraPos = playerPos;
            transform.position = CameraPos;

            if (subScreen != null)
            {
                playerPos.y = subScreen.transform.position.y;
                playerPos.z = subScreen.transform.position.z;
                Vector3 subPos = new Vector3(
                    playerPos.x / 2.0f, playerPos.y, playerPos.z);
                subScreen.transform.position = subPos;
            }
        }
    }
}
