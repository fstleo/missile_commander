using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityFire : MonoBehaviour
{

    [SerializeField]
    private GameObject _fireEffectParent;

    private void Awake()
    {
        GetComponent<IDestroyable>().OnDestroy += (owner) => ShowEffect();      
        GameManager.OnLevelChange += Reset;
    }

    private void Reset(int level)
    {
        if (level == 0)
        {
            HideEffect();
        }
    }

    public void HideEffect()
    {
        GetComponent<BoxCollider2D>().enabled = true;
        _fireEffectParent.SetActive(false);
    }

    public void ShowEffect()
    {
        GetComponent<BoxCollider2D>().enabled = false;
        _fireEffectParent.SetActive(true);
        foreach (Animator anim in _fireEffectParent.GetComponentsInChildren<Animator>())
        {
            anim.SetFloat("Speed", Random.Range(1f,3f));
        }
    }
    
    
}
