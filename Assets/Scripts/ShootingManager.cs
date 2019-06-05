using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class ShootingManager : MonoBehaviour
{
    
    public LaserBeam LaserBeamPrefab;
    public float LaserBeamSpeed = 7.5f;

    [Header("Overheat Data")]

    public bool OverheatEnabled = true;
    public float OverheatPointsLevelZero = 3;
    public float OverheatTimeStep = 0.15f;
    public Slider OverheatSlider;
    public Image OverheatSliderFill;

    private int laserLevel = 0;

    private Dictionary<int, List<Vector3>> laserBeamDirections = new Dictionary<int, List<Vector3>>();
    private Transform laserBeamParent;

    private bool isContinuousShooting = false;

    private float overheatPoints;
    private bool isOverheated = false;

    private float timer;

    private Transform self;

    private void Awake()
    {

        self = transform;

        if (CompareTag("Player"))
        {
            laserBeamDirections.Add(0, new List<Vector3> { new Vector3(0.00f, 1, 0) });
            laserBeamDirections.Add(1, new List<Vector3> { new Vector3(-0.10f, 1, 0), new Vector3(0.10f, 1, 0) });
            laserBeamDirections.Add(2, new List<Vector3> { new Vector3(-0.20f, 1, 0), new Vector3(0.00f, 1, 0), new Vector3(0.20f, 1, 0) });
            laserBeamDirections.Add(3, new List<Vector3> { new Vector3(-0.30f, 1, 0), new Vector3(-0.10f, 1, 0), new Vector3(0.10f, 1, 0), new Vector3(0.30f, 1, 0) });
            laserBeamDirections.Add(4, new List<Vector3> { new Vector3(-0.40f, 1, 0), new Vector3(-0.20f, 1, 0), new Vector3(0.00f, 1, 0), new Vector3(0.20f, 1, 0), new Vector3(0.40f, 1, 0) });
        }

        else
        {
            laserBeamDirections.Add(0, new List<Vector3> { new Vector3(0.00f, -1, 0) });
        }

        laserBeamParent = transform;

    }

    private void Update()
    {

        if (OverheatEnabled == false)
            return;

        if (isContinuousShooting && isOverheated == false)
        {
            UpdateOverheatSlider();
            return;
        }

        timer += Time.deltaTime;

        if (timer >= OverheatTimeStep)
        {
            timer = 0;
            overheatPoints -= OverheatPointsLevelZero;
        }

        if (overheatPoints <= 0)
        {
            overheatPoints = 0;
            isOverheated = false;
            if (OverheatSliderFill)
                OverheatSliderFill.color = Color.white;
        }

        UpdateOverheatSlider();

    }

    private void UpdateOverheatSlider()
    {
        if (OverheatSlider)
            OverheatSlider.value = (float)overheatPoints / 100;
    }

    public void BoostLaser()
    {
        if (laserLevel < laserBeamDirections.Count - 1)
            laserLevel++;
    }

    public void ResetLaser()
    {
        laserLevel = 0;
    }

    public void SetContinousShooting(bool isContinous)
    {
        isContinuousShooting = isContinous;
    }

    public void SetLaserBeamParent(Transform parent)
    {
        laserBeamParent = parent;
    }

    public void Shoot()
    {

        if (LaserBeamPrefab && isOverheated == false)
        {

            foreach (Vector3 direction in laserBeamDirections[laserLevel])
            {
                LaserBeam laserBeam = Instantiate(LaserBeamPrefab, self.position, Quaternion.identity, laserBeamParent);
                laserBeam.Shoot(direction, LaserBeamSpeed);
            }

            if (OverheatEnabled)
            {
                overheatPoints += (OverheatPointsLevelZero + laserLevel);
                if (overheatPoints >= 100)
                {
                    overheatPoints = 100;
                    isOverheated = true;
                    if (OverheatSliderFill)
                        OverheatSliderFill.color = Color.red;
                    UpdateOverheatSlider();
                }
            }

        }

    }

}
