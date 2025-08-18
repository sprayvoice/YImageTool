using SkiaSharp;
using System;
using System.IO;

namespace core_admin.utils
{
    public class ImageSplitter
    {
        public static void GetWidthHeight(string inputImagePath,
            ref int width,ref int height)
        {
            var original = SKBitmap.Decode(inputImagePath);
            width = original.Width;
            height = original.Height;

        }

        public static void SplitImageTopBottom(
            string inputImagePath,
            string topOutputPath,
            string bottomOutputPath,
            int topHeight)
        {
            // 加载原始图片
            var original = SKBitmap.Decode(inputImagePath);

            // 计算上下分割点（取整）
            //int topHeight = original.Width / 2;
            int bottomHeight = original.Height - topHeight;

            // 创建上半部分图片
            var topBitmap = new SKBitmap(original.Width, topHeight);
            using (var topCanvas = new SKCanvas(topBitmap))
            {
                topCanvas.DrawBitmap(
                    original,
                   new SKRect(0, 0, original.Width, topHeight),
                    new SKRect(0, 0, original.Width, topHeight)
                );
            }

            // 创建下半部分图片
            var bottomBitmap = new SKBitmap(original.Width, bottomHeight);
            using (var rightCanvas = new SKCanvas(bottomBitmap))
            {
                rightCanvas.DrawBitmap(
                  original,
                    new SKRect(0, topHeight, original.Width, original.Height),
                   new SKRect(0, 0, original.Width, bottomHeight)
                );
            }

            // 保存分割后的图片
            using (var leftStream = File.OpenWrite(topOutputPath))
            using (var rightStream = File.OpenWrite(bottomOutputPath))
            {
                topBitmap.Encode(leftStream, SKEncodedImageFormat.Png, 100);
                bottomBitmap.Encode(rightStream, SKEncodedImageFormat.Png, 100);
            }
        }


        public static void SplitImageLeftRight(
            string inputImagePath,
            string leftOutputPath,
            string rightOutputPath,
            int leftWidth)
        {
            // 加载原始图片
            var original = SKBitmap.Decode(inputImagePath);

            // 计算左右分割点（取整）
            //int topHeight = original.Width / 2;
            int rightWidth = original.Width - leftWidth;

            // 创建左半部分图片
            var leftBitmap = new SKBitmap(leftWidth, original.Height);
            using (var leftCanvas = new SKCanvas(leftBitmap))
            {
                leftCanvas.DrawBitmap(
                    original,
                   new SKRect(0, 0, leftWidth, original.Height),
                    new SKRect(0, 0, leftWidth, original.Height)
                );
            }

            // 创建右半部分图片
            var rightBitmap = new SKBitmap(rightWidth, original.Height);
            using (var rightCanvas = new SKCanvas(rightBitmap))
            {
                rightCanvas.DrawBitmap(
                  original,
                    new SKRect(leftWidth, 0, original.Width, original.Height),
                   new SKRect(0, 0, rightWidth, original.Height)
                );
            }

            // 保存分割后的图片
            using (var leftStream = File.OpenWrite(leftOutputPath))
            using (var rightStream = File.OpenWrite(rightOutputPath))
            {
                leftBitmap.Encode(leftStream, SKEncodedImageFormat.Png, 100);
                rightBitmap.Encode(rightStream, SKEncodedImageFormat.Png, 100);
            }
        }
    }
}
