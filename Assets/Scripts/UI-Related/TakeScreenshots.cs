using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class TakeScreenshots : MonoBehaviour {

    [SerializeField] GameObject m_confirmationPanel;

    string m_defaultPath;

    Texture2D m_screenshotTexture;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    public void TakeSnapshot() {
        new WaitForEndOfFrame();
        
        string screenshotName = "SumusunodScreenshot" + System.DateTime.Now.ToString("yyyyMMdd-HHmmss") + ".png";

#if UNITY_EDITOR
        ScreenshotEditorVersion(screenshotName);
#else
        ScreenshotAndroidVersion(screenshotName);
#endif
    }

    IEnumerator ConfirmScreenshot(string path) {

        yield return new WaitForSeconds(1.5f);
        m_confirmationPanel.SetActive(true);
    }

    void ScreenshotEditorVersion(string screenshotName) {
        m_defaultPath = "../Augmented_Focus_screenshots/" + screenshotName;

        /// This doesn't work on Android, only here for testing/debugging:
        Application.CaptureScreenshot(m_defaultPath);

        if (File.Exists(m_defaultPath)) {
            Debug.Log("inside 'if (File.Exists(m_defaultPath))'.");
            WWW loadedImage = new WWW("file://" + m_defaultPath);
            StartCoroutine(ConfirmScreenshot(m_defaultPath));
        }
    }

    void ScreenshotAndroidVersion(string screenshotName) {
        //m_defaultPath = System.IO.Path.Combine(Application.persistentDataPath, "/Sumusunod/SumusunodScreenshot" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".png");
        //m_defaultPath = System.IO.Path.Combine(Application.persistentDataPath, "/Sumusunod/" + screenshotName);
        /// Trying to use a custom directory like above doesn't work; that's why I just used the default.
        m_defaultPath = System.IO.Path.Combine(Application.persistentDataPath, screenshotName);

        //string myFolderLocation = "/mnt/sdcard/Pictures/Screenshots/Sumusunod";
        string myFolderLocation = "/storage/sdcard0/Pictures/Screenshots/Sumusunod";
        string myScreenshotLocation = myFolderLocation + screenshotName;

        /// ENSURE THAT FOLDER LOCATION EXISTS
        if (!System.IO.Directory.Exists(myFolderLocation)) {
                System.IO.Directory.CreateDirectory(myFolderLocation);
        }

        //Application.CaptureScreenshot(m_defaultPath);

        /// This works on Android:
        /// These next 2 lines take the screenshot:
        m_screenshotTexture = new Texture2D(Screen.width, Screen.height, TextureFormat.ARGB32, true);
        m_screenshotTexture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0, true);
        m_screenshotTexture.Apply();

        /// These next 2 lines save the screenshot:
        //byte[] numberOfBytes = m_screenshotTexture.EncodeToPNG();
        //File.WriteAllBytes(m_defaultPath, numberOfBytes);
        //Application.CaptureScreenshot(screenshotName);
        Application.CaptureScreenshot(m_defaultPath);

        /// MOVE THE SCREENSHOT WHERE WE WANT IT TO BE STORED
        System.IO.File.Move(m_defaultPath, myScreenshotLocation);
        
        /// These refresh the Android gallery:
        AndroidJavaClass classPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject objActivity = classPlayer.GetStatic<AndroidJavaObject>("currentActivity");

        AndroidJavaClass classUri = new AndroidJavaClass("android.net.Uri");
        AndroidJavaObject objIntent = new AndroidJavaObject("android.content.Intent", new object[2] { "android.intent.action.MEDIA_MOUNTED", classUri.CallStatic<AndroidJavaObject>("parse", "file://" + myScreenshotLocation) });
        objActivity.Call("sendBroadcast", objIntent);
        //REFRESHING THE ANDROID PHONE PHOTO GALLERY IS COMPLETE

        //AUTO LAUNCH/VIEW THE SCREENSHOT IN THE PHOTO GALLERY
        Application.OpenURL(myScreenshotLocation);
        //AFTERWARDS IF YOU MANUALLY GO TO YOUR PHOTO GALLERY, 
        //YOU WILL SEE THE FOLDER WE CREATED CALLED "myFolder"
        
        if (File.Exists(myScreenshotLocation)) {
            WWW loadedImage = new WWW("file://" + myScreenshotLocation);
            StartCoroutine(ConfirmScreenshot(myScreenshotLocation));
        }
    }
}
