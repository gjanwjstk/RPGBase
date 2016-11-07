using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class FieldManager : MonoBehaviour
{
    [SerializeField]
    GameObject owner_field;
    [SerializeField]
    GameObject[] enemys;
    [SerializeField, Range(1, 5)]
    int field_level;

    List<Enemy> enemy_list;
    int max_enemy;
    void Awake()
    {
        enemy_list = new List<Enemy>();
        max_enemy = 10;

        for (int i = 0; i < 5; i++)
        {
            Create_Enemy();
        }
        InvokeRepeating("Update_FieldEnemy", 1.0f, 5.0f);
    }
    void Update_FieldEnemy()
    {
        if (enemy_list.Count < max_enemy)
            Create_Enemy();
    }
    void Create_Enemy()
    {
        int enemy_type = Random.Range(0, enemy)
    }
    public void Delete_Enemy(Enemy _enemy)
    {

    }
}
