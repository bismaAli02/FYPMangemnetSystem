﻿#pragma checksum "..\..\..\..\UserControlls\AdvisorsUsercontrolls\AddAdvUC.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "DF23C75FF94467E03E3F498BFB29DF657A2864824E262D742B8A5AE51B74DE27"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using FYPManagementSystem.UserControlls.AdvisorsUsercontrolls;
using MahApps.Metro.IconPacks;
using MahApps.Metro.IconPacks.Converter;
using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Converters;
using MaterialDesignThemes.Wpf.Transitions;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace FYPManagementSystem.UserControlls.AdvisorsUsercontrolls {
    
    
    /// <summary>
    /// AddAdvUC
    /// </summary>
    public partial class AddAdvUC : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 33 "..\..\..\..\UserControlls\AdvisorsUsercontrolls\AddAdvUC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BackButton;
        
        #line default
        #line hidden
        
        
        #line 74 "..\..\..\..\UserControlls\AdvisorsUsercontrolls\AddAdvUC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox GenderComboBox;
        
        #line default
        #line hidden
        
        
        #line 130 "..\..\..\..\UserControlls\AdvisorsUsercontrolls\AddAdvUC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox DesComboBox;
        
        #line default
        #line hidden
        
        
        #line 139 "..\..\..\..\UserControlls\AdvisorsUsercontrolls\AddAdvUC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button CancleButton;
        
        #line default
        #line hidden
        
        
        #line 148 "..\..\..\..\UserControlls\AdvisorsUsercontrolls\AddAdvUC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SaveButton;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/FYPManagementSystem;component/usercontrolls/advisorsusercontrolls/addadvuc.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\UserControlls\AdvisorsUsercontrolls\AddAdvUC.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.BackButton = ((System.Windows.Controls.Button)(target));
            
            #line 33 "..\..\..\..\UserControlls\AdvisorsUsercontrolls\AddAdvUC.xaml"
            this.BackButton.Click += new System.Windows.RoutedEventHandler(this.BackButton_Click_1);
            
            #line default
            #line hidden
            return;
            case 2:
            this.GenderComboBox = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 3:
            this.DesComboBox = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 4:
            this.CancleButton = ((System.Windows.Controls.Button)(target));
            
            #line 139 "..\..\..\..\UserControlls\AdvisorsUsercontrolls\AddAdvUC.xaml"
            this.CancleButton.Click += new System.Windows.RoutedEventHandler(this.CancleButton_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.SaveButton = ((System.Windows.Controls.Button)(target));
            
            #line 148 "..\..\..\..\UserControlls\AdvisorsUsercontrolls\AddAdvUC.xaml"
            this.SaveButton.Click += new System.Windows.RoutedEventHandler(this.SaveButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

