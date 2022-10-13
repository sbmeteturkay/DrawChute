using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;
/// <summary>
/// A static class for general helpful methods
/// </summary>
public static class Helpers
{
    /// <summary>
    /// Destroy all child objects of this transform (Unintentionally evil sounding).
    /// Use it like so:
    /// <code>
    /// transform.DestroyChildren();
    /// </code>
    /// </summary>
    public static void DestroyChildren(this Transform t)
    {
        foreach (Transform child in t) Object.Destroy(child.gameObject);
    }
    public static void Wait(this MonoBehaviour mono, float delay, UnityAction action)
    {
        mono.StartCoroutine(ExecuteAction(delay, action));
    }

    private static IEnumerator ExecuteAction(float delay, UnityAction action)
    {
        yield return new WaitForSecondsRealtime(delay);
        action.Invoke();
        yield break;
    }
    public static bool IsPointerOverUIObject()
    {
        // Referencing this code for GraphicRaycaster https://gist.github.com/stramit/ead7ca1f432f3c0f181f
        // the ray cast appears to require only eventData.position.
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
    public static bool CoinFlip()
    {
        return Random.Range(0, 2) == 0;
    }
    private static Matrix4x4 _isoMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));
    public static Vector3 ToIso(this Vector3 input) => _isoMatrix.MultiplyPoint3x4(input);
}
