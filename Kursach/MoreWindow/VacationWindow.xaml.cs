﻿using Kursach.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Kursach.MoreWindow
{
    /// <summary>
    /// Логика взаимодействия для VacationWindow.xaml
    /// </summary>
    public partial class VacationWindow : Window
    {
        public VacationWindow()
        {
            InitializeComponent();
            DataContext = new VacationVM();
        }
    }
}
