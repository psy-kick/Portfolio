using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float gravity = 9.8f;
    [SerializeField]
    private CharacterController character;
    public float speed=10f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        character.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 movement = transform.forward*vertical + transform.right*horizontal;
        character.Move(movement *speed* Time.deltaTime);
    }
}
