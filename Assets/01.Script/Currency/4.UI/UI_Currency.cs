
using TMPro;
using Unity.FPS.Game;
using Unity.FPS.Gameplay;
using UnityEngine;

public class UI_Currency : MonoBehaviour
{
    public TextMeshProUGUI GoldCountText;
    public TextMeshProUGUI DiamondCountText;
    public TextMeshProUGUI BuyHealthText;

    //�ʱ�ȭ

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

    //3���� ������ �Ǹ���

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            BuyHealth();
        }
    }

    //�� �����ؼ� ����
    public void BuyHealth()
    {
        Debug.Log("��ư");


        if (CurrencyManager.Instance.TryBuy(ECurrencyType.Gold, 300))
        {
            var player = GameObject.FindFirstObjectByType<PlayerCharacterController>();
            Health playerHealth = player.GetComponent<Health>();
            playerHealth.Heal(100);

            // ���� ��ƼŬ ���ٴ���
        }
        else
        {
            // �˸��� ����.
            // �佺Ʈ �޽����� ���ٴ���..
        }


    }
}
