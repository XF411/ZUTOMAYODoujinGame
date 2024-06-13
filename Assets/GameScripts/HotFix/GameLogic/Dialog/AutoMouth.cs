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
    public float speechRate = 0.1f; // ˵���ٶȣ���λ��

    private Queue<float> mouthMovements = new Queue<float>();
    private float currentMouthValue = 0f;
    private float targetMouthValue = 0f;
    public float transitionSpeed = 6f; // ��ֵ���ɵ��ٶ�
    private bool isSpeaking = false;
    private bool isClosingMouth = false; // �������������ڼ������Ƿ����ڱպ�
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
                isClosingMouth = targetMouthValue == 0f; // ���Ŀ��ֵΪ0�����������ڱպ�
                nextMovementTime = Time.time + speechRate;
            }
            else if (isClosingMouth)
            {
                if (Mathf.Abs(currentMouthValue) <= 0.01f) // ȷ���������ȫ�պ�
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

        // ʹ��Lerpƽ��������͵�ֵ
        if (Mathf.Abs(currentMouthValue - targetMouthValue) > 0.01f)
        {
            currentMouthValue = Mathf.Lerp(currentMouthValue, targetMouthValue, transitionSpeed * Time.deltaTime);
            SetMouthParameter(currentMouthValue);
        }
        else if (isClosingMouth && Mathf.Abs(currentMouthValue) <= 0.01f)
        {
            SetMouthParameter(0); // ȷ�����ֵΪ0
        }
    }

    private void StartSpeaking()
    {
        string text = inputField.text;
        GenerateMouthMovements(text);
        isSpeaking = true;
        nextMovementTime = Time.time; // Start immediately
    }

    // Ϊ�����ı��е�ÿ���ַ������������Ͷ���ֵ
    private void GenerateMouthMovements(string text)
    {
        mouthMovements.Clear();
        foreach (char c in text)
        {
            float randomValue = Random.Range(0.1f, 0.5f); // ����һ��0.2��0.7֮������ֵ
            mouthMovements.Enqueue(randomValue); // �������
            //float randomValue2 = Random.Range(0.1f, 0.2f); // ����һ��0.2��0.7֮������ֵ
            mouthMovements.Enqueue(0); // �����ż�һ��0��ȷ����Ϳ��Ապ�
        }
        mouthMovements.Enqueue(0f);
    }

    public void Speak(string text) 
    {
        GenerateMouthMovements(text);
        isSpeaking = true;
        nextMovementTime = Time.time; // Start immediately
    }

    // ������͵Ĳ���
    private void SetMouthParameter(float value)
    {
        var mouthParam = model.Parameters[56]; // ȷ��������ȷ������ſ�����
        mouthParam.Value = value;
    }
}
