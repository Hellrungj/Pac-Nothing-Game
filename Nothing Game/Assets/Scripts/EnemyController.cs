using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
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
            agent.SetDestination(Player.position);
        }

        if (GameManager.PowerUpActive)
        {
            StartCoroutine(Flash());
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
        }
    }

}