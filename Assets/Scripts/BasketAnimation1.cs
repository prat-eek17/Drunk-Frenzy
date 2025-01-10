using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BasketAnimation : MonoBehaviour
{
    public Image m_Image;
    public Sprite[] m_SpriteArray;
    public float m_Speed = 0.035f;
    private int m_IndexSprite;
    private Coroutine m_CoroutineAnim;
    private bool IsDone;
    private bool IsDoneChecker;

    public GameObject basketLeft;
    public GameObject basketRight;
    private float xOffset = 0.16f; // X offset for the image spawn position
    private float yOffset = -0.3f; // Y offset for the image spawn position

    // Start is called before the first frame update
    void Start()
    {
        IsDoneChecker = true;
        // Set the initial position of m_Image to the position of basketLeft with offsets
        m_Image.rectTransform.position = new Vector3(basketLeft.transform.position.x + xOffset, basketLeft.transform.position.y + yOffset, basketLeft.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        // Check which basket is visible and flip the sprite accordingly
        if (basketLeft.activeSelf)
        {
            xOffset = 0.16f;
            m_Image.rectTransform.localScale = new Vector3(-1, 1, 1);
            // Set the position of m_Image to the position of basketLeft with offsets
            m_Image.rectTransform.position = new Vector3(basketLeft.transform.position.x + xOffset, basketLeft.transform.position.y + yOffset, basketLeft.transform.position.z);
            
            if(IsDoneChecker){
                Func_PlayUIAnim();
                IsDoneChecker = false;    
            }
        }
        else if (basketRight.activeSelf)
        {
            xOffset = -0.16f;
            m_Image.rectTransform.localScale = new Vector3(1, 1, 1);
            // Set the position of m_Image to the position of basketRight with offsets
            m_Image.rectTransform.position = new Vector3(basketRight.transform.position.x + xOffset, basketRight.transform.position.y + yOffset, basketRight.transform.position.z);
            
            if(!IsDoneChecker){
                Func_PlayUIAnim();
                IsDoneChecker = true;    
            }
        }
    }

    public void Func_PlayUIAnim()
    { 
        IsDone = false;
        m_CoroutineAnim = StartCoroutine(Func_PlayAnimUI());
    }

    public void Func_StopUIAnim()
    {
        IsDone = true;  
        StopCoroutine(m_CoroutineAnim);
    }

    IEnumerator Func_PlayAnimUI()
    {
        while (!IsDone)
        {
            yield return new WaitForSeconds(m_Speed);

            // Increment sprite index
            m_IndexSprite++;
            if (m_IndexSprite >= m_SpriteArray.Length)
            {
                m_IndexSprite = 0;
                Func_StopUIAnim(); // Stop the animation when all frames are played
                yield break; // Exit the coroutine
            }

            // Set the sprite
            m_Image.sprite = m_SpriteArray[m_IndexSprite];
        }
    }
}