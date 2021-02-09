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

    public int waveNumber = 0;

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
    public GameObject boss1;
    public int bossActive;
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

        

        //Gets enemy count if the game is active
        if (isGameActive)
        {
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        bossActive = GameObject.FindGameObjectsWithTag("Boss").Length;
        


            if (enemyCount == 0)
            {
                waveNumber++;
                waveCount.SetText("Wave: " + waveNumber);
            }

            //Controls most of the games functions, including powerup spawns, enemy spawns, and progressing waves
            if (enemyCount == 0 && waveNumber != 5)
        {
            
            Debug.Log(enemyCount);
            Debug.Log(waveNumber);

            //Destroys obstacles so new ones can spawn in a new, random place each wave (including 5, which is unwanted...)
            GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
            foreach(GameObject obstacle in obstacles)
            GameObject.Destroy(obstacle);
            
            //Spawns a new wave, including setting the wave number text, spawning a powerup (need to put this on wave 5 as well, because it's integral to defeating the boss), and enemies
            SpawnEnemyWave(waveNumber);
            Instantiate(powerupPrefab, GenerateSpawnPosition(), powerupPrefab.transform.rotation);
            
            

            //This increases the play area size on waves 4+, but not too big yet as I can't seem to get the camera to move correctly with the size increase
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
            
        }

        //Spawns Boss #1 (Who currently is useless)
        //Make a function to check for boss tag, and then one so when it no longer can find the boss tag add wave
        if (waveNumber == 5)
        {
            bossBar.gameObject.SetActive(true);
            GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
            foreach(GameObject obstacle in obstacles)
            GameObject.Destroy(obstacle);
            bossOne();
            
        }
        }

        //Standard gameover
       if (isGameActive == false)
       {
           GameOver();
       }

       //Pauses game (Don't pause for too long, apparently Unity likes momentum)
       if (Input.GetKeyDown(KeyCode.Escape))
       {
           Time.timeScale = 0;
           resumeGame.gameObject.SetActive(true);
           pauseGame.gameObject.SetActive(true);
           quitGame.gameObject.SetActive(true);

       }
       
    }

    //This does not work however much I try to get it to, it's to make sure enemies can't spawn inside the player, but the enemies don't like listening to me
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

    //Spawns enemies according to wave number, and adds in new enemy variants on and above wave 4 (Should probably raise the number higher with how annoying Bouncers are)
    void SpawnEnemyWave(int enemiesToSpawn)
    {
        
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            int index = Random.Range(0, enemyPrefab.Length-1);
            //Adds in new type of enemy
            if (waveNumber >= 6)
            {
            index = Random.Range(0, enemyPrefab.Length);
            }
            Instantiate(enemyPrefab[index], GenerateSpawnPosition(), enemyPrefab[index].transform.rotation);
        }
        //Spawns random number of Obstacles
        obstacleNum = Random.Range(0,randObstacleNum);
        
        for (int i = 0; i < obstacleNum; i++)
        {
           
            Instantiate(obstaclePrefab, GenerateSpawnPosition(), obstaclePrefab.transform.rotation);
        }
    }

    //Standard gameover menu
    void GameOver()
    {
        gameOver.gameObject.SetActive(true);
        restartGame.gameObject.SetActive(true); 
        quitGame.gameObject.SetActive(true);
    }

    //Restarts the game
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //Resumes from pause
    public void ResumeGame()
    {
        Time.timeScale = 1;
        resumeGame.gameObject.SetActive(false);
        pauseGame.gameObject.SetActive(false);
        quitGame.gameObject.SetActive(false);
    }

    //Quits the game, but why would you ever want to do that?
    public void QuitGame()
    {
        Application.Quit();
    }

    //The function that actually spawns Boss #1, and he's still useless for now
    void bossOne()
    {
        maxHealth=3;
        currentHealth=3;
        currentHealth -= 1;
        bossBar.UpdateBar(currentHealth, maxHealth);
        boss1.gameObject.SetActive(true);
    }

}
