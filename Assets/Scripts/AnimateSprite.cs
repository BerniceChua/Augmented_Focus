using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimateSprite : MonoBehaviour {

    public Image m_image;
    public Sprite[] m_sprites;
    public float m_animSpeed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        StartCoroutine(NextSprite());
    }

    public IEnumerator NextSprite() {
        for (int i = 0; i < m_sprites.Length; i++) {
            m_image.sprite = m_sprites[i];
            Debug.Log(m_image.sprite);

            yield return new WaitForSecondsRealtime(m_animSpeed);
            Debug.Log("End of 'yield return'.");
        }
    }

}
