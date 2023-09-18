using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BoardLayout))]
public class CustomScriptEditor : Editor
{
    private BoardLayout boardLayout;
    static bool showBoardLayout = true;
    int sizeX;
    int sizeY;

    void OnEnable()
    {
        boardLayout = target as BoardLayout;

        if (boardLayout.boardCellsType != null)
        {
            sizeX = boardLayout.boardSizeX;
            sizeY = boardLayout.boardSizeY;
        }
    }

    private void OnValidate()
    {
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        EditorGUILayout.Space();

        if (GUILayout.Button("Validate Layout"))
        {
            sizeX = boardLayout.boardSizeX;
            sizeY = boardLayout.boardSizeY;
            boardLayout.boardCellsType = new CellType[boardLayout.boardSizeX * boardLayout.boardSizeY];
        }

        EditorGUILayout.Space();
        showBoardLayout = EditorGUILayout.Foldout(showBoardLayout, "Grid Layout:");

        if (showBoardLayout)
        {
            int index = -1;
            for (int row = 0; row < sizeX; row++) // Use boardLayout.boardSize aqui
            {
                EditorGUILayout.BeginHorizontal();
                for (int col = 0; col < sizeY; col++) // Use boardLayout.boardSize aqui
                {
                    index++; // Calcule o índice com base na linha e na coluna

                    try
                    {
                        boardLayout.boardCellsType[index] = (CellType)EditorGUILayout.EnumPopup(boardLayout.boardCellsType[index]);
                    }
                    catch { }
                }
                EditorGUILayout.EndHorizontal();
            }
        }

        EditorUtility.SetDirty(boardLayout);
    }
}
