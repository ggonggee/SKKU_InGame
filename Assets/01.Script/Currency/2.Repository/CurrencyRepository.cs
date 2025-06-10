using System.Collections.Generic;
using UnityEngine;

public class CurrencyRepository
{
    // Repository: �������� ���Ӽ� ����
    // ���Ӽ�: ���α׷��� �����ص� �����Ͱ� �����Ǵ� ��

    private const string SAVE_KEY = nameof(CurrencyRepository);

    //Save
    public void Save(List<CurrencyDTO> dataList)
    {
        CurrencySaveData data = new CurrencySaveData();
        data.DataList = dataList;
        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString(SAVE_KEY, json);

    }

    public List<CurrencyDTO> Load()
    {
        if (!PlayerPrefs.HasKey(SAVE_KEY))
        {
            return null;
        }
        
        string json = PlayerPrefs.GetString(SAVE_KEY);
        CurrencySaveData data =  JsonUtility.FromJson<CurrencySaveData>(json);

        return data.DataList;
    }
}


//����Ʈ�� Ŭ����ȭ �ؼ� �����ϱ� ���� ��� ���δ�.
public class CurrencySaveData
{
    public List<CurrencyDTO> DataList;
}