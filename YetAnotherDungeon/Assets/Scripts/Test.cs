using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var allies = GameObject.FindObjectsOfType<Character>().Where(x => x.IsAlly).ToList();
        var enemies = GameObject.FindObjectsOfType<Character>().Where(x => !x.IsAlly).ToList();
        CombatManager.Instance.StartBattle(allies, enemies);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
