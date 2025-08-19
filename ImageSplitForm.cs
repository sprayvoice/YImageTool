using core_admin.utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace YImageForm
{
    public partial class ImageSplitForm : Form
    {
        public ImageSplitForm()
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
                pictureBox1.Width = width;
                pictureBox1.Height = height;
                pictureBox1.Image = img;
                image_path = selectedFilePath;
                label1.Text = selectedFilePath;
                label2.Text = img.Width + " x " + img.Height;

            }
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

        private void button2_Click(object sender, EventArgs e)
        {
            if (image_path == "")
                return;

            if (comboBox1.SelectedItem.ToString() == "左右")
            {
                string[] width_array = textBox1.Text.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                if (width_array.Length == 2)
                {
                    int width = Int32.Parse(width_array[0]);
                    string extension = System.IO.Path.GetExtension(image_path);
                    string image_left_name = System.IO.Path.GetFileNameWithoutExtension(image_path)+"_left"+ extension;
                    string image_right_name = System.IO.Path.GetFileNameWithoutExtension(image_path)+"_right"+ extension;
                    string dir = System.IO.Path.GetDirectoryName(image_path);
                    string left_full = System.IO.Path.Combine(dir, image_left_name);
                    string right_full = System.IO.Path.Combine(dir,image_right_name);
                    ImageSplitter.SplitImageLeftRight(image_path, left_full, right_full, width);
                }
            }
            if (comboBox1.SelectedItem.ToString() == "上下")
            {
                string[] width_array = textBox1.Text.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                if (width_array.Length == 2)
                {
                    int top = Int32.Parse(width_array[1]);
                    string extension = System.IO.Path.GetExtension(image_path);
                    string image_left_name = System.IO.Path.GetFileNameWithoutExtension(image_path) + "_top" + extension;
                    string image_right_name = System.IO.Path.GetFileNameWithoutExtension(image_path) + "_bottom" + extension;
                    string dir = System.IO.Path.GetDirectoryName(image_path);
                    string left_full = System.IO.Path.Combine(dir, image_left_name);
                    string right_full = System.IO.Path.Combine(dir, image_right_name);
                    ImageSplitter.SplitImageTopBottom(image_path, left_full, right_full, top);
                }
            }
        }
    }
}
