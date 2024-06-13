using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Live2D.Cubism.Core;
using TMPro;
using UnityEngine.UI;

public class AutoMouth : MonoBehaviour
{
    public CubismModel model;
    public TMP_InputField inputField;
    public Button sendBtn;
    public float speechRate = 0.1f; // 说话速度，单位秒

    private Queue<float> mouthMovements = new Queue<float>();
    private float currentMouthValue = 0f;
    private float targetMouthValue = 0f;
    public float transitionSpeed = 6f; // 插值过渡的速度
    private bool isSpeaking = false;
    private bool isClosingMouth = false; // 新增变量，用于检测嘴巴是否正在闭合
    private float nextMovementTime = 0f;

    private void Start()
    {
        if (sendBtn != null)
        {
            sendBtn.onClick.AddListener(StartSpeaking);
        }
        model = this.FindCubismModel();
    }

    private void LateUpdate()
    {
        if (isSpeaking && Time.time >= nextMovementTime)
        {
            if (mouthMovements.Count > 0 && !isClosingMouth)
            {
                targetMouthValue = mouthMovements.Dequeue();
                isClosingMouth = targetMouthValue == 0f; // 如果目标值为0，标记嘴巴正在闭合
                nextMovementTime = Time.time + speechRate;
            }
            else if (isClosingMouth)
            {
                if (Mathf.Abs(currentMouthValue) <= 0.01f) // 确保嘴巴已完全闭合
                {
                    isClosingMouth = false;
                    targetMouthValue = 0;
                }
            }
            else
            {
                isSpeaking = false;
            }
        }

        // 使用Lerp平滑过渡嘴巴的值
        if (Mathf.Abs(currentMouthValue - targetMouthValue) > 0.01f)
        {
            currentMouthValue = Mathf.Lerp(currentMouthValue, targetMouthValue, transitionSpeed * Time.deltaTime);
            SetMouthParameter(currentMouthValue);
        }
        else if (isClosingMouth && Mathf.Abs(currentMouthValue) <= 0.01f)
        {
            SetMouthParameter(0); // 确保嘴巴值为0
        }
    }

    private void StartSpeaking()
    {
        string text = inputField.text;
        GenerateMouthMovements(text);
        isSpeaking = true;
        nextMovementTime = Time.time; // Start immediately
    }

    // 为输入文本中的每个字符生成随机的嘴巴动作值
    private void GenerateMouthMovements(string text)
    {
        mouthMovements.Clear();
        foreach (char c in text)
        {
            float randomValue = Random.Range(0.1f, 0.5f); // 生成一个0.2到0.7之间的随机值
            mouthMovements.Enqueue(randomValue); // 加入队列
            //float randomValue2 = Random.Range(0.1f, 0.2f); // 生成一个0.2到0.7之间的随机值
            mouthMovements.Enqueue(0); // 紧跟着加一个0，确保嘴巴可以闭合
        }
        mouthMovements.Enqueue(0f);
    }

    public void Speak(string text) 
    {
        GenerateMouthMovements(text);
        isSpeaking = true;
        nextMovementTime = Time.time; // Start immediately
    }

    // 设置嘴巴的参数
    private void SetMouthParameter(float value)
    {
        var mouthParam = model.Parameters[56]; // 确保这是正确的嘴巴张开参数
        mouthParam.Value = value;
    }
}
