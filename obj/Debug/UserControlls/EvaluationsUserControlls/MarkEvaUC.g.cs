#pragma checksum "..\..\..\..\UserControlls\EvaluationsUserControlls\MarkEvaUC.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "7392BA2FF5776824C445A894D92C8970DEB70151FE76D37EDC94CE2BEED56BE2"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using FYPManagementSystem.UserControlls.StudentsUserControlls;
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


namespace FYPManagementSystem.UserControlls.EvaluationsUserControlls {
    
    
    /// <summary>
    /// MarkEvaUC
    /// </summary>
    public partial class MarkEvaUC : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 45 "..\..\..\..\UserControlls\EvaluationsUserControlls\MarkEvaUC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox GIdComboBox;
        
        #line default
        #line hidden
        
        
        #line 54 "..\..\..\..\UserControlls\EvaluationsUserControlls\MarkEvaUC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox EvaComboBox;
        
        #line default
        #line hidden
        
        
        #line 64 "..\..\..\..\UserControlls\EvaluationsUserControlls\MarkEvaUC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TMTextBox;
        
        #line default
        #line hidden
        
        
        #line 73 "..\..\..\..\UserControlls\EvaluationsUserControlls\MarkEvaUC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox OMTextBox;
        
        #line default
        #line hidden
        
        
        #line 76 "..\..\..\..\UserControlls\EvaluationsUserControlls\MarkEvaUC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button CancelButton;
        
        #line default
        #line hidden
        
        
        #line 85 "..\..\..\..\UserControlls\EvaluationsUserControlls\MarkEvaUC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SaveButton;
        
        #line default
        #line hidden
        
        
        #line 88 "..\..\..\..\UserControlls\EvaluationsUserControlls\MarkEvaUC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock SaveButtonTxt;
        
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
            System.Uri resourceLocater = new System.Uri("/FYPManagementSystem;component/usercontrolls/evaluationsusercontrolls/markevauc.x" +
                    "aml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\UserControlls\EvaluationsUserControlls\MarkEvaUC.xaml"
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
            this.GIdComboBox = ((System.Windows.Controls.ComboBox)(target));
            
            #line 45 "..\..\..\..\UserControlls\EvaluationsUserControlls\MarkEvaUC.xaml"
            this.GIdComboBox.DropDownClosed += new System.EventHandler(this.GIdComboBox_DropDownClosed);
            
            #line default
            #line hidden
            return;
            case 2:
            this.EvaComboBox = ((System.Windows.Controls.ComboBox)(target));
            
            #line 54 "..\..\..\..\UserControlls\EvaluationsUserControlls\MarkEvaUC.xaml"
            this.EvaComboBox.DropDownClosed += new System.EventHandler(this.EvaComboBox_DropDownClosed);
            
            #line default
            #line hidden
            return;
            case 3:
            this.TMTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.OMTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.CancelButton = ((System.Windows.Controls.Button)(target));
            
            #line 76 "..\..\..\..\UserControlls\EvaluationsUserControlls\MarkEvaUC.xaml"
            this.CancelButton.Click += new System.Windows.RoutedEventHandler(this.CancelButton_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.SaveButton = ((System.Windows.Controls.Button)(target));
            
            #line 85 "..\..\..\..\UserControlls\EvaluationsUserControlls\MarkEvaUC.xaml"
            this.SaveButton.Click += new System.Windows.RoutedEventHandler(this.SaveButton_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.SaveButtonTxt = ((System.Windows.Controls.TextBlock)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

