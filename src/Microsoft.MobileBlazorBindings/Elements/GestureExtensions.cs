// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using System;
using Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public static class GestureExtensions
    {
        /// <summary>
        /// Calls the action specified in <paramref name="configureGesture"/> so that code can be called inline
        /// in a collection initializer when creating the <paramref name="gesture"/>.
        /// </summary>
        /// <typeparam name="TGesture"></typeparam>
        /// <param name="gesture">The gesture to which the events will be added.</param>
        /// <param name="configureGesture">The action to run that will add the events.</param>
        /// <returns></returns>
        public static TGesture AddEvents<TGesture>(this TGesture gesture, Action<TGesture> configureGesture)
            where TGesture : IGestureRecognizer
        {
            if (gesture is null)
            {
                throw new ArgumentNullException(nameof(gesture));
            }
            if (configureGesture is null)
            {
                throw new ArgumentNullException(nameof(configureGesture));
            }
            configureGesture(gesture);
            return gesture;
        }
    }
}
