using System;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private PlayerInput playerInput;

    //移动
    private Vector2 playerMovement;
    private Vector2 previousPlayerMovement;
    private bool isHiding;
    private bool isSleeping;

    //交互
    public InteractType currentActivatedInteractType;     //E：与物体交互

    [Header("参数")]
    [SerializeField] private float playerSpeed;

    private void Awake() {
        playerInput = new PlayerInput();
    }

    private void OnEnable() {
        playerInput.Enable();

        playerInput.Player.Interact.performed += OnInteractButtonPressed;

        EventHandler.Sleep += OnSleep;
    }

    private void OnDisable() {
        playerInput.Disable();

        playerInput.Player.Interact.performed -= OnInteractButtonPressed;

        EventHandler.Sleep -= OnSleep;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentActivatedInteractType = InteractType.Null;
    }

    // Update is called once per frame
    void Update()
    {
        playerMovement = playerInput.Player.Move.ReadValue<Vector2>();

        MovePlayer();
    }

    private void MovePlayer(){
        if(isHiding || isSleeping){return;}

        //更改朝向
        if(playerMovement.x < -0.5){
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        if(playerMovement.x > 0.5){
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }

        Vector3 moveVector = new Vector3(playerMovement.x, playerMovement.y, 0) * playerSpeed * Time.deltaTime;
        transform.Translate(moveVector, Space.World);

        previousPlayerMovement = playerMovement;
    }

    private void OnInteractButtonPressed(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if(currentActivatedInteractType == InteractType.Null){
            return;
        }
        if(currentActivatedInteractType == InteractType.Hide){
            EventHandler.CallHide();
        }
        if(currentActivatedInteractType == InteractType.Sleep){
            EventHandler.CallSleep();
        }
    }

    private void OnSleep()
    {
        isHiding = !isHiding;           //玩家可以按E进入，也可以按E退出
    }
}
