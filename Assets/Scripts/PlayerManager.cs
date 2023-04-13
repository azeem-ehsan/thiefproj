using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.EnhancedTouch;
using ETouch = UnityEngine.InputSystem.EnhancedTouch;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private Vector2 JoystickSize = new Vector2(200, 200);
    public JoyStick joyStick;
    public NavMeshAgent PlayerNavmeshAgent;
    private Finger MovementFinger;
    public Vector2 MovementAmount;
    public Animator playerAnimator;

    void Start()
    {
        PlayerNavmeshAgent = GetComponent<NavMeshAgent>();
        playerAnimator = GetComponentInChildren<Animator>();
    }


    private void OnEnable()
    {
        EnhancedTouchSupport.Enable();
        ETouch.Touch.onFingerDown += HandleFingerDown;
        ETouch.Touch.onFingerUp += HandleLooseFinger;
        ETouch.Touch.onFingerMove += HandleFingerMove;
    }
    private void OnDisable()
    {
        ETouch.Touch.onFingerDown -= HandleFingerDown;
        ETouch.Touch.onFingerUp -= HandleLooseFinger;
        ETouch.Touch.onFingerMove -= HandleFingerMove;
        EnhancedTouchSupport.Disable();
    }

    private void HandleFingerMove(Finger movedFinger)
        {
            if(movedFinger == MovementFinger)
                {
                    Vector2 knobPosition;
                    float maxMovement = JoystickSize.x / 2f;
                    ETouch.Touch currentTouch = movedFinger.currentTouch;

                    if(Vector2.Distance(
                        currentTouch.screenPosition,
                        joyStick.JoyStickObj.anchoredPosition
                        ) > maxMovement)
                        {
                            knobPosition = (
                                currentTouch.screenPosition - joyStick.JoyStickObj.anchoredPosition
                                ).normalized * maxMovement;
                        }
                    else
                        {
                            knobPosition = currentTouch.screenPosition - joyStick.JoyStickObj.anchoredPosition;
                        }

                    joyStick.Knob.anchoredPosition = knobPosition;
                    MovementAmount = knobPosition / maxMovement;
                }
        }


        private void HandleFingerDown(Finger touchedFinger)
            {
                if(MovementFinger == null && touchedFinger.screenPosition.x <= Screen.width)
                    {
                        MovementFinger = touchedFinger;
                        MovementAmount = Vector2.zero;
                        joyStick.gameObject.SetActive(true);
                        joyStick.JoyStickObj.sizeDelta = JoystickSize;
                        joyStick.JoyStickObj.anchoredPosition = ClampStartPosition(touchedFinger.screenPosition);
                    }
            }


        private void HandleLooseFinger(Finger lostFinger)
            {
                if(lostFinger == MovementFinger)
                    {
                        MovementFinger = null;
                        joyStick.Knob.anchoredPosition = Vector2.zero;
                        joyStick.gameObject.SetActive(false);
                        MovementAmount = Vector2.zero;
                    }
            }

        private Vector2 ClampStartPosition(Vector2 startPosition)
            {
                if(startPosition.x < JoystickSize.x / 2f)
                    {
                        startPosition.x = JoystickSize.x / 2f;
                    }
                  
                  if(startPosition.y < JoystickSize.y / 2f)
                    {
                        startPosition.y = JoystickSize.y / 2f;
                    }
                else if(startPosition.y > Screen.height - JoystickSize.y / 2f)
                    {
                        startPosition.y = Screen.height - JoystickSize.y / 2f;
                    }

                return startPosition;
            }


    // Update is called once per frame
    void Update()
    {
        Vector3 scaledMovement = PlayerNavmeshAgent.speed * Time.deltaTime * new Vector3(MovementAmount.x, 0, MovementAmount.y);
        PlayerNavmeshAgent.Move(scaledMovement);
        PlayerNavmeshAgent.transform.LookAt(PlayerNavmeshAgent.transform.position + scaledMovement, Vector3.up);

        // moveX,moveZ are the parameters in the animator controller
        playerAnimator.SetFloat("moveX", MovementAmount.x);
        playerAnimator.SetFloat("moveZ", MovementAmount.y);
    }





/////////

    public void PlayPickup_Anim()
        {
            playerAnimator.SetTrigger("pickup");
        }







}
