using UnityEngine;
using System.Collections.Generic;

public class FieldManager : MonoBehaviour
{
    //--------------FIELD---------------------//
    [SerializeField]
    GameObject owner_field;
    [SerializeField]
    GameObject[] enemys;
    [SerializeField, Range(1, 5)]
    int field_level;

    List<Enemy> enemy_list;
    int max_enemy;
    //------------EVENTMETHOD-----------------//
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
    //--------------METHOD--------------------//
    void Update_FieldEnemy()
    {
        if (enemy_list.Count < max_enemy)
            Create_Enemy();
    }
    void Create_Enemy()
    {
        int enemy_type = Random.Range(0, enemys.Length);
        GameObject clone = Instantiate(enemys[enemy_type]) as GameObject;
        clone.transform.SetParent(transform);

        Enemy enemy = clone.GetComponent<Enemy>() as Enemy;
        float fX = transform.position.x + Random.Range(- transform.localScale.x * 0.5f + 4.0f, transform.localScale.x * 0.5f - 4.0f);
        float fZ = transform.position.x + Random.Range(- transform.localScale.z * 0.5f + 4.0f, transform.localScale.z * 0.5f - 4.0f);
        enemy.Set_Pos(new Vector3(fX, .0f, fZ));
        enemy.Init(Random.Range((field_level-1)*10+1, field_level*10+1));

        enemy_list.Add(enemy);
    }
    public void Delete_Enemy(Enemy _enemy)
    {
        enemy_list.Remove(_enemy);
        Destroy(_enemy.gameObject);
    }
    //------작성자: 201202971 문지환----------//

}
