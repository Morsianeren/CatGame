using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public delegate void AlertAction(Transform position);
    public static event AlertAction AlertActionEvent;

    public static void OnAlert(Transform position)
    {
        if (AlertActionEvent != null)
            AlertActionEvent(position);
    }
}
