using UnityEngine;
using UnityEngine.UI;

namespace POP.UI.Common
{
    /// <summary>
    /// UI Image를 x,y로 기울일 수 있게 해주는 Image
    /// ref: https://answers.unity.com/questions/1074814/is-it-possible-to-skew-or-shear-ui-elements-in-uni.html
    /// </summary>
    public class SkewedImage : Image
    {
        public float SkewX;
        public float SkewY;

        protected override void OnPopulateMesh(VertexHelper vh)
        {
            base.OnPopulateMesh(vh);

            var height = rectTransform.rect.height;
            var width = rectTransform.rect.width;
            if (height == 0 || width == 0)
                return;

            var xskew = height * Mathf.Tan(Mathf.Deg2Rad * SkewX);
            var yskew = width * Mathf.Tan(Mathf.Deg2Rad * SkewY);

            var ymin = rectTransform.rect.yMin;
            var xmin = rectTransform.rect.xMin;
            UIVertex v = new UIVertex();
            var pivot = rectTransform.pivot;
            for (int i = 0; i < vh.currentVertCount; i++)
            {
                vh.PopulateUIVertex(ref v, i);
                var x = Mathf.Lerp(0, xskew, (v.position.y - ymin) / height);
                var y = Mathf.Lerp(0, yskew, (v.position.x - xmin) / width);
                var xAdd = -xskew * pivot.x;
                var yAdd = -yskew * pivot.y;
                v.position += new Vector3(x + xAdd, y + yAdd, 0);

                vh.SetUIVertex(v, i);
            }

        }
    }
}
