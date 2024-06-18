using UnityEngine.UI;
namespace GameLogic.Common
{
    public class NoDrawCallImage : Image
    {
        protected override void OnPopulateMesh(VertexHelper toFill)
        {
            toFill.Clear();
        }
    }
}
