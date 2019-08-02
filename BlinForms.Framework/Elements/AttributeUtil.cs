using Microsoft.AspNetCore.Components.RenderTree;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlinForms.Framework.Elements
{
    static class AttributeUtil
    {
        public static IEnumerable<RenderTreeFrame> ElementAttributeFrames(RenderTreeFrame[] frames, int elementFrameIndex)
        {
            var endIndexExcl = elementFrameIndex + frames[elementFrameIndex].ElementSubtreeLength;
            for (var attributeIndex = elementFrameIndex + 1; attributeIndex < endIndexExcl; attributeIndex++)
            {
                var candidateFrame = frames[attributeIndex];
                if (candidateFrame.FrameType == RenderTreeFrameType.Attribute)
                {
                    yield return candidateFrame;
                }
                else
                {
                    break;
                }
            }
        }

    }
}
