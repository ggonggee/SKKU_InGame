public class AttendanceRewardDTO
{
    public readonly ECurrencyType CurrencyType;
    public readonly int Amount;
    public readonly bool Claimed;
    public readonly bool CanClaim;

    public AttendanceRewardDTO(ECurrencyType currencyType, int amount, bool claimed, bool canClaim)
    {
        CurrencyType = currencyType;
        Amount = amount;
        Claimed = claimed;
        CanClaim = canClaim;
    }
}