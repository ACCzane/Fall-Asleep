using System;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private PlayerInput playerInput;

    //移动
    private Vector2 playerMovement;
    private Vector2 previousPlayerMovement;
    private bool isHiding;

    //交互
    public bool canPressE;     //E：与书柜交互，躲进去
    public bool canPressQ;      //Q：与配电箱交互，摧毁配电箱

    [Header("参数")]
    [SerializeField] private float playerSpeed;

    private void Awake() {
        playerInput = new PlayerInput();
    }

    private void OnEnable() {
        playerInput.Enable();

        playerInput.Player.Hide.performed += OnHideButtonPerformed;
        playerInput.Player.Mess.performed += OnMessButtonPerformed;

        EventHandler.Hide += OnHide;
    }

    private void OnDisable() {
        playerInput.Disable();

        playerInput.Player.Hide.performed -= OnHideButtonPerformed;
        playerInput.Player.Mess.performed -= OnMessButtonPerformed;

        EventHandler.Hide += OnHide;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canPressE = false;
    }

    // Update is called once per frame
    void Update()
    {
        playerMovement = playerInput.Player.Move.ReadValue<Vector2>();

        MovePlayer();
    }

    private void MovePlayer(){
        if(isHiding){return;}

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

    private void OnHideButtonPerformed(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if(!canPressE){return;}

        EventHandler.CallHide();

    }

    private void OnMessButtonPerformed(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if(!canPressQ){return;}

        EventHandler.CallMess();
    }

    private void OnHide()
    {
        isHiding = !isHiding;
    }
}
