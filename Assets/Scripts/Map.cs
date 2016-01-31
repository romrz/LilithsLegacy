using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Map : MonoBehaviour {

    public GameObject[] obstacles;
    public GameObject floorTile;
    public GameObject exitTile;
    public Renderer transition;

    public GameObject player;

    public float transitionTime = 1.0f;

    private int width = 10;
    private int height = 10;

    private List<Level> levels;
    private Level currentLevel;
    private int currentLevelNum;

    private int changeLevel = 0;

    struct Level {
        public int[,] map;
        public Vector2 playerPosition;
        public Vector2 exitPosition;

        public Level(int[,] _map, Vector2 _playerPosition, Vector2 _exitPosition) {
            map = _map;
            playerPosition = _playerPosition;
            exitPosition = _exitPosition;
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
        {1, 1, 1, 0, 1, 4, 0, 0, 0, 1},
        {1, 1, 1, 0, 1, 4, 3, 0, 0, 1},
        {1, 1, 1, 0, 1, 4, 4, 4, 0, 1},
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
        {1, 0, 0, 0, 0, 0, 0, 0, 3, 1},
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
        {1, 0, 0, 0, 0, 0, 0, 0, 0, 1},
        {1, 0, 0, 0, 0, 0, 0, 0, 0, 1},
        {1, 0, 0, 0, 0, 0, 0, 0, 0, 1},
        {1, 0, 0, 0, 0, 0, 0, 0, 0, 1},
        {1, 0, 0, 0, 0, 0, 0, 0, 0, 1},
        {1, 0, 0, 0, 0, 0, 0, 0, 0, 1},
        {1, 0, 0, 0, 0, 0, 0, 0, 0, 1},
        {1, 0, 0, 0, 0, 0, 0, 0, 0, 1},
        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
    };
    private Vector2 position5 = new Vector2(1, 8);
    private Vector2 exit5 = new Vector2(8, 1);


    void Start () {
        transition.material.color = new Color(0, 0, 0, 0);

        levels = new List<Level>();

        levels.Add(new Level(map1, position1, exit1));
        levels.Add(new Level(map2, position2, exit2));
        levels.Add(new Level(map3, position3, exit3));
        levels.Add(new Level(map4, position4, exit4));
        levels.Add(new Level(map5, position5, exit5));

        LoadLevel(0);
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
    }

    void ClearLevel() {
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
        GameObject instance = (GameObject)Instantiate(exitTile, new Vector3(position.x, position.y, exitTile.transform.position.z), Quaternion.identity);
        instance.transform.parent = this.transform;
    }


    void CreateFloor() {
        for (int y = 0; y < height; y++) {
            for (int x = 0; x < width; x++) {
                GameObject instance = (GameObject)Instantiate(floorTile, new Vector3(x, height-y-1, 20), Quaternion.identity);
                instance.transform.parent = this.transform;
            }
        }
    }

    void InstantiateMap() {
        for (int y = 0; y < height; y++) {
            for (int x = 0; x < width; x++) {
                if (currentLevel.map[y, x]-1 == -1) continue;

                GameObject tile = (GameObject)Instantiate(
                    obstacles[currentLevel.map[y, x] - 1],
                    new Vector3(x, height-y-1, obstacles[currentLevel.map[y, x]-1].transform.position.z),
                    Quaternion.identity);
                tile.transform.parent = this.transform;
            }
        }
    }

}
