using BingoBlitzClone.Gameplay;
using UnityEditor;
using UnityEngine;

namespace BingoBlitzClone.Editor
{
    [CustomPropertyDrawer(typeof(Combination))]
    public class CombinationDrawer : PropertyDrawer
    {
        private const int GridSize = 5;
        private const float CellSize = 20f;
        private const float Padding = 2f;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            var cells = property.FindPropertyRelative("cells");

            if (cells.arraySize != GridSize * GridSize)
            {
                cells.arraySize = GridSize * GridSize;
            }

            position.height = CellSize;

            EditorGUI.LabelField(position, label);
            position.y += CellSize + Padding;

            for (int y = 0; y < GridSize; y++)
            {
                for (int x = 0; x < GridSize; x++)
                {
                    int index = x * GridSize + y;

                    Rect cellRect = new Rect(
                        position.x + x * (CellSize + Padding),
                        position.y + y * (CellSize + Padding),
                        CellSize,
                        CellSize
                    );

                    SerializedProperty cell = cells.GetArrayElementAtIndex(index);
                    cell.boolValue = EditorGUI.Toggle(cellRect, cell.boolValue);
                }
            }

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return (CellSize + Padding) * (GridSize + 1);
        }
    }
}