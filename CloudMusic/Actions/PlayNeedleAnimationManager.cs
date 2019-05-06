using CloudMusic.Models;
using CloudMusic.Models.ENUM;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CloudMusic.Actions
{
   public class PlayNeedleAnimationManager
    {
        double angle;
        uint duration;
        PlayNeedleRuningState needleRuningState;
        PlayNeedleRunMode MarkState;
        PlayNeedleRunMode NowState;
        VisualElement PlayNeedle;
        bool ismark=false;
        public PlayNeedleAnimationManager(VisualElement element,double ang,uint du, PlayNeedleRunMode needleState)
        {
            PlayNeedle = element;
            angle = ang;
            duration = du;
            NowState = needleState;
            if (NowState == PlayNeedleRunMode.down)
                needleRuningState = PlayNeedleRuningState.down;
            else if(NowState == PlayNeedleRunMode.up)
                needleRuningState = PlayNeedleRuningState.up;
        }
        public async System.Threading.Tasks.Task RunAnimationAsync(PlayNeedleRunMode state)
        {
            if (needleRuningState == PlayNeedleRuningState.Runing)
            {
                ismark = true;
                MarkState = state;
            }
            else if (state == PlayNeedleRunMode.up&& needleRuningState == PlayNeedleRuningState.down)
            {
                needleRuningState = PlayNeedleRuningState.Runing;
                await PlayNeedle.RelRotateTo(-angle, duration).ContinueWith(async (e) => { NowState = PlayNeedleRunMode.up; needleRuningState = PlayNeedleRuningState.up;await CheckStateAsync(); });
            }
            else if (state == PlayNeedleRunMode.down && needleRuningState == PlayNeedleRuningState.up)
            {
                needleRuningState = PlayNeedleRuningState.Runing;
                await PlayNeedle.RelRotateTo(angle, duration).ContinueWith(async (e) => { NowState = PlayNeedleRunMode.down; needleRuningState = PlayNeedleRuningState.down; await CheckStateAsync(); });
            }
        }
         async System.Threading.Tasks.Task CheckStateAsync()
        {
            if (NowState != MarkState&&ismark)
            {
                ismark = false;
                await RunAnimationAsync(MarkState);
            }
        }
    }
}
