using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollectUI : MonoBehaviour
{

    public Text score;
    public ItemController itemController;

    // Start is called before the first frame update
    void Start()
    {
        score.text = "0/" + itemController.requiredItems;
    }

    // Update is called once per frame
    public void updateUI()
    {
        score.text = itemController.collectedItems + "/" + itemController.requiredItems;
    }
}
