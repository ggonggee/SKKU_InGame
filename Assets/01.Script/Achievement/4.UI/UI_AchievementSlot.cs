using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_AchievementSlot : MonoBehaviour
{
    public TextMeshProUGUI NameTextUI;
    public TextMeshProUGUI DescriptionTextUI;
    public TextMeshProUGUI RewardCountTextUI;
    public Slider ProgressSlider;
    public TextMeshProUGUI ProgressTextUI;
    public TextMeshProUGUI RewardClaimDateTextUI;
    public Button RewardClaimButton;

    private AchievementDTO _achievementDTO;

    public void Refresh(AchievementDTO achievementDTO)
    {
        _achievementDTO = achievementDTO;

        NameTextUI.text = _achievementDTO.Name;
        DescriptionTextUI.text = _achievementDTO.Description;
        RewardCountTextUI.text = _achievementDTO.RewardAmount.ToString();
        ProgressSlider.value = (float)_achievementDTO.CurrentValue / _achievementDTO.GoalValue;
        ProgressTextUI.text = $"{_achievementDTO.CurrentValue} / {_achievementDTO.GoalValue}";

        RewardClaimButton.interactable = achievementDTO.CanClaimReward();
    }

    public void ClaimReward()
    {
        if (AchievementManager.Instance.TryClaimReward(_achievementDTO))
        {
            // 꽃 가루 뿌려주고   
        }
        else
        {
            // 진행도가 부족합니다...
        }
    }
    
}
