using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathByFalling : MonoBehaviour
{
    public GameController GameManager;

    void OnTriggerEnter(Collider other) => GameManager.GameOver();
}
