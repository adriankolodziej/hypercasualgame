using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionIndicator : MonoBehaviour
{
    [SerializeField]
    private Image hoverImage;
    [SerializeField]
    private Image selectImage;

    private void Start()
    {
        PlayerInput.Instance.PlayerHoveredOver += OnHover;
    }

    private void OnDestroy()
    {
        PlayerInput.Instance.PlayerHoveredOver -= OnHover;
    }

    private void OnHover(GameObject gameObject)
    {
        if (this.transform.root.gameObject == gameObject && selectImage.enabled == false)
        {
            hoverImage.enabled = true;
            selectImage.enabled = false;
        }
        else
        {
            Unhover();
        }
    }

    private void Unhover()
    {
        hoverImage.enabled = false;
    }

    public void Select()
    {
        hoverImage.enabled = false;
        selectImage.enabled = true;
    }

    public void Unselect()
    {
        hoverImage.enabled = false;
        selectImage.enabled = false;
    }
}
