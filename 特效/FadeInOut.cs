using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class FadeInOut : MonoBehaviour
{
    public static FadeInOut instance;
    public PostProcessVolume postProcessVolume;
    public float fadeDuration = 1f;
    public Image image;
    public Image board;
    public List<Sprite> images;
    private float targetWeight = 0f;
    private float currentWeight = 0f;
    private float targetAlpha = 0f;
    private float currentAlpha = 0f;
    private float timer = 0f;
    void Awake() {
        // 检查是否已经有一个FadeInOut实例
        if (instance != null)
        // 如果已经有一个实例，销毁当前实例
        Destroy(this.gameObject);
        // 将当前实例设置为全局实例
        instance = this;
    }
    private void Start()
    {
        // 设置后处理音量权重为0
        postProcessVolume.weight = 0f;
        // 设置图像透明度为0
        image.color = new Color(image.color.r, image.color.g, image.color.b, 0f);
        
        board.color = new Color(board.color.r, board.color.g, board.color.b, 0f);
    }

    private void Update()
    {
        // 更新计时
        timer += Time.deltaTime;
        // 使用线性插值计算当前权重和透明度
        currentWeight = Mathf.Lerp(0f, targetWeight, timer / fadeDuration);
        currentAlpha = Mathf.Lerp(0f, targetAlpha, timer / fadeDuration);
        // 更新图像透明度
        image.color = new Color(image.color.r, image.color.g, image.color.b, currentAlpha);
        board.color = new Color(board.color.r, board.color.g, board.color.b, currentAlpha);
        // 更新后处理音量
        postProcessVolume.weight = currentWeight;
    }
    //渐入
    public void FadeIn(BaseFightRole role)
    {
        image.sprite=images[role.Id];
        //设置可见
        postProcessVolume.gameObject.SetActive(true);
        // 设置目标透明度为1
        targetAlpha = 1f;
        // 激活后处理音量游戏对象
        postProcessVolume.gameObject.SetActive(true);
        // 设置目标权重为1
        targetWeight = 1f;
        // 重置计时
        timer = 0f;
    }
    //渐出
    public void FadeOut()
    {
        // 设置目标透明度为0
        targetAlpha = 0f;
        // 设置目标权重为0
        targetWeight = 0f;
        // 重置计时
        timer = 0f;
        // 销毁当前游戏对象
        this.gameObject.SetActive(false);
    }
    public void FadeInAndOUt(BaseFightRole role)
    {
        FadeIn(role);
        StartCoroutine(fadeOut());
    }

    IEnumerator fadeOut()
    {
        yield return new WaitForSeconds(3f);
        //设置隐藏
        postProcessVolume.gameObject.SetActive(false);
        // 设置目标透明度为0
        targetAlpha = 0f;
        // 设置目标权重为0
        targetWeight = 0f;
        // 重置计时
        timer = 0f;
    }
}
