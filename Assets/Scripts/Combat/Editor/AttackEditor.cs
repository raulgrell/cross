using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Serialization;
using UnityEngine.SocialPlatforms;


[CustomEditor(typeof(UnitAttack), true)]
public class UnitAttackEditor : Editor
{
    public bool showTargets = true;

    Dictionary<Vector2Int, Target> targetMap;

    private void OnEnable()
    {
        UnitAttack attack = (UnitAttack) target;
        targetMap = new Dictionary<Vector2Int, Target>();

        foreach (var t in attack.targets)
        {
            targetMap.Add(t.position, t);
        }
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        UnitAttack attack = (UnitAttack) target;

        EditorGUILayout.Space();

        showTargets = EditorGUILayout.Foldout(showTargets, $"Targets ({attack.targets.Count})");
        if (showTargets)
        {
            EditorGUI.indentLevel = 0;

            GUIStyle tableStyle = new GUIStyle("box");
            tableStyle.padding = new RectOffset(10, 10, 10, 10);
            tableStyle.margin.left = 32;

            GUIStyle headerColumnStyle = new GUIStyle {fixedWidth = 35};

            GUIStyle columnStyle = new GUIStyle();
            columnStyle.fixedWidth = 65;

            GUIStyle rowStyle = new GUIStyle();
            rowStyle.fixedHeight = 25;

            GUIStyle rowHeaderStyle = new GUIStyle();
            rowHeaderStyle.fixedWidth = columnStyle.fixedWidth - 1;

            GUIStyle columnHeaderStyle = new GUIStyle();
            columnHeaderStyle.fixedWidth = 30;
            columnHeaderStyle.fixedHeight = 25.5f;

            GUIStyle columnLabelStyle = new GUIStyle();
            columnLabelStyle.fixedWidth = rowHeaderStyle.fixedWidth - 6;
            columnLabelStyle.alignment = TextAnchor.MiddleCenter;
            columnLabelStyle.fontStyle = FontStyle.Bold;

            GUIStyle cornerLabelStyle = new GUIStyle();
            cornerLabelStyle.fixedWidth = 42;
            cornerLabelStyle.alignment = TextAnchor.MiddleRight;
            cornerLabelStyle.fontStyle = FontStyle.BoldAndItalic;
            cornerLabelStyle.fontSize = 14;
            cornerLabelStyle.padding.top = -5;

            GUIStyle rowLabelStyle = new GUIStyle();
            rowLabelStyle.fixedWidth = 25;
            rowLabelStyle.alignment = TextAnchor.MiddleRight;
            rowLabelStyle.fontStyle = FontStyle.Bold;

            GUIStyle enumStyle = new GUIStyle("popup");
            rowStyle.fixedWidth = 65;

            EditorGUILayout.BeginHorizontal(tableStyle);

            var cols = attack.spread * 2 + 1;
            var rows = attack.range + 1;

            for (int x = -1; x < cols; x++)
            {
                EditorGUILayout.BeginVertical((x == -1) ? headerColumnStyle : columnStyle);
                for (int y = -1; y < rows; y++)
                {
                    var pos = new Vector2Int(x - attack.spread, attack.range - y);

                    if (x == -1 && y == -1)
                    {
                        EditorGUILayout.BeginHorizontal(rowHeaderStyle);
                        EditorGUILayout.LabelField("", cornerLabelStyle);
                        EditorGUILayout.EndHorizontal();
                    }
                    else if (x == -1)
                    {
                        EditorGUILayout.BeginHorizontal(columnHeaderStyle);
                        EditorGUILayout.LabelField(pos.y.ToString(), rowLabelStyle);
                        EditorGUILayout.EndHorizontal();
                    }
                    else if (y == -1)
                    {
                        EditorGUILayout.BeginHorizontal(rowHeaderStyle);
                        EditorGUILayout.LabelField(pos.x.ToString(), columnLabelStyle);
                        EditorGUILayout.EndHorizontal();
                    }

                    if (pos.x == 0 && pos.y == 0)
                    {
                        EditorGUILayout.BeginHorizontal(rowHeaderStyle);
                        EditorGUILayout.LabelField("▲", columnLabelStyle);
                        EditorGUILayout.EndHorizontal();
                    }
                    else if (x >= 0 && y >= 0)
                    {
                        EditorGUILayout.BeginHorizontal(rowStyle);
                        if (targetMap.ContainsKey(pos))
                        {
                            var t = targetMap[pos];
                            t.effect = (EffectType) EditorGUILayout.EnumPopup(t.effect, enumStyle);
                            if (t.effect == EffectType.None)
                            {
                                attack.targets.Remove(t);
                                targetMap.Remove(pos);
                            }
                        }
                        else
                        {
                            var effect = (EffectType) EditorGUILayout.EnumPopup(EffectType.None, enumStyle);
                            if (effect != EffectType.None)
                            {
                                var t = new Target {damage = 0, effect = effect, position = pos};
                                attack.targets.Add(t);
                                targetMap[pos] = t;
                            }
                        }

                        EditorGUILayout.EndHorizontal();
                    }
                }

                EditorGUILayout.EndVertical();
            }

            EditorGUILayout.EndHorizontal();
        }

        if (GUI.changed)
        {
            attack.targets.Clear();
            attack.targets.AddRange(targetMap.Values);

            Undo.RecordObject(attack, "Edit attack");
            EditorUtility.SetDirty(attack);
        }
    }
}