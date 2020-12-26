﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#nullable enable

using Windows.UI.Xaml.Media.Animation;
using static Microsoft.Toolkit.Uwp.UI.Animations.Extensions.AnimationExtensions;

namespace Microsoft.Toolkit.Uwp.UI.Animations
{
    /// <summary>
    /// A base model representing a typed keyframe that can be used in XAML.
    /// </summary>
    /// <typeparam name="TValue">
    /// The type to use for the public <see cref="Value"/> property.
    /// This can differ from <typeparamref name="TKeyFrame"/> to facilitate XAML parsing.
    /// </typeparam>
    /// <typeparam name="TKeyFrame">The actual type of keyframe values in use.</typeparam>
    public abstract class KeyFrame<TValue, TKeyFrame> : IKeyFrame<TKeyFrame>
    {
        /// <summary>
        /// Gets or sets the key time for the current keyframe. This is a normalized
        /// value in the [0, 1] range, relative to the total animation duration.
        /// </summary>
        public double Key { get; set; }

        /// <summary>
        /// Gets or sets the animation value for the current keyframe.
        /// </summary>
        public TValue? Value { get; set; }

        /// <summary>
        /// Gets or sets the optional expression for the current keyframe.
        /// If this is set, <see cref="Value"/> will be ignored.
        /// </summary>
        public string? Expression { get; set; }

        /// <summary>
        /// Gets or sets the optional easing function type for the keyframe.
        /// </summary>
        public EasingType? EasingType { get; set; }

        /// <summary>
        /// Gets or sets the optional easing function mode for the keyframe.
        /// </summary>
        public EasingMode? EasingMode { get; set; }

        /// <inheritdoc/>
        public INormalizedKeyFrameAnimationBuilder<TKeyFrame> AppendToBuilder(INormalizedKeyFrameAnimationBuilder<TKeyFrame> builder)
        {
            if (Expression is not null)
            {
                return builder.ExpressionKeyFrame(Key, Expression, EasingType ?? DefaultEasingType, EasingMode ?? DefaultEasingMode);
            }

            return builder.KeyFrame(Key, GetParsedValue()!, EasingType ?? DefaultEasingType, EasingMode ?? DefaultEasingMode);
        }

        /// <summary>
        /// Gets the parsed <typeparamref name="TKeyFrame"/> values for <see cref="Value"/>.
        /// </summary>
        /// <returns>The parsed keyframe values a <typeparamref name="TKeyFrame"/>.</returns>
        protected abstract TKeyFrame? GetParsedValue();
    }
}
