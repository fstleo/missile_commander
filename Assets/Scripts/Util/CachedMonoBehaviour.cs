using UnityEngine;

namespace Util
{
    public abstract class CachedMonoBehaviour : MonoBehaviour
    {

        public Transform CachedTransform { get; private set; }
        public GameObject CachedGameObject { get; private set; }

        private void Awake()
        {
            CachedTransform = transform;
            CachedGameObject = gameObject;
            Init();
        }

        protected virtual void Init() {}


    }
}

