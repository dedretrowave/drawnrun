using System;
using System.Collections.Generic;
using CharacterGroup.Character.View;
using CharacterGroup.Presenter;
using Player;
using Points.Presenter;
using Points.View;
using UnityEngine;

public class LevelSceneInstaller : MonoBehaviour
{
    [Header("Characters")]
    [SerializeField] private Transform _initialCharactersContainer;
    [Header("UI")]
    [SerializeField] private PointsView _pointsView;
    [Header("Player")]
    [SerializeField] private Movement _movement;
    
    private CharacterGroupPresenter _characterGroup;
    private PointsPresenter _points;

    private void OnEnable()
    {
        List<CharacterView> initialCharacters = new(_initialCharactersContainer.GetComponentsInChildren<CharacterView>());

        _characterGroup = new(initialCharacters);
        _points = new(_pointsView);

        _movement.Move();

        _characterGroup.PointPickup += OnPointAdd;
        _characterGroup.NoCharactersLeft += OnLose;
        _characterGroup.FinishLineReached += OnWin;
    }

    private void OnDisable()
    {
        _characterGroup.Disable();
        
        _characterGroup.PointPickup -= OnPointAdd;
        _characterGroup.NoCharactersLeft -= OnLose;
        _characterGroup.FinishLineReached -= OnWin;
    }

    private void OnWin()
    {
        _movement.Stop();
        Debug.Log("WIN)))");
    }

    private void OnLose()
    {
        Debug.Log("LOSE(((");
    }

    private void OnPointAdd()
    {
        _points.Add();
    }
}