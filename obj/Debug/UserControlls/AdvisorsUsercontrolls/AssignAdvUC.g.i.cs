// Updated by XamlIntelliSenseFileGenerator 01/03/2023 11:53:13 pm
#pragma checksum "..\..\..\..\UserControlls\AdvisorsUsercontrolls\AssignAdvUC.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "95BD35B7F2C10AEAC2B9D154135FFAED35FB7E3A9EBF03C4B398FD86371766FF"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using FYPManagementSystem;
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


namespace FYPManagementSystem.UserControlls.AdvisorsUsercontrolls
{


    /// <summary>
    /// AssignAdvUC
    /// </summary>
    public partial class AssignAdvUC : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector
    {


#line 28 "..\..\..\..\UserControlls\AdvisorsUsercontrolls\AssignAdvUC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button AssignProjectButton;

#line default
#line hidden


#line 30 "..\..\..\..\UserControlls\AdvisorsUsercontrolls\AssignAdvUC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid AssignAdvDataGrid;

#line default
#line hidden


#line 87 "..\..\..\..\UserControlls\AdvisorsUsercontrolls\AssignAdvUC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.UserControl addProjectUC;

#line default
#line hidden

        private bool _contentLoaded;

        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent()
        {
            if (_contentLoaded)
            {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/FYPManagementSystem;component/usercontrolls/advisorsusercontrolls/assignadvuc.xa" +
                    "ml", System.UriKind.Relative);

#line 1 "..\..\..\..\UserControlls\AdvisorsUsercontrolls\AssignAdvUC.xaml"
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
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target)
        {
            switch (connectionId)
            {
                case 1:
                    this.AssignProjectButton = ((System.Windows.Controls.Button)(target));

#line 28 "..\..\..\..\UserControlls\AdvisorsUsercontrolls\AssignAdvUC.xaml"
                    this.AssignProjectButton.Click += new System.Windows.RoutedEventHandler(this.AssignProjectButton_Click);

#line default
#line hidden
                    return;
                case 2:
                    this.AssignAdvDataGrid = ((System.Windows.Controls.DataGrid)(target));
                    return;
                case 5:
                    this.AddStuScroll = ((System.Windows.Controls.ScrollViewer)(target));
                    return;
                case 6:
                    this.addProjectUC = ((System.Windows.Controls.UserControl)(target));
                    return;
            }
            this._contentLoaded = true;
        }

        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        void System.Windows.Markup.IStyleConnector.Connect(int connectionId, object target)
        {
            switch (connectionId)
            {
                case 3:

#line 68 "..\..\..\..\UserControlls\AdvisorsUsercontrolls\AssignAdvUC.xaml"
                    ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.EditButton_Click);

#line default
#line hidden
                    break;
                case 4:

#line 78 "..\..\..\..\UserControlls\AdvisorsUsercontrolls\AssignAdvUC.xaml"
                    ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.DeleteButton_Click);

#line default
#line hidden
                    break;
            }
        }

        internal System.Windows.Controls.ScrollViewer AssignAdvScroll;
    }
}

