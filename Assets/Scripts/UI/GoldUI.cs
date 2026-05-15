using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GoldUI : MonoBehaviour
{
    public TextMeshProUGUI goldText;

    private GameManager gm;

    private void Start()
    {
        var gm = Core.Instance.Game;
        goldText.text = gm.SaveData.gold.ToString();
        gm.OnGoldChanged += UpdateGold;
    }

    private void OnDestroy()
    {
        if (gm != null)
            gm.OnGoldChanged -= UpdateGold;
    }

    private void UpdateGold(int gold)
    {
        goldText.text = gold.ToString();
    }
}