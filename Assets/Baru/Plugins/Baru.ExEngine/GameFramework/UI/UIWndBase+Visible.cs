using System;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

using DG.Tweening;

using Extension;

[RequireComponent(typeof(Image))]
public partial class UIWndBase : MonoBehaviour
{
    UIVisibleState visibleState;
    public UIVisibleTweenType visibleTweenType;
    public float VisibleTweenDurationSec = 1;
    public DG.Tweening.Ease TweeningEase;

    Tweener tweener;

    void Clear_Appearing()
    {
        visibleState = UIVisibleState.Disappeared;
        tweener?.Kill();
        tweener = null;
    }

    void Open_Appearing()
    {
        visibleState = UIVisibleState.Appearing;

        switch (visibleTweenType)
        {
            case UIVisibleTweenType.None:
                break;
            case UIVisibleTweenType.Fade:
                mBG.SetAlpha(0);
                tweener = mBG.DOFade(1.0f, VisibleTweenDurationSec)
                    .SetEase(TweeningEase)
                    .OnComplete(() => { Open_Appeared(); })
                    .SetUpdate(true); // No TimeScale
                break;
            case UIVisibleTweenType.MoveLeftToCenter:
                mRectTransform.anchoredPosition = new Vector2(-mUIRoot.mCanvasScaler.referenceResolution.x, 0);
                tweener = mRectTransform.DOAnchorPos(new Vector2(0, 0), VisibleTweenDurationSec)
                    .SetEase(TweeningEase)
                    .OnComplete(() => { Open_Appeared(); })
                    .SetUpdate(true);
                break;
            case UIVisibleTweenType.MoveRightToCenter:
                mRectTransform.anchoredPosition = new Vector2(mUIRoot.mCanvasScaler.referenceResolution.x, 0);
                tweener = mRectTransform.DOAnchorPos(new Vector2(0, 0), VisibleTweenDurationSec)
                    .SetEase(TweeningEase)
                    .OnComplete(() => { Open_Appeared(); })
                    .SetUpdate(true);
                break;
            case UIVisibleTweenType.MoveTopToCenter:
                mRectTransform.anchoredPosition = new Vector2(0, mUIRoot.mCanvasScaler.referenceResolution.y);
                tweener = mRectTransform.DOAnchorPos(new Vector2(0, 0), VisibleTweenDurationSec)
                    .SetEase(TweeningEase)
                    .OnComplete(() => { Open_Appeared(); })
                    .SetUpdate(true);
                break;
            case UIVisibleTweenType.MoveBottomToCneter:
                mRectTransform.anchoredPosition = new Vector2(0, -mUIRoot.mCanvasScaler.referenceResolution.y);
                tweener = mRectTransform.DOAnchorPos(new Vector2(0, 0), VisibleTweenDurationSec)
                    .SetEase(TweeningEase)
                    .OnComplete(() => { Open_Appeared(); })
                    .SetUpdate(true);
                break;
            case UIVisibleTweenType.ScaleXY:
                mRectTransform.localScale = new Vector3(0.05f, 0.05f, 1.0f);
                tweener = mRectTransform.DOScale(new Vector3(1.0f, 1.0f, 1.0f), VisibleTweenDurationSec)
                    .SetEase(TweeningEase)
                    .OnComplete(() => { Open_Appeared(); })
                    .SetUpdate(true);
                break;
            case UIVisibleTweenType.ScaleX:
                mRectTransform.localScale = new Vector3(0.05f, 1.0f, 1.0f);
                tweener = mRectTransform.DOScale(new Vector3(1.0f, 1.0f, 1.0f), VisibleTweenDurationSec)
                    .SetEase(TweeningEase)
                    .OnComplete(() => { Open_Appeared(); })
                    .SetUpdate(true);
                break;
            case UIVisibleTweenType.ScaleY:
                mRectTransform.localScale = new Vector3(1.0f, 0.05f, 1.0f);
                tweener = mRectTransform.DOScale(new Vector3(1.0f, 1.0f, 1.0f), VisibleTweenDurationSec)
                    .SetEase(TweeningEase)
                    .OnComplete(() => { Open_Appeared(); })
                    .SetUpdate(true);
                break;
        }
    }

    void Open_Appeared()
    {
        visibleState = UIVisibleState.Appeared;

        switch (visibleTweenType)
        {
            case UIVisibleTweenType.None:
                break;
            case UIVisibleTweenType.Fade:
                void Close_Disappearing()
();
                break;
            case UIVisibleTweenType.MoveLeftToCenter:
                break;
            case UIVisibleTweenType.MoveRightToCenter:
                break;
            case UIVisibleTweenType.MoveTopToCenter:
                break;
            case UIVisibleTweenType.MoveBottomToCneter:
                break;
        }
    }

    void Close_Disappearing()
    {
        visibleState = UIVisibleState.Disappearing;

        switch (visibleTweenType)
        {
            case UIVisibleTweenType.None:
                break;
            case UIVisibleTweenType.Fade:
                mBG.DOFade(0, VisibleTweenDurationSec)
                    .SetEase(Ease.OutQuad)
                    .OnComplete(() => { Close(); })
                    .SetUpdate(true);
                break;
            case UIVisibleTweenType.MoveLeftToCenter:
                break;
            case UIVisibleTweenType.MoveRightToCenter:
                break;
            case UIVisibleTweenType.MoveTopToCenter:
                break;
            case UIVisibleTweenType.MoveBottomToCneter:
                break;
        }
    }
}
