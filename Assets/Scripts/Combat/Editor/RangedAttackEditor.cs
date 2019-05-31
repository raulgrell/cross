using System.Collections.Generic;
using Dialogue;
using UnityEngine;
using UnityEditor;
using UnityEngine.Serialization;
using UnityEngine.SocialPlatforms;


[CustomEditor(typeof(RangedAttack), true)]
public class RangedAttackEditor : Editor
{
    public bool showFrames = true;
    HashSet<Target> frameSet;

    protected void OnEnable()
    {
        RangedAttack attack = (RangedAttack) target;
        frameSet = new HashSet<Target>();

        foreach (var t in attack.frames)
        {
            frameSet.Add(t);
        }
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        RangedAttack attack = (RangedAttack) target;

        showFrames = EditorGUILayout.Foldout(showFrames, $"Frames");
        if (showFrames)
        {
            EditorGUI.indentLevel = 0;

            GUIStyle tableStyle = new GUIStyle("box");
            tableStyle.padding = new RectOffset(10, 10, 10, 10);
            tableStyle.margin.left = 32;

            GUIStyle headerColumnStyle = new GUIStyle {fixedWidth = 35};

            GUIStyle columnStyle = new GUIStyle();
            columnStyle.fixedWidth = 25;
            columnStyle.alignment = TextAnchor.MiddleCenter;

            GUIStyle rowStyle = new GUIStyle();
            rowStyle.fixedHeight = 25;
            rowStyle.alignment = TextAnchor.MiddleCenter;

            GUIStyle rowHeaderStyle = new GUIStyle();
            rowHeaderStyle.fixedWidth = columnStyle.fixedWidth - 1;

            GUIStyle columnHeaderStyle = new GUIStyle();
            columnHeaderStyle.fixedWidth = 25;
            columnHeaderStyle.fixedHeight = 25.5f;

            GUIStyle columnLabelStyle = new GUIStyle();
            columnLabelStyle.fixedWidth = rowHeaderStyle.fixedWidth - 6;
            columnLabelStyle.alignment = TextAnchor.MiddleCenter;
            columnLabelStyle.fontStyle = FontStyle.Bold;

            GUIStyle rowLabelStyle = new GUIStyle();
            rowLabelStyle.fixedWidth = 25;
            rowLabelStyle.alignment = TextAnchor.MiddleRight;
            rowLabelStyle.fontStyle = FontStyle.Bold;

            GUIStyle boolStyle = new GUIStyle("Toggle");
            boolStyle.fixedWidth = 25;
            boolStyle.alignment = TextAnchor.MiddleCenter;

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

                    if (pos.x == 0 && pos.y == 0)
                    {
                        EditorGUILayout.BeginHorizontal(rowStyle);
                        EditorGUILayout.LabelField("▲", columnLabelStyle);
                        EditorGUILayout.EndHorizontal();
                    }
                    else if (x >= 0 && y >= 0)
                    {
                        EditorGUILayout.BeginHorizontal(rowStyle);

                        var frame = new Target
                        {
                            position = pos,
                            effect = EffectType.Damage,
                            damage = 1,
                            knockback = 1,
                            frame = attack.currentFrame
                        };
                        
                        if (frameSet.Contains(frame))
                        {
                            var c = EditorGUILayout.Toggle(true, boolStyle);
                            if (!c)
                            {
                                attack.frames.Remove(frame);
                                frameSet.Remove(frame);
                            }
                        }
                        else
                        {
                            var c = EditorGUILayout.Toggle(false, boolStyle);
                            if (c)
                            {
                                attack.frames.Add(frame);
                                frameSet.Add(frame);
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
            attack.frames.AddRange(frameSet);
            
            Undo.RecordObject(attack, "Edit attack");
            EditorUtility.SetDirty(attack);
        }
    }
}