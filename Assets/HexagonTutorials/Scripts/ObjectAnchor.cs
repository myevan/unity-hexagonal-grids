using UnityEngine;

public class ObjectAnchor : MonoBehaviour
{
    public RectTransform canvasTransform;

    private void Awake()
    {
        _cachedRectTransform = GetComponent<RectTransform>();
    }

    public void SetTargetTransform(Transform targetTransform)
    {
        _targetTransform = targetTransform;

        var screenPoint = RectTransformUtility.WorldToScreenPoint(
            Camera.main, _targetTransform.position);

        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasTransform, screenPoint, Camera.main, out localPoint);

        _cachedRectTransform.anchoredPosition = localPoint;
    }

    private Transform _targetTransform;
    private RectTransform _cachedRectTransform;
}
