using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectController : MonoBehaviour
{
    public GameObject waterDripEffectFront = null;
    public GameObject waterDripEffectBack = null;
    public AudioSource waterDripSE = null;
    public GameObject flowerGrown = null;
    public AudioSource flowerGrownSE = null;
    public AudioSource flowerGrownMaxSE = null;
    public GameObject flowerTimber = null;
    public AudioSource flowerTimberSE = null;
    public GameObject lionEat = null;
    public AudioSource lionEatSE = null;
    public static EffectController m_instance = null;
    [HeaderAttribute("Background music")]
    public AudioClip happyMusic = null;
    public AudioClip peaceMusic = null;
    private AudioSource myBGM = null;
    // Start is called before the first frame update

    private void Awake()
    {
        if(m_instance == null)
            m_instance = this;
    }
    void Start()
    {
        m_instance = this;
        myBGM = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static void PlayWaterDripEffect(Transform sourcePos, float duration)
    {
        AudioSource SE = Instantiate(m_instance.waterDripSE, sourcePos);
        GameObject frontEffect = (GameObject)Instantiate(m_instance.waterDripEffectFront, sourcePos);
        GameObject backEffect = (GameObject)Instantiate(m_instance.waterDripEffectBack, sourcePos);

        Destroy(SE, duration);
        Destroy(frontEffect, duration);
        Destroy(backEffect, duration);
    }
    public static void PlayFlowerGrownEffect(Transform sourcePos, float duration)
    {

    }
    public static void PlayFlowerTimberEffect(Transform sourcePos, float duration)
    {
        AudioSource SE = Instantiate(m_instance.flowerTimberSE, sourcePos);
        GameObject effect = (GameObject)Instantiate(m_instance.flowerTimber, sourcePos);

        Destroy(SE, duration);
        Destroy(effect, duration);
    }
    public static void PlayLionEatEffect(Transform sourcePos, float duration)
    {
        AudioSource SE = Instantiate(m_instance.lionEatSE, sourcePos);
        GameObject effect = (GameObject)Instantiate(m_instance.lionEat, sourcePos);

        Destroy(SE, duration);
        Destroy(effect, duration);
    }
    public static void PlayHappyBGM()
    {
        m_instance.myBGM.clip = m_instance.happyMusic;
        m_instance.myBGM.Play();
    }
    public static void PlayNormalBGM()
    {
        m_instance.myBGM.clip = m_instance.peaceMusic;
        m_instance.myBGM.Play();
    }
}
