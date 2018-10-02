using Common.Sound;
using UnityEngine;    

public class Explosion : MonoBehaviour
{   
    [SerializeField]
    private float _maxSize;
    [SerializeField]
    private float _lifeTime;
    [SerializeField]
    private float _startSize = 0.3f;

    public float LifeTime => _lifeTime;
    public float LifeTimeLeft => _back ? _lifeTime - _timer : _timer;

    private float _timer = 0;
    
    private bool _back = false;

    private CircleCollider2D _collider;

    private Owner _owner;

    private void Awake()
    {
        _collider = GetComponent<CircleCollider2D>();
        GameManager.OnLevelChange += OnLevelFinish;
        SoundPlayer.Play("explosion");
    }

    public void SetOwner(Owner damageSource)
    {
        _owner = damageSource;
    }

    private void OnLevelFinish(int nextLevel)
    {
        _collider.enabled = false;
    }

    private void Update()
    {        
        if (!_back)
        {
            _timer += Time.deltaTime;
        }
        else
        {
            _timer -= Time.deltaTime;
            
        }
        
        _collider.radius = Mathf.Lerp(_startSize, _maxSize, _timer/(_lifeTime/2)); 
        if (_timer > _lifeTime/2)
        {
            _back = true;
        }

        if (_back && _timer < 0)
        {
            GameManager.OnLevelChange -= OnLevelFinish;
            Destroy(gameObject);         
        }
    }

    protected void OnTriggerEnter2D(Collider2D other)
    {
        other.GetComponent<IDestroyable>()?.Destroy(_owner);        
    }
}
