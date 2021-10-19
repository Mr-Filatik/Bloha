using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopPanel : MonoBehaviour
{
    [SerializeField] private GameObject prefabProduct = null;
    [SerializeField] private GameObject productsParent = null;

    [SerializeField] private Sprite sprite1 = null;

    private void Awake()
    {
        GameObject gameObject = Instantiate(prefabProduct, productsParent.transform);
        ProductController productController = gameObject.GetComponent<ProductController>();
        productController.CreateProduct("BHV", "HGhg", "dVD", sprite1);
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }
}
