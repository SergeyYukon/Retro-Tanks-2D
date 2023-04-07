using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingCurtain : MonoBehaviour
{
    [SerializeField] private CanvasGroup curtain;
    [SerializeField] private float hideSpeed;
    private bool isHide;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void Update()
    {
        if (isHide)
        {
            if (curtain.alpha > 0)
            {
                curtain.alpha -= Time.deltaTime * hideSpeed;
            }
            else
            {
                curtain.alpha = 0;
                isHide = false;
                gameObject.SetActive(false);
            }
        }
    }

    public void Show()
    {
        curtain.alpha = 1;
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        isHide = true;
    }
}
