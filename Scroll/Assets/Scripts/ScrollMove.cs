using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ScrollRect))]
public class ScrollMove : MonoBehaviour, IScrollMove
{
    [HideInInspector]
    public ScrollRect scrollRect;
    private Coroutine smoothControll;

    private void Start()
    {
        scrollRect = GetComponent<ScrollRect>();
    }

    public void Move(ScrollMoveType moveType)
    {
        float finalPos = moveType == ScrollMoveType.Down ? 0 : moveType == ScrollMoveType.Up ? 1 : 0.5f;
        Action action = () => { StopCoroutine(smoothControll); smoothControll = smoothControll != null ? null : smoothControll; };

        if (smoothControll == null)
        {
            smoothControll = StartCoroutine(SmothScroll(finalPos, action));
        }
    }

    public void MoveTo(float value)
    {
        scrollRect.verticalNormalizedPosition = value;
    }

    IEnumerator SmothScroll(float value, Action onEnd = null)
    {
        float scrollPos = scrollRect.verticalNormalizedPosition;
        while (!(scrollRect.verticalNormalizedPosition == value))
        {
            yield return new WaitForSeconds(0.01f);

            // scrollPos = scrollPos < value ? +0.1f : scrollPos > value ? -0.1f : 0;

            if (scrollPos > value)
            {
                scrollPos -= 0.1f;
                if (scrollPos < 0)
                    scrollPos = 0;
            }

            if (scrollPos < value)
            {
                scrollPos += 0.1f;
            }

            scrollPos = (float)Math.Round(scrollPos, 1, MidpointRounding.ToEven);

            scrollRect.verticalNormalizedPosition = scrollPos;

            if (scrollPos == value)
                break;
        }
        onEnd?.Invoke();
    }

    #region Buttons
    public void MoveUp()
    {
        Move(ScrollMoveType.Up);
    }

    public void MoveCenter()
    {
        Move(ScrollMoveType.Center);
    }

    public void MoveDown()
    {
        Move(ScrollMoveType.Down);
    }
    #endregion
}
