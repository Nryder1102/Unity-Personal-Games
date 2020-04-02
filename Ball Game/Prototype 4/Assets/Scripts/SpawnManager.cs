using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemyPrefab;
    public GameObject obstaclePrefab;
    private float spawnRange = 9.0f;

    public int enemyCount;

    public int waveNumber = 1;

    public GameObject powerupPrefab;

    public TextMeshProUGUI waveCount;
    public bool isGameActive = true;
    public int player;
    public GameObject playerObj;
    public TextMeshProUGUI gameOver;
    public TextMeshProUGUI pauseGame;
    public Button restartGame;
    public Button resumeGame;
    public Button quitGame;
    private int obstacleNum;
    public GameObject island;
    private Vector3 islandscale;
    private int randObstacleNum = 3;
    public GameObject mainCamera;
    private Vector3 cameraMove;
    public GameObject focalPoint;
    private Vector3 focalMove;
    public SimpleHealthBar bossBar;
    int islandCount = 24;
    public int maxHealth;
    public int currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        focalPoint = GameObject.Find("Focal Point");
        island = GameObject.Find("Island");
        mainCamera = GameObject.Find("Main Camera");
        playerObj = GameObject.Find("Player");
        isGameActive = true;
        if (isGameActive)
        {
        SpawnEnemyWave(waveNumber);
        Instantiate(powerupPrefab, GenerateSpawnPosition(), powerupPrefab.transform.rotation);
        }
    }

    
    
    
    // Update is called once per frame
    void Update()
    {
        
        player = GameObject.FindGameObjectsWithTag("Player").Length;
        if (transform.position.y < -10)
        {
           Destroy(gameObject);

        } 
        if (player == 0 && isGameActive)
        {
            isGameActive = false;
        }
        if (isGameActive){
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if (enemyCount == 0 /*&& waveNumber != 10*/)
        {
            GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
            foreach(GameObject obstacle in obstacles)
            GameObject.Destroy(obstacle);
            waveNumber++;
            SpawnEnemyWave(waveNumber);
            Instantiate(powerupPrefab, GenerateSpawnPosition(), powerupPrefab.transform.rotation);
            waveCount.SetText("Wave: " + waveNumber);
            if(waveNumber >= 4 && islandCount != 27)
            {
            islandCount++;
            islandscale = new Vector3(0.5f,0,0.5f);
            island.transform.localScale += islandscale;
            spawnRange += 0.5f;
            /* cameraMove = new Vector3(0, 1f, 0);
            mainCamera.transform.position += cameraMove;
            focalMove = new Vector3(0,0,-1f);
            focalPoint.transform.position += focalMove; */
            }
            if (waveNumber > 4)
            {
                randObstacleNum = Random.Range(3,6);
            }
        }
        if (waveNumber == 10)
        {
            bossBar.gameObject.SetActive(true);

            bossOne();
            
        }
        }
       if (isGameActive == false)
       {
           GameOver();
       }
       if (Input.GetKeyDown(KeyCode.Escape))
       {
           Time.timeScale = 0;
           resumeGame.gameObject.SetActive(true);
           pauseGame.gameObject.SetActive(true);
           quitGame.gameObject.SetActive(true);

       }
       
    }

    private Vector3 GenerateSpawnPosition() 
    { 
        float playerPosX = Mathf.Round(playerObj.transform.position.x);
        float playerPosZ = Mathf.Round(playerObj.transform.position.z);
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        float roundSpawnPosX = Mathf.Round(spawnPosX);
        float roundSpawnPosZ = Mathf.Round(spawnPosZ);
        if ((roundSpawnPosX == playerPosX || roundSpawnPosX <= playerPosX + 3 && roundSpawnPosX >= playerPosX -3) && (roundSpawnPosZ == playerPosZ || roundSpawnPosZ <= playerPosZ + 3 && roundSpawnPosZ >= playerPosZ - 3))
        {
            Debug.Log("Test");
            spawnPosZ += 2;
            spawnPosX += 2;

        }
        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);
        
        return randomPos;
        
    }
    void SpawnEnemyWave(int enemiesToSpawn)
    {
        
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            int index = Random.Range(0, enemyPrefab.Length-1);
            if (waveNumber >= 4)
            {
            index = Random.Range(0, enemyPrefab.Length);
            }
            Instantiate(enemyPrefab[index], GenerateSpawnPosition(), enemyPrefab[index].transform.rotation);
        }
        obstacleNum = Random.Range(0,randObstacleNum);
        
        for (int i = 0; i < obstacleNum; i++)
        {
           
            Instantiate(obstaclePrefab, GenerateSpawnPosition(), obstaclePrefab.transform.rotation);
        }
    }

    void GameOver()
    {
        gameOver.gameObject.SetActive(true);
        restartGame.gameObject.SetActive(true); 
        quitGame.gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        resumeGame.gameObject.SetActive(false);
        pauseGame.gameObject.SetActive(false);
        quitGame.gameObject.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    void bossOne()
    {
        maxHealth=3;
        currentHealth=3;
        currentHealth -= 1;
        bossBar.UpdateBar(currentHealth, maxHealth);
    }

}
