using UnityEngine;

public class Market : MonoBehaviour
{
    [SerializeField] private GameObject marketMenu; 

    private void OnTriggerEnter(Collider other)
    {
        // Fox, markete yaklaştığında menüyü aç
        if (other.CompareTag("Fox"))
        {
            marketMenu.SetActive(true);
            Cursor.lockState = CursorLockMode.None; 
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Fox, marketten uzaklaştığında menüyü kapat
        if (other.CompareTag("Fox"))
        {
            marketMenu.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked; 
        }
    }
}
