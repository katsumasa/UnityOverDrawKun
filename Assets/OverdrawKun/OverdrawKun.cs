// (C) UTJ
using UnityEngine;

namespace Utj.OverdrawKun {

    [ExecuteInEditMode]
    [RequireComponent(typeof(Camera))]
    [DefaultExecutionOrder(100)]
    class OverdrawKun : MonoBehaviour
    {        
        public Camera MasterCamera = null;

        Camera thisCamera_ = null;
        protected Camera ThisCamera
        {
            get { return thisCamera_ = thisCamera_ ?? GetComponent<Camera>(); }
        }

        const string ReplacementShaderName = "Utj/OverdrawKun/Overdraw";

        static Shader replacementShader_ = null;
        protected static Shader ReplacementShader
        {
            get { return replacementShader_ = replacementShader_ ?? Shader.Find(ReplacementShaderName); }
        }

#if UNITY_EDITOR
        public enum STATE
        {
            IDLE = 0,
            RECORDING,
        };

        [SerializeField] int recordingInterval = 5;
        [SerializeField] int captureFramerate = 30;
        STATE state = STATE.IDLE;
        public STATE State
        {
            get { return state; }
        }        
        int recordNo;
        public int RecordNum
        {
            get { return recordNo; }
        }
        int counter;
        string fpath;
#endif


        void Start()
        {
            if(MasterCamera == null)
            {
                MasterCamera = Camera.main;
            }

            if (ThisCamera != null)
            {
                ThisCamera.clearFlags = CameraClearFlags.SolidColor;
                ThisCamera.backgroundColor = Color.clear;   // clear: (0,0,0,0)
                ThisCamera.SetReplacementShader(ReplacementShader, null);
            }
        }

        void OnPreRender()
        {
            if (ThisCamera != null && MasterCamera != null)
            {
                ThisCamera.transform.position = MasterCamera.transform.position;
                ThisCamera.transform.rotation = MasterCamera.transform.rotation;
                ThisCamera.transform.localScale = MasterCamera.transform.localScale;
                ThisCamera.rect = MasterCamera.rect;
                ThisCamera.fieldOfView = MasterCamera.fieldOfView;
                ThisCamera.nearClipPlane = MasterCamera.nearClipPlane;
                ThisCamera.farClipPlane = MasterCamera.farClipPlane;
            }
        }

#if UNITY_EDITOR
        private void OnPostRender()
        {
            if (state == STATE.RECORDING)
            {
                if (counter <= 0)
                {
                    var texture2D = new Texture2D(ThisCamera.pixelWidth,ThisCamera.pixelHeight,TextureFormat.RGB24, false);
                    if (texture2D != null) {
                        texture2D.ReadPixels(ThisCamera.pixelRect, 0, 0);
                        texture2D.Apply();
                        var bytes = texture2D.EncodeToPNG();
                        System.IO.File.WriteAllBytes(fpath + "/" + recordNo.ToString() + ".png", bytes);
                        recordNo++;
                        counter = recordingInterval;
                        texture2D = null;
                    }
                }
                else
                {
                    counter--;
                }
            }
        }


        public void BeginProfile()
        {
            if (state == STATE.IDLE)
            {
                Time.captureFramerate = captureFramerate;                                               
                recordNo = 0;
                counter = 0;
                var dateTimes = System.DateTime.Now.ToString("yyyyMMddHHmmss");
                fpath = Application.dataPath + "/../" + dateTimes;
                if (System.IO.Directory.Exists(fpath) == false)
                {
                    System.IO.Directory.CreateDirectory(fpath);
                }
                state = STATE.RECORDING;                
            }
        }


        public void EndProfile()
        {
            if (state == STATE.RECORDING)
            {
                state = STATE.IDLE;
            }            
        }
#endif
    }
} // namespace
