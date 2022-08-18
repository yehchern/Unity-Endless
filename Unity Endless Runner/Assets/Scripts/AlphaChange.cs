using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphaChange : MonoBehaviour
{
    [SerializeField] private Material myMaterial;
    [SerializeField] private Renderer myModel;
    // Start is called before the first frame update
    void Start()
    {
        Color color = myModel.material.color;
        color.a = 0;
        myModel.material.color = color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
