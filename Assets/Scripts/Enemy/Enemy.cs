using Cinemachine;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public abstract class Enemy : MonoBehaviour
{
    #region 引用
    [SerializeField] protected Animator anim;
    public Animator Anim{
        get{
            return anim;
        }
        set{
            anim = value;
        }
    }
    [SerializeField] protected SpriteRenderer spr;
    public SpriteRenderer Spr{
        get{
            return spr;
        } 
        protected set{
            spr = value;
        }
    }
    
    [SerializeField] protected Material unlitMaterial;
    [SerializeField] protected Material litMaterial;
    #endregion

    #region 移动
    [Header("参数")]
    [SerializeField] protected float enemySpeed = 5f;
    public float EnemySpeed{
        get{return enemySpeed;}
        protected set{enemySpeed = value;}
    }
    #endregion

    #region 视线转动
    [SerializeField] protected Transform view;
    public Transform View{
        get{
            return view;
        }
        set{
            view = value;
        }
    }
    [SerializeField] protected float rotateSpeed = 20f;            //每秒旋转的角度值
    public float RotateSpeed{
        get{
            return rotateSpeed;
        }
        protected set{
            rotateSpeed = value;
        }
    }
    #endregion

    #region 找到玩家/物体相关
    protected Transform targetTransform;
    public Transform TargetTransform{
        get{
            return targetTransform;
        }
        protected set{
            targetTransform = value;
        }
    }
    #endregion

    // #region 光照相关
    // [SerializeField] private Light2D circleLight;
    // public Light2D CircleLight{
    //     get{
    //         return circleLight;
    //     }
    //     protected set{
    //         circleLight = value;
    //     }
    // }
    // public bool lightOn;
    // #endregion

    #region 状态相关
    protected EnemyStateMachine stateMachine;   //初始化在具体类中实现!

    protected NightStat nightStat;

    #endregion

    private void OnEnable() {
        EventHandler.NightFall += OnNightFall;
    }

    private void OnDisable() {
        EventHandler.NightFall -= OnNightFall;
    }

    private void OnNightFall()
    {
        if(nightStat == NightStat.Normal){
            spr.material = litMaterial;
        }
        if(nightStat == NightStat.Tired){
            spr.material = unlitMaterial;
            spr.color = new Color(1,1,1,0.3f);
        }
        if(nightStat == NightStat.Drunk){
        
        }
    }

    public void ChangeState(EnemyState enemyState){
        if(enemyState == EnemyState.Move){
            stateMachine.ChangeState(new MoveState());
            return;
        }
        if(enemyState == EnemyState.Alert){
            stateMachine.ChangeState(new AlertState());
            return;
        }
        if(enemyState == EnemyState.ResetView){
            stateMachine.ChangeState(new ResetViewState());
            return;
        }
        if(enemyState == EnemyState.FoundPlayer){
            stateMachine.ChangeState(new FoundPlayerState());
            return;
        }
        if(enemyState == EnemyState.FoundFixedTank){
            stateMachine.ChangeState(new FoundTankFixedState());
            return;
        }
    }

    public void Target(Transform transform){
        TargetTransform = transform;
    }

    public void DestroySelf(){
        Destroy(gameObject);
    }

    public void UpdateNightStat(NightStat nightStat){
        this.nightStat = nightStat;
    }

    #region CinemachineImpulse
    [SerializeField] protected CinemachineImpulseSource impulseSource;

    public void GenImpulse(){
        impulseSource?.GenerateImpulse();
    }
    #endregion
}
