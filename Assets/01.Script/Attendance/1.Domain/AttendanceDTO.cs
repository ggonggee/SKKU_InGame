using System;
using System.Collections.Generic;

public class AttendanceDTO
{
    public readonly string ID;
    public readonly int DayCount;
    public readonly DateTime LastAttendanceDate;
    public readonly List<AttendanceRewardDTO> Rewards;
    public readonly DateTime StartDate;

    public AttendanceDTO(string id, DateTime startDate, int dayCount, DateTime lastAttendanceDate, List<AttendanceReward> rewards)
    {
        ID = id;
        StartDate = startDate;
        DayCount = dayCount;
        LastAttendanceDate = lastAttendanceDate;
        Rewards = new List<AttendanceRewardDTO>(rewards.Count);

        for (var i = 0; i < rewards.Count; i++)
        {
            bool canClaim = !rewards[i].Claimed && i >= dayCount;
            
            Rewards.Add(new AttendanceRewardDTO(rewards[i].CurrencyType, rewards[i].Amount, rewards[i].Claimed, canClaim));
        }
    }
}