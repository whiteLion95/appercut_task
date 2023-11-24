using UnityEngine;

namespace PajamaNinja.Common
{
    /// <summary>
    /// Unity Singleton template
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        [SerializeField] private bool _dontDestroyOnLoad;

        protected static T instance;

        /// <summary>
        /// Returns the instance of this singleton. 
        /// </summary>
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = (T)FindObjectOfType(typeof(T));

                    if (instance == null)
                    {
                        Debug.LogError("An instance of " + typeof(T) +
                            " is needed in the scene, but there is none.");
                    }
                }
                return instance;
            }
        }

        protected virtual void Awake()
        {
            if (Instance != null)
            {
                if (Instance != this)
                {
                    // If instance exists, destroy this gameobject
                    Debug.LogWarning(gameObject.name + " Has been destroyed. Another instance of " + typeof(T) + " already exists");
                    DestroyImmediate(gameObject);
                }
                else if (_dontDestroyOnLoad)
                {
                    DontDestroyOnLoad(gameObject);
                }
            }
        }
    }
}