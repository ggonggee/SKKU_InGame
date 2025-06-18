using System;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance;
    
    // 도메인에 변화가 있을 때 호출되는 액션
    public event Action OnDataChanged;
    
    [SerializeField] 
    private List<StageLevelSO> _levelSOList;
    private Stage _stage;
    
    // Todo: StageDTO 반환하게끔
    public Stage Stage => _stage;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        Init();
    }

    private void Init()
    {
        _stage = new Stage(1, 2, 17, _levelSOList);
        OnDataChanged?.Invoke();
    }

    private void Update()
    {
        _stage.Progress(Time.deltaTime, OnDataChanged);
    }
}
