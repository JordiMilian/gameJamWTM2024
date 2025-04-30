using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityToTIme : MonoBehaviour
{
    [SerializeField] List<string> agesList = new List<string>();
    [SerializeField] Vector2 LowestHighestVelocityRank;
    [SerializeField] float TestVelocity;
    [SerializeField] string TestString;
    [SerializeField] int ChoosenImageIntTest;
    private void Update()
    {
        TestString = CalculateTime(TestVelocity);
        ChoosenImageIntTest = ChooseImage(TestVelocity);
    }
    public string CalculateTime(float velocity)
    {
        float timeUnit = (LowestHighestVelocityRank.y - LowestHighestVelocityRank.x)/ agesList.Count;
        float comparetorTop = LowestHighestVelocityRank.x + timeUnit;
        float comparetorBottom = LowestHighestVelocityRank.x;
        for (int i = 0; i < agesList.Count; i++)
        {
            if(velocity > comparetorBottom && velocity <= comparetorTop) { return agesList[i]; }
            else if(i == 0 && velocity < comparetorBottom) { return agesList[i]; }
            comparetorTop += timeUnit;
            comparetorBottom += timeUnit;   
        }
        return agesList[agesList.Count];
    }
    public int ChooseImage(float velocity)
    {
        float timeUnit = (LowestHighestVelocityRank.y - LowestHighestVelocityRank.x) / 3;
        float comparetorTop = LowestHighestVelocityRank.x + timeUnit;
        float comparetorBottom = LowestHighestVelocityRank.x;
        for (int i = 0; i < 3; i++)
        {
            if (velocity > comparetorBottom && velocity <= comparetorTop) { return i; }
            else if (i == 0 && velocity < comparetorBottom) { return 0; }
            comparetorTop += timeUnit;
            comparetorBottom += timeUnit;
        }
        return 2;
    }

}
