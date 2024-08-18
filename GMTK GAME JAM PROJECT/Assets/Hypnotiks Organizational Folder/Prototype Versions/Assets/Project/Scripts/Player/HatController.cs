using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HatController : MonoBehaviour
{
    [SerializeField] private List<Sprite> hatsSprites = new();
    [SerializeField] private UnityEvent OnChangedHat;
    private int currentIndex;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        ChangeHat(0, false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            ChangeHat(-1, true);
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            ChangeHat(1, true);
        }
    }

    public void ChangeHat(int increase, bool callEvent)
    {
        for (int i = 0; i < increase; i++)
        {
            currentIndex += increase > 0 ? 1 : -1;

            if (currentIndex > hatsSprites.Count - 1)
            {
                currentIndex = 0;
            }
            else if (currentIndex < 0)
            {
                currentIndex = hatsSprites.Count - 1;
            }
        }

        spriteRenderer.sprite = hatsSprites[currentIndex];

        if (callEvent)
        {
            OnChangedHat?.Invoke();
        }
    }
}
