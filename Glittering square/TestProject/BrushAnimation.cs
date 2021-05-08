using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace TestProject
{
   
    
    public class BrushAnimation : AnimationTimeline
    {
       

       

        public static readonly DependencyProperty EasingFunctionProperty = DependencyProperty.Register
        (
            "EasingFunction",
            typeof(IEasingFunction),
            typeof(BrushAnimation),
            new PropertyMetadata(null)
        );

      
      

       
        public static readonly DependencyProperty FromProperty = DependencyProperty.Register
        (
            "From",
            typeof(Brush),
            typeof(BrushAnimation),
            new PropertyMetadata(null)
        );

       
        public static readonly DependencyProperty ToProperty = DependencyProperty.Register
        (
            "To",
            typeof(Brush),
            typeof(BrushAnimation),
            new PropertyMetadata(null)
        );

        


       
        public override Type TargetPropertyType => typeof(Brush);

        

       
        public IEasingFunction EasingFunction
        {
            get
            {
                return (IEasingFunction)GetValue(EasingFunctionProperty);
            }
            set
            {
                SetValue(EasingFunctionProperty, value);
            }
        }

       

     
        public Brush From
        {
            get
            {
                return (Brush)GetValue(FromProperty);
            }
            set
            {
                SetValue(FromProperty, value);
            }
        }

 

   
        public Brush To
        {
            get
            {
                return (Brush)GetValue(ToProperty);
            }
            set
            {
                SetValue(ToProperty, value);
            }
        }

    
       

        public override object GetCurrentValue(object defaultOriginValue, object defaultDestinationValue, AnimationClock animationClock)
        {
            if(!animationClock.CurrentProgress.HasValue)
            {
                return Brushes.Transparent;
            }

            Brush originValue      = From ?? defaultOriginValue      as Brush;
            Brush destinationValue = To   ?? defaultDestinationValue as Brush;

            double progress = animationClock.CurrentProgress.Value;

            if(progress == 0)
            {
                return originValue;
            }

            if(progress == 1)
            {
                return destinationValue;
            }

            IEasingFunction easingFunction = EasingFunction;

            if(easingFunction != null)
            {
                progress = easingFunction.Ease(progress);
            }

            return new VisualBrush
            (
                new Border()
                {
                    Width      = 1,
                    Height     = 1,
                    Background = originValue,
                    Child      = new Border()
                    {
                        Background = destinationValue,
                        Opacity    = progress,
                    }
                }
            );
        }


      

      
        protected override Freezable CreateInstanceCore()
        {
            return new BrushAnimation();
        }

      
    }
}