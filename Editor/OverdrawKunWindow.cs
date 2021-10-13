using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


namespace Utj
{
    namespace OverdrawKun
    {
        /// <summary>
        /// 測定したOverdrawのデータを解析する
        /// </summary>
        public class OverdrawKunWindow : EditorWindow
        {
            [System.Serializable]
            public class Style
            {
                public static readonly GUIContent TitleContent = new GUIContent("UnityOverdrawKun");
                public static readonly GUIContent OpenFolderContens = new GUIContent((Texture2D)EditorGUIUtility.Load("d_OpenedFolder Icon"),"Open Folder");
                public static readonly GUIContent SaveAsContens = new GUIContent((Texture2D)EditorGUIUtility.Load("d_SaveAs@2x"), "Save As CSV");                
            }


            List<string> m_fpaths;
            List<float> m_avgs;
            List<float> m_totals;
            List<Texture2D> m_textures;
            List<float> m_prots;
            string m_folderName;
            string m_path="";
            int mSlider;
            int mSelect;


            [MenuItem("Window/UTJ/UnityOverdrawKun")]
            public static void Create()
            {
                var window = (OverdrawKunWindow)EditorWindow.GetWindow(typeof(OverdrawKunWindow));
                window.titleContent = Style.TitleContent;
                window.wantsMouseMove = true;
                window.Show();
            }

            public void OnEnable()
            {
                m_prots = new List<float>();
            }


            void OnInspectorUpdate()
            {
                if (EditorWindow.mouseOverWindow)
                {
                    EditorWindow.mouseOverWindow.Focus();
                }

                this.Repaint();
            }


            private void OnGUI()
            {

                // TOOL BAR
                EditorGUILayout.BeginHorizontal();                

                // Load
                if (GUILayout.Button(Style.OpenFolderContens, EditorStyles.miniButtonLeft, GUILayout.Width(32),GUILayout.Height(32)))
                {
                    m_path = EditorUtility.OpenFolderPanel("Load png Texture of Directory", m_path, "");
                    if (!string.IsNullOrEmpty(m_path))
                    {
                        m_folderName = System.IO.Path.GetFileName(m_path);
                        Analyze(m_path, out m_fpaths, out m_avgs, out m_totals, out m_textures);
                        mSlider = 0;
                    }
                }

                // Save
                if (GUILayout.Button(Style.SaveAsContens, EditorStyles.miniButtonRight, GUILayout.Width(32),GUILayout.Height(32)))
                {
                    if(m_fpaths != null)
                    {
                        // CSV形式でデータを出力
                        var path = EditorUtility.SaveFilePanel("Save Overdraw as csv", m_path, m_folderName, "csv");
                        if(!string.IsNullOrEmpty(path))
                        {
                            var sw = new System.IO.StreamWriter(path, false);
                            for(var i = 0; i < m_fpaths.Count; i++)
                            {
                                var fname = System.IO.Path.GetFileName(m_fpaths[i]);
                                sw.WriteLine(string.Format("\"{0}\",{1}",fname,m_avgs[i]));
                            }
                            sw.Close();
                        }
                    }
                }
                EditorGUILayout.EndHorizontal();

                

                if (m_avgs != null && m_avgs.Count > 0)
                {
                    var ofst = 0;
                    var count = 0;
                    int w = (int)EditorGUIUtility.currentViewWidth;
                    ofst = 0;
                    count = mSlider + 1;
                    m_prots.Clear();
                    for (var i = 0; i < count; i++)
                    {
                        m_prots.Add(m_avgs[i + ofst]);
                    }                    

                    // グラフの描画
                    var rect = Graph(m_prots,m_avgs.Max());


                    if (rect.Contains(Event.current.mousePosition))
                    {
                        mSelect = (int)(Event.current.mousePosition.x - rect.x);
                    }


                    // 画像の表示
                    var texture = m_textures[mSlider];
                    var r1 = EditorGUILayout.GetControlRect(true, 0);
                    var h = position.height - (r1.y + r1.height) - 30.0f;
                    var r2 = new Rect(r1.x, r1.y, r1.width, h);
                    EditorGUI.DrawPreviewTexture(r2, m_textures[mSlider], null, ScaleMode.ScaleToFit);

                    // スライダーの描画
                    r1 = new Rect(r2.x, r2.y + r2.height, r2.width, 20);
                    mSlider = EditorGUI.IntSlider(r1, mSlider, 0, m_fpaths.Count - 1);
                } 
                else
                {
                    // 何も表示されていないと味気ないので
                    Graph(m_prots,0);

                    var r1 = EditorGUILayout.GetControlRect(true, 0);
                    var h = position.height - (r1.y + r1.height) - 30.0f;
                    var r2 = new Rect(r1.x, r1.y, r1.width, h);

                    EditorGUI.DrawRect(r2, new Color32(13, 99, 137,255));
                    r1 = new Rect(r2.x, r2.y + r2.height, r2.width, 20);
                     EditorGUI.IntSlider(r1, 0, 0, 0);
                }                                    
            }


