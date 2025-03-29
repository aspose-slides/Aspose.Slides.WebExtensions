// Copyright (c) 2001-2021 Aspose Pty Ltd. All Rights Reserved.

using System;
using System.Collections.Generic;
using System.Drawing;
using Aspose.Slides.Animation;
using Aspose.Slides.Export;
using Aspose.Slides.Export.Web;
using Aspose.Slides.SlideShow;

namespace Aspose.Slides.WebExtensions.Helpers
{
    public static class SlideHelper
    {
        static int prevPage = 0;
        static int nextPage = 2;

        public static string GetNextSlide(TemplateContext<Slide> Model)
        {
            string result = "";
            int slidesCount = Model.Global.ContainsKey("slideIndicies") 
                ? Model.Global.Get<int[]>("slideIndicies").Length 
                : Model.Object.Presentation.Slides.Count;

            if (nextPage<=slidesCount) result = string.Format("location.href='slide{0}.html';", nextPage);
            nextPage++;
            return result;
        }
        public static string GetPrevSlide(TemplateContext<Slide> Model)
        {
            string result = "";
            if (prevPage > 0) result = string.Format("location.href='slide{0}.html';", prevPage);
            prevPage++;
            return result;
        }
        private static string GetNavigationButtonStyle(TemplateContext<Slide> Model, int leftShiftFromCenter, int paddingTop) 
        { 
            IPresentation presentation = Model.Object.Presentation;
            int slideMargin = Model.Global.ContainsKey("slideMargin") ? Model.Global.Get<int>("slideMargin") : 10;
            string result = "display: unset !important;";
            if (presentation != null)
            {
                var top = presentation.SlideSize.Size.Height + 40;
                var left = slideMargin + presentation.SlideSize.Size.Width / 2 + leftShiftFromCenter;
                result += string.Format(" top: {0}px; left: {1}px;", NumberHelper.ToCssNumber(top), NumberHelper.ToCssNumber(left));
            }
            return result;
        }
        public static string GetPrevStyle(TemplateContext<Slide> Model)
        {
            return GetNavigationButtonStyle(Model, -105, 40);
        }
        public static string GetNextStyle(TemplateContext<Slide> Model)
        {
            return GetNavigationButtonStyle(Model, 5, 40);
        }
        public static int GetVisibleSlideNumber(ISlide slide)
        {
            int hiddenSlidesCount = 0;
            for (int i = 0; i < slide.SlideNumber; i++)
                if (slide.Presentation.Slides[i].Hidden)
                    hiddenSlidesCount++;

            return slide.SlideNumber - hiddenSlidesCount;
        }

        public static string GetSlideTransitionDirection(ISlide slide)
        {
            string result = "";

            switch (slide.SlideShowTransition.Type)
            {
                case TransitionType.Push:
                case TransitionType.Cube:
                case TransitionType.Box:
                case TransitionType.Pan:
                case TransitionType.Orbit:
                case TransitionType.Rotate:
                case TransitionType.Wipe:
                    result = ((ISideDirectionTransition)slide.SlideShowTransition.Value).Direction.ToString();
                    break;
                case TransitionType.Pull:
                case TransitionType.Cover:
                    result = ((IEightDirectionTransition)slide.SlideShowTransition.Value).Direction.ToString();
                    break;
                case TransitionType.RandomBar:
                    result = ((IOrientationTransition)slide.SlideShowTransition.Value).Direction.ToString();
                    break;
                case TransitionType.Zoom:
                case TransitionType.Warp:
                    result = ((IInOutTransition)slide.SlideShowTransition.Value).Direction.ToString();
                    break;
                case TransitionType.Flythrough:
                    result = ((IFlyThroughTransition)slide.SlideShowTransition.Value).Direction.ToString();
                    break;
                case TransitionType.Reveal:
                    result = ((IRevealTransition)slide.SlideShowTransition.Value).Direction.ToString();
                    break;
                case TransitionType.Split:
                    result = ((ISplitTransition)slide.SlideShowTransition.Value).Direction.ToString();
                    break;
                case TransitionType.Gallery:
                case TransitionType.Flip:
                case TransitionType.Conveyor:
                case TransitionType.Switch:
                case TransitionType.Ferris:
                    result = ((ILeftRightDirectionTransition)slide.SlideShowTransition.Value).Direction.ToString();
                    break;
                case TransitionType.Comb:
                    result = ((IOrientationTransition)slide.SlideShowTransition.Value).Direction.ToString();
                    break;
            }

            return result;
        }

