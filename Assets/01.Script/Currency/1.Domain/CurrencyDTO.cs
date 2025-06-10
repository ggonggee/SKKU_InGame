using JetBrains.Annotations;
using UnityEngine;

public class CurrencyDTO
{
    //변수
    public readonly ECurrencyType Type;
    public readonly int Value;

    //생성자
    public CurrencyDTO(Currency currency)
    {
        Type = currency.Type;
        Value = currency.Value;
    }

    //넘치는지 확인 메서드 bool
    public bool HaveEnough(int value)
    {
        return Value >= value;
    }
}
