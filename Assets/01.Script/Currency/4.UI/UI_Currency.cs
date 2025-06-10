
using TMPro;
using Unity.FPS.Game;
using Unity.FPS.Gameplay;
using UnityEngine;

public class UI_Currency : MonoBehaviour
{
    public TextMeshProUGUI GoldCountText;
    public TextMeshProUGUI DiamondCountText;
    public TextMeshProUGUI BuyHealthText;

    //초기화

    private void Start()
    {
        Refresh();
        CurrencyManager.Instance.OnDataChanged += Refresh;
    }

    void Refresh()
    {
        GoldCountText.text = $"{CurrencyManager.Instance.Get(ECurrencyType.Gold).Value}";
        DiamondCountText.text = $"{CurrencyManager.Instance.Get(ECurrencyType.Diamonde).Value}";
        BuyHealthText.text = "";
    }

    //3번기 누르면 피먹음

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            BuyHealth();
        }
    }

    //피 구입해서 먹음
    public void BuyHealth()
    {
        Debug.Log("버튼");


        if (CurrencyManager.Instance.TryBuy(ECurrencyType.Gold, 300))
        {
            var player = GameObject.FindFirstObjectByType<PlayerCharacterController>();
            Health playerHealth = player.GetComponent<Health>();
            playerHealth.Heal(100);

            // 성공 파티클 띄운다던지
        }
        else
        {
            // 알림을 띄운다.
            // 토스트 메시지르 띄운다던지..
        }


    }
}
