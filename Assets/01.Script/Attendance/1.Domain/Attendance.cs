using System;
using System.Collections.Generic;
using UnityEngine;

public class Attendance
{
    public readonly string ID;
    public readonly DateTime StartDate;                         // 이 출석(이벤트) 언제부터 시작할 것인가?
    public int DayCount { get; private set; }                   // 출석일
    public DateTime LastAttendanceDate { get; private set; }    // 마지막 출석일

    private List<AttendanceReward> _rewards;
  
    public Attendance(string id, DateTime startDate, DateTime lastAttendanceDate, int dayCount)
    {
        if (string.IsNullOrEmpty(id))
        {
            throw new Exception("ID는 비어있을 수 없습니다.");
        }
        
        if (startDate == new DateTime())
        {
            throw new Exception("출석 시작일 startDate가 지정되지 않았습니다.");
        }

        if (dayCount <= 0)
        {
            throw new Exception("출석일은 0보다 작을 수 없습니다.");
        }

        if (lastAttendanceDate == new DateTime())
        {
            throw new Exception("출석 시작일 startDate가 지정되지 않았습니다.");
        }

        if (lastAttendanceDate < startDate)
        {
            throw new Exception("출석일을 이벤트 시작일보다 작을 수 없습니다.");
        }

        ID = id;
        StartDate = startDate;
        DayCount = dayCount;
        LastAttendanceDate = lastAttendanceDate;
        
        _rewards = new List<AttendanceReward>();
    }

    public void Check(DateTime date)
    {
        if (date == new DateTime())
        {
            throw new Exception("출석 체크하는 date가 지정되지 않앗습니다.");
        }
        
        // ToDo: year과 month도 비교한다.
        if (LastAttendanceDate.Day < date.Day)
        {
            DayCount += 1;
            LastAttendanceDate = date;
        }
    }

    public void AddReward(AttendanceReward reward)
    {
        if (reward == null)
        {
            throw new Exception("출석 보상은 null일 수 없습니다.");
        }
        
        _rewards.Add(reward);
    }

    public AttendanceRewardDTO GetReward(int index)
    {
        if (index < 0 || _rewards.Count <= index)
        {
            throw new Exception("유효하지 않은 보상 범위입니다.");
        }

        AttendanceReward reward = _rewards[index];
        return new AttendanceRewardDTO(reward.CurrencyType, reward.Amount, reward.Claimed, CanClaim(index));
    }

    public bool CanClaim(int index)
    {
        if (index < 0 || _rewards.Count <= index)
        {
            throw new Exception("유효하지 않은 보상 범위입니다.");
        }

        return !_rewards[index].Claimed && DayCount >= index;
    }

    public bool TryClaim(int day)
    {
        if (day < 1 || _rewards.Count < day)
        {
            throw new Exception("출석 인덱스가 올바르지 않습니다.");
        }

        if (DayCount < day)
        {
            return false;
        }
        
        return _rewards[day - 1].TryClaim();
    }

    public AttendanceDTO ToDTO()
    {
        return new AttendanceDTO(ID, StartDate, DayCount, LastAttendanceDate, _rewards);
    }
}
