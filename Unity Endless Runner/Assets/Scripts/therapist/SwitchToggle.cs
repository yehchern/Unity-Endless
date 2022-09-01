using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SwitchToggle : MonoBehaviour
{
    [SerializeField] RectTransform uiHandleRectTransform;
    [SerializeField] Color backgroundActiveColor;
    [SerializeField] Color handleActiveColor;

    Image backgroundImage, handleImage;

    Color backgroundDefaultColor, handleDefaultColor;

    Toggle toggle;

    Vector2 handlePosition;

    void Awake()
    {
        toggle = GetComponent<Toggle>();

        handlePosition = uiHandleRectTransform.anchoredPosition;

        backgroundImage = uiHandleRectTransform.parent.GetComponent<Image>();
        handleImage = uiHandleRectTransform.GetComponent<Image>();

        backgroundDefaultColor = backgroundImage.color;
        handleDefaultColor = handleImage.color;

        toggle.onValueChanged.AddListener(OnSwitch);

        if (toggle.isOn)//猜測這邊可以知道哪個訓練館開著並存入資料<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        {
            OnSwitch(true);

        }
            
    }

    void OnSwitch(bool on)
    {
        //uiHandleRectTransform.anchoredPosition = on ? handlePosition * -1 : handlePosition;
        uiHandleRectTransform.DOAnchorPos(on ? handlePosition * -1 : handlePosition, .4f).SetEase(Ease.InOutBack);

        //backgroundImage.color = on ? backgroundActiveColor : backgroundDefaultColor;
        backgroundImage.DOColor (on ? backgroundActiveColor : backgroundDefaultColor, .6f);

        //handleImage.color = on ? handleActiveColor : handleDefaultColor;
        handleImage.DOColor (on ? handleActiveColor : handleDefaultColor, .4f);
    }

    void OnDestroy()
    {
        toggle.onValueChanged.RemoveListener(OnSwitch);
    }
}
