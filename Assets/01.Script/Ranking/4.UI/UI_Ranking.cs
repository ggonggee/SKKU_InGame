using System.Collections.Generic;
using UnityEngine;

public class UI_Ranking : MonoBehaviour
{
    public List<UI_RankingSlot> RankinSlots;
    public UI_RankingSlot MyRankingSlot;
    
    private void Start()
    {
        Refresh();

        RankingManager.Instance.OnDataChanged += Refresh;
    }
    
    public void Refresh()
    {
        var rankins = RankingManager.Instance.Rankings;

        int index = 0;
        foreach (var ui_ranking in RankinSlots)
        {
            ui_ranking.Refresh(rankins[index]);
            index++;
        }
        
        MyRankingSlot.Refresh(RankingManager.Instance.MyRanking);
    }
}