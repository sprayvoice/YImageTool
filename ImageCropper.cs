using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

public class ImageCropper
{
    /// <summary>
    /// 根据指定坐标裁剪图片并保存
    /// </summary>
    /// <param name="sourceImagePath">源图片路径</param>
    /// <param name="topLeft">左上角坐标 (X, Y)</param>
    /// <param name="bottomRight">右下角坐标 (X, Y)</param>
    /// <param name="savePath">保存路径</param>
    /// <returns>成功返回true，失败返回false</returns>
    public static bool CropImage(string sourceImagePath, Point topLeft, Point bottomRight, string savePath)
    {
        // 验证输入参数
        if (string.IsNullOrWhiteSpace(sourceImagePath))
        {
            throw new ArgumentException("源图片路径不能为空", nameof(sourceImagePath));
        }

        if (string.IsNullOrWhiteSpace(savePath))
        {
            throw new ArgumentException("保存路径不能为空", nameof(savePath));
        }

        if (!File.Exists(sourceImagePath))
        {
            throw new FileNotFoundException($"找不到源图片: {sourceImagePath}");
        }

        // 验证坐标有效性
        if (topLeft.X < 0 || topLeft.Y < 0 || bottomRight.X < 0 || bottomRight.Y < 0)
        {
            throw new ArgumentException("坐标值不能为负数");
        }

        if (bottomRight.X <= topLeft.X || bottomRight.Y <= topLeft.Y)
        {
            throw new ArgumentException("右下角坐标必须大于左上角坐标");
        }

        // 计算裁剪区域
        int width = bottomRight.X - topLeft.X;
        int height = bottomRight.Y - topLeft.Y;

        if (width <= 0 || height <= 0)
        {
            throw new ArgumentException("裁剪区域宽度和高度必须大于0");
        }

        Rectangle cropArea = new Rectangle(topLeft.X, topLeft.Y, width, height);

        Bitmap sourceImage = null;
        Bitmap croppedImage = null;

        try
        {
            // 加载源图片
            using (var tempImage = new Bitmap(sourceImagePath))
            {
                sourceImage = new Bitmap(tempImage);
            }

            // 验证裁剪区域是否在图片范围内
            if (cropArea.Right > sourceImage.Width || cropArea.Bottom > sourceImage.Height)
            {
                throw new ArgumentOutOfRangeException("裁剪区域超出图片范围");
            }

            // 创建裁剪后的图片
            croppedImage = new Bitmap(cropArea.Width, cropArea.Height);

            // 设置高质量绘图选项
            using (Graphics g = Graphics.FromImage(croppedImage))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;

                // 执行裁剪
                g.DrawImage(
                    sourceImage,
                    new Rectangle(0, 0, croppedImage.Width, croppedImage.Height),
                    cropArea,
                    GraphicsUnit.Pixel
                );
            }

            // 确定保存格式
            ImageFormat format = GetImageFormat(savePath);

            // 创建目录（如果不存在）
            string directory = Path.GetDirectoryName(savePath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            // 保存图片
            croppedImage.Save(savePath, format);

            return true;
        }
        catch (Exception ex)
        {
            // 在实际应用中，这里可以记录日志
            Console.WriteLine($"裁剪图片时出错: {ex.Message}");
            return false;
        }
        finally
        {
            // 清理资源
            sourceImage?.Dispose();
            croppedImage?.Dispose();
        }
    }

    /// <summary>
    /// 根据文件扩展名获取图片格式
    /// </summary>
    private static ImageFormat GetImageFormat(string filePath)
    {
        string extension = Path.GetExtension(filePath).ToLower();

        if (extension == ".jpg" || extension == ".jpeg")
            return ImageFormat.Jpeg;
        if (extension == ".png")
            return ImageFormat.Png;
        if (extension == ".bmp")
            return ImageFormat.Bmp;

        if (extension == ".gif")
            return ImageFormat.Gif;
        if (extension == ".tif" || extension == ".tiff")
            return ImageFormat.Tiff;
        return ImageFormat.Png;


    }
}