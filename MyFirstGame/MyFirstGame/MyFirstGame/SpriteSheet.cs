using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyFirstGame
{
    class SpriteSheet
    {
        public Texture2D texture;
        public Point sheetSize; //Width and height of sheet
        public SpriteSheetSegment[] segments = new SpriteSheetSegment[4];
        public SpriteSheetSegment currentSegment;
        private int numSegment;

        public SpriteSheet(Texture2D aTexture, Point aSheetSize)
        {
            this.texture = aTexture;
            this.sheetSize = aSheetSize;
            numSegment = 0;
        }

        public void addSegment(Point frameSize, Point startFrame, Point endFrame, int millisPerFrame)
        {
            segments[numSegment] = new SpriteSheetSegment(frameSize, startFrame, endFrame, millisPerFrame);
            
            ++numSegment;
        }

        public void setCurrentSegment(int which)
        {
            currentSegment = segments[which];
        }


    }
}
