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



            [MenuItem("Window/UnityOverdrawKun")]
            public static void Create()
            {
                var window = (OverdrawKunWindow)EditorWindow.GetWindow(typeof(OverdrawKunWindow));
                window.titleContent = Style.TitleContent;
                window.Show();
            }

            public void OnEnable()
            {
                m_prots = new List<float>();
            }


            private void OnGUI()
            {
                EditorGUILayout.BeginHorizontal();
                var contentSize = EditorStyles.miniButtonMid.CalcSize(Style.OpenFolderContens);
                if (GUILayout.Button(Style.OpenFolderContens,EditorStyles.miniButtonLeft,GUILayout.MaxWidth(contentSize.x+10)))
                {
                    // データの取り込み
                    m_path = EditorUtility.OpenFolderPanel("Load png Texture of Directory", m_path, "");
                    if (!string.IsNullOrEmpty(m_path))
                    {
                        m_folderName = System.IO.Path.GetFileName(m_path);
                        Analyze(m_path, out m_fpaths, out m_avgs, out m_totals, out m_textures);
                        mSlider = 0;
                    }
                }

                contentSize = EditorStyles.miniButtonMid.CalcSize(Style.SaveAsContens);
                if(GUILayout.Button(Style.SaveAsContens, EditorStyles.miniButtonRight, GUILayout.MaxWidth(contentSize.x + 10)))
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
                    for(var i = 0; i < count; i++)
                    {
                        m_prots.Add(m_avgs[i+ofst]);
                    }
                    // グラフの描画
                    GraphFieldFloat(new GUIContent(m_folderName), m_prots, m_avgs.Average(),m_avgs.Max());
                                        
                    // 画像の表示
                    var texture = m_textures[mSlider];                    
                    var r1 = EditorGUILayout.GetControlRect(true,0);                    
                    var h = position.height - (r1.y + r1.height) - 30.0f;
                    var r2 = new Rect(r1.x, r1.y, r1.width, h);                   
                    EditorGUI.DrawPreviewTexture(r2, m_textures[mSlider], null, ScaleMode.ScaleToFit);


                    // スライダーの描画
                    r1 = new Rect(r2.x, r2.y + r2.height, r2.width,20);                    
                    mSlider = EditorGUI.IntSlider(r1,mSlider, 0, m_fpaths.Count - 1);
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


            static public void GraphFieldFloat(GUIContent content, List<float> list, float avg,float max)
            {

                if (content != null)
                {
                    EditorGUILayout.LabelField(content);
                }
                var area = GUILayoutUtility.GetRect(Mathf.Min(EditorGUIUtility.currentViewWidth, 300f), 200.0f);
                EditorGUI.DrawRect(area, UnityEngine.Color.gray);



                if (list.Count != 0)
                {
                    var maxValue = list.Max();
                    var avgValue = list.Average();
                    var scale = area.height / max * 0.90f; // 最大値の高さが描画範囲の80%位に

                    for (var i = 0; i < list.Count; i++)
                    {
                        var w = 4f;
                        var h = list[list.Count - (i + 1)] * scale;
                        var x = area.x + EditorGUIUtility.currentViewWidth - (i + 1) * w - (w / 2.0f); // 太さの半分をオフセットする
                        var y = area.y + area.height;
                        var rect = new Rect(x, y, w, -h);


                        if(float.Equals(max, list[list.Count - (i + 1)]))
                        {
                            EditorGUI.DrawRect(rect, Color.red);
                        }
                        else if (i % 2 == 0)
                        {
                            EditorGUI.DrawRect(rect, new Color32(87, 166, 74, 255));
                        } else
                        {
                            EditorGUI.DrawRect(rect, new Color32(17, 61, 111, 255));
                        }
                    }

                    // 最大値の補助線
                    {
                        var x = area.x;
                        var y = area.y + area.height - max * scale;
                        var w = area.width;
                        var h = 1.0f;
                        EditorGUI.DrawRect(
                            new Rect(x, y, w, h),
                            Color.white

                            );
                        var label = new GUIContent(Format("Max:{0}", max));
                        var contentSize = EditorStyles.label.CalcSize(label);
                        EditorGUI.DrawRect(new Rect(x, y - contentSize.y / 2, contentSize.x, contentSize.y), Color.black);
                        EditorGUI.LabelField(new Rect(x, y - contentSize.y / 2, contentSize.x, contentSize.y), label);
                    }

                    // 平均値の補助線
                    {
                        var x = area.x;
                        var y = area.y + area.height - avg * scale;
                        var w = area.width;
                        var h = 1.0f;
                        EditorGUI.DrawRect(
                            new Rect(x, y, w, h),
                            Color.white

                            );
                        var label = new GUIContent(Format("Avg:{0}", avg));
                        var contentSize = EditorStyles.label.CalcSize(label);
                        EditorGUI.DrawRect(new Rect(x, y - contentSize.y / 2, contentSize.x, contentSize.y), Color.black);
                        EditorGUI.LabelField(new Rect(x, y - contentSize.y / 2, contentSize.x, contentSize.y), label);
                    }

                    // 現在値の表示
                    {                        
                        var label = new GUIContent(Format("Val:{0}", list[list.Count-1]));
                        var contentSize = EditorStyles.label.CalcSize(label);
                        var x = area.x;
                        var y = area.y + area.height - contentSize.y;
                        var w = area.width;
                        EditorGUI.DrawRect(new Rect(x, y - contentSize.y / 2, contentSize.x, contentSize.y), Color.black);
                        EditorGUI.LabelField(new Rect(x, y - contentSize.y / 2, contentSize.x, contentSize.y), label);
                    }
                }


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