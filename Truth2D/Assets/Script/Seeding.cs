using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Seeding : MonoBehaviour
{
    public SpriteRenderer m_SpriteRenderer;
    public Sprite seeding;
    public Sprite flower;

    public SeedType m_SeedType = SeedType.seed;

    public enum SeedType
    {
        seed,
        flower,
    }

    public void MouseClick()
    {
        if(m_SeedType == SeedType.seed)
        {
            m_SpriteRenderer.sprite = flower;
            m_SeedType = SeedType.flower;
        }
        else if (m_SeedType == SeedType.flower)
        {
            m_SpriteRenderer.sprite = seeding;
            m_SeedType = SeedType.seed;
        }
    }
}
