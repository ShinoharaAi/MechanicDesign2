using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
	[SerializeField] GameObject ghost;
	[SerializeField] float ghostDelay;

	public bool makeGhost = false; 

	private float ghostDelaySeconds;

    InputHandler InputHandler;

    void Awake()
    {
        InputHandler = GetComponent<InputHandler>();
		ghostDelaySeconds = ghostDelay;
    }

    private void Update()
    {
        if (ghostDelaySeconds > 0)
        {
            ghostDelaySeconds -= Time.deltaTime;
        }
        else if (makeGhost && InputHandler.m_b_InSlowMoActive)
        {
            GameObject currentGhost = Instantiate(ghost, transform.position, transform.rotation);
            Sprite currentSprite = GetComponent<SpriteRenderer>().sprite;
            currentGhost.transform.localScale = this.transform.localScale;
            currentGhost.GetComponent<SpriteRenderer>().sprite = currentSprite;
            ghostDelaySeconds = ghostDelay;
            Destroy(currentGhost, 0.5f);
        }
    }
}
