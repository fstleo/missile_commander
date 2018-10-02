using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SilosInfo : MonoBehaviour
{

    [SerializeField] private PlayerSpawner _rocketSilo;

    [SerializeField] private TextMeshProUGUI _countLabel;

    private void Awake()
    {
        _rocketSilo.OnCapacityChange += UpdateCount;
    }

    private void UpdateCount(int count)
    {
        _countLabel.text = count.ToString();
    }
}
