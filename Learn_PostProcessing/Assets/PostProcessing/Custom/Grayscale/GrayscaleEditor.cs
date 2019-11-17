using UnityEngine.Rendering.PostProcessing;

namespace UnityEditor.Rendering.PostProcessing
{
    [PostProcessEditor(typeof(Grayscale))]
    public sealed class GrayscaleEditor : PostProcessEffectEditor<Grayscale>
    {
        SerializedParameterOverride m_Blend;

        public override void OnEnable()
        {
            m_Blend = FindParameterOverride(x => x.blend);
        }

        public override void OnInspectorGUI()
        {
            PropertyField(m_Blend);
        }
    }
}