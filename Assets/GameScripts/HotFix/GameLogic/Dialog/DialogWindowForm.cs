using System.Collections;
using UnityEngine;
using UnityGameFramework.Runtime;
using System;
using TMPro;
using UnityEngine.UI;
using GameProto;
using DG.Tweening;

namespace GameLogic
{
    public class DialogWindowForm : UIFormLogic
    {
        public Image DialogBG;
        public Image Actor;
        public TextMeshProUGUI DialogTMP;
        public Button NextBtn;
        public Button TouchBtn;
        public Button AutoBtn;
        public Button SkipBtn;

        public Image dialogTextBG1;
        public Image dialogTextBG2;

        bool OnFadeIn = false;
        bool OnFadeOut = false;

        cfg.Story nextCfg;
        private int dialogID = 0;

        private GameObject currentLive2DInstance;
        private string lastLoadedPrefabName;

        public RectTransform Live2D;
        Camera mainCamera;

        string defaultStr = "这是一句没有读取配置表的测试对话，\n这句话结束后，\n下一句话会开始读取配置表显示对话";
        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            DialogTMP.richText = true;
            DialogTMP.text = "";
            ShowDialogText(defaultStr);
            dialogID = 0;

            NextBtn.onClick.AddListener(OnNextBtnClick);
            TouchBtn.onClick.AddListener(OnNextBtnClick);

            DialogBG.SetSprite("default");
            SkipBtn.gameObject.SetActive(false);
            Actor.gameObject.SetActive(false);
            DialogBG.gameObject.SetActive(true);
            mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        }

        private void ShowDialogText(string dialogStr)
        {
            DialogTMP.text = "";
            if (nextCfg != null)
            {
                if (nextCfg.StoryType == 2)//自述
                {
                    dialogTextBG1.gameObject.SetActive(true);
                    dialogTextBG2.gameObject.SetActive(false);
                    DialogTMP.color = Color.white;
                    DialogTMP.alignment = TextAlignmentOptions.Top;
                }
                else if (nextCfg.StoryType == 3)//对白
                {
                    dialogTextBG1.gameObject.SetActive(false);
                    dialogTextBG2.gameObject.SetActive(true);
                    DialogTMP.color = Color.white;
                    DialogTMP.alignment = TextAlignmentOptions.TopLeft;
                }

            }

            //处理字符串，一次性替换所有的转义换行符 '\\n' 为真实的换行符 '\n'
            dialogStr = dialogStr.Replace("\\n", "\n");
            DialogTMP.text = dialogStr;

            //TODO 让人物的嘴动起来
            if (nextCfg != null && nextCfg.StoryType == 3)
            {
                if (currentLive2DInstance != null)
                {
                    var sc = currentLive2DInstance.GetComponent<AutoMouth>();
                    if (sc != null)
                    {
                        sc.Speak(dialogStr);
                    }
                }
            }
        }

        private void OnNextBtnClick()
        {
            if (OnFadeIn || OnFadeOut)
            {
                return;
            }

            if (dialogID == 0)
            {
                LoadAndPrepareNextDialog();  // 没有淡出，直接加载下一句
                return;
            }

            // 从当前对话配置中获取淡出效果，决定是否需要先执行淡出
            cfg.Story currentCfg = ConfigSystem.Instance.Tables.TbStory.Get(dialogID);
            if (currentCfg != null && IsFadeOut(currentCfg.CamareEffect))
            {
                OnFadeOut = true;
                // 执行淡出
                StartCoroutine(ApplyCameraEffect(currentCfg.CamareEffect, () => {
                    OnFadeOut = false;
                    LoadAndPrepareNextDialog();
                }));
            }
            else
            {
                LoadAndPrepareNextDialog();  // 没有淡出，直接加载下一句
            }
        }

        private void LoadAndPrepareNextDialog()
        {
            dialogID++;
            nextCfg = ConfigSystem.Instance.Tables.TbStory.Get(dialogID);
            if (nextCfg == null)
            {
                // 处理配置获取失败的情况
                return;
            }

            SetupDialogBackgroundAndActor();  // 更新背景和立绘

            if (IsFadeIn(nextCfg.CamareEffect))
            {
                OnFadeIn = true;
                // 执行淡入
                StartCoroutine(ApplyCameraEffect(nextCfg.CamareEffect, () => {
                    OnFadeIn = false;
                    ShowDialogText(nextCfg.Text);  // 显示对话文本
                }));
            }
            else
            {
                ShowDialogText(nextCfg.Text);  // 没有淡入，直接显示文本
            }
        }

        //private void TryPlayNextDialog()
        //{
        //    dialogID++;
        //    nextCfg = ConfigSystem.Instance.Tables.TbStory.Get(dialogID);
        //    if (nextCfg == null)
        //    {
        //        // 处理配置获取失败的情况
        //        return;
        //    }
        //    // 更新背景和立绘
        //    SetupDialogBackgroundAndActor();
        //    if (IsFadeIn(nextCfg.CamareEffect))
        //    {
        //        OnFadeIn = true;
        //        // 执行淡入
        //        StartCoroutine(ApplyCameraEffect(nextCfg.CamareEffect, () => {
        //            // 显示对话文本
        //            OnFadeIn = false;
        //            ShowDialogText(nextCfg.Text);
        //        }));
        //    }
        //    if (IsFadeOut(nextCfg.CamareEffect))
        //    {
        //        OnFadeOut = true;
        //        // 执行淡出
        //        StartCoroutine(ApplyCameraEffect(nextCfg.CamareEffect, () => {
        //            // 显示对话文本
        //            OnFadeOut = false;
        //            ShowDialogText(nextCfg.Text);
        //        }));
        //    }
        //}

        private string lastLoadedCharacter = ""; // 上一次加载的立绘资源

        private void SetupDialogBackgroundAndActor()
        {
            if (nextCfg.Bg != "0")
            {
                if (nextCfg.Bg.Equals("黑屏"))
                {
                    DialogBG.sprite = null;
                    DialogBG.color = Color.black; // 黑屏设置
                }
                else
                {
                    var bgName = nextCfg.Bg.Contains(".") ? nextCfg.Bg.Substring(0, nextCfg.Bg.LastIndexOf(".")) : nextCfg.Bg;
                    DialogBG.SetSprite(bgName);
                    DialogBG.color = Color.white; // 确保背景是不透明的
                }
            }
            else
            {
                // 如果没有背景设置，确保背景是默认的或透明的
                DialogBG.color = Color.clear;
            }

            // 立绘设置
            if (nextCfg.Character != "0" && nextCfg.Character != lastLoadedCharacter)
            {
                lastLoadedCharacter = nextCfg.Character; // 更新最后加载的立绘
                Actor.color = new Color(1, 1, 1, 0); // 设置立绘为透明
                Actor.gameObject.SetActive(true);
                Actor.SetSprite(nextCfg.Character);
                Actor.DOFade(1, 0.1f); // 加载完成后淡入
            }
            else if (nextCfg.Character != "0" && nextCfg.Character == lastLoadedCharacter)
            {
                // 立绘与上一次相同，直接确保它是可见的，无需淡入
                Actor.gameObject.SetActive(true);
                Actor.color = Color.white; // 确保立绘是不透明的
            }
            else
            {
                Actor.gameObject.SetActive(false);
            }

            SetupCharacterAnimation();
        }

        private void SetupCharacterAnimation()
        {
            string characterAniConfig = nextCfg.CharacterAni;

            if (!characterAniConfig.Equals("0"))
            {
                // 分解配置字符串，获取预设名称和动画名称
                string[] parts = characterAniConfig.Split('#');
                if (parts.Length > 0)
                {
                    string prefabName = parts[0];  // 获取预设名

                    if (currentLive2DInstance != null && lastLoadedPrefabName == prefabName)
                    {
                        // 如果当前已经加载并且与新配置相同，则无需重新加载
                        Live2D.gameObject.SetActive(true);
                        return;
                    }

                    // 卸载当前实例化的 Live2D 对象
                    if (currentLive2DInstance != null)
                    {
                        Destroy(currentLive2DInstance);
                    }

                    // 加载新的 Live2D 预设
                    GameObject prefab = GameModule.Resource.LoadAsset<GameObject>(prefabName);
                    if (prefab != null)
                    {
                        // 实例化 Live2D 预设
                        currentLive2DInstance = Instantiate(prefab, mainCamera.gameObject.GetComponent<Transform>());
                        if (prefabName == "donghua01") 
                        {
                            currentLive2DInstance.GetComponent<Transform>().localPosition = new Vector3(0, -0.55f, 0.38f);
                        }
                        if (prefabName == "donghua02")
                        {
                            currentLive2DInstance.GetComponent<Transform>().localPosition = new Vector3(0, 0f, 5f);
                        }
                        lastLoadedPrefabName = prefabName;  // 更新已加载预设的记录
                    }
                    else
                    {
                        Debug.LogError("Failed to load Live2D prefab: " + prefabName);
                    }

                    Live2D.gameObject.SetActive(true);
                }
            }
            else
            {
                // 如果配置为 "0"，隐藏 Live2D gameObject
                Live2D.gameObject.SetActive(false);
            }
        }

        /// <summary>
        /// 是否淡入
        /// </summary>
        /// <param name="effect"></param>
        /// <returns></returns>
        private bool IsFadeIn(string effect) 
        {
            if (string.IsNullOrEmpty(effect) || effect == "0")
            {
                return false;
            }
            string[] parts = effect.Split('#');
            return parts[0] == "4";
        }

        /// <summary>
        /// 是否淡出
        /// </summary>
        /// <param name="effect"></param>
        /// <returns></returns>
        private bool IsFadeOut(string effect) 
        {
            if (string.IsNullOrEmpty(effect) || effect == "0")
            {
                return false;
            }
            string[] parts = effect.Split('#');
            return parts[0] == "3";
        }


        private IEnumerator ApplyCameraEffect(string effect, Action onComplete)
        {
            if (string.IsNullOrEmpty(effect) || effect == "0")
            {
                DialogBG.color = new Color(DialogBG.color.r, DialogBG.color.g, DialogBG.color.b, 1); // 确保不透明
                onComplete?.Invoke();
                yield break;
            }

            string[] parts = effect.Split('#');
            if (parts.Length != 2)
            {
                DialogBG.color = new Color(DialogBG.color.r, DialogBG.color.g, DialogBG.color.b, 1); // 确保不透明
                onComplete?.Invoke();
                yield break;
            }

            int effectType = int.Parse(parts[0]);
            float duration = float.Parse(parts[1]);

            switch (effectType)
            {
                case 3:  // 淡出
                    yield return DialogBG.DOFade(0, duration).WaitForCompletion();
                    break;
                case 4:  // 淡入
                    DialogBG.color = new Color(DialogBG.color.r, DialogBG.color.g, DialogBG.color.b, 0); // 设置透明开始
                    yield return DialogBG.DOFade(1, duration).WaitForCompletion();
                    break;
            }

            onComplete?.Invoke();
        }

    }
}