            void Analyze(string path,out List<string> files,out List<float> avgs,out List<float>totals,out List<Texture2D> textures)
            {
                files = System.IO.Directory.GetFiles(path, "*.png").ToList();
                files = files.OrderByAlphaNumeric(e => e).ToList();                
                avgs = new List<float>();
                totals = new List<float>();
                textures = new List<Texture2D>();
                for(var i = 0; i < files.Count; i++)
                {
                    float total = 0.0f;
                    float avg = 0;
                    var texture = new Texture2D(2, 2);
                    var bytes = System.IO.File.ReadAllBytes(files[i]);                    
                    texture.LoadImage(bytes);
                    if (texture != null)
                    {
                        var colors = texture.GetPixels();
                        for (var y = 0; y < texture.height; y++)
                        {
                            for (var x = 0; x < texture.width; x++)
                            {
                                var c = colors[y * texture.width + x];
                                total += c.r;
                            }
                        }
                        avg = total / (texture.height * texture.width);
                    }
                    avgs.Add(avg);
                    totals.Add(total);
                    textures.Add(texture);
                }                
            }



            static public Rect Graph(List<float> srcs,float max)
            {
                var rect = GUILayoutUtility.GetRect(Mathf.Min(EditorGUIUtility.currentViewWidth, 300f), 200.0f);
                rect = new Rect(rect.x + 16, rect.y, rect.width - 32, rect.height);
                EditorGUI.DrawRect(rect, UnityEngine.Color.gray);


                int len = (int)rect.width;
                var index = srcs.Count - len;
                index = Mathf.Max(0, index);
                len = Mathf.Min(len, srcs.Count - index);

                var select = -1;

                // indexの位置からグラフに表示される範囲でリストを作成する
                var list = new List<float>();
                for (var i = 0; i < len; i++)
                {
                    list.Add(srcs[i + index]);
                }


                // 背景
                UnityEditor.EditorGUI.DrawRect(rect, UnityEngine.Color.gray);


                if (list.Count != 0)
                {
                    var minValue = list.Min();
                    var maxValue = list.Max();
                    var avgValue = list.Average();

                    /// 底上げされる可能性がある為、最小値・最大値・平均値を退避
                    var realMin = minValue;
                    var realMax = maxValue;
                    var realAvg = avgValue;

                    // 最小値が0より小さい場合、グラフ的には最小値が0となるように底上げする
                    if (realMin < 0f)
                    {
                        for (var i = 0; i < len; i++)
                        {
                            list[i] += Mathf.Abs(realMin);
                        }
                        minValue = list.Min();
                        maxValue = list.Max();
                        avgValue = list.Average();
                    }

                    // 最大値の高さが描画範囲の90%位に                                
                    var scale = rect.height / maxValue * 0.90f;

                    // グラフを描画
                    for (var i = 0; i < list.Count; i++)
                    {
                        var w = 1.0f;
                        var h = list[i] * scale;
                        var x = rect.x + rect.width - len + i * w;
                        var y = rect.y + rect.height - h;

                        var r = new Rect(x, y, w, h);

                        if (r.Contains(Event.current.mousePosition))
                        {
                            select = i;
                            UnityEditor.EditorGUI.DrawRect(r, Color.white);
                        } else if(list[i] == max)
                        {
                            UnityEditor.EditorGUI.DrawRect(r, Color.red);
                        }
                        else
                        {
                            UnityEditor.EditorGUI.DrawRect(r, Color.green);
                        }
                    }

                    // 最大値の補助線
                    {
                        var x = rect.x;
                        var y = rect.y + rect.height - maxValue * scale;
                        var w = rect.width;
                        var h = 1.0f;
                        DrawAdditionalLine(new Rect(x, y, w, h), realMax, Color.white);
                    }

                    // 平均値の補助線
                    {
                        var x = rect.x;
                        var y = rect.y + rect.height - avgValue * scale;
                        var w = rect.width;
                        var h = 1.0f;
                        DrawAdditionalLine(new Rect(x, y, w, h), realAvg, Color.white);
                    }

                    // 最小値の補助線
                    {
                        var x = rect.x;
                        var y = rect.y + rect.height - minValue * scale;
                        var w = rect.width;
                        var h = 1.0f;
                        DrawAdditionalLine(new Rect(x, y, w, h), realMin, Color.white);
                    }

                    // 選択された値を表示する
                    if (select >= 0 && select < list.Count)
                    {
                        var value = list[select];
                        if(realMin < 0f)
                        {
                            value -= Mathf.Abs(realMin);
                        }
                        var label = new GUIContent(Format("{0,3:F6}", value));
                        var contentSize = UnityEditor.EditorStyles.label.CalcSize(label);                        
                        var x = rect.x + rect.width - len + select * 1.0f - contentSize.x / 2;
                        var y = rect.y + rect.height - value * scale - contentSize.y;
                        var w = contentSize.x;
                        var h = contentSize.y;

                        var r = new Rect(x, y, w, h);
                        UnityEditor.EditorGUI.DrawRect(r, new Color32(0, 0, 0, 128));
                        UnityEditor.EditorGUI.LabelField(r, label);
                    }
                }


                return rect;
            }


