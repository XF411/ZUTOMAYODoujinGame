using System.Collections.Generic;
using UnityEngine;

public class VideoAdaptionComponent : MonoBehaviour
{
    public AdaptionStyle adaptionStyle;

    /// <summary>
    /// 实际屏幕的rect宽度
    /// </summary>
    private float screenWidth;

    /// <summary>
    /// 实际屏幕的rect高度
    /// </summary>
    private float screenHeight;

    /// <summary>
    /// 视频文件宽度，用于自适应
    /// </summary>
    public float videoWidth = 1560;
    /// <summary>
    /// 视频文件高度，用于自适应
    /// </summary>
    public float videoHeight = 720;

    public Vector2 anchorMin = new Vector2(0.5f, 0.5f);
    public Vector2 anchorMax = new Vector2(0.5f, 0.5f);
    public Vector2 anchoredPosition = new Vector2(0.5f, 0.5f);

    public List<RectTransform> DisplayList = new List<RectTransform>();
    RectTransform rectTransform;


    void Awake()
    {
        rectTransform = gameObject.GetComponent<RectTransform>();
        screenWidth = rectTransform.rect.width;
        screenHeight = rectTransform.rect.height;
        AdaptionDisplay();
    }

    void Update()
    {
        screenWidth = rectTransform.rect.width;
        screenHeight = rectTransform.rect.height;
        AdaptionDisplay();
    }

    void AdaptionDisplay()
    {
        switch (adaptionStyle)
        {
            case AdaptionStyle.None:
                AdaptionStyle_None();
                break;
            case AdaptionStyle.FullScreen:
                AdaptionStyle_FullScreen();
                break;
            case AdaptionStyle.Letterbox:
                //AdaptionStyle_Letterbox();
                AdaptionStyle_SelfAdaptionLetterbox();
                break;
            default:
                break;
        }
    }

    private void AdaptionStyle_SelfAdaptionLetterbox()
    {
        float commonDivisor = CommonDivisor();//求出视频分辨率的最大公约数
        float ratioX = videoWidth / commonDivisor;
        float ratioY = videoHeight / commonDivisor;
        if ((screenWidth / screenHeight) - (ratioX / ratioY) <= - 0.4f)//平时都全屏，但是当真实分辨率与视频分辨率相比，两边裁剪的太多，就换为上下黑边的适应方式
        {
            AdaptionStyle_Letterbox();
        }
        else
        {
            AdaptionStyle_FullScreen();
        }

    }

    void AdaptionStyle_None()
    {
        foreach (var item in DisplayList)
        {
            item.anchorMin = anchorMin;
            item.anchorMax = anchorMax;
            item.sizeDelta = new Vector2(videoWidth, videoHeight);
            item.anchoredPosition = anchoredPosition;
        }
    }

    void AdaptionStyle_FullScreen()
    {
        float commonDivisor = CommonDivisor();//求出视频分辨率的最大公约数
        float ratioX = videoWidth / commonDivisor;
        float ratioY = videoHeight / commonDivisor;

        float x, y, temp;
        if (screenWidth / screenHeight > ratioX / ratioY)
        {
            //实际画面比例大于视频比例
            temp = screenWidth / ratioX;
        }
        else
        {
            //实际画面比例小于视频比例
            temp = screenHeight / ratioY;
        }
        x = temp * ratioX;
        y = temp * ratioY;
        foreach (var item in DisplayList)
        {
            item.anchorMin = anchorMin;
            item.anchorMax = anchorMax;
            item.sizeDelta = new Vector2(x, y);
            item.localPosition = Vector2.zero;
            item.anchoredPosition = anchoredPosition;
        }

    }

    void AdaptionStyle_Letterbox()
    {
        float commonDivisor = CommonDivisor();//求出视频分辨率的最大公约数
        float ratioX = videoWidth / commonDivisor;
        float ratioY = videoHeight / commonDivisor;
        float x, y;
        x = screenWidth;
        y = x / ratioX * ratioY;
        foreach (var item in DisplayList)
        {
            item.anchorMin = anchorMin;
            item.anchorMax = anchorMax;
            item.sizeDelta = new Vector2(x, y);
            item.anchoredPosition = anchoredPosition;
        }
    }

    /// <summary>
    /// 求视频分辨率的宽高比的最大公约数
    /// </summary>
    /// <returns></returns>
    float CommonDivisor()
    {
        float _w = videoWidth;
        float _h = videoHeight;
        float temp = _w % _h;
        while (temp != 0)
        {
            _w = _h;
            _h = temp;
            temp = _w % _h;
        }
        return _h;
    }

}

