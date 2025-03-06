using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class CountDown : MonoBehaviour
{
    public float countDownTime = 20.0f;
    public GameObject countDownDamage;
    

    public TextMeshProUGUI txtTimer;
    public TextMeshProUGUI txtCountDown;

    private bool soundPlayed = false;
    private bool damageActivated = false;
    public AudioClip countSound;

    private float elapsedTime = 0f;
    private bool isFlashing = false; 

    void Start()
    {
        if (txtTimer != null)
            txtTimer.text = "Time: 0.00";

        if (txtCountDown != null)
        {
            txtCountDown.text = Mathf.CeilToInt(countDownTime).ToString();
            txtCountDown.gameObject.SetActive(false); // Ocultamos hasta que sea necesario
        }

        if (countDownDamage != null)
            countDownDamage.SetActive(false);
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;
        countDownTime -= Time.deltaTime;

        if (txtTimer != null)
        {
            txtTimer.text = "Time: " + elapsedTime.ToString("n2");
            txtTimer.ForceMeshUpdate();
        }

        if (txtCountDown != null)
        {
            txtCountDown.text = Mathf.CeilToInt(Mathf.Max(countDownTime, 0)).ToString();
            txtCountDown.ForceMeshUpdate();
        }

        // Cuando queden 11 segundos, activar efectos
        if (countDownTime <= 11)
        {
            
            if (!soundPlayed && countSound != null && AudioManager.Instance != null)
            {
                AudioManager.Instance.PlaySFX(countSound);
                soundPlayed = true;
            }

            if (!isFlashing)
            {
                txtCountDown.gameObject.SetActive(true); // Mostrar el texto de cuenta regresiva
                StartCoroutine(FlashTextEffect());
                isFlashing = true;
            }
        }

        // Cuando queden 3 segundos, activar el daño
        if (countDownTime <= 3 && !damageActivated)
        {
            if (countDownDamage != null) countDownDamage.SetActive(true);
            damageActivated = true;
        }

        // Cuando el tiempo se acabe, terminar el juego
        if (countDownTime <= 0)
        {
            if (GameManager.Instance != null) GameManager.Instance.GameOverLose(true);
            Debug.Log("Time is up!");
            enabled = false;
        }
    }

    IEnumerator FlashTextEffect()
    {
        Vector3 originalScale = txtCountDown.transform.localScale;

        while (countDownTime > 0 && countDownTime <= 11)
        {
            if (txtCountDown != null)
            {
                txtCountDown.color = Color.red;
                txtCountDown.transform.localScale = originalScale * 1.2f; // Solo un 20% más grande
                yield return new WaitForSeconds(0.2f);

                txtCountDown.color = Color.white;
                txtCountDown.transform.localScale = originalScale; // Volver al tamaño normal
                yield return new WaitForSeconds(0.2f);
            }
        }
        
        // Asegurar que al final vuelva a su tamaño original
        if (txtCountDown != null)
        {
            txtCountDown.transform.localScale = originalScale;
            txtCountDown.color = Color.white;
        }
    }
}
