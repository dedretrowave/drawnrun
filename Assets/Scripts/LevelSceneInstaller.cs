using System.Collections.Generic;
using CharacterGroup.Character.View;
using CharacterGroup.Presenter;
using Drawing;
using Player;
using Points.Presenter;
using Points.View;
using Splines;
using UnityEngine;

public class LevelSceneInstaller : MonoBehaviour
{
    [Header("Core Gameplay")]
    [SerializeField] private Transform _initialCharactersContainer;
    [SerializeField] private Spline _spline;
    [SerializeField] private PointerDrawer _drawer;
    [Header("UI")]
    [SerializeField] private PointsView _pointsView;
    [SerializeField] private GameObject _startWindow;
    [SerializeField] private GameObject _winWindow;
    [SerializeField] private GameObject _loseWindow;
    [Header("Player")]
    [SerializeField] private Movement _movement;

    private bool _hasBegun;
    private CharacterGroupPresenter _characterGroup;
    private PointsPresenter _points;

    private void OnEnable()
    {
        List<CharacterView> initialCharacters = new(_initialCharactersContainer.GetComponentsInChildren<CharacterView>());

        _characterGroup = new(initialCharacters);
        _points = new(_pointsView);

        _characterGroup.PointPickup += OnPointAdd;
        _characterGroup.NoCharactersLeft += OnLose;
        _characterGroup.FinishLineReached += OnWin;
        _drawer.LineDrawn += OnLineDrawn;
    }

    private void OnDisable()
    {
        _characterGroup.Disable();
        
        _characterGroup.PointPickup -= OnPointAdd;
        _characterGroup.NoCharactersLeft -= OnLose;
        _characterGroup.FinishLineReached -= OnWin;
        _drawer.LineDrawn -= OnLineDrawn;
    }

    public void Begin()
    {
        _movement.Move();
    }

    private void OnLineDrawn(List<Vector3> points)
    {
        if (!_hasBegun)
        {
            _hasBegun = true;
            Begin();
            _startWindow.SetActive(false);
        }
        
        _characterGroup.MoveToPointsAll(_spline.Build(points));
    }

    private void OnWin()
    {
        _movement.Stop();
        _characterGroup.DanceAll();
        _winWindow.SetActive(true);
    }

    private void OnLose()
    {
        _movement.Stop();
        _loseWindow.SetActive(true);
    }

    private void OnPointAdd()
    {
        _points.Add();
    }
}