using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    Vector2 lastMovement;
    Rigidbody2D rb;
    [SerializeField]
    float moveSpeed;
    DoorController activeDoor = null;
    private GameObject buttons;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        lastMovement = Vector2.zero;
        rb = GetComponent<Rigidbody2D>();
        buttons = GameObject.Find("buttons");
        if (buttons != null)
        {
            buttons.SetActive(false); // Aluksi piilossa

        }
    }

    // Update is called once per frame
    void Update()
    {
            if (buttons != null)
            {
                buttons.SetActive(activeDoor != null);
            }
        
    }

    private void FixedUpdate()
    {

        rb.MovePosition(rb.position + lastMovement * moveSpeed * Time.fixedDeltaTime);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        // Huomaa mitä pelaaja löytää
        if (collision.CompareTag("Door"))
        {
            Debug.Log("Found Door");
            activeDoor = collision.gameObject.GetComponent<DoorController>();

            if (buttons != null)
            {
                buttons.SetActive(true); // Näytä buttons
            }
            else if (collision.CompareTag("Merchant"))
            {
                Debug.Log("Found Merchant");
            }
        }
    }


    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Door"))
        {
            activeDoor = null;
        }
    }



    public void OnOpenButtonPress()
    {
        activeDoor.ReceiveAction(DoorController.OvenToiminto.Avaa);
    }
    public void OnCloseButtonPress()
    {
        activeDoor.ReceiveAction(DoorController.OvenToiminto.Sulje);
    }
    public void OnUnlockButtonPress()
    {
        activeDoor.ReceiveAction(DoorController.OvenToiminto.AvaaLukko);
    }
    public void OnLockButtonPress()
    {
        activeDoor.ReceiveAction(DoorController.OvenToiminto.Lukitse);
    }
    public void OnMoveAction(InputValue value)
    {
        Vector2 v = value.Get<Vector2>();
        lastMovement = v;
    }


}
