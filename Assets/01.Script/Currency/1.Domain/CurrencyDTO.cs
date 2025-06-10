using JetBrains.Annotations;
using UnityEngine;

public class CurrencyDTO
{
    //����
    public readonly ECurrencyType Type;
    public readonly int Value;

    //������
    public CurrencyDTO(Currency currency)
    {
        Type = currency.Type;
        Value = currency.Value;
    }

    //��ġ���� Ȯ�� �޼��� bool
    public bool HaveEnough(int value)
    {
        return Value >= value;
    }
}
