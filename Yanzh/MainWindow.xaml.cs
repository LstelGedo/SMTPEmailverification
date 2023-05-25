using System.Windows;
using System.Net;
using System.Net.Mail;
using System;

namespace Yanzh
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        //定义一个全局的string类型验证码（存放）
        string code = "";

        //验证码
        //定义一个字符串，用于包含验证码所需要的元素
        string a = "abcdefghijklmnopqrstuvwxyz0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        
        //发送验证码
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //实例化一个随机数
            Random b = new Random();

            

            //用于循环验证码的长度
            //默认6位数、8为数、12位数
            for (int i = 0; i < 8; i++)
            {
                code = code + a.Substring(b.Next(0, a.Length), 1);
            }

            //创建服务器对象
            //前提邮箱需要先启动SMTP协议
            SmtpClient smtp = new SmtpClient("smtp.qq.com");

            //创建邮件发送人
            //授权码需要在邮箱验证后获取
            //授权码在开启SMTP后就会提供
            MailAddress mail1 = new MailAddress("XXXXXXXXX@qq.com");

            try
            {
                //获取文本框的收件人邮箱
                MailAddress mail2 = new MailAddress(textBox1.Text);



                //创建邮件对象，准备发送
                //注意：mail1是发件人、而mail2是收件人的邮箱地址
                MailMessage mess = new MailMessage(mail1,mail2);


                //设置邮件的标题
                mess.Subject = "邮件验证码";

                //设置邮箱的附件
                //附件需要设置格式自行查找教程
               // mess.Attachments = "邮箱附件";



                //邮件的内容
                //内容可以是死数据或者是text文本框获取
                //mess发送的内容
                mess.Body = "您的验证码为  " + code + "  请在30分钟内容验证，系统邮件请勿回复！";

                //创建互联网安全证书（邮箱的授权码）
                //授权码在开启SMTP后就会提供
                NetworkCredential cred = new NetworkCredential("XXXXXXXXX@qq.com", "rsuwdynohjff");

                //证书绑定到服务器对象以便服务器验证
                //也就是获取发件人的邮箱，和邮箱的授权码来验证
                smtp.Credentials = cred;

                //开始发送
                smtp.Send(mess);

                //设置按钮，在点击发送后将按钮设置为不可点击的状态
                button1.IsEnabled = false;
                
                
                MessageBox.Show("验证码发送成功！");


            }
            catch (Exception)
            {

                //没有做邮箱格式验证，自行添加正则表达式
                MessageBox.Show("请输入正确的邮箱格式！");
            }
        }

        //验证验证码
        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            if (code == textBox2.Text)
            {
                MessageBox.Show("验证成功!");
                button1.IsEnabled = true;
            }
            else
            {
                MessageBox.Show("验证码错误请检查！");
                button1.IsEnabled = true;
            }
        }


    }
}
