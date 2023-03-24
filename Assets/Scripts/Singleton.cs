using System;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Utilities
{
    public class Singleton<T> : SerializedMonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;

        private static object _lock = new object();

        public static T Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        //Find instances and delete all except the first one.
                        var found = FindFirstObjectByType<T>();

                        _instance = found;

                        var objects = FindObjectsByType<T>(FindObjectsInactive.Include, FindObjectsSortMode.None);
                        if (objects.Length > 1)
                        {
                            for (int i = 1; i < objects.Length; i++)
                            {
                                Destroy(objects[i]);
                            }
                            
                            return _instance;
                        }

                        if (_instance == null)
                        {
                            GameObject singleton = new GameObject();
                            _instance = singleton.AddComponent<T>();
                            singleton.name = "(singleton) " + typeof(T);
                        }
                    }

                    return _instance;
                }
            }
        }

        private void Start()
        {
            SceneManager.sceneLoaded += (arg0, mode) =>
            {
                if (Instance)
                {
                    var objects = FindObjectsByType<T>(FindObjectsInactive.Include, FindObjectsSortMode.None)
                        .Where(x => x != Instance).ToArray();
                    foreach (var t in objects)
                    {
                        Destroy(t.gameObject);
                    }
                }
                else _instance = Instance;
            };
        }
    }
}