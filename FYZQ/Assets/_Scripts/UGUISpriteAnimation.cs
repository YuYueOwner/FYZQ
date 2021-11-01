using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

[RequireComponent(typeof(Image))]
public class UGUISpriteAnimation : MonoBehaviour
{
    private Image ImageSource;
    private int mCurFrame = 0;
    private float mDelta = 0;

    public float FPS = 5;
    public List<Sprite> SpriteFrames;
    public bool Loop = false;

    public int FrameCount
    {
        get
        {
            return SpriteFrames.Count;
        }
    }

    void Awake()
    {
        ImageSource = GetComponent<Image>();
    }

    private void SetSprite(int idx)
    {
        ImageSource.sprite = SpriteFrames[idx];
        ImageSource.SetNativeSize();
    }


    void Update()
    {
        if ( 0 == FrameCount)
        {
            return;
        }

        mDelta += Time.deltaTime;
        if (mDelta > 1 / FPS)
        {
            mDelta = 0;

                mCurFrame++;


            if (mCurFrame >= FrameCount)
            {
                if (Loop)
                {
                    mCurFrame = 0;
                }
                else
                {
                    return;
                }
            }
            else if (mCurFrame < 0)
            {
                if (Loop)
                {
                    mCurFrame = FrameCount - 1;
                }
                else
                {
                    return;
                }
            }

            SetSprite(mCurFrame);
        }
    }
}