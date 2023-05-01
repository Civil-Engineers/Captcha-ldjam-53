using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingCounter : MonoBehaviour
{
 public float countingSpeed;         // Controls the speed of countingNumber.
 [Space]
 public int number;                  // Controls where the countingNumber counts to.
 public float countingNumber;        // CountingNumber controls the frames of sprites for each digits.
 [Space]
 public SpriteRenderer[] digits;     // The amount of digits in a row e.g. element 0 is for the unit, element 1 is for the tens, element 2 is for the hundreds, etc.
 public Sprite[] digitSprites;       // Frames of sprites from 0 to 9 for each digits, you can also add frames between numbers like 0 to 1, 1 to 2 and so on.
 void Start()
 {
     countingNumber = number;    // Makes countingNumber set to the value of number in the start of the scene.
 }
 void Update()
 {
     int multiplyConvert = digitSprites.Length / 10;
     int convert = 1;    // Starting value for convert to multiply by itself in the forloop.
     // For animating the digits.
     for (int i = 0; i < digits.Length; i++)
     {
         float numberConvert = (countingNumber / convert) % 10 * multiplyConvert;
         convert *= 10;      // Convert multiples by itself by the size of the digits array e.g. 1 x 10 = 10 x 10 = 100 x 10 = 1,000 etc.
         digits[i].sprite = digitSprites[(int)numberConvert];
     }
     // Adding clamp to number and countingNumber.
     int numberLimit = convert - 1;
     number = Mathf.Clamp(number, 0, numberLimit);
     countingNumber = Mathf.Clamp(countingNumber, 0, numberLimit);
     
     CountingToNumbers();
 }
 // Allows countingNumber to count to number.
 void CountingToNumbers()
 {
     if (countingNumber < number)
     {
         countingNumber += countingSpeed * Time.deltaTime;
         if (countingNumber >= number)
         {
             countingNumber = number;
         }
     }
     else
     {
         countingNumber -= countingSpeed * Time.deltaTime;
         if (countingNumber <= number)
         {
             countingNumber = number;
         }
     }
 }
}
