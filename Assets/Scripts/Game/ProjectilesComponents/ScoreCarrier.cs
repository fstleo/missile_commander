using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCarrier : MonoBehaviour
{
    [SerializeField]
    private int _score;

    public int Score => _score;

    private void Awake()
    {
        GetComponent<IDestroyable>().OnDestroy += ProcessDestroy;
    }

    private void ProcessDestroy(Owner owner)
    {
        if (owner == Owner.Player)
        {
            GameScore.AddScore(Score);
        }
    }
}
