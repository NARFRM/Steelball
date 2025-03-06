using UnityEngine;
using UnityEngine.UI;

public class SetCanvasImage : MonoBehaviour
{
    public Image imageComponent;

    void Start()
    {
        Sprite sprite = Resources.Load<Sprite>("StartGame"); // Asegúrate de que la imagen esté en "Assets/Resources/"
        if (sprite != null)
        {
            imageComponent.sprite = sprite;
        }
        else
        {
            Debug.LogError("No se pudo cargar la imagen. Verifica la ruta.");
        }
    }
}
