//using System;

//public class BurnInstance : StatusInstance
//{
//    private float tickDamage;
//    private float tickInterval;
//    private float tickTimer;

//    public BurnInstance(float duration, float damage, float interval)
//    {
//        this.duration = duration;
//        this.tickDamage = damage;
//        this.tickInterval = interval;
//    }

//    protected override void OnUpdate(float dt)
//    {
//        tickTimer += dt;

//        if (tickTimer >= tickInterval)
//        {
//            tickTimer = 0f;
//            target.TakeDamage(tickDamage);
//        }
//    }
//}