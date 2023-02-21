using OpenAI.GPT3.Managers;
using OpenAI.GPT3.ObjectModels.RequestModels;
using OpenAI.GPT3.ObjectModels;
using OpenAI.GPT3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using WpfApp1.Command;
using WpfApp1.MyControls;
using System.IO;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string OPENAPI_TOKEN = "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";//输入自己的api-key
        readonly OpenAIService service = new OpenAIService(new OpenAiOptions() { ApiKey = OPENAPI_TOKEN });

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
        }


        /// <summary>
        /// 非忙碌状态
        /// </summary>
        public bool IsNotBusy
        {
            get { return (bool)GetValue(IsNotBusyProperty); }
            set { SetValue(IsNotBusyProperty, value); }
        }

        public static readonly DependencyProperty IsNotBusyProperty =
            DependencyProperty.Register("IsNotBusy", typeof(bool), typeof(MainWindow), new FrameworkPropertyMetadata(true));

        /// <summary>
        /// 用户输入的内容
        /// </summary>
        public string Message
        {
            get { return (string)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }

        public static readonly DependencyProperty MessageProperty =
            DependencyProperty.Register("Message", typeof(string), typeof(MainWindow), new FrameworkPropertyMetadata());


        ///// <summary>
        ///// 消息队列
        ///// </summary>
        //public BindingList<UserControl> MessageList
        //{
        //    get { return (BindingList<UserControl>)GetValue(MessageListProperty); }
        //    set { SetValue(MessageListProperty, value); }
        //}

        //public static readonly DependencyProperty MessageListProperty =
        //    DependencyProperty.Register("MessageList", typeof(BindingList<UserControl>), typeof(MainWindow), new FrameworkPropertyMetadata(new BindingList<UserControl>() { new ChatLeft()}));



        private CommandBase btnPostCommand;
        public CommandBase BtnPostCommand =>
            btnPostCommand ?? (btnPostCommand = new CommandBase(Post));

        /// <summary>
        /// 发送消息
        /// </summary>
        async void Post()
        {
            try
            {
                IsNotBusy = false;
                AddMessage(new ChatRight()
                {
                    UserName = "我",
                    Messages = new BindingList<string>() { Message },
                    LastTime = DateTime.Now,
                });
                CompletionCreateRequest createRequest = new CompletionCreateRequest()
                {
                    Prompt = Message,
                    Temperature = 0.9f,
                    MaxTokens = 2048,
                    Model = Models.TextDavinciV3,
                    Stream = false,
                };
                var res = await service.Completions.CreateCompletion(createRequest, Models.TextDavinciV3);
                if (res.Successful)
                {
                    var result = res.Choices.FirstOrDefault().Text;
                    AddMessage(new ChatLeft()
                    {
                        //Picture = gptImage,
                        UserName = "AI",
                        Messages = new BindingList<string>() { result.Trim('\n') },
                        LastTime = DateTime.Now,
                    });
                }
                else
                {
                    AddMessage(new ChatLeft()
                    {
                        //Picture = gptImage,
                        UserName = "AI",
                        Messages = new BindingList<string>() { "连接失败" },
                        LastTime = DateTime.Now,
                    });
                }
            }
            catch (Exception ex)
            {
                AddMessage(new ChatLeft()
                {
                    //Picture = gptImage,
                    UserName = "AI",
                    Messages = new BindingList<string>() { "连接失败：" + ex.Message },
                    LastTime = DateTime.Now,
                });
            }
            finally
            {
                Message = string.Empty;
                IsNotBusy = true;
            }

        }

        /// <summary>
        /// 添加消息记录
        /// </summary>
        /// <param name="control"></param>
        private void AddMessage(UserControl control)
        {
            pMessage.Children.Add(control);
            if (pMessage.Children.Count >= 100)
            {
                pMessage.Children.RemoveAt(0);
                GC.Collect();
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {

        }
    }
}
