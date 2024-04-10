using System.Linq;
using System.Reflection;
using Unity.Collections;
using UnityEngine;
using UserCreation;

public class GameManager : Singleton<GameManager>
{
    [ReadOnly, SerializeField]
    PlayerData m_currentPlayer;

    protected override void Awake()
    {
        base.Awake();
        InitializeManager();
        
        EventBetter.Listen(this, (OnBeginNewGame newGame) =>
        {
            m_currentPlayer = newGame.PlayerData;
        });
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