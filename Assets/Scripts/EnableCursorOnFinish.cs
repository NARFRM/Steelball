using UnityEngine;

public class EnableCursorOnFinish : MonoBehaviour
{
    void Start()
    {
        Cursor.lockState = CursorLockMode.None; // Libera el cursor
        Cursor.visible = true; // Hace visible el cursor
    }
}
