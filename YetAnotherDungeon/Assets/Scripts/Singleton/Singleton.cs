using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                // 查找场景中是否已经存在该类型的实例
                _instance = FindObjectOfType<T>();

                if (_instance == null)
                {
                    // 如果场景中没有该类型的实例，则创建一个新的GameObject并附加此组件
                    GameObject obj = new GameObject(typeof(T).Name);
                    _instance = obj.AddComponent<T>();
                }
            }
            return _instance;
        }
    }

    protected virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = this as T;
            DontDestroyOnLoad(gameObject); // 确保对象在加载新场景时不被销毁
        }
        else
        {
            Destroy(gameObject); // 如果已存在实例，则销毁新创建的对象
        }
    }
}