﻿using BookRecordMVVMSQLite.ViewModel;
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
using System.Windows.Shapes;

namespace BookRecordMVVMSQLite.View
{
    /// <summary>
    /// Interaction logic for NewBookWindow.xaml
    /// </summary>
    public partial class NewBookWindow : Window
    {
        public NewBookWindow()
        {
            InitializeComponent();

            NewBookVM vm = new NewBookVM();
            BookWindowGrid.DataContext = vm;
            vm.HasAddedBook += Vm_HasAddedBook;
        }

        private void Vm_HasAddedBook(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
