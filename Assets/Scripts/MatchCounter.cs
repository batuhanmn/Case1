using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchCounter : Singleton<MatchCounter>
{
    TMPro.TextMeshProUGUI countText;
    private int count = 0;
    // Start is called before the first frame update
    void Start()
    {
        countText = GetComponentInChildren<TMPro.TextMeshProUGUI>();
        UpdateCount(count);
    }

    public void UpdateCount(int count)
    {
        this.count += count;
        countText.text = "Match Count: "+ this.count;
    }
    public void ResetCount()
    {
        this.count = 0;
        UpdateCount(0);
    }
}
