using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
            entity._target = null;
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
                entity.ChangeState(Wander.Instance);
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
    /*
     * Class : GlobalState
     * Desc
     *  : 전역 상태로 현재 진행중인 상태와 별개로 지속적으로 검사를 수행하는 상태
     */
     public class GlobalState : State<Enemy>
    {
        static readonly GlobalState instance = new GlobalState();
        public static GlobalState Instance
        {
            get { return instance; }
        }
        public override void Enter(Enemy entity)
        {
        }
        public override void Execute(Enemy entity)
        {
            if (entity._target == null)
            {
                //현재는 Target(플레이어)이 한명이지만 여러명일때는 다수를 불러와서
                Entity targets = GameObject.Find("PC_01").GetComponent<Entity>();

                //반복문을 이용해서 검사
                float dis = Vector3.Distance(targets.Get_Pos(), entity.Get_Pos());

                if (dis <= 4.0f)
                {
                    entity._target = targets;
                    entity.ChangeState(Pursuit.Instance);
                }
            }
        }
        public override void Exit(Enemy entity)
        {
        }
    } 
    /*
     * Class : Pursuit
     * Desc
     * :상태의 소유주(entity)가 어떤 대상(Target)을 쫓아갈 때 행동 정의
     */
     public class Pursuit : State<Enemy>
    {
        static readonly Pursuit instance = new Pursuit();
        public static Pursuit Instance
        {
            get { return instance; }
        }
        public override void Enter(Enemy entity)
        {
            entity.move_speed = 2.0f;
            entity.Anim.Play("Run");
        }
        public override void Execute(Enemy entity)
        {
            float dis = Vector3.Distance(entity._target.Get_Pos(), entity.Get_Pos());

            //적 추적 범위를 벗어나면 추적 해제
            if (dis > 6.0f)
            {
                entity._target = null;
                entity.ChangeState(Wander.Instance);
            }
            //추적
            else if (dis> 0.5f)
            {
                entity.transform.localRotation = Quaternion.LookRotation(entity._target.Get_Pos() - entity.Get_Pos());
                Vector3 move_dir = Vector3.Normalize(entity._target.Get_Pos() - entity.Get_Pos());
                entity.Add_Pos(move_dir * entity.move_speed * Time.deltaTime);
                Debug.DrawLine(entity.Get_Pos(), entity._target.Get_Pos());
            }
            //추적 종료
            else
            {
                if (entity._target != null)
                    entity.ChangeState(Attack.Instance);
                else
                    entity.ChangeState(Wander.Instance);
            }
        }
        public override void Exit(Enemy entity)
        {
            entity.move_speed = 0.5f;
        }
    }
    /*
     * Class : Attack
     * Desc
     *  : 상태의 소유주(entity)가 어떤 대상(Target)을 공격할 때 행동 정의
     */
     public class Attack : State<Enemy>
    {
        static readonly Attack instance = new Attack();
        public static Attack Instance
        {
            get { return instance; }
        }
        public override void Enter(Enemy entity)
        {
            entity.Anim.Play(null);
            entity.Anim.Play("Enemy_Attack");
            entity.attack_state = 0;
        }
        public override void Execute(Enemy entity)
        {
            if (entity.attack_state ==1)
            {
                entity.attack_state = 0;
                Update_Physics_Attack(entity);
            }
            else if (entity.attack_state ==2)
            {
                End_Physics_Attack(entity);
            }
        }
        public override void Exit(Enemy entity)
        {
            
        }
        void Update_Physics_Attack(Enemy entity)
        {
            if (entity._target == null) entity.ChangeState(Wander.Instance);

            Player player = entity._target.GetComponent<Player>() as Player;

            if (player == null) entity.ChangeState(Wander.Instance);

            player.Hit(entity);
        }
        void End_Physics_Attack(Enemy entity)
        {
            if (entity._target != null)
            {
                float dis = Vector3.Distance(entity._target.Get_Pos(), entity.Get_Pos());

                if (dis <= 0.7f) entity.ChangeState(Attack.instance);
                else entity.ChangeState(Pursuit.Instance);
            }
            else entity.ChangeState(Wander.Instance);
        }
    }
    
}