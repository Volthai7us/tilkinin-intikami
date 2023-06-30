using UnityEngine;
using UnityEngine.UI;

public class HealthBarColorChange : MonoBehaviour
{
    [SerializeField] private Image healthBarImage;

    // Sağlık değerini 0 ile 1 arasında bir oran olarak alır ve sağlık çubuğunun rengini buna göre ayarlar.
    public void SetHealth(float healthRatio)
    {
        healthBarImage.fillAmount = healthRatio;
        healthBarImage.color = Color.Lerp(Color.red, Color.green, healthRatio);
    }
}
