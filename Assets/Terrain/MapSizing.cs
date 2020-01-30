using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MapSizing : MonoBehaviour
{
    public GameObject backGround;
    public GameObject topWall;
    public GameObject bottomWall;
    public GameObject leftWall;
    public GameObject rightWall;
    public GameObject cover;
    public GameObject decision;
    public GameObject cam;

    private float zoomRate = 0.1f;
    private float mouseMoveRate = 1f;
    public int numPeople = 2;
    public List<GameObject> players;
    public List<GameObject> physicalPlayers = new List<GameObject>();

    private void Awake()
    {
        numPeople = players.Count;

        float xRatio = (numPeople / 2) * 20;
        float yRatio = (numPeople / 2) * 15;
        Vector3 backgroundSize = new Vector3(xRatio, yRatio, 1);
        Vector3 tbWallSize = new Vector3(xRatio, 1, 1);
        Vector3 lrWallSize = new Vector3(1, yRatio, 1);

        topWall.transform.localScale = tbWallSize;
        bottomWall.transform.localScale = tbWallSize;
        leftWall.transform.localScale = lrWallSize;
        rightWall.transform.localScale = lrWallSize;
        backGround.transform.localScale = backgroundSize;

        backGround.transform.position = new Vector3(0, 0, 0);
        topWall.transform.position = new Vector3(0, yRatio / 2, 1);
        bottomWall.transform.position = new Vector3(0, -yRatio / 2, 1);
        leftWall.transform.position = new Vector3(-xRatio / 2, 0, 1);
        rightWall.transform.position = new Vector3(xRatio / 2, 0, 1);

        for (int i = 0; i < numPeople * 3; i++)
        {
            Vector3 spawnPosition = new Vector3(Random.Range((-xRatio / 2) + 1, (xRatio / 2) - 1), Random.Range((-yRatio / 2) + 1, (yRatio / 2)) - 1, 1);
            Instantiate(cover, spawnPosition, Quaternion.identity);
        }

        for (int i = 0; i < numPeople; i++)
        {
            Vector3 spawnPosition = new Vector3(Random.Range((-xRatio / 2) + 1, (xRatio / 2) - 1), Random.Range((-yRatio / 2) + 1, (yRatio / 2)) - 1, 1);
            GameObject newPlayer = Instantiate(decision, spawnPosition, Quaternion.identity);
            newPlayer.name = players[i].name;
            newPlayer.GetComponent<AI>().playerInfo = players[i].GetComponent<Text>();
            physicalPlayers.Add(newPlayer);
        }
    }
    // Start is called before the first frame update
    void Start()
    {




    }

    // Update is called once per frame
    void Update()
    {
        cam.transform.Translate(cam.transform.forward * Input.mouseScrollDelta.y * zoomRate);
        if (Input.GetMouseButton(0))
        {
            cam.transform.Translate(-cam.transform.right * Input.GetAxisRaw("Mouse X") * mouseMoveRate);
            cam.transform.Translate(-cam.transform.up * Input.GetAxisRaw("Mouse Y") * mouseMoveRate);
        }
        if (Input.GetKey(KeyCode.E))
        {
            cam.transform.Translate(cam.transform.forward);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            cam.transform.Translate(-cam.transform.forward);
        }
        if (Input.GetKey(KeyCode.D))
        {
            cam.transform.Translate(cam.transform.right);
        }
        if (Input.GetKey(KeyCode.A))
        {
            cam.transform.Translate(-cam.transform.right);
        }
        if (Input.GetKey(KeyCode.S))
        {
            cam.transform.Translate(-cam.transform.up);
        }
        if (Input.GetKey(KeyCode.W))
        {
            cam.transform.Translate(cam.transform.up);
        }



    }

    public string GetWinner()
    {
        return physicalPlayers[0].GetComponent<AI>().decision.name;
    }


}
