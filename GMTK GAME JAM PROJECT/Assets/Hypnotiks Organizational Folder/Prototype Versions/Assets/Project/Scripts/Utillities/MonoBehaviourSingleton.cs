using UnityEngine;

public class MonoBehaviourSingleton<T> : MonoBehaviour where T : Component
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                //Debug.Log(typeof(T) + " Instance is null, trying to find a object of the type " + typeof(T));
                _instance = FindObjectOfType<T>();

                if (_instance == null)
                {
                    //Debug.Log(typeof(T) + " Instance is null, trying to create a new object to replace it");

                    GameObject newObj = new GameObject("Auto-Generated " + typeof(T));
                    _instance = newObj.AddComponent<T>();
                }
            }

            return _instance;
        }
    }
}
