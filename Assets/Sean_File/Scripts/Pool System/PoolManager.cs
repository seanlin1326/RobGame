using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Sean
{
    public class PoolManager : MonoBehaviour
    {
        [SerializeField] Pool[] playerProjectilePools;

      static Dictionary<GameObject, Pool> dictionary;
        // Start is called before the first frame update
        void Start()
        {
            dictionary = new Dictionary<GameObject, Pool>();
            Initialize(playerProjectilePools);
        }
        private void OnDestroy()
        {
         #if UNITY_EDITOR
            CheckPoolSize(playerProjectilePools);
         #endif
        }
        void CheckPoolSize(Pool[] _pools)
        {
            foreach (var _pool in _pools)
            {
                if(_pool.RuntimeSize > _pool.Size)
                {
                    Debug.LogWarning(
                         string.Format("Pool:{0} has a runtime size {1} bigger than its initial size {2}!",
                         _pool.Prefab.name,
                         _pool.RuntimeSize,
                         _pool.Size));
                }
            }
        }
        void Initialize(Pool[] _pools)
        {
            foreach (var _pool in  _pools)
            {
#if UNITY_EDITOR
                if (dictionary.ContainsKey(_pool.Prefab))
                {
                    Debug.LogError("Same prefab in multiple pool! "+_pool.Prefab.name);
                    continue;
                }
#endif
                dictionary.Add(_pool.Prefab, _pool);
             Transform _poolParent = new GameObject("Pool: " + _pool.Prefab.name).transform;
                _poolParent.parent = transform;
                _pool.Initialize(_poolParent);
            }
        }
#region -- Release ���䭫�� --
        /// <summary>
        /// �ھڶǤJ�� <paramref name="_prefab"/>�ѼơA��^��H�����������C����H
        /// </summary>
        /// <param name="_prefab"></param>
        /// <returns></returns>
        public static GameObject Release(GameObject _prefab)
        {
            if (!dictionary.ContainsKey(_prefab))
            {
                Debug.LogError("Pool Manager could not find prefab :" + _prefab.name);
                return null;
            }

            return dictionary[_prefab].PreparedObject();

        }
        /// <summary>
        /// �ھڶǤJ�� <paramref name="_prefab"/>�ѼơA��^��H�����������C����H
        /// </summary>
        /// <param name="_prefab"><para>���w���C����H�w�s��</para>
        /// </param>
        /// <param name="_position"><para>���w���C����H����m</para>
        /// </param>
        /// <returns></returns>
        public static GameObject Release(GameObject _prefab,Vector3 _position)
        {
            if (!dictionary.ContainsKey(_prefab))
            {
                Debug.LogError("Pool Manager could not find prefab :" + _prefab.name);
                return null;
            }

            return dictionary[_prefab].PreparedObject(_position);

        }
        /// <summary>
        /// �ھڶǤJ�� <paramref name="_prefab"/>�ѼơA��^��H�����������C����H
        /// </summary>
        /// <param name="_prefab"><para>���w���C����H�w�s��</para></param>
        /// <param name="_position"><para>���w���C����H����m</para></param>
        /// <param name="_rotation"><para>���w���C����H�����ਤ��</para></param>
        /// <returns></returns>
        public static GameObject Release(GameObject _prefab, Vector3 _position,Quaternion _rotation)
        {
            if (!dictionary.ContainsKey(_prefab))
            {
                Debug.LogError("Pool Manager could not find prefab :" + _prefab.name);
                return null;
            }

            return dictionary[_prefab].PreparedObject(_position,_rotation);

        }
        /// <summary>
        ///�ھڶǤJ�� <paramref name="_prefab"/>�ѼơA��^��H�����������C����H
        /// </summary>
        /// <param name="_prefab"><para>���w���C����H�w�s��</para></param>
        /// <param name="_position"><para>���w���C����H����m</para></param>
        /// <param name="_rotation"><para>���w���C����H�����ਤ��</para></param>
        /// <param name="_localScale"><para>���w���C����H���Y��j�p</para></param>
        /// <returns></returns>
        public static GameObject Release(GameObject _prefab, Vector3 _position, Quaternion _rotation,Vector3 _localScale)
        {
            if (!dictionary.ContainsKey(_prefab))
            {
                Debug.LogError("Pool Manager could not find prefab :" + _prefab.name);
                return null;
            }
           
            return dictionary[_prefab].PreparedObject(_position, _rotation,_localScale);
            
        }
#endregion
    }
}