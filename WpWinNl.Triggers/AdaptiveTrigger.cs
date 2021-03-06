﻿using System;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.UI.Core;
using Windows.UI.Xaml;
using WindowsStateTriggers;

namespace WpWinNl.Triggers
{
  /// <summary>
  /// A composable AdaptiveTrigger based on this https://github.com/dotMorten/WindowsStateTriggers/pull/48 pull request
  /// This implementation apparently does work in a CompositeStateTrigger  with a StateTrigger
  /// </summary>
  public class AdaptiveTrigger : StateTriggerBase, ITriggerValue
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="AdaptiveTrigger"/> class.
    /// </summary>
    public AdaptiveTrigger()
    {
      var window = CoreApplication.GetCurrentView()?.CoreWindow;
      if (window != null)
      {
        var weakEvent = new WeakEventListener<AdaptiveTrigger, CoreWindow, WindowSizeChangedEventArgs>(this)
        {
          OnEventAction = (instance, s, e) => OnCoreWindowOnSizeChanged(s, e),
          OnDetachAction = (instance, weakEventListener) => window.SizeChanged -= weakEventListener.OnEvent
        };
        window.SizeChanged += weakEvent.OnEvent;
      }
    }

    private void OnCoreWindowOnSizeChanged(CoreWindow sender, WindowSizeChangedEventArgs args)
    {
      OnCoreWindowOnSizeChanged(args.Size);
    }

    private void OnCoreWindowOnSizeChanged(Size size)
    {
      IsActive = size.Height >= MinWindowHeight && size.Width >= MinWindowWidth &&
                 size.Height <= MaxWindowHeight && size.Width <= MaxWindowWidth &&
                 MinWindowHeight <= MaxWindowHeight && MinWindowWidth <= MaxWindowWidth;
    }


    private void OnWindowSizePropertyChanged()
    {
      var window = CoreApplication.GetCurrentView()?.CoreWindow;
      if (window != null)
      {
        OnCoreWindowOnSizeChanged(new Size(window.Bounds.Width, window.Bounds.Height));
      }
    }

    private bool _isActive;

    /// <summary>
    /// Gets a value indicating whether this trigger is active.
    /// </summary>
    /// <value><c>true</c> if this trigger is active; otherwise, <c>false</c>.</value>
    public bool IsActive
    {
      get
      {
        return _isActive;
      }
      private set
      {
        if (_isActive != value)
        {
          _isActive = value;
          SetActive(value);
          IsActiveChanged?.Invoke(this, EventArgs.Empty);
        }
      }
    }

    /// <summary>
    /// Occurs when the <see cref="IsActive" /> property has changed.
    /// </summary>
    public event EventHandler IsActiveChanged;


    /// <summary>
    /// Max size for a screen
    /// </summary>
    private const double MaxSize = 32762;

    #region MinWindowHeight

    /// <summary>
    /// MinWindowHeight Property name
    /// </summary>
    private const string MinWindowHeightPropertyName = "MinWindowHeight";

    /// <summary>
    /// Minimum window height to trigger
    /// </summary>
    public double MinWindowHeight
    {
      get { return (double)GetValue(MinWindowHeightProperty); }
      set { SetValue(MinWindowHeightProperty, value); }
    }

    /// <summary>
    /// MinWindowHeight Property definition
    /// </summary>
    public static readonly DependencyProperty MinWindowHeightProperty = DependencyProperty.Register(
        MinWindowHeightPropertyName,
        typeof(double),
        typeof(AdaptiveTrigger),
        new PropertyMetadata(default(double), MinWindowHeightChanged));

    /// <summary>
    /// MinWindowHeight property changed callback.
    /// </summary>
    /// <param name="d">The depency object (i.e. the behavior).</param>
    /// <param name="e">The property event args <see cref="DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>  
    private static void MinWindowHeightChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      var thisobj = d as AdaptiveTrigger;
      thisobj?.OnWindowSizePropertyChanged();
    }

    #endregion


    #region MinWindowWidth

    /// <summary>
    /// MinWindowWidth Property name
    /// </summary>
    private const string MinWindowWidthPropertyName = "MinWindowWidth";

    /// <summary>
    /// Minimum window width to trigger
    /// </summary>
    public double MinWindowWidth
    {
      get { return (double)GetValue(MinWindowWidthProperty); }
      set { SetValue(MinWindowWidthProperty, value); }
    }

    /// <summary>
    /// MinWindowWidth Property definition
    /// </summary>
    public static readonly DependencyProperty MinWindowWidthProperty = DependencyProperty.Register(
        MinWindowWidthPropertyName,
        typeof(double),
        typeof(AdaptiveTrigger),
        new PropertyMetadata(default(double), MinWindowWidthChanged));

    /// <summary>
    /// MinWindowWidth property changed callback.
    /// </summary>
    /// <param name="d">The depency object (i.e. the behavior).</param>
    /// <param name="e">The property event args <see cref="DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>  
    private static void MinWindowWidthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      var thisobj = d as AdaptiveTrigger;
      thisobj?.OnWindowSizePropertyChanged();
    }

    #endregion


    #region MaxWindowHeight

    /// <summary>
    /// MaxWindowHeight Property name
    /// </summary>
    private const string MaxWindowHeightPropertyName = "MaxWindowHeight";

    /// <summary>
    /// Maximum window height to trigger
    /// </summary>
    public double MaxWindowHeight
    {
      get { return (double)GetValue(MaxWindowHeightProperty); }
      set { SetValue(MaxWindowHeightProperty, value); }
    }

    /// <summary>
    /// MaxWindowHeight Property definition
    /// </summary>
    public static readonly DependencyProperty MaxWindowHeightProperty = DependencyProperty.Register(
        MaxWindowHeightPropertyName,
        typeof(double),
        typeof(AdaptiveTrigger),
        new PropertyMetadata(MaxSize, MaxWindowHeightChanged));

    /// <summary>
    /// MaxWindowHeight property changed callback.
    /// </summary>
    /// <param name="d">The depency object (i.e. the behavior).</param>
    /// <param name="e">The property event args <see cref="DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>  
    private static void MaxWindowHeightChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      var thisobj = d as AdaptiveTrigger;
      thisobj?.OnWindowSizePropertyChanged();
    }

    #endregion


    #region MaxWindowWidth

    /// <summary>
    /// MaxWindowWidth Property name
    /// </summary>
    private const string MaxWindowWidthPropertyName = "MaxWindowWidth";

    /// <summary>
    /// Maximum window width to trigger
    /// </summary>
    public double MaxWindowWidth
    {
      get { return (double)GetValue(MaxWindowWidthProperty); }
      set { SetValue(MaxWindowWidthProperty, value); }
    }

    /// <summary>
    /// MaxWindowWidth Property definition
    /// </summary>
    public static readonly DependencyProperty MaxWindowWidthProperty = DependencyProperty.Register(
        MaxWindowWidthPropertyName,
        typeof(double),
        typeof(AdaptiveTrigger),
        new PropertyMetadata(MaxSize, MaxWindowWidthChanged));

    /// <summary>
    /// MaxWindowWidth property changed callback.
    /// </summary>
    /// <param name="d">The depency object (i.e. the behavior).</param>
    /// <param name="e">The property event args <see cref="DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>  
    private static void MaxWindowWidthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      var thisobj = d as AdaptiveTrigger;
      thisobj?.OnWindowSizePropertyChanged();
    }

    #endregion
  }
}
