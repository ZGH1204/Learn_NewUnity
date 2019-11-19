using UnityEngine;

public class InstancedColor : MonoBehaviour
{
    static MaterialPropertyBlock propertyBlock;
    [SerializeField]
    Color color = Color.white;

    private void Awake()
    {
        OnValidate();
    }

    void OnValidate()
    {
        if (propertyBlock == null)
        {
            propertyBlock = new MaterialPropertyBlock();
        }
        propertyBlock.SetColor(Shader.PropertyToID("_Color"), color);
        GetComponent<MeshRenderer>().SetPropertyBlock(propertyBlock);
    }
}
