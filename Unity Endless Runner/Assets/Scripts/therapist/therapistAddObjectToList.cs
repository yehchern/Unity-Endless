using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class therapistAddObjectToList : MonoBehaviour
{
    int index = 0;

    public GameObject itemTemplate;

    public GameObject content;

    public void AddButton_Click()
    {
        var copy = Instantiate(itemTemplate);
        copy.transform.parent = content.transform;
        copy.transform.localPosition = Vector3.zero;

        copy.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = (index + 1).ToString();

        int copyOfIndex = index;

        copy.GetComponent<Button>().onClick.AddListener(

            () =>
            {
                // TODO : Anything you want to do for the current index
                Debug.Log("Index number" + copyOfIndex);
            }

        );

        index++;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
