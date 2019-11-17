using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[Serializable]
[PostProcess(typeof(GrayscaleRenderer), PostProcessEvent.AfterStack, "Custom/Grayscale")]
public sealed class Grayscale : PostProcessEffectSettings
{
    [Range(0f, 1f), Tooltip("Grayscale effect intensity.")]
    public FloatParameter blend = new FloatParameter { value = 0.5f };

    public override bool IsEnabledAndSupported(PostProcessRenderContext context)
    {
        return enabled.value && blend.value > 0f;
    }
}

public sealed class GrayscaleRenderer : PostProcessEffectRenderer<Grayscale>
{
    //创建渲染器时调用
    public override void Init()
    {
        base.Init();
    }

    //用于设置摄像机标志并请求深度图，运动矢量等。
    public override DepthTextureMode GetCameraFlags()
    {
        return DepthTextureMode.None;
    }

    //在调度“重置历史记录”事件时调用。主要用于时间效果，以清除历史记录缓冲区和其他内容。
    public override void ResetHistory()
    {
        m_ResetHistory = true;
    }

    //在渲染器被销毁时调用。如果需要，请在那里进行清理
    public override void Release()
    {
        ResetHistory();
    }

    //当效果被渲染时，<see cref="PostProcessLayer"/>调用的渲染方法。
    public override void Render(PostProcessRenderContext context)
    {
        var sheet = context.propertySheets.Get(Shader.Find("Hidden/Custom/Grayscale"));
        sheet.properties.SetFloat("_Blend", settings.blend);
        context.command.BlitFullscreenTriangle(context.source, context.destination, sheet, 0);
    }
}