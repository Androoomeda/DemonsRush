using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    [HideInInspector] public bool inputBlocked;

    private IClickable lastClickedObject;

    private void Start()
    {
        ServiceLocator.instance.Register(this);
    }

    private void Update()
    {
#if UNITY_ANDROID
                if (!inputBlocked  && Input.touchCount > 0)
                    CheckTouches();
#endif
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
        if (!inputBlocked && Input.GetMouseButtonDown(0))
            CheckClickes();
#endif
    }

    private void CheckTouches()
    {
        Touch touch = Input.GetTouch(0);
        Ray ray = Camera.main.ScreenPointToRay(touch.position);
        RaycastHit hit;
        IClickable clickable;

        if (!Physics.Raycast(ray, out hit))
            return;

        if (hit.collider.gameObject.TryGetComponent<IClickable>(out clickable))
        {
            if (lastClickedObject != null)
                lastClickedObject.Undo();

            clickable.Execute();
            lastClickedObject = clickable;
        }
    }

    private void CheckClickes()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        IClickable clickable;

        if (!Physics.Raycast(ray, out hit))
            return;

        if (hit.collider.gameObject.TryGetComponent<IClickable>(out clickable))
        {
            if (lastClickedObject != null)
                lastClickedObject.Undo();

            clickable.Execute();
            lastClickedObject = clickable;
        }
    }
}
