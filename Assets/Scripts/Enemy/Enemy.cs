using Cinemachine;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public abstract class Enemy : MonoBehaviour
{

    [SerializeField] protected Animator anim;

    protected EnemyStateMachine stateMachine;   //初始化在具体类中实现!
    [SerializeField] protected SpriteRenderer spr;
    public SpriteRenderer Spr{
        get{
            return spr;
        } 
        protected set{
            spr = value;
        }
    }

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

    #region 找到玩家相关
    protected Transform playerTransform;
    public Transform PlayerTransform{
        get{
            return playerTransform;
        }
        protected set{
            playerTransform = value;
        }
    }
    #endregion

    #region 光照相关
    [SerializeField] private Light2D circleLight;
    public Light2D CircleLight{
        get{
            return circleLight;
        }
        protected set{
            circleLight = value;
        }
    }
    public bool lightOn;
    #endregion

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
        if(enemyState == EnemyState.Found){
            stateMachine.ChangeState(new FoundState());
            return;
        }
    }

    public void TargetPlayer(Transform transform){
        PlayerTransform = transform;
    }

    public void DestroySelf(){
        Destroy(gameObject);
    }

    #region CinemachineImpulse
    [SerializeField] protected CinemachineImpulseSource impulseSource;

    public void GenImpulse(){
        impulseSource?.GenerateImpulse();
    }
    #endregion
}
