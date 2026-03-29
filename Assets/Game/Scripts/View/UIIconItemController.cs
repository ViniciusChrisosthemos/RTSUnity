using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UIIconItemController : UIItemController
{
    [SerializeField] private Image m_imgIcon;

    protected override void HandleInit(object obj)
    {
        var iconItem = obj as IHasIcon;
        m_imgIcon.sprite = iconItem.Icon;

        var buttonComponent = GetComponent<Button>();
        buttonComponent.onClick.AddListener(SelectItem);
    }
}
