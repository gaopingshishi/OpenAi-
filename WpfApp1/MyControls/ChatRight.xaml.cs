﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace WpfApp1.MyControls
{
    /// <summary>
    /// ChatRight.xaml 的交互逻辑
    /// </summary>
    public partial class ChatRight : UserControl
    {
        public ChatRight()
        {
            InitializeComponent();
            this.DataContext = this;
        }


        /// <summary>
        /// 头像图片
        /// </summary>
        public Brush Picture
        {
            get { return (Brush)GetValue(PictureProperty); }
            set { SetValue(PictureProperty, value); }
        }

        public static readonly DependencyProperty PictureProperty =
            DependencyProperty.Register("Picture", typeof(Brush), typeof(ChatRight), new FrameworkPropertyMetadata());


        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName
        {
            get { return (string)GetValue(UserNameProperty); }
            set { SetValue(UserNameProperty, value); }
        }

        public static readonly DependencyProperty UserNameProperty =
            DependencyProperty.Register("UserName", typeof(string), typeof(ChatRight), new FrameworkPropertyMetadata());

        /// <summary>
        /// 聊天内容
        /// </summary>
        public BindingList<string> Messages
        {
            get { return (BindingList<string>)GetValue(MessagesProperty); }
            set { SetValue(MessagesProperty, value); }
        }

        public static readonly DependencyProperty MessagesProperty =
            DependencyProperty.Register("Messages", typeof(BindingList<string>), typeof(ChatRight), new FrameworkPropertyMetadata(new BindingList<string>() { "111111111111111111111111", "333333333333333333333333333333333333333333333333333333333333" }));


        /// <summary>
        /// 聊天框背景色
        /// </summary>
        public Brush MessageBackGround
        {
            get { return (Brush)GetValue(MessageBackGroundProperty); }
            set { SetValue(MessageBackGroundProperty, value); }
        }

        public static readonly DependencyProperty MessageBackGroundProperty =
            DependencyProperty.Register("MessageBackGround", typeof(Brush), typeof(ChatRight), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromRgb(0xC4, 0x9D, 0xFF))));

        /// <summary>
        /// 消息字体颜色
        /// </summary>
        public Brush MessageForeground
        {
            get { return (Brush)GetValue(MessageForegroundProperty); }
            set { SetValue(MessageForegroundProperty, value); }
        }

        public static readonly DependencyProperty MessageForegroundProperty =
            DependencyProperty.Register("MessageForeground", typeof(Brush), typeof(ChatRight), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromRgb(0xff, 0xff, 0xff))));



        public DateTime LastTime
        {
            get { return (DateTime)GetValue(LastTimeProperty); }
            set { SetValue(LastTimeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LastTime.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LastTimeProperty =
            DependencyProperty.Register("LastTime", typeof(DateTime), typeof(ChatRight), new FrameworkPropertyMetadata(DateTime.Now));



    }
}
