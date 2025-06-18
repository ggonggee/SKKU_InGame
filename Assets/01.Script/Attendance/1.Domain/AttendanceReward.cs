using System;
using UnityEngine;

public class AttendanceReward
{
    public readonly ECurrencyType CurrencyType;
    public readonly int Amount;
    public bool Claimed { get; private set; }

    public AttendanceReward(ECurrencyType currencyType, int amount, bool claimed)
    {
        if (amount < 0)
        {
            throw new Exception("출석 보상량은 0보다 작을 수 없습니다.");
        }
        
        CurrencyType = currencyType;
        Amount = amount;
        Claimed = claimed;
    }

    public bool TryClaim()
    {
        if (Claimed == true)
        {
            return false;
        }

        Claimed = true;

        return true;
    }
}