using System.Collections.Generic;
using UnityEngine;

namespace BingoBlitzClone.Utils
{
    public class ObjectPool : MonoBehaviour
    {
        [SerializeField] private Transform parentTransform;
        [SerializeField] private PooledObject prefab;
        [SerializeField] private List<PooledObject> pool = new List<PooledObject>();

        public PooledObject GetPooledObject()
        {
            foreach (PooledObject pooledObject in pool)
            {
                if (!pooledObject.Busy)
                {
                    pooledObject.Busy = true;
                    pooledObject.gameObject.SetActive(true);

                    return pooledObject;
                }
            }

            PooledObject newPooledObject = Instantiate(prefab, parentTransform);

            return newPooledObject;
        }

        public void ReturnObject(PooledObject returnedObject)
        {
            returnedObject.Busy = false;
            returnedObject.gameObject.SetActive(false);
            pool.Add(returnedObject);
        }
    }

    public abstract class PooledObject : MonoBehaviour
    {
        [Header("Pooled Object")]
        public bool Busy = false;
    }
}


