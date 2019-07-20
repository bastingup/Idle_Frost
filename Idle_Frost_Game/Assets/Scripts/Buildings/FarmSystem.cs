using UnityEngine;
using UnityEngine.UI;

public class FarmSystem : MonoBehaviour
{
    [SerializeField]
    private Button veganButton, veggiButton, meatButton;
    public Farm farmTarget;

    void Start()
    {
        veganButton.onClick.AddListener(AccessVegan);
        veggiButton.onClick.AddListener(AccessVeggi);
        meatButton.onClick.AddListener(AccessMeat);
    }

    public void ChangeGUI(bool b)
    {
        veganButton.gameObject.SetActive(b);
        veggiButton.gameObject.SetActive(b);
        meatButton.gameObject.SetActive(b);
    }

    void AccessVegan()
    {
        if (farmTarget != null)
        {
            farmTarget.GetComponent<Farm>().Vegan();
        }
    }

    void AccessVeggi()
    {
        if (farmTarget != null)
        {
            farmTarget.GetComponent<Farm>().Veggi();
        }
    }

    void AccessMeat()
    {
        if (farmTarget != null)
        {
            farmTarget.GetComponent<Farm>().Meat();
        }
    }
}
