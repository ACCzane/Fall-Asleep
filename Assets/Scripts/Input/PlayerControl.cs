using System;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private PlayerInput playerInput;


    [SerializeField] private Hide hide;     //无奈之举

    [SerializeField] private PlayerAudio playerAudio;


    //移动
    private Vector2 playerMovement;
    private Vector2 previousPlayerMovement;
    private bool isMoving;              //用于传递给Animator


    [SerializeField] private Animator animator;
    private float lastDeltaX;           //用于动画BlendTree状态的停留
    private float lastDeltaY;
    private bool isGhost;               //用于针对不同BlendTree

    //交互
    // public InteractType currentActivatedInteractType;     //E：与物体交互
    public IInteractable currentInteractableTarget;
    [SerializeField]private HoldObj playerHold;

    [Header("参数")]
    [SerializeField] private float playerSpeed;
    public float PlayerSpeed{
        get{
            return playerSpeed;
        }
        private set{
            playerSpeed = value;
        }
    }

    private void Awake() {
        playerInput = new PlayerInput();
    }

    private void OnEnable() {
        playerInput.Enable();

        playerInput.Player.Interact.performed += OnInteractButtonPressed;
        playerInput.Player.Pick.performed += OnPickButtonPressed;
        playerInput.Player.Drop.performed += OnDropButtonPressed;

        // EventHandler.Sleep += OnSleep;

        EventHandler.NightFall += OnNightFall;
    }

    private void OnDisable() {
        playerInput.Disable();

        playerInput.Player.Interact.performed -= OnInteractButtonPressed;
        playerInput.Player.Pick.performed -= OnPickButtonPressed;
        playerInput.Player.Drop.performed -= OnDropButtonPressed;

        // EventHandler.Sleep -= OnSleep;

        EventHandler.NightFall -= OnNightFall;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // currentActivatedInteractType = InteractType.Null;
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(currentInteractableTarget);
        Debug.Log(playerInput);

        playerMovement = playerInput.Player.Move.ReadValue<Vector2>();

        MovePlayer();
    }

    private void MovePlayer(){
        if(hide.IsHiding){return;}

        

        if(Vector2.Distance(playerMovement, Vector2.zero) != 0){
            isMoving = true;
        }else{
            isMoving = false;
        }

        if(!isGhost){
            if(Vector2.Distance(playerMovement,Vector2.zero) > 0.1f){
                lastDeltaX = playerMovement.x;
                lastDeltaY = playerMovement.y;
            }
        }else{
            if(playerMovement.x != 0){
                lastDeltaX = playerMovement.x;
            }
        }
        

        animator.SetBool("IsMoving", isMoving);
        animator.SetFloat("DeltaX",lastDeltaX);
        animator.SetFloat("DeltaY",lastDeltaY);

        Vector3 moveVector = new Vector3(playerMovement.x, playerMovement.y, 0) * playerSpeed * Time.deltaTime;
        transform.Translate(moveVector, Space.World);

        previousPlayerMovement = playerMovement;
    }

    private void OnInteractButtonPressed(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if(currentInteractableTarget == null){
            return;
        }
        currentInteractableTarget.Interact();
    }

    private void OnPickButtonPressed(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        //如果是幽灵形态
        if(isGhost){
            //无法再拾取
            return;
        }

        //如果没有可交互物体，返回
        if(playerHold.currentTargetObj == null){
            return;
        }
        //如果场景中有可交互物体
        else{
            // Debug.Log("检测到");
            //如果当前手上有物体
            if(playerHold.holdingObj != null){
                //先丢出去
                playerHold.holdingObj.Drop(playerHold);
                playerAudio.PlayDrop();
            }
            //如果当前手上没有物体
            playerHold.currentTargetObj.Pick(playerHold);
            playerAudio.PlayPick();
        }
    }

    private void OnDropButtonPressed(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        //如果手上没物体
        if(playerHold.holdingObj == null){
            return;
        }
        playerHold.holdingObj.Drop(playerHold);
        playerAudio.PlayDrop();
    }

    // private void OnSleep()
    // {
    //     isSleeping = true;           
    // }

    private void OnNightFall()
    {
        //如果手上还有物品，丢弃手上的物品
        if(playerHold.holdingObj!=null)
            playerHold.holdingObj.Drop(playerHold);

    }
}
