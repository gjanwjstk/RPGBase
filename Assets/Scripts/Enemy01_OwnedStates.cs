using System;
using UnityEngine;
using System.Collections;

namespace Enemy01.States
{
    /*
     * Class : Wander
     * Desc
     *  : 끊임없이 임이의 목표지점을 정해 배회하는 행동 정의
     */
    public class Wander : State<Enemy>
    {
        static readonly Wander instance = new Wander();
        public static Wander Instance
        {
            get { return instance; }
        }
        public override void Enter(Enemy entity)
        {
            UpdateWander(entity);
        }
        public override void Execute(Enemy entity)
        {
            Vector3 move_pos = Vector3.Normalize(entity.Wander_Target_Pos - entity.Get_Pos());
            entity.Add_Pos(move_pos * entity.move_speed * Time.deltaTime);
            Debug.DrawLine(entity.Get_Pos(), entity.Wander_Target_Pos);

            if (Vector3.Distance(entity.Wander_Target_Pos, entity.Get_Pos()) < 0.02f)
            {
                UpdateWander(entity);
            }
        }
        public override void Exit(Enemy entity)
        {
        }
        void UpdateWander(Enemy entity)
        {
            float wander_radius = 3.0f;
            int wander_jitter = 0;
            int wander_jitter_min = 0;
            int wander_jitter_max = 360;

            Vector3 parent_pos = entity.transform.parent.transform.position;
            Vector3 parent_scale = entity.transform.parent.transform.localScale;

            wander_jitter = UnityEngine.Random.Range(wander_jitter_min, wander_jitter_max);
            Vector3 target_pos = entity.Get_Pos() + Set_Angle(wander_radius, wander_jitter);

            target_pos.x = Mathf.Clamp(target_pos.x,
                parent_pos.x - parent_scale.x * 0.5f + 4.0f, parent_pos.x + parent_scale.x * 0.5f - 4.0f);
            target_pos.y = .0f;
            target_pos.z = Mathf.Clamp(target_pos.z,
               parent_pos.z - parent_scale.x * 0.5f + 4.0f, parent_pos.z + parent_scale.x * 0.5f + 4.0f);

            entity.Wander_Target_Pos = target_pos;
            entity.Anim.Play("Walk");
            entity.transform.localRotation = Quaternion.LookRotation(entity.Wander_Target_Pos - entity.Get_Pos());
        }
        Vector3 Set_Angle(float radius, int angle)
        {
            Vector3 pos = Vector3.zero;
            pos.x = Mathf.Cos(angle) * radius;
            pos.z = Mathf.Sin(angle) * radius;

            return pos;
        }
    }
    /*
     * Class : Hit
     * Desc
     *  : 적에게 맞았을 때의 행동 정의
     */
    public class Hit : State<Enemy>
    {
        static readonly Hit instance = new Hit();
        public static Hit Instance
        {
            get { return instance; }
        }
        public override void Enter(Enemy entity)
        {
            entity.Anim.Play(null);
            entity.Anim.Play("Hit_Front");

            entity.HP -= entity._target.Physics_Damage - entity.Physics_Defense;
            if (entity.HP <= 0)
            {
                entity.ChangeState(Die.Instance);
            }
        }
        public override void Execute(Enemy entity)
        {
            if (entity.Anim.GetCurrentAnimatorStateInfo(0).IsName("Idle_Base"))
                entity.RevertToPreviousState();
        }
        public override void Exit(Enemy entity)
        {
        }
    }
    /*
   * Class : Die
   * Desc
   *  : 사애의 소유주(entity)가 죽었을 때의 행동 정의
   */
    public class Die : State<Enemy>
    {
        Renderer[] renders;
        Color[] colors;

        static readonly Die instance = new Die();
        public static Die Instance
        {
            get { return instance; }
        }
        public override void Enter(Enemy entity)
        {
            entity.Anim.Play("Die");

            entity._target.GetComponent<Player>().Exp += entity.Level * 30;
            entity.Set_TargetMarkOff();

            renders = entity.GetComponentsInChildren<Renderer>();
            colors = new Color[renders.Length];
            for (int i = 0; i < renders.Length; ++i)
            {
                colors[i] = renders[i].material.color;
                renders[i].material.shader = Shader.Find("Legacy Shaders/Transparent/VertexLit");
            }
        }
        public override void Execute(Enemy entity)
        {
            for (int i = 0; i < renders.Length; ++i)
            {
                if (colors[i].a > .0f)
                {
                    colors[i].a -= Time.deltaTime;
                    renders[i].material.color = colors[i];
                }
                else
                {
                    entity.transform.parent.GetComponent<FieldManager>().Delete_Enemy(entity);
                    return;
                }
            }
        }
        public override void Exit(Enemy entity)
        {
        }
    }
}