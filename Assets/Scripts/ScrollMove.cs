using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

[RequireComponent(typeof(ScrollRect))]
public class ScrollMove : MonoBehaviour, IScrollMove
{
    public ScrollRect scrollRect;
    private Coroutine smoothControll;
    [SerializeField]
    private ScrollType scrollType;

    public void Move(ScrollMoveType moveType)
    {
        float finalPos = moveType == ScrollMoveType.Down ? 0 : moveType == ScrollMoveType.Up ? 1 : 0.5f;
        Action action = () => { StopCoroutine(smoothControll); smoothControll = smoothControll != null ? null : smoothControll; };

        if (smoothControll == null)
            smoothControll = StartCoroutine(SmothScroll(finalPos, action));
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

    #region Flags

    [Serializable]
    public class Flag
    {
        public string NameFlag;
        public ScrollType ScrollType;
        public float NormalizateValue;

        public Flag() { }
        public Flag(Flag _f)
        {
            NameFlag = _f.NameFlag;
            ScrollType = _f.ScrollType;
            NormalizateValue = _f.NormalizateValue;
        }
    }

    public List<Flag> Flags = new List<Flag>();

    public void SaveFlag(string _nameFlag)
    {
        Flag flag = new Flag();
        flag.NameFlag = _nameFlag;
        flag.ScrollType = scrollType;
        flag.NormalizateValue = scrollType == ScrollType.Horizontal ? scrollRect.horizontalNormalizedPosition : scrollRect.verticalNormalizedPosition;
        Flags.Add(flag);
    }

    /// <summary>
    /// Number flag only > 1
    /// </summary>
    /// <param name="number"></param>
    /// <param name="scrollType"></param>
    public void MoveToFlag(int number, ScrollType scrollType = ScrollType.Default)
    {
        var _flag = Flags[number - 1];
        if (_flag != null)
            MoveTo(_flag.NormalizateValue);
        else
            throw new Exception("Flag is not be find!");
    }

    public void MoveToFlag(string id, ScrollType scrollType = ScrollType.Default)
    {
        var _flag = Flags.Find(x => x.NameFlag == id);
        if (_flag != null)
            MoveTo(_flag.NormalizateValue);
        else
            throw new Exception($"Flag {id} is not be find!");
    }

    #endregion
}