            /// <summary>
            /// 補助線を引く
            /// </summary>
            /// <param name="rect"></param>
            /// <param name="value"></param>
            /// <param name="color"></param>
            static void DrawAdditionalLine(Rect rect, float value, Color color)
            {
                UnityEditor.EditorGUI.DrawRect(rect, color);
                var label = new GUIContent(Format("{0,3:F5}", value));
                var contentSize = UnityEditor.EditorStyles.label.CalcSize(label);
                var rect2 = new Rect(rect.x, rect.y - contentSize.y / 2, contentSize.x, contentSize.y);
                UnityEditor.EditorGUI.DrawRect(rect2, Color.black);
                UnityEditor.EditorGUI.LabelField(rect2, label);
            }


            public static string Format(string fmt, params object[] args)
            {
                return String.Format(System.Globalization.CultureInfo.InvariantCulture.NumberFormat, fmt, args);

            }            

        }


        public static class Extensions
        {
            // Natural sort.  When you use this extension method, "MyFile_2" is less than (<) "MyFile_10".
            // https://stackoverflow.com/a/11720793
            public static IOrderedEnumerable<T> OrderByAlphaNumeric<T>(this IEnumerable<T> source, Func<T, string> selector)
            {
                int max = source
                    .SelectMany(i => System.Text.RegularExpressions.Regex.Matches(selector(i), @"\d+")
                    .Cast<System.Text.RegularExpressions.Match>()
                    .Select(m => (int?)m.Value.Length))
                    .Max() ?? 0;
                return source.OrderBy(i => System.Text.RegularExpressions.Regex.Replace(selector(i), @"\d+", m => m.Value.PadLeft(max, '0')));
            }
        }
    }
}