using UnityEngine;

public class Player_AttackRange : MonoBehaviour
{
    //--------------FIELD---------------------//
    [SerializeField]
    Player owner;
    //------------EVENTMETHOD-----------------//
    void OnTriggerEnter(Collider col)
    {
        if (!col.name.Contains("Enemy")) return;
        if (owner._target == null) return;
        if (col.GetComponent<Entity>() != owner._target) return;

        owner.Base_Attack();
    }
    //--------------METHOD--------------------//
    //------작성자: 201202971 문지환----------//
}
