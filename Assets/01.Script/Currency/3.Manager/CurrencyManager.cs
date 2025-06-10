using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Android.Gradle;
using UnityEngine;
    
// ��Ű��ó: ���� �� ��ä(���踶�� ö���� �ִ�.)
// ������ ����: ���踦 �����ϴ� �������� ���̴� ����

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager Instance;

    private Dictionary<ECurrencyType, Currency> _currencies;

    public CurrencyRepository _repository;

    public Action OnDataChanged;

    // ��ƾ ������: �̸��ϴ� ���� ����ȭ�� 90%�� �ǹ̰� ����.
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


    //�ʱ�ȭ
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

    //���ϱ�
    public void Add(ECurrencyType type, int value)
    {
        _currencies[type].Add(value);

        //save

        _repository.Save(ToDtoList());
        //�ݹ�

        OnDataChanged?.Invoke();
    }


    //���

    public bool TryBuy(ECurrencyType Type, int value)
    {
        if (!_currencies[Type].TryBuy(value))
        {
            return false;
        }

        _repository.Save(ToDtoList());
        //�ݹ�
        OnDataChanged?.Invoke();
        return true;
    }
}
