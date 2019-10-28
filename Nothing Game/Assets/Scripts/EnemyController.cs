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
    public MeshRenderer GameMesh;
    public float FlashSpeed = 1f;
    public float FlashTime = 10.0f;

    public int TimeToWait = 3;

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
            StartCoroutine(FlashObject(GameMesh, NormalColor, FlashColor, FlashTime, FlashSpeed));
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

    IEnumerator FlashObject(MeshRenderer toFlash, Color originalColor, Color flashColor, float flashTime, float flashSpeed, float flashingFor = 0)
    {
        // Very Helpfull: https://answers.unity.com/questions/1367570/how-to-make-enemies-flash-on-hit.html

        Color newColor = flashColor;
        Debug.Log("flashTime: " + flashTime);
        while (flashingFor < flashTime)
        {
            
            toFlash.material.color = newColor;
            flashingFor += Time.deltaTime;
            yield return new WaitForSeconds(flashSpeed);
            flashingFor += flashSpeed;
            Debug.Log("flashingFor: " + flashingFor);
            if (newColor == flashColor)
            {
                newColor = originalColor;
            }
            else
            {
                newColor = flashColor;
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