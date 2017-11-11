using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class TakeScreenshots : MonoBehaviour {

    [SerializeField] GameObject m_confirmationPanel;

    string m_path;

    Texture2D m_screenshotTexture;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public IEnumerator TakeSnapshot() {
        yield return new WaitForEndOfFrame();

        string screenshotName = "SumusunodScreenshot" + System.DateTime.Now.ToString("yyyyMMdd-HHmmss") + ".png";

#if UNITY_EDITOR
        m_path = "../Augmented_Focus_screenshots/" + screenshotName;

        /// This doesn't work on Android, only here for testing/debugging:
        Application.CaptureScreenshot(m_path);
#else
        //m_path = System.IO.Path.Combine(Application.persistentDataPath, "/Sumusunod/SumusunodScreenshot" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".png");
        //m_path = System.IO.Path.Combine(Application.persistentDataPath, "/Sumusunod/" + screenshotName);
        /// Trying to use a custom directory like above doesn't work; that's why I just used the default.
        m_path = System.IO.Path.Combine(Application.persistentDataPath, screenshotName);
        
        /// This works on Android:
        /// These next 2 lines take the screenshot:
        m_screenshotTexture = new Texture2D(Screen.width, Screen.height, TextureFormat.ARGB32, true);
        m_screenshotTexture.Apply();
        /// These next 2 lines save the screenshot:
        byte[] numberOfBytes = m_screenshotTexture.EncodeToPNG();
        File.WriteAllBytes(m_path, numberOfBytes);

        /// These refresh the Android gallery:
#endif
        AndroidJavaClass classPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject objActivity = classPlayer.GetStatic<AndroidJavaObject>("parse");

        if (File.Exists(m_path)) {
            WWW loadedImage = new WWW("file://" + m_path);
            StartCoroutine(ConfirmScreenshot(m_path));
        }

    }

    IEnumerator ConfirmScreenshot(string path) {

        yield return new WaitForSeconds(1.5f);
        m_confirmationPanel.SetActive(true);
    }

}
