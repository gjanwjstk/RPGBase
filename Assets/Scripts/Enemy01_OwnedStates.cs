using System;
using UnityEngine;

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

        }
        public override void Excute(Enemy entity)
        {
        }
        public override void Exit(Enemy entity)
        {
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

            entity.HP -= entity._target.Physice_Damage - entity.Physice_Defense;
            if (entity.HP <=0)
            {
                entity.ChangeState(Die.Instance);
            }
        }
        public override void Excute(Enemy entity)
        {
            if (entity.Anim.GetCurrentAnimatorStateInfo(0).IsName("Idle_Base"))
                entity.RevertPreviousState();
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
            entity._target.GetComponent<Player>().Exp += entity.Level * 30;
            entity.Set_TargetMarkOff();

            renders = entity.GetComponentsInChildren<Renderer>();
            colors = new Color[renders.Length];
            for (int i = 0; i < renders.Length; ++i)
            {
                colors[i] = renders[i].material.color;
                renders[i].material.shader = Shader.Find("Legacy Shaders/Trasparent/Vertexlit");
            }
        }
        public override void Excute(Enemy entity)
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