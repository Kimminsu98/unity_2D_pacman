using UnityEngine.UI;
using UnityEngine;

public class PlayHPViewer : MonoBehaviour
{
    [SerializeField]
    private PlayerController playerController;
    private Slider sliderHP;

    private void Awake(){
        sliderHP = GetComponent<Slider>();
    }

    private void Update()
    {
        sliderHP.value = playerController.CurrentHP / playerController.MaxHP;
    }
}
