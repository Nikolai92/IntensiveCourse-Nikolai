using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationScript : MonoBehaviour
{
    [SerializeField] private GameObject _greenCircle;
    [SerializeField] private GameObject _redCircle;

    [SerializeField] private ParticleSystem _particles;

    [SerializeField] private TowerPlacement tower;

    private IEnumerator coroutine;

    private bool _towerHasBeenPlaced = false;

    public void Start()
    {
        DeactivateParticles();
    }

    private void OnEnable()
    {
        TowerPlacement.placeTower += ActivateParticles;
        TowerPlacement.towerPlaced += DeactivateParticles;
    }

    private void OnDisable()
    {
        TowerPlacement.placeTower -= ActivateParticles;
        TowerPlacement.towerPlaced -= DeactivateParticles;
    }

    public void OnMouseEnter()
    {
        tower.SnapTower(this.gameObject.transform.position);

        if (_towerHasBeenPlaced == true)
        {
            _redCircle.gameObject.SetActive(true);
        }

        else
        {
            _greenCircle.gameObject.SetActive(true);
            _redCircle.gameObject.SetActive(false);
        }
    }

    public void OnMouseDown()
    {
        if (_towerHasBeenPlaced == false)
        {
            tower.PlaceTower(this);
            _towerHasBeenPlaced = true;
        }
    }

    public void OnMouseExit()
    {
        _greenCircle.gameObject.SetActive(false);
        _redCircle.gameObject.SetActive(true);
        coroutine = WaitAndDisable();
        StartCoroutine(coroutine);
    }

    public void ActivateParticles()
    {
        _particles.Play();
    }

    public void DeactivateParticles()
    {
        _particles.Stop();
    }

    IEnumerator WaitAndDisable()
    {
        yield return new WaitForSeconds(1.5f);
        _redCircle.gameObject.SetActive(false);
    }
}
