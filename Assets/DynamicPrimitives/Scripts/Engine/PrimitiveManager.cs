using UnityEngine;

public class PrimitiveManager : MonoBehaviour
{
    private void Awake()
    {
        _cachedTransform = this.transform;
    }

    public void MakePoints(float radius, Vector3[] positions, string objNameForm)
    {
        var localScale = new Vector3(radius, radius, radius);
        for (int i = 0; i != positions.Length; ++i)
        {
            var eachPos = positions[i];
            var newObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            newObject.name = string.Format(objNameForm, i);

            var newObjTransform = newObject.GetComponent<Transform>();
            newObjTransform.parent = _cachedTransform; 
            newObjTransform.localPosition = eachPos;
            newObjTransform.localScale = localScale;
        }
    }

    public void MakePoint(float radius, Vector3 position, string objName)
    {
        var localScale = new Vector3(radius, radius, radius);

        var newObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        newObject.name = objName;

        var newObjTransform = newObject.GetComponent<Transform>();
        newObjTransform.parent = _cachedTransform; 
        newObjTransform.localPosition = position;
        newObjTransform.localScale = localScale; 
    }

    private Transform _cachedTransform;
}