using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class Map : MonoBehaviour {

    public GameObject[] obstacles;
    public GameObject floorTile;
    public GameObject exitTile;
    public GameObject lilith;
    public Renderer transition;

    public GameObject player;
    private Renderer playerRenderer;

    public float transitionTime = 1.0f;

    private int width = 10;
    private int height = 10;

    private List<Level> levels;
    private Level currentLevel;
    private int currentLevelNum;

    private bool isLilith = false;
    private bool isExit = false;


    private int changeLevel = 0;

    public struct Level {
        public int width;
        public int height;
        public int[,] map;
        public Vector2 playerPosition;
        public Vector2 exitPosition;

        public List<Trap> traps;

        public Level(int[,] _map, int w, int h, Vector2 _playerPosition, Vector2 _exitPosition) {
            map = _map;
            width = w;
            height = h;
            playerPosition = _playerPosition;
            exitPosition = _exitPosition;
            traps = new List<Trap>();
        }    
        
        public bool HasFilledTraps() {
            bool result = true;
            foreach(Trap trap in traps) {
                result = result && trap.HasDemon();
            }
            return result;
        }
        
        public void Reset() {
            traps.Clear();
        } 
    }

    private int[,] map1 = new int[,] {
        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
        {1, 0, 0, 0, 1, 1, 1, 1, 1, 1},
        {1, 0, 0, 0, 1, 1, 1, 1, 1, 1},
        {1, 1, 0, 0, 1, 1, 1, 1, 1, 1},
        {1, 1, 0, 0, 1, 1, 1, 1, 1, 1},
        {1, 1, 0, 0, 1, 1, 1, 1, 1, 1},
        {1, 1, 0, 0, 0, 0, 0, 0, 0, 1},
        {1, 1, 0, 0, 0, 0, 0, 0, 0, 1},
        {1, 1, 0, 0, 0, 0, 0, 0, 0, 1},
        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
    };
    private Vector2 position1 = new Vector2(3, 2);
    private Vector2 exit1 = new Vector2(3, 5);


    private int[,] map2 = new int[,] {
        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
        {1, 0, 0, 0, 1, 0, 0, 0, 0, 1},
        {1, 1, 1, 0, 1, 0, 0, 0, 0, 1},
        {1, 1, 1, 0, 1, 0, 3, 0, 0, 1},
        {1, 1, 1, 0, 1, 0, 0, 4, 0, 1},
        {1, 1, 1, 0, 1, 1, 1, 1, 0, 1},
        {1, 0, 0, 0, 1, 1, 1, 1, 0, 1},
        {1, 0, 1, 4, 1, 1, 1, 1, 0, 1},
        {1, 0, 3, 0, 0, 0, 0, 0, 0, 1},
        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
    };
    private Vector2 position2 = new Vector2(1, 8);
    private Vector2 exit2 = new Vector2(7, 7);



    private int[,] map3 = new int[,] {
        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
        {1, 0, 0, 0, 0, 0, 0, 0, 0, 1},
        {1, 0, 1, 0, 0, 0, 0, 0, 0, 1},
        {1, 0, 1, 0, 2, 2, 2, 0, 0, 1},
        {1, 0, 1, 2, 0, 0, 2, 0, 0, 1},
        {1, 0, 1, 0, 2, 2, 2, 0, 0, 1},
        {1, 0, 1, 0, 0, 0, 0, 0, 0, 1},
        {1, 0, 0, 0, 0, 0, 0, 0, 0, 1},
        {1, 0, 0, 0, 0, 0, 0, 0, 0, 1},
        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
    };
    private Vector2 position3 = new Vector2(1, 8);
    private Vector2 exit3 = new Vector2(5, 5);


    private int[,] map4 = new int[,] {
        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
        {1, 0, 0, 0, 0, 0, 0, 3, 2, 1},
        {1, 0, 0, 0, 2, 2, 2, 2, 2, 1},
        {1, 2, 0, 4, 0, 0, 0, 4, 0, 1},
        {1, 0, 0, 0, 0, 0, 0, 0, 0, 1},
        {1, 0, 0, 0, 0, 0, 0, 0, 0, 1},
        {1, 0, 2, 0, 0, 0, 0, 0, 0, 1},
        {1, 0, 2, 0, 0, 0, 0, 0, 0, 1},
        {1, 3, 2, 0, 0, 0, 0, 0, 3, 1},
        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
    };
    private Vector2 position4 = new Vector2(1, 8);
    private Vector2 exit4 = new Vector2(5, 5);

    private int[,] map5 = new int[,] {
        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
        {1, 0, 1, 0, 0, 3, 1, 0, 0, 1},
        {1, 4, 2, 0, 0, 0, 1, 0, 0, 1},
        {1, 3, 1, 0, 1, 1, 1, 2, 0, 1},
        {1, 0, 2, 0, 1, 2, 0, 2, 0, 1},
        {1, 0, 2, 0, 1, 0, 2, 0, 2, 1},
        {1, 0, 2, 0, 1, 2, 0, 2, 0, 1},
        {1, 0, 1, 0, 1, 1, 1, 1, 0, 1},
        {1, 0, 0, 0, 0, 0, 0, 0, 0, 1},
        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
    };
    private Vector2 position5 = new Vector2(1, 8);
    private Vector2 exit5 = new Vector2(8, 8);


    private int[,] map6 = new int[,] {
        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
        {1, 2, 0, 2, 0, 0, 2, 0, 2, 1},
        {1, 0, 2, 0, 0, 2, 0, 0, 2, 1},
        {1, 1, 1, 0, 0, 2, 0, 1, 1, 1},
        {1, 1, 1, 2, 1, 1, 0, 1, 1, 1},
        {1, 1, 1, 0, 1, 1, 0, 1, 1, 1},
        {1, 1, 1, 2, 0, 2, 2, 0, 1, 1},
        {1, 2, 0, 2, 0, 2, 0, 2, 0, 1},
        {1, 0, 2, 0, 0, 0, 2, 0, 0, 1},
        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
    };
    private Vector2 position6 = new Vector2(1, 1);
    private Vector2 exit6 = new Vector2(7, 7);


    private int[,] map7 = new int[,] {
        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
        {1, 3, 0, 0, 0, 0, 0, 0, 3, 1},
        {1, 0, 0, 0, 0, 0, 0, 0, 0, 1},
        {1, 0, 0, 0, 0, 0, 0, 0, 0, 1},
        {1, 0, 0, 0, 4, 0, 0, 0, 0, 1},
        {1, 0, 0, 4, 0, 4, 0, 0, 0, 1},
        {1, 0, 0, 0, 4, 0, 0, 0, 0, 1},
        {1, 0, 0, 0, 0, 0, 0, 0, 0, 1},
        {1, 3, 0, 0, 0, 0, 0, 0, 3, 1},
        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
    };
    private Vector2 position7 = new Vector2(1, 4);
    private Vector2 exit7 = new Vector2(4, 4);

    private int[,] map8 = new int[,] {
        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
        {1, 3, 2, 0, 0, 0, 1, 3, 0, 1},
        {1, 0, 2, 0, 0, 0, 0, 0, 4, 1},
        {1, 0, 2, 0, 1, 0, 0, 4, 0, 1},
        {1, 0, 2, 0, 2, 2, 2, 0, 0, 1},
        {1, 0, 2, 0, 0, 0, 1, 0, 3, 1},
        {1, 0, 1, 2, 0, 0, 1, 0, 0, 1},
        {1, 0, 0, 4, 1, 0, 1, 0, 0, 1},
        {1, 0, 0, 3, 1, 0, 2, 0, 0, 1},
        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
    };
    private Vector2 position8 = new Vector2(1, 5);
    private Vector2 exit8 = new Vector2(8, 8);

    private int[,] map9 = new int[,] {
        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
        {1, 0, 0, 0, 0, 0, 0, 3, 0, 1},
        {1, 0, 0, 0, 0, 2, 0, 3, 3, 1},
        {1, 4, 0, 4, 0, 2, 0, 3, 3, 1},
        {1, 4, 0, 4, 0, 2, 0, 2, 2, 1},
        {1, 0, 4, 0, 0, 0, 2, 0, 0, 1},
        {1, 0, 0, 0, 0, 0, 0, 0, 0, 1},
        {1, 0, 0, 0, 0, 0, 0, 0, 0, 1},
        {1, 0, 0, 0, 0, 0, 0, 0, 0, 1},
        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
    };


    private Vector2 position9 = new Vector2(1, 1);
    private Vector2 exit9 = new Vector2(8, 8);

    private int[,] map10 = new int[,] {
        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
        {1, 3, 0, 0, 1, 3, 1, 0, 0, 1},
        {1, 0, 0, 0, 1, 0, 0, 2, 0, 1},
        {1, 4, 0, 0, 1, 2, 0, 2, 0, 1},
        {1, 0, 4, 0, 1, 0, 2, 2, 0, 1},
        {1, 0, 2, 0, 1, 2, 0, 2, 0, 1},
        {1, 4, 2, 3, 1, 0, 2, 0, 2, 1},
        {1, 0, 3, 0, 1, 2, 2, 0, 2, 1},
        {1, 0, 0, 2, 0, 0, 0, 0, 0, 1},
        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
    };
    private Vector2 position10 = new Vector2(1, 7);
    private Vector2 exit10 = new Vector2(8, 8);


    private int[,] map11 = new int[,] {
        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
        {1, 3, 3, 0, 0, 0, 0, 3, 3, 1},
        {1, 3, 0, 0, 4, 0, 0, 0, 3, 1},
        {1, 0, 0, 2, 2, 2, 0, 0, 0, 1},
        {1, 0, 4, 2, 0, 2, 4, 0, 0, 1},
        {1, 0, 0, 2, 2, 2, 0, 0, 0, 1},
        {1, 0, 0, 0, 4, 0, 0, 0, 0, 1},
        {1, 3, 0, 0, 0, 0, 0, 0, 3, 1},
        {1, 3, 3, 0, 0, 0, 0, 3, 3, 1},
        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
    };
    private Vector2 position11 = new Vector2(4, 5);
    private Vector2 exit11 = new Vector2(3, 2);


    private int[,] map12 = new int[,] {
        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
        {1, 0, 0, 0, 0, 0, 0, 0, 0, 1},
        {1, 0, 4, 0, 0, 0, 4, 0, 0, 1},
        {1, 0, 0, 3, 0, 3, 0, 0, 0, 1},
        {1, 0, 0, 0, 3, 0, 0, 0, 0, 1},
        {1, 0, 0, 3, 0, 3, 0, 0, 0, 1},
        {1, 4, 0, 0, 0, 0, 0, 4, 0, 1},
        {1, 0, 0, 0, 4, 0, 0, 0, 0, 1},
        {1, 0, 0, 0, 0, 0, 0, 0, 0, 1},
        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
    };
    private Vector2 position12 = new Vector2(1, 1);
    private Vector2 exit12 = new Vector2(7, 1);

    private int[,] map13 = new int[,] {
        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
        {1, 0, 0, 0, 0, 0, 0, 0, 0, 1},
        {1, 0, 0, 0, 0, 0, 0, 0, 0, 1},
        {1, 1, 0, 0, 0, 0, 5, 0, 0, 1},
        {1, 1, 0, 0, 0, 0, 0, 0, 0, 1},
        {1, 1, 0, 0, 0, 0, 0, 0, 0, 1},
        {1, 1, 0, 0, 0, 0, 0, 0, 0, 1},
        {1, 1, 0, 0, 0, 0, 0, 0, 0, 1},
        {1, 1, 0, 0, 0, 0, 0, 0, 0, 1},
        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
    };
    private Vector2 position13 = new Vector2(3, 2);
    private Vector2 exit13 = new Vector2(3, 5);

    void Start () {
        transition.material.color = new Color(0, 0, 0, 0);

        levels = new List<Level>();

        levels.Add(new Level(map1, width, height, position1, exit1));
        levels.Add(new Level(map2, width, height, position2, exit2));
        levels.Add(new Level(map3, width, height, position3, exit3));
        levels.Add(new Level(map4, width, height, position4, exit4));
        levels.Add(new Level(map5, width, height, position5, exit5));
        levels.Add(new Level(map6, width, height, position6, exit6));
        levels.Add(new Level(map7, width, height, position7, exit7));
        levels.Add(new Level(map8, width, height, position8, exit8));
        levels.Add(new Level(map9, width, height, position9, exit9));
        levels.Add(new Level(map10, width, height, position10, exit10));
        levels.Add(new Level(map11, width, height, position11, exit11));
        levels.Add(new Level(map12, width, height, position12, exit12));
        levels.Add(new Level(map13, width, height, position13, exit13));

        LoadLevel(0);

        playerRenderer = player.GetComponent<Renderer>();
    }

    void FixedUpdate()
    {
        if(changeLevel == 1) {
            StartCoroutine("FadeInTransition");
            changeLevel = 0;
        }
        if(changeLevel == 2)
        {
            ChangeLevel();
            StartCoroutine("FadeOutTransition");
            changeLevel = 0;
        }
    }

    void Update()
    {
        if(currentLevelNum == 12)
        {
            if(player.GetComponent<Player>().steps >= 10 && !isLilith)
            {
                ShowLilith();
                isLilith = true;
            }

            if(player.GetComponent<Player>().steps >= 20 && !isExit)
            {
                ShowExit();
                isExit = true;
            }
        }
    }

    void ShowExit()
    {
        GameObject instance = (GameObject)Instantiate(exitTile, new Vector3(currentLevel.exitPosition.x, currentLevel.exitPosition.y, exitTile.transform.position.z), Quaternion.identity);
        instance.transform.parent = this.transform;
    }

    void ShowLilith()
    {
        GameObject old = GameObject.FindGameObjectWithTag("Demon");

        Vector3 position = old.transform.position;
        Destroy(old);

        GameObject newInstance = (GameObject)Instantiate(lilith, position, Quaternion.identity);
    }

    public Level GetCurrentLevel() {
        return currentLevel;
    }

    public void SetNextLevel() {
        if (changeLevel != 0) return;
        changeLevel = 1;
    }

    IEnumerator FadeInTransition() {
        for (float f = 0; f < 1; f += 0.1f)
        {
            Color c = transition.material.color;
            c.a = f;
            transition.material.color = c;
            yield return new WaitForSeconds(0.05f);
        }

        transition.material.color = new Color(0, 0, 0, 1);
        changeLevel = 2;
    }

    IEnumerator FadeOutTransition() {
        for (float f = 1; f > 0; f -= 0.1f) {
            Color c = transition.material.color;
            c.a = f;
            transition.material.color = c;
            yield return new WaitForSeconds(0.05f);
        }

        transition.material.color = new Color(0, 0, 0, 0);
        changeLevel = 0;
    }

    void ChangeLevel() {
        if (currentLevelNum + 1 < levels.Count) {
            ClearLevel();
            LoadLevel(currentLevelNum + 1);
        }
        else {
            SceneManager.LoadScene("Credits");
        }
    }

    public void RestartLevel() {
        ClearLevel();
        currentLevel.Reset();
        player.GetComponent<Player>().steps = 0;
        isExit = false;
        isLilith = false;
        if(currentLevelNum == 12)
        {
            Destroy(GameObject.FindGameObjectWithTag("Demon"));
            Destroy(GameObject.FindGameObjectWithTag("Exit"));
        }
        LoadLevel(currentLevelNum);
        InvokeRepeating("BlinkPlayer", 0, 0.1f);
        Invoke("CancelPlayerBlinking", 1);
    }

    void BlinkPlayer()
    {
        playerRenderer.enabled = !playerRenderer.enabled;
    }

    void CancelPlayerBlinking()
    {
        CancelInvoke("BlinkPlayer");
        playerRenderer.enabled = true;
    }

    void ClearLevel() {
        player.GetComponent<Player>().Reset();
        foreach(Transform child in transform) {
            GameObject.Destroy(child.gameObject);
        }
    }

    void LoadLevel(int numLevel) {
        currentLevelNum = numLevel;
        currentLevel = levels[numLevel];
        SetPlayerPosition(currentLevel.playerPosition);
        SetExit(currentLevel.exitPosition);

        CreateFloor();
        InstantiateMap();
    }

    void SetPlayerPosition(Vector2 position) {
        player.transform.position = new Vector3(position.x, position.y, player.transform.position.z);
    }

    void SetExit(Vector2 position) {
        if (currentLevelNum == 12) return;
        GameObject instance = (GameObject)Instantiate(exitTile, new Vector3(position.x, position.y, exitTile.transform.position.z), Quaternion.identity);
        instance.transform.parent = this.transform;
    }

    void CreateFloor() {
        for (int y = 0; y < height; y++) {
            for (int x = 0; x < width; x++) {
                GameObject instance = (GameObject)Instantiate(floorTile, new Vector3(x, height - y - 1, 20), Quaternion.identity);
                instance.transform.parent = this.transform;
            }
        }
    }

    void InstantiateMap() {
        for (int y = 0; y < height; y++) {
            for (int x = 0; x < width; x++) {
                if (currentLevel.map[y, x] - 1 == -1) continue;

                GameObject tile = (GameObject)Instantiate(
                    obstacles[currentLevel.map[y, x] - 1],
                    new Vector3(x, height - y - 1, obstacles[currentLevel.map[y, x] - 1].transform.position.z),
                    Quaternion.identity);
                tile.transform.parent = this.transform;

                if(currentLevel.map[y, x] == 4) {
                    currentLevel.traps.Add(tile.GetComponent<Trap>());
                }
            }
        }
    }


}
