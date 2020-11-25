using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Getinput : MonoBehaviour
{
    public float buttonX, buttonY, mouseX, mouseY;
    public float scroll;
    public bool leftMouse, rightMouse, esc, R;

    private void Start()
    {
        scroll = -1;
    }
    void Update()
    {
        GetInput();
    }

    void GetInput()
    {
        //aracade/pc input
#if false
        //gets all the input needed
        buttonX = (Input.GetAxis("Horizontal"));
        buttonY = Mathf.Clamp((Input.GetAxis("Vertical")), -0.5f, 1f);

        //fire2 is clamped so the player wont rotate too fast
        if (Input.GetButton("Fire2"))
        {
            mouseX = Mathf.Clamp((Input.GetAxis("Mouse X")), -0.5f, 0.5f);
            mouseY = Mathf.Clamp((Input.GetAxis("Mouse Y")), -0.5f, 0.5f);
        }
        else
        {
            mouseX = Mathf.Clamp((Input.GetAxis("Mouse X")), -2f, 2f);
            mouseY = Mathf.Clamp((Input.GetAxis("Mouse Y")), -2f, 2f);
        }

        leftMouse = (Input.GetButton("Fire1"));
        rightMouse = (Input.GetButton("Fire2"));
        esc = (Input.GetKeyDown(KeyCode.Escape));
        scroll = Input.GetAxisRaw("Mouse ScrollWheel");
        R = (Input.GetKeyDown(KeyCode.R));
#else
        buttonX = (Input.GetAxis("Horizontal"));

        if (Input.GetKey(KeyCode.LeftShift))
            buttonY = 1;
        else if (Input.GetKey(KeyCode.LeftControl))
            buttonY = -0.5f;
        else
            buttonY = 0;

        if (Input.GetKey(KeyCode.LeftControl))
        {
            mouseX = Mathf.Clamp((Input.GetAxis("Horizontal")), -0.5f, 0.5f);
            mouseY = Mathf.Clamp((Input.GetAxis("Vertical")), -0.5f, 0.5f);
        }
        else
        {
            mouseX = Mathf.Clamp((Input.GetAxis("Horizontal")), -2f, 2f);
            mouseY = Mathf.Clamp((Input.GetAxis("Vertical")), -2f, 2f);
        }

        leftMouse = (Input.GetKey(KeyCode.Space));
        rightMouse = (Input.GetKey(KeyCode.R));
        esc = (Input.GetKeyDown(KeyCode.Escape));

        if (Input.GetKeyDown(KeyCode.E))
            scroll = -scroll;

        R = (Input.GetKeyDown(KeyCode.F));
#endif
    }
}