        public static string GetSlideTransitionExtraData(ISlide slide)
        {
            string result = "";

            if (slide.SlideShowTransition.Type == TransitionType.Flythrough)
                result = ((IFlyThroughTransition)slide.SlideShowTransition.Value).HasBounce ? "HasBounce" : "";
            else if (slide.SlideShowTransition.Type == TransitionType.Reveal)
                result = ((IRevealTransition)slide.SlideShowTransition.Value).ThroughBlack ? "ThroughBlack" : "";
            else if (slide.SlideShowTransition.Type == TransitionType.Split)
                result = ((ISplitTransition)slide.SlideShowTransition.Value).Orientation.ToString();

            return result;
        }

        public static Dictionary<IShape, Tuple<string, string, float, float, string, string, int>> GetSlidesAnimationCollection(ISlide slide)
        {
            var result = new Dictionary<IShape, Tuple<string, string, float, float, string, string, int>>();

            int onclickIndex;
            onclickIndex = FillSequenceEffectCollection(slide.LayoutSlide.MasterSlide.Timeline.MainSequence, result, 0);
            onclickIndex = FillSequenceEffectCollection(slide.LayoutSlide.Timeline.MainSequence, result, onclickIndex);
            onclickIndex = FillSequenceEffectCollection(slide.Timeline.MainSequence, result, onclickIndex);
            
            foreach (var sequence in slide.LayoutSlide.MasterSlide.Timeline.InteractiveSequences)
                FillSequenceEffectCollection(sequence, result, -1);
            foreach (var sequence in slide.LayoutSlide.Timeline.InteractiveSequences)
                FillSequenceEffectCollection(sequence, result, -1);
            foreach (var sequence in slide.Timeline.InteractiveSequences)
                FillSequenceEffectCollection(sequence, result, -1);

            return result;
        }

        private static int FillSequenceEffectCollection(ISequence sequence, Dictionary<IShape, Tuple<string, string, float, float, string, string, int>> shapeEffectsCollection, int onclickIndex)
        {
            float prevDelay = 0;
            float maxPrevDuration = 0;
            
            foreach (var effect in sequence)
            {
                var shape = effect.TargetShape;
                var classType = effect.PresetClassType;
                var type = effect.Type;
                var subType = effect.Subtype;
                var triggerType = effect.Timing.TriggerType;

                var delay = effect.Timing.TriggerDelayTime;

                string targetShapeId = "";

                IColorFormat toColor = null;
                string extra = null;
                float duration = 0;
                foreach (var behavior in effect.Behaviors)
                {
                    if (behavior.GetType() == typeof(ColorEffect))
                        toColor = ((ColorEffect)behavior).To;

                    if (behavior.GetType() == typeof(SetEffect) && ((SetEffect)behavior).To is IColorFormat)
                    {
                        toColor = (IColorFormat)((SetEffect)behavior).To;
                        duration = behavior.Timing.Duration;
                        break;
                    }

                    if (type == EffectType.Wave && behavior.GetType() == typeof(MotionEffect))
                        extra = ((MotionEffect)behavior).RotationCenter.ToString();

                    if (type == EffectType.Transparency && behavior.GetType() == typeof(SetEffect))
                        extra = ((SetEffect)behavior).To.ToString();

                    if (type == EffectType.Spin && behavior.GetType() == typeof(RotationEffect))
                        extra = ((RotationEffect)behavior).By.ToString();

                    if (type == EffectType.GrowShrink && behavior.GetType() == typeof(ScaleEffect))
                        extra = ((ScaleEffect)behavior).By.ToString();

                    if (behavior.GetType() == typeof(PropertyEffect) 
                        || behavior.GetType() == typeof(FilterEffect)
                        || behavior.GetType() == typeof(RotationEffect)
                        || behavior.GetType() == typeof(ScaleEffect)
                        || behavior.GetType() == typeof(ColorEffect))
                    {
                        if (type == EffectType.CenterRevolve || type == EffectType.Bounce || type == EffectType.Teeter || type == EffectType.Flicker || type == EffectType.Wave) // a crutch...
                        {
                            duration += behavior.Timing.Duration;
                        }
                        else
                        {
                            duration = behavior.Timing.Duration;
                            break;
                        }
                    }
                }

                if (float.IsInfinity(duration))
                    duration = 0.5f;

                if (type == EffectType.CenterRevolve || type == EffectType.Bounce) // a crutch...
                    duration /= 2;

                if (triggerType == EffectTriggerType.AfterPrevious)
                {
                    prevDelay += maxPrevDuration;
                    maxPrevDuration = delay + duration;
                }
                else if (triggerType == EffectTriggerType.WithPrevious)
                {
                    maxPrevDuration = Math.Max(maxPrevDuration, delay + duration);
                }
                else // OnClick
                {
                    onclickIndex++;
                    prevDelay = 0;
                    maxPrevDuration = delay + duration;
                }

                if (sequence.TriggerShape != null)
                    targetShapeId = "slide-" + sequence.TriggerShape.Slide.SlideId + "-shape-" + sequence.TriggerShape.UniqueId;
                else
                    targetShapeId = "slide";

                float totalDelay = delay + prevDelay;
                
                if (toColor != null && toColor.ColorType != ColorType.NotDefined)
                    extra = GetDestinationColor(shape, toColor);

                if (!shapeEffectsCollection.ContainsKey(shape))
                    shapeEffectsCollection.Add(shape, new Tuple<string, string, float, float, string, string, int>(classType.ToString() + type.ToString(), subType.ToString(), duration, totalDelay, targetShapeId, extra, onclickIndex));
            }

            return onclickIndex;
        }

        private static string GetDestinationColor(IShape shape, IColorFormat colorFormat)
        {
            var cloneShape = shape.Slide.Shapes.AddClone(shape);
            cloneShape.FillFormat.FillType = FillType.Solid;
            cloneShape.FillFormat.SolidFillColor.CopyFrom(colorFormat);

            Color effectiveColor = cloneShape.FillFormat.GetEffective().SolidFillColor;
            shape.Slide.Shapes.Remove(cloneShape);

            return "rgb(" + effectiveColor.R + "," + effectiveColor.G + "," + effectiveColor.B + ")";
        }
        public static string GetBackgroundStyle(TemplateContext<Slide> model)
        {
            var backgroundFillFormat = model.Object.Background.GetEffective().FillFormat;
            string backgroundStyle = FillHelper.GetFillStyle(backgroundFillFormat, model);
            if (backgroundFillFormat.FillType == FillType.Picture && backgroundFillFormat.PictureFillFormat.PictureFillMode == PictureFillMode.Stretch)
            {
                backgroundStyle += " background-size: cover;";
            }
            return backgroundStyle;
        }
        public static IEnumerable<IComment> GetCommentsOrdered(Slide slide)
        {
            List<IComment> allSlideComments = new List<IComment>(slide.GetSlideComments(null));
            List<IComment> orderedComments = new List<IComment>();

            allSlideComments.Sort((a, b) => (int)a.CreatedTime.Subtract(b.CreatedTime).TotalMilliseconds);

            foreach (IComment comment in allSlideComments)
            {
                if (comment.ParentComment == null) orderedComments.Add(comment);
            }

            for (int index = 0; index < orderedComments.Count; index++)
            {
                IComment current = orderedComments[index];
                foreach (var comm in allSlideComments)
                {
                    if (comm.ParentComment == current) orderedComments.Insert(++index, comm);
                }
            }


            return orderedComments;
        }

