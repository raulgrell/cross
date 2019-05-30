using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Serialization;
using UnityEngine.SocialPlatforms;


[CustomEditor(typeof(SpecialAttack), true)]
public class SpecialAttackEditor : Editor
{
    public bool showTargets = true;

    Dictionary<Vector2Int, Target> targetMap;

    protected void OnEnable()
    {
        SpecialAttack attack = (SpecialAttack) target;
        targetMap = new Dictionary<Vector2Int, Target>();

        foreach (var t in attack.frames)
        {
            targetMap.Add(t.position, t);
        }
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        SpecialAttack attack = (SpecialAttack) target;

        showTargets = EditorGUILayout.Foldout(showTargets, $"Targets ({attack.frames.Count})");
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
            rowHeaderStyle.fixedHeight = 25;
            rowHeaderStyle.fixedWidth = columnStyle.fixedWidth - 1;

            GUIStyle columnHeaderStyle = new GUIStyle();
            columnHeaderStyle.fixedWidth = 30;
            columnHeaderStyle.fixedHeight = 25.5f;

            GUIStyle columnLabelStyle = new GUIStyle();
            columnLabelStyle.fixedWidth = rowHeaderStyle.fixedWidth - 6;
            columnLabelStyle.alignment = TextAnchor.MiddleCenter;
            columnLabelStyle.fontStyle = FontStyle.Bold;

            GUIStyle rowLabelStyle = new GUIStyle();
            rowLabelStyle.fixedWidth = 25;
            rowLabelStyle.alignment = TextAnchor.MiddleRight;
            rowLabelStyle.fontStyle = FontStyle.Bold;

            GUIStyle enumStyle = new GUIStyle("popup");
            rowStyle.fixedWidth = 65;

            EditorGUILayout.BeginHorizontal(tableStyle);

            var cols = attack.spread * 2 + 1;
            var rows = attack.range + 2;

            for (int x = -1; x < cols; x++)
            {
                EditorGUILayout.BeginVertical((x == -1) ? headerColumnStyle : columnStyle);
                for (int y = -1; y < rows; y++)
                {
                    var pos = new Vector2Int(x - attack.spread, attack.range - y);

                    if (x == -1 && y == -1)
                    {
                        EditorGUILayout.BeginHorizontal(rowHeaderStyle);
                        EditorGUILayout.LabelField("", columnLabelStyle);
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
                    else if (pos.x == 0 && pos.y == 0 || pos.x == 0 && pos.y - attack.distance <= 0 && pos.y > 0)
                    {
                        EditorGUILayout.BeginHorizontal(rowStyle);
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
                                attack.frames.Remove(t);
                                targetMap.Remove(pos);
                            }
                        }
                        else
                        {
                            var effect = (EffectType) EditorGUILayout.EnumPopup(EffectType.None, enumStyle);
                            if (effect != EffectType.None)
                            {
                                var t = new Target
                                {
                                    position = pos,
                                    effect = effect,
                                    damage = 1,
                                    knockback = 1,
                                    frame = 0,
                                };
                                attack.frames.Add(t);
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
            attack.frames.Clear();
            attack.frames.AddRange(targetMap.Values);

            Undo.RecordObject(attack, "Edit attack");
            EditorUtility.SetDirty(attack);
        }
    }
}