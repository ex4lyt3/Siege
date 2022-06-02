
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public Light sun;
    public Text time;
    public Text dayText;
    public Text fpsCounter;
    public Text dialogueCounter;
    public Text playerHP;
    public Text enemyRemainingCounter;

    public GameObject player;

    public GameObject sphereSoldier;
    public GameObject coneSoldier;

    public Transform playerSpawn;

    public Transform spawnOne;
    public Transform spawnTwo;
    public Transform spawnThree;

    int enemyRemaining = 0;

    int day = 1;
    int timeOfDay = 12;

    float timer = 0f;
    float hourDuration = 8f;
    bool timerReached;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        fpsCounter.text = (Mathf.Floor(1 / Time.deltaTime)).ToString() + "FPS";

        if (timer >= hourDuration)
        {
            DayNight();
            timerReached = true;

        }
        if (timerReached == false)
        {
            timer += Time.deltaTime;
        } else if (timerReached == true)
        {
            timer = 0f;
            timerReached = false;
        }

    }

    void DayNight()
    {
        timeOfDay += 1;
        sun.transform.rotation = Quaternion.Euler((timeOfDay*180)/12 - 90, 0, 0);
        time.text = timeOfDay.ToString() + "00";

        if (timeOfDay == 18)
        {
            dialogueCounter.text = "Grandmaster: Enemies arriving! Prepare yourself!";
        }

        if (timeOfDay == 19)
        {
            hourDuration = 21;
            dialogueCounter.text = "Grandmaster: Enemies have arrived! Defend the Core!";
            LevelStart(day);
        }
        if (timeOfDay == 6)
        {
            hourDuration = 8;
        }

        if (timeOfDay == 23)
        {
            timeOfDay = -1;
            day += 1;
            dayText.text = day.ToString();
        }
    }

    void SpawnEnemy (int number, string type)
    {
        
        Transform[] spawns = { spawnOne, spawnTwo, spawnThree };
        GameObject EnemyToSpawn = sphereSoldier;

        switch (type)
        {
            case "SphereSoldier":
                EnemyToSpawn = sphereSoldier;
                break;

            case "ConeSoldier":
                EnemyToSpawn = coneSoldier;
                break;
        }
        for (int i = 0;i < number; i++)
        {
            float spawnNumber = Random.Range(0f, 3f);
            Instantiate(EnemyToSpawn, spawns[(int)spawnNumber]);
        }

    }

    void Tutorial()
    {
        
    }

    void LevelStart(int level)
    {
        switch (level)
        {
            case 1:
                SpawnEnemy(7, "SphereSoldier");
                AddEnemyUI(7);
                break;

            case 2:
                SpawnEnemy(10, "SphereSoldier");
                AddEnemyUI(10);
                break;

            case 3:
                SpawnEnemy(5, "SphereSoldier");
                SpawnEnemy(3, "ConeSoldier");
                AddEnemyUI(8);
                break;

            case 4:
                SpawnEnemy(6, "SphereSoldier");
                SpawnEnemy(5, "ConeSoldier");
                AddEnemyUI(11);
                break;

            case 5:
                SpawnEnemy(6, "SphereSoldier");
                SpawnEnemy(7, "ConeSoldier");
                AddEnemyUI(13);
                break;


        }
    }
    void LevelEnd()
    {

    }

    public void GameDefeat()
    {

    }
    
    public void GameVictory()
    {

    }

    public void Respawn()
    {
        playerHP.text = "100";
        player.transform.position = playerSpawn.position;
    }

    public void AddEnemyUI(int number)
    {
        enemyRemaining += number;
        enemyRemainingCounter.text = enemyRemaining.ToString();
    }

}
