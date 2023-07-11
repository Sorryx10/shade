using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ItemCollector : MonoBehaviour
{
    private int Melons = 0;
    [SerializeField] private Text melonsText;
    [SerializeField] private AudioSource CollectinSoundEffect;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Melon"))
        {
            CollectinSoundEffect.Play();
            Destroy(collision.gameObject);
            Melons++;
            Debug.Log("Melons: " +  Melons);
            melonsText.text = "Melons: " + Melons;
        }
    }
}
