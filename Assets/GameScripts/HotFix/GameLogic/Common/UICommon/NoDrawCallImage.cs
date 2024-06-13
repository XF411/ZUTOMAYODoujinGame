using UnityEngine.UI;

public class NoDrawCallImage : Image
{
    protected override void OnPopulateMesh(VertexHelper toFill)
    {
        toFill.Clear();
    }
}
