using System.Linq;
using System.Reflection;
using UnityEngine;
using UserCreation;

public class GameManager : Singleton<GameManager>
{
    PlayerData m_currentPlayer;

    void Awake()
    {
        InitializeManager();
    }

    void Start()
    {
    }

    void Update()
    {
    }


    void InitializeManager()
    {
        var singletonTypes = Assembly.GetExecutingAssembly().GetTypes().Where(t =>
            t.IsClass && !t.IsAbstract && t.BaseType != null && t.BaseType.IsGenericType &&
            t.BaseType.GetGenericTypeDefinition() == typeof(Singleton<>));

        foreach (var type in singletonTypes)
        {
            if (FindObjectOfType(type) == null)
            {
                GameObject obj = new GameObject(type.Name);
                obj.AddComponent(type);
            }
        }
    }
}