using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour {
    static int _nextLevelIndex = 1;
    static int max = 2;
    
    private Enemy[] _enemies;
    private void OnEnable(){
        _enemies = FindObjectsOfType<Enemy>();

    }

    // Update is called once per frame
    void Update()
    {
        foreach(Enemy enemy in _enemies){
            if(enemy != null){
                return;
            }
        }

        Debug.Log("You killed all enemies");
        _nextLevelIndex++;
        if(_nextLevelIndex > max){
            return;
        }
        string nextLevelName = "Level" + _nextLevelIndex;
        SceneManager.LoadScene(nextLevelName);
    }
}
