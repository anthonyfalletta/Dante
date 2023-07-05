using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObject : MonoBehaviour
{
    //Rigidbody2D _playerRb;
    GameObject _playerGO;
    GameObject _attackPrefab;
    GameObject _projectilePrefab;

    //public Rigidbody2D PlayerRb {get{return _playerRb;} set{_playerRb = value;}}
    public GameObject PlayerGO {get{return _playerGO;} set{_playerGO = value;}}
    public GameObject AttackObject {get{return _attackPrefab;} set{_attackPrefab = value;}}
    public GameObject ProjectilePrefab {get{return _projectilePrefab;} set{_projectilePrefab = value;}}

    private void Awake() {
        //Setup Player Variables
        _playerGO = this.gameObject;
        //_playerRb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _projectilePrefab = (GameObject)Resources.Load("Projectile");
        _attackPrefab = (GameObject)Resources.Load("AttackSwipeField");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
