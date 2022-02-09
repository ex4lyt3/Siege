
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{

    public GameManager GameManager;

    public float lookRadius = 10f;
    public float damageRange = 3f;
    public float damage = 1f;
    public GameObject HPObject;
    public Text HP;

    public GameObject coreBox;
    public GameObject target;
    public GameObject door;
    public NavMeshAgent agent;

    bool inHouse = false;

    // Start is called before the first frame update
    void Start()
    {
       target = GameObject.FindGameObjectWithTag("Player");
       coreBox = GameObject.FindGameObjectWithTag("Core");
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        HPObject = GameObject.Find("HP");
        HP = HPObject.GetComponent<Text>();

    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.transform.position, transform.position);

        if (distance <= lookRadius)
        {
            Attacking();
            if (distance <= damageRange)
            {
                DealDamage(damage * Time.deltaTime);
            }
        }
        else
        {
            Patrolling();
        }
 
    }

    void Patrolling()
    {
            agent.SetDestination(coreBox.transform.position);
    }

    void Attacking()
    {
        agent.SetDestination(target.transform.position);
    }

    public void DealDamage(float arg)
    {
        float healthRemaining = float.Parse(HP.text) - arg;
        healthRemaining = Mathf.Floor(healthRemaining);
        HP.text = healthRemaining.ToString();

        if (healthRemaining <= 0)
        {
            GameManager.Respawn();
        }
    }
}
