using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TapSprite : MonoBehaviour
{
    public Image m_Image;
    public Sprite[] m_SpriteArray;
    public float m_Speed = 0.035f;

    private int m_IndexSprite;
    private Coroutine m_CoroutineAnim;

    public void Start()
    {
        // Set the initial position of m_Image
        Func_PlayUIAnim();
    }



    private void Func_PlayUIAnim()
    {
        m_CoroutineAnim = StartCoroutine(Func_PlayAnimUI());
    }

    private void Func_StopUIAnim()
    {
        StopCoroutine(m_CoroutineAnim);
    }

    public IEnumerator Func_PlayAnimUI()
    {
        while (true)
        {
            yield return new WaitForSeconds(m_Speed);

            // Increment sprite index
            m_IndexSprite++;
            if (m_IndexSprite >= m_SpriteArray.Length)
            {
                m_IndexSprite = 0;
            }

            // Set the sprite
            m_Image.sprite = m_SpriteArray[m_IndexSprite];
        }
    }
}
