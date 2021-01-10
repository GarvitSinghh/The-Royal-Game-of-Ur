using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RollDice : MonoBehaviour
{

    public int total;

    public Text valueField;

    private int[] diceVals = { 1, 1, 2, 3, 3, 2, 1, 4, 2, 1, 2, 1, 2, 3, 3, 2 };


    public void Roll()
    {
        StartCoroutine(RollEffect());   
    }

    IEnumerator RollEffect()
    {

        for (int i = 0; i < 20; i++)
        {

            int r = Random.Range(0, diceVals.Length);

            total = diceVals[r];

            valueField.text = total.ToString();
            yield return new WaitForSeconds(0.05f);
        }

        valueField.text = total.ToString();
        Debug.Log("Rolled: " + total);
    }
}