        private static float calculateSlideScale(HandoutType handout)
        {
            switch (handout)
            {
                case HandoutType.Handouts1: return 0.55f;
                case HandoutType.Handouts2: return 0.55f;
                case HandoutType.Handouts3: return 0.25f;
                case HandoutType.Handouts4Horizontal: return 0.25f;
                case HandoutType.Handouts4Vertical: return 0.25f;
                case HandoutType.Handouts6Horizontal: return 0.25f;
                case HandoutType.Handouts6Vertical: return 0.25f;
                case HandoutType.Handouts9Horizontal: return 0.15f;
                case HandoutType.Handouts9Vertical: return 0.15f;
            }
            return 1f;
        }
        private static void calculateFrameSize(TemplateContext<Presentation> Model, float slideScale, ref int frameWidth, ref int frameHeight)
        { 
            var size = Model.Object.SlideSize.Size;
            size = new SizeF(size.Width * slideScale+2, size.Height * slideScale+2);
            frameWidth = (int)size.Width;
            frameHeight = (int)size.Height;
        }
        private static void calculateFrameMargin(TemplateContext<Presentation> Model, float slideScale, int dpi, SizeF divider, ref int frameMarginHor, ref int frameMarginVert)
        {
            var size = Model.Object.SlideSize.Size;
            size = new SizeF(size.Width * slideScale+2, size.Height * slideScale+2);
            var pageSize = new SizeF(dpi * 7.5f, dpi * 10.0f);
            var margin = new SizeF(
                (float)Math.Ceiling((pageSize.Width - size.Width * divider.Width) / (2 * divider.Width)),
                (float)Math.Ceiling((pageSize.Height - size.Height * divider.Height) / (2 * divider.Height)));
            frameMarginVert = (int)margin.Width - 4;
            frameMarginHor = (int)margin.Height - 4;
            if (divider.Width == 1 && divider.Height == 3)
                frameMarginVert = frameMarginHor;
        }
        private static SizeF calculateDivider(HandoutType handout)
        {
            switch (handout)
            {
                case HandoutType.Handouts1: return new SizeF(1, 1);
                case HandoutType.Handouts2: return new SizeF(1, 2);
                case HandoutType.Handouts3: return new SizeF(1, 3);
                case HandoutType.Handouts4Horizontal: return new SizeF(2, 2);
                case HandoutType.Handouts4Vertical: return new SizeF(2, 2);
                case HandoutType.Handouts6Horizontal: return new SizeF(2, 3);
                case HandoutType.Handouts6Vertical: return new SizeF(2, 3);
                case HandoutType.Handouts9Horizontal: return new SizeF(3, 3);
                case HandoutType.Handouts9Vertical: return new SizeF(3, 3);
            }
            return new SizeF(1, 1);
        }

        public static void CalculateHandoutSizes(TemplateContext<Presentation> Model, ref string slideFrameStyles, ref string slideHandoutStyles, ref string slideHeght)
        {
            const int dpi = 96;

            var handoutIsSet = Model.Global.ContainsKey("handout");
            var handout = handoutIsSet ? Model.Global.Get<HandoutType>("handout") : HandoutType.Handouts1;
            var printComments = handoutIsSet ? Model.Global.Get<bool>("printComments") : false;
            var printSlideNumbers = handoutIsSet ? Model.Global.Get<bool>("printSlideNumbers") : true;
            var printFrameSlide = handoutIsSet ? Model.Global.Get<bool>("printFrameSlide") : true;
            float slideScale = calculateSlideScale(handout);
            SizeF divider =  calculateDivider(handout);

            int frameMarginHor = 0;
            int frameMarginVert = 0;
            int frameWidth = 0;
            int frameHeight = 0;

            calculateFrameMargin(Model, slideScale, dpi, divider, ref frameMarginHor, ref frameMarginVert);
            calculateFrameSize(Model, slideScale, ref frameWidth, ref frameHeight);

            slideFrameStyles = string.Format("margin: {0}px {1}px; width: {2}px; height: {3}px;", frameMarginHor, frameMarginVert, frameWidth, frameHeight);
            slideHandoutStyles = string.Format("transform: scale({0}); transform-origin: top left; margin: 0;", slideScale.ToString().Replace(',','.'));
            slideHeght = frameHeight.ToString();
            if (!handoutIsSet) slideHandoutStyles = "";
        }
    }
}