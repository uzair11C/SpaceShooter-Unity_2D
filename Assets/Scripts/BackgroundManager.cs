using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    [SerializeField]
    private Sprite[] backgroundImages;

    [SerializeField]
    GameObject Background;

    // Start is called before the first frame update
    void Start()
    {
        if (backgroundImages.Length == 0)
        {
            Debug.LogError("No background images assigned in the inspector.");
            return;
        }

        // Set the selected background image to the Background GameObject
        Background.GetComponent<SpriteRenderer>().sprite = backgroundImages[
            Random.Range(0, backgroundImages.Length)
        ];
    }

    // Update is called once per frame
    void Update() { }
}
