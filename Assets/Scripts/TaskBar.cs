using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskBar : MonoBehaviour
{
    [SerializeField] private Button grimmIcon;

    // Start is called before the first frame update
    void Start()
    {
        grimmIcon.onClick.AddListener (toggleGrimm);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void toggleGrimm() {
        UpgradesWindow.Instance.toggleWindowVisibility();
    }
}
