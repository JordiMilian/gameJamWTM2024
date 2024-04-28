using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityToTIme : MonoBehaviour
{
    [SerializeField] List<string> agesList = new List<string>();
    [SerializeField] Vector2 LowestHighestVelocityRank;
    [SerializeField] float TestVelocity;
    [SerializeField] string TestString;
    private void Update()
    {
        TestString = CalculateTime(TestVelocity);
    }
    string CalculateTime(float velocity)
    {
        float timeUnit = LowestHighestVelocityRank.y / agesList.Count;
        float comparetorTop = timeUnit;
        float comparetorBottom = 0;
        for (int i = 0; i < agesList.Count; i++)
        {
            if(velocity > comparetorBottom && velocity <= comparetorTop) { return agesList[i]; }
            comparetorTop += timeUnit;
            comparetorBottom += timeUnit;   
        }
        return agesList[agesList.Count];
    }
}
