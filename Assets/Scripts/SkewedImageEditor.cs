using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace POP.UI.Common.Editor
{
    [CustomEditor(typeof(SkewedImage)), CanEditMultipleObjects]
    public class SkewedImageEditor : UnityEditor.UI.ImageEditor
    {
        SkewedImage skewedImage = null;


        protected override void OnEnable()
        {
            base.OnEnable();
            skewedImage = (SkewedImage)target;

        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();


            EditorGUI.BeginChangeCheck();

            skewedImage.SkewX = EditorGUILayout.FloatField("Skew X", skewedImage.SkewX);
            skewedImage.SkewY = EditorGUILayout.FloatField("Skew Y", skewedImage.SkewY);

            if (EditorGUI.EndChangeCheck())
            {
                foreach (var t in targets)
                {
                    if (t is SkewedImage skewed)
                    {
                        skewed.SkewX = skewedImage.SkewX;
                        skewed.SkewY = skewedImage.SkewY;

                        if (skewed.fillAmount != 0f)
                        {
                            skewed.SetVerticesDirty();
                            EditorUtility.SetDirty(t);
                        }
                    }
                }
            }
        }
    }
}
