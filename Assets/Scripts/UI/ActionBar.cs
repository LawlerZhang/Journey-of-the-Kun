using UnityEngine;

public class ActionBar : MonoBehaviour
{
    //[SerializeField] Transform palyerPos;
    [SerializeField] Transform actionBarPos;
    Vector2 newScreenPos;
    private void Update()
    {
        newScreenPos = Camera.main.WorldToScreenPoint(actionBarPos.position);
        this.GetComponent<RectTransform>().position = newScreenPos;
    }
}
