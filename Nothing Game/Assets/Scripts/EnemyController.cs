using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public NavMeshAgent _agent;
    public float EnemeyDistanceRun = 5.0f;

    public GameController GameManager;
    public Transform Player;

    public Color NormalColor;
    public Color FlashColor = Color.blue;
    public Renderer GameMesh;
    public int FlashDelay = 3;
    public int TimesToFlash = 15;

    public int TimeToWait = 5;  

    public UnityEngine.AI.NavMeshAgent agent;
    private Vector3 startPos;
    public bool IsPaused;

    void Start()
    {
        startPos = transform.position;
        _agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (IsPaused)
        {
            Invoke("UnPause", TimeToWait);
            agent.SetDestination(startPos);
        }
        else
        {
            if (!GameManager.PowerUpActive)
            {
                agent.SetDestination(Player.position);
            }
            else
            {
                Run();
            }
        }

        if (GameManager.PowerUpActive)
        {
            StartCoroutine(Flash());
        }

    }

    public void Run()
    {
        float distance = Vector3.Distance(transform.position, Player.transform.position);

        if (distance < EnemeyDistanceRun)
        {
            Vector3 dirToPlayer = transform.position - Player.transform.position;

            Vector3 newPos = transform.position + dirToPlayer;

            _agent.SetDestination(newPos);
        }
    }

public void UnPause()
    {
        IsPaused = false;
    }

    public IEnumerator Flash()
    {
        var renderer = GameMesh;
        if (renderer != null)
        {

            for (int i = 1; i <= TimesToFlash; i++)
            {
                renderer.material.color = FlashColor;
                yield return new WaitForSecondsRealtime(FlashDelay);
                renderer.material.color = NormalColor;
                yield return new WaitForSecondsRealtime(FlashDelay);
            }
        }
        GameManager.PowerUpActive = false;
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Player") && GameManager.PowerUpActive)
        {
            this.gameObject.SetActive(false);
            transform.position = startPos;
            this.gameObject.SetActive(true);
            IsPaused = true;
            GameManager.Counter(GameManager.enemyValue, "enemy");
        }
    }

}