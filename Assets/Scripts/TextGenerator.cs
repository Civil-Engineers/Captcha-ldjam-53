using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TextGenerator : ScriptableObject
{
   public TexMex[] captchas;

   public static int index = 0;

   public TexMex Generate() {
        int size = Random.Range(0, captchas.Length);
        return captchas[size];
        // return captchas[(index++ % captchas.Length)];
   }

   public bool isCodeValid(string input, TexMex t) {
        return (input == t.Value);
   }
}
