using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyFirstGame
{
    class SpriteSheetSegment
    {

        public Point frameSize;
        public Point startFrame;
        public Point endFrame;
        public int millisPerFrame;

        public SpriteSheetSegment(Point aFrameSize, Point aStartFrame, Point aEndFrame, int aMillisPerFrame)
        {
            this.frameSize = aFrameSize;
            this.startFrame = aStartFrame;
            this.endFrame = aEndFrame;
            this.millisPerFrame = aMillisPerFrame;
        }

    }
}
