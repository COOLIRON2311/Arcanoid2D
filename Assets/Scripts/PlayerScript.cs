using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        var mouse_pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // if (mouse_pos.x < -7.1 || 7.1 < mouse_pos.x)
        //     return;
        var pos = transform.position;
        pos.x = mouse_pos.x;
        transform.position = pos;
    }
}
