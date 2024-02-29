using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        Normal,
        Combat
    }

    public static GameManager Instance { get; private set; }
    public GameState CurrentState { get; private set; }
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // 初始状态设为Normal
        ChangeState(GameState.Normal);
    }

    public void ChangeState(GameState newState)
    {
        CurrentState = newState;
        switch (newState)
        {
            case GameState.Normal:
                EnterNormalState();
                break;
            case GameState.Combat:
                EnterCombatState();
                break;
        }
    }

    private void EnterNormalState()
    {
        Debug.Log("Entered Normal State");
    }

    private void EnterCombatState()
    {
        Debug.Log("Entered Combat State");
    }
    
    private void Update()
    {
        switch (CurrentState)
        {
            case GameState.Normal:
                break;
            case GameState.Combat:
                break;
        }
    }
}