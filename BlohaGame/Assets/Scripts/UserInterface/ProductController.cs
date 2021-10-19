using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProductController : MonoBehaviour
{
    [SerializeField] private Text nameProduct = null;
    [SerializeField] private Image imageProduct = null;
    [SerializeField] private Text descriptionProduct = null;
    [SerializeField] private Button buttonProduct = null;

    public void CreateProduct(string inputName, string inputButton, string inputDescription, Sprite inputImage)
    {
        nameProduct.text = inputName;
        descriptionProduct.text = inputDescription;
        buttonProduct.GetComponentInChildren<Text>().text = inputButton;
        imageProduct.sprite = inputImage;
    }

    public void SetState()
    {

    }
}
