using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Android.Gradle;
using UnityEngine;
    
// 아키텍처: 설계 그 잡채(설계마다 철학이 있다.)
// 디자인 패턴: 설계를 구현하는 과정에서 쓰이는 패턴

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager Instance;

    private Dictionary<ECurrencyType, Currency> _currencies;

    public CurrencyRepository _repository;

    public Action OnDataChanged;

    // 마틴 아저씨: 미리하는 성능 최적화의 90%는 의미가 없다.
    // public event Action OnGoldChanged;
    // public event Action OnDiamondChanged;


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


    //초기화
    public void Init()
    {
        _currencies = new Dictionary<ECurrencyType, Currency>();
        _repository = new CurrencyRepository();

        List<CurrencyDTO> loadedCurrencies = _repository.Load();
        if(loadedCurrencies == null)
        for(int i = 0; i < (int)ECurrencyType.Count; i++)
        {
            ECurrencyType type = (ECurrencyType)i;
            Currency currency = new Currency(type, 0);
            _currencies.Add(type, currency);
        }
        else
        {
            foreach (var data in loadedCurrencies)
            {
                Currency currency = new Currency(data.Type, data.Value);
                _currencies.Add(currency.Type, currency);
            }
        }
    }

    private List<CurrencyDTO> ToDtoList()
    {
        return _currencies.ToList().ConvertAll(currency => new CurrencyDTO(currency.Value));
    }

    public CurrencyDTO Get(ECurrencyType type)
    {
        return new CurrencyDTO (_currencies[type]);
    }

    //더하기
    public void Add(ECurrencyType type, int value)
    {
        _currencies[type].Add(value);

        //save

        _repository.Save(ToDtoList());
        //콜백

        OnDataChanged?.Invoke();
    }


    //사기

    public bool TryBuy(ECurrencyType Type, int value)
    {
        if (!_currencies[Type].TryBuy(value))
        {
            return false;
        }

        _repository.Save(ToDtoList());
        //콜백
        OnDataChanged?.Invoke();
        return true;
    }
}
