using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public InputField addPlayerInput;
    public GameObject playerPosition;
    public GameObject addPlayerForFont;
    public Canvas startCanvas;
    public Canvas playCanvas;
    public Canvas winCanvas;
    private List<GameObject> players = new List<GameObject>();
    public GameObject map;
    public Text winnerText;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            AddPlayer();
        }

        if (map.active && map.GetComponent<MapSizing>().physicalPlayers.Count < 2)
        {
            string winner = "";
            foreach (GameObject p in players)
            {
                if (!p.GetComponent<Text>().text.Contains("OUT"))
                {
                    winner = p.name;
                }
            }
            winCanvas.enabled = true;
            winnerText.text = winner;
            Time.timeScale = 0;

            
        }
    }

    private void Awake()
    {
        Time.timeScale = 0;
        winCanvas.enabled = false;
        playCanvas.enabled = false;
        startCanvas.enabled = true;
    }


    public void AddPlayer()
    {
        if (addPlayerInput.text.Length == 0)
        {
            return;
        }
        string newPlayer = addPlayerInput.text;
        addPlayerInput.text = "";
        GameObject npgo = new GameObject();
        npgo.name = newPlayer;
        npgo.transform.SetParent(startCanvas.transform);
        Text newPlayerText = npgo.AddComponent<Text>();
        newPlayerText.color = Color.white;
        newPlayerText.text = newPlayer;
        newPlayerText.font = addPlayerForFont.GetComponent<Text>().font;
        npgo.transform.position = playerPosition.transform.position;
        npgo.GetComponent<RectTransform>().sizeDelta = new Vector2(150, 30);
        players.Add(npgo);
        npgo.transform.SetPositionAndRotation(new Vector3(playerPosition.transform.position.x,
            playerPosition.transform.position.y + (players.Count) * -30,
            playerPosition.transform.position.z), new Quaternion(0, 0, 0, 0));


    }

    public void StartGame()
    {
        if (players.Count <= 1)
        {
            return;
        }
        foreach(GameObject p in players)
        {
            p.transform.SetParent(playCanvas.transform);
        }
        map.GetComponent<MapSizing>().players = players;
        Time.timeScale = 1;
        startCanvas.enabled = false;
        playCanvas.enabled = true;
        map.SetActive(true);
    }
    public void Replay()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
    }
}
