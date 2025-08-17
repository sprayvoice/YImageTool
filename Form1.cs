using core_admin.utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YImageForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
        }

      

        public string image_path = "";

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "选择文件"; // 对话框标题
            openFileDialog.InitialDirectory = @"C:\"; // 初始目录
            //openFileDialog.Filter = "文本所有文件|*.*"; // 文件过滤器
            //openFileDialog.FilterIndex = 1; // 默认过滤器索引
            openFileDialog.Multiselect = false; // 是否允许多选
            if (openFileDialog.ShowDialog() == DialogResult.OK) // 显示对话框
            {
                string selectedFilePath = openFileDialog.FileName; // 获取选中文件路径
                Image img = Image.FromFile(selectedFilePath);                                                 // 处理文件...
                int width = img.Width;
                int height = img.Height;
                pictureBox1.Width  = width; 
                pictureBox1.Height = height;
                pictureBox1.Image = img;
                image_path = selectedFilePath;
                label4.Text = selectedFilePath;
            }
        }

        private Rectangle GetImageRectangle()
        {
            if (pictureBox1.Image == null) return Rectangle.Empty;

            int picWidth = pictureBox1.Width;
            int picHeight = pictureBox1.Height;
            int imgWidth = pictureBox1.Image.Width;
            int imgHeight = pictureBox1.Image.Height;

            switch (pictureBox1.SizeMode)
            {
                case PictureBoxSizeMode.Normal:
                case PictureBoxSizeMode.AutoSize:
                    return new Rectangle(0, 0, imgWidth, imgHeight);

                case PictureBoxSizeMode.StretchImage:
                    return new Rectangle(0, 0, picWidth, picHeight);

                case PictureBoxSizeMode.CenterImage:
                    int x = (picWidth - imgWidth) / 2;
                    int y = (picHeight - imgHeight) / 2;
                    return new Rectangle(x, y, imgWidth, imgHeight);

                case PictureBoxSizeMode.Zoom:
                    float ratio = Math.Min((float)picWidth / imgWidth, (float)picHeight / imgHeight);
                    int scaledWidth = (int)(imgWidth * ratio);
                    int scaledHeight = (int)(imgHeight * ratio);
                    int posX = (picWidth - scaledWidth) / 2;
                    int posY = (picHeight - scaledHeight) / 2;
                    return new Rectangle(posX, posY, scaledWidth, scaledHeight);

                default:
                    return new Rectangle(0, 0, picWidth, picHeight);
            }
        }

        private Point GetImagePoint(Point controlPoint)
        {
            if (pictureBox1.Image == null) return Point.Empty;

            // 获取图片在控件中的显示区域
            Rectangle imageRect = GetImageRectangle();

            // 计算相对于图片的位置
            float scaleX = (float)pictureBox1.Image.Width / imageRect.Width;
            float scaleY = (float)pictureBox1.Image.Height / imageRect.Height;

            int x = (int)((controlPoint.X - imageRect.X) * scaleX);
            int y = (int)((controlPoint.Y - imageRect.Y) * scaleY);

            // 确保坐标在图片范围内
            x = Math.Max(0, Math.Min(x, pictureBox1.Image.Width - 1));
            y = Math.Max(0, Math.Min(y, pictureBox1.Image.Height - 1));

            return new Point(x, y);
        }

        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                return;
            }
            MouseEventArgs e1 = (MouseEventArgs)e;
            Point controlPoint = e1.Location;
            Point imagePoint = GetImagePoint(controlPoint);
            //lastClickPoint = imagePoint;
            if (textBox1.Text.Length == 0)
            {
                textBox1.Text = imagePoint.X + "," + imagePoint.Y;
            } else if (textBox2.Text.Length == 0)
            {
                textBox2.Text = imagePoint.X + "," + imagePoint.Y;
            }
                
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 0 && textBox2.Text.Length > 0)
            {
                int b1 = 0;
                int b2 = 0;
                int b3 = 0;
                int b4 = 0;
                string[] a1 = textBox1.Text.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                if (a1.Length == 2)
                {
                    b1 =  Convert.ToInt32( a1[0]);
                    b2 = Convert.ToInt32(a1[1]);
                }
                string[] a2 = textBox2.Text.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                if (a2.Length == 2)
                {
                    b3 = Convert.ToInt32(a2[0]);
                    b4 = Convert.ToInt32(a2[1]);
                }
                string file_ext = System.IO.Path.GetExtension(image_path);
                string file_name = System.IO.Path.GetFileNameWithoutExtension(image_path);
                string file_path = System.IO.Path.GetDirectoryName(image_path);
                string new_file_name = file_name + "_out" + file_ext;
                string full_new_file_path = System.IO.Path.Combine(file_path, new_file_name);
                ImageCropper.CropImage(image_path,
                    new Point(b1, b2),
                    new Point(b3, b4),
                    full_new_file_path);
                if (comboBox1.SelectedItem.ToString() == "pdf文件")
                {
                    if (System.IO.File.Exists(full_new_file_path))
                    {
                        string new_file_name_pdf = file_name + "_out.pdf";
                        string full_new_file_pdf_path = System.IO.Path.Combine(file_path, new_file_name_pdf);
                        Image2PdfA4.convert(full_new_file_path, full_new_file_pdf_path);
                    }
                }

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 0 && textBox2.Text.Length > 0)
            {
                int b1 = 0;
                int b2 = 0;
                int b3 = 0;
                int b4 = 0;
                string[] a1 = textBox1.Text.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                if (a1.Length == 2)
                {
                    b1 = Convert.ToInt32(a1[0]);
                    b2 = Convert.ToInt32(a1[1]);
                }
                string[] a2 = textBox2.Text.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                if (a2.Length == 2)
                {
                    b3 = Convert.ToInt32(a2[0]);
                    b4 = Convert.ToInt32(a2[1]);
                }

                string file_path = System.IO.Path.GetDirectoryName(image_path);
                string[] files = System.IO.Directory.GetFiles(file_path);
                foreach(string a_file in files)
                {
                    string file_ext = System.IO.Path.GetExtension(a_file);
                    file_ext = file_ext.ToLower();
                    if (file_ext == ".jpg" || file_ext == ".jpeg" || file_ext == ".png" 
                        || file_ext == ".gif"||file_ext==".tif"||file_ext==".tiff" || file_ext == ".bmp")
                    {
                        string file_name = System.IO.Path.GetFileNameWithoutExtension(a_file);

                        string new_file_name = file_name + "_out" + file_ext;
                        string full_new_file_path = System.IO.Path.Combine(file_path, new_file_name);
                        try
                        {
                            ImageCropper.CropImage(a_file,
                                new Point(b1, b2),
                                new Point(b3, b4),
                                full_new_file_path);
                            if (comboBox1.SelectedItem.ToString() == "pdf文件")
                            {

                                if (System.IO.File.Exists(full_new_file_path))
                                {
                                    string new_file_name_pdf = file_name + "_out.pdf";
                                    string full_new_file_pdf_path = System.IO.Path.Combine(file_path, new_file_name_pdf);
                                    Image2PdfA4.convert(full_new_file_path, full_new_file_pdf_path);
                                }
                             
                            }
                        } catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                    
               
                }


            }
        }
    }
}
