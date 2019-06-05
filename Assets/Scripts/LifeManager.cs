using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public enum OnAllLivesLost
{
    Destroy,
    ReturnToMenu
}

public class LifeManager : MonoBehaviour
{

    public bool IsActive = true;

    public int MaximumLives = 1;

    public OnAllLivesLost OnAllLivesLostAction = OnAllLivesLost.Destroy;

    public int Points = 50;

    //public LifePanel LifeGridPanel;

    public AudioClip HitAudioClip;

    [Header("Explosion Effect Data")]

    public Transform ExplosionPrefab;
    public Vector2 ExplosionScale;

    protected bool isActive;

    protected int lives;

    protected Transform self;
    protected ShootingManager selfShootingManager;

    private void Awake()
    {

        isActive = IsActive;
        lives = MaximumLives;

        self = transform;
        selfShootingManager = GetComponent<ShootingManager>();

    }

    private void Start()
    {
      //  if (LifeGridPanel)
        //    LifeGridPanel.RefreshLifeImages(lives);
    }

    private void AddToScore()
    {
        //GameManager.GM.AddToScore(Points);
    }

    public virtual void Hit()
    {

        if (isActive == false)
            return;

        lives--;
        
        //if (LifeGridPanel)
          //  LifeGridPanel.RefreshLifeImages(lives);

        if (HitAudioClip)
            AudioSource.PlayClipAtPoint(HitAudioClip, self.position);
        
        if (lives == 0)
        {

            if (OnAllLivesLostAction == OnAllLivesLost.Destroy)
            {

                AddToScore();
                /*
                if (ExplosionPrefab)
                {
                    Transform explosion = Instantiate(ExplosionPrefab, self.position, Quaternion.identity);
                    if (ExplosionScale != Vector2.zero)
                        explosion.localScale = Vector2.one * ExplosionScale.RandomValue();
                    explosion.eulerAngles = new Vector3(0, 0, new Vector2(0, 360).RandomValue());
                }*/

                Destroy(gameObject);

            }

            if (OnAllLivesLostAction == OnAllLivesLost.ReturnToMenu)
            {
                
                //GameManager.GM.ResetScore();
                GameManager.GM.ReturnToMenu();
            }
        }

        else
        {
            if (selfShootingManager)
                selfShootingManager.ResetLaser();
        }

    }

}
