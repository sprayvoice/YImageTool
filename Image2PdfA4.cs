using iText.Commons.Utils;
using iText.IO.Image;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.Kernel.Pdf.Xobject;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Pdfa.Checker;
using Org.BouncyCastle.Ocsp;
using SkiaSharp;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.Encodings;
using System.IO;
using System;

namespace core_admin.utils
{
    public class Image2PdfA4
    {


        public static SKBitmap Rotate90Clockwise(SKBitmap original)
        {
            // 创建新位图，尺寸为原图的高和宽
            SKBitmap rotatedBitmap = new SKBitmap(original.Height, original.Width);

            using (SKCanvas canvas = new SKCanvas(rotatedBitmap))
            {
                canvas.Clear(); // 清除画布，可选

                // 平移画布到右侧边缘，然后旋转90度
                canvas.Translate(original.Height, 0);
                canvas.RotateDegrees(90);

                // 绘制原图到变换后的位置
                canvas.DrawBitmap(original, 0, 0);
            }

            return rotatedBitmap;
        }

        public static SKBitmap Rotate90CounterClockwise(SKBitmap original)
        {
            SKBitmap rotatedBitmap = new SKBitmap(original.Height, original.Width);

            using (SKCanvas canvas = new SKCanvas(rotatedBitmap))
            {
                canvas.Clear();

                // 平移到底部边缘，然后旋转-90度（或270度）
                canvas.Translate(0, original.Width);
                canvas.RotateDegrees(-90);

                canvas.DrawBitmap(original, 0, 0);
            }

            return rotatedBitmap;
        }

        public static void SaveAsPng(SKBitmap bitmap, string filePath)
        {
            // 确保目标文件夹存在
            Directory.CreateDirectory(System.IO.Path.GetDirectoryName(filePath));

            // 使用 SkiaSharp 编码为 PNG 格式
            using (SKData data = bitmap.Encode(SKEncodedImageFormat.Png, 100))
            {
                // 将数据保存到文件
                using (FileStream stream = File.OpenWrite(filePath))
                {
                    data.SaveTo(stream);
                }
            }
        }

        public static void convert(string imagePath,string outPath)
        {

            string fileName1 = System.IO.Path.GetFileName(imagePath);
            string dir11 = System.IO.Path.GetDirectoryName(imagePath);
            Random random = new Random();
            int r1 = random.Next();
            string tmp_file_name = "temp" + DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + r1.ToString();
            string tmpFile = System.IO.Path.Combine(dir11, tmp_file_name);
            string imagePath1 = tmpFile;
            int image_width = 0;
            int image_height = 0;
            try
            {
                SKBitmap image = SKBitmap.Decode(imagePath);
                if (image != null)
                {
                    image_width = image.Width;
                    image_height = image.Height;

                    if (image_width > image_height)
                    {
                        image = Rotate90Clockwise(image);
                    }

                    SaveAsPng(image, imagePath1);
                    // image.save(stream, SKImageEncoder EncodedFormat.Png);
                    image.Dispose();
                }



                PdfDocument pdfDoc = new PdfDocument(new PdfWriter(outPath));
                Document document = new Document(pdfDoc);
                Table wrapperTable = new Table(1);
                iText.Layout.Element.Image whiteImages =
                 new iText.Layout.Element.Image(ImageDataFactory.Create(imagePath1));
                whiteImages.SetBorder(Border.NO_BORDER);
                whiteImages.SetAutoScale(true);
                whiteImages.SetWidth(794);

                wrapperTable.AddCell(new Cell().Add(whiteImages).SetBorder(Border.NO_BORDER));

                document.Add(wrapperTable);
                pdfDoc.Close();

                System.IO.File.Delete(tmpFile);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
