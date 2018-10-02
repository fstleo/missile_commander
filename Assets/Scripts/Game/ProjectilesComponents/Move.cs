using System;
using UnityEngine;

namespace Game.Components
{
    public class Move : MonoBehaviour
    {
        private Transform _transform;

        private Action<GameObject, int> _reachTargetCallback;

        private float _timer;
        private float _time;
        private float _speed;

        private Vector3[] _positions;        
        private Vector3 _previousPosition;        
        private Vector3 _nextPosition;
        private int _currentTargetNum;
    
        private void Awake()
        {
            _transform = transform;
        }

        public void Init(Vector3 [] positions, float speed, Action<GameObject,int> reachTargetCallback)
        {
            _timer = 0;
            _speed = speed;
            _transform.position = positions[0];
            _positions = positions;
            _reachTargetCallback = reachTargetCallback;
            InitNextPosition(1);
        }

        private void InitNextPosition(int num)
        {
            if (num == _positions.Length)
            {
                return;
            }

            _currentTargetNum = num;
            _previousPosition = _positions[num - 1];
            _nextPosition = _positions[num];
            _timer = 0;
            _time = (_previousPosition - _nextPosition).magnitude / _speed;            
            _transform.right =  _nextPosition - _previousPosition;            
        }
        
    
        private void Update()
        {
            _timer += Time.deltaTime;
            _transform.position = Vector3.Lerp(_previousPosition, _nextPosition, _timer / _time);
            if (_timer > _time)
            {
                _reachTargetCallback?.Invoke(gameObject, _currentTargetNum);
                InitNextPosition(_currentTargetNum + 1);
            }
        }
    }
}
