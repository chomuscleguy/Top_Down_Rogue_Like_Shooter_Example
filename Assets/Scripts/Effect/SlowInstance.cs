//public class SlowInstance : StatusInstance
//{
//    private float slowAmount;

//    public SlowInstance(float duration, float slow)
//    {
//        this.duration = duration;
//        this.slowAmount = slow;
//    }

//    protected override void OnApply()
//    {
//        var move = target.GetComponent<PlayerMovement>();
//        if (move != null)
//            move.SetSpeed(move.GetSpeed() * (1f - slowAmount));
//    }

//    protected override void OnExpire()
//    {
//        var move = target.GetComponent<PlayerMovement>();
//        if (move != null)
//            move.SetSpeed(move.GetSpeed() / (1f - slowAmount));
//    }
//}