﻿#pragma checksum "..\..\..\..\Pages\EditScheduleTabPage.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "28A567DBEE8246C5F9470343B03EDDFDCE39314B"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Scheduler.Pages;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
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


namespace Scheduler.Pages {
    
    
    /// <summary>
    /// EditScheduleTabPage
    /// </summary>
    public partial class EditScheduleTabPage : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 17 "..\..\..\..\Pages\EditScheduleTabPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button GoBackBttn;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\..\..\Pages\EditScheduleTabPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox ClassesTimingConboBox;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\..\..\Pages\EditScheduleTabPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid EditedGrid;
        
        #line default
        #line hidden
        
        
        #line 51 "..\..\..\..\Pages\EditScheduleTabPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridTemplateColumn SubjectColumn;
        
        #line default
        #line hidden
        
        
        #line 66 "..\..\..\..\Pages\EditScheduleTabPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridComboBoxColumn TutorColumn;
        
        #line default
        #line hidden
        
        
        #line 67 "..\..\..\..\Pages\EditScheduleTabPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridComboBoxColumn CabinetColumn;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.3.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Scheduler;component/pages/editscheduletabpage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Pages\EditScheduleTabPage.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.3.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 10 "..\..\..\..\Pages\EditScheduleTabPage.xaml"
            ((Scheduler.Pages.EditScheduleTabPage)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Page_Loaded);
            
            #line default
            #line hidden
            
            #line 11 "..\..\..\..\Pages\EditScheduleTabPage.xaml"
            ((Scheduler.Pages.EditScheduleTabPage)(target)).Unloaded += new System.Windows.RoutedEventHandler(this.Page_Unloaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.GoBackBttn = ((System.Windows.Controls.Button)(target));
            
            #line 17 "..\..\..\..\Pages\EditScheduleTabPage.xaml"
            this.GoBackBttn.Click += new System.Windows.RoutedEventHandler(this.GoBackBttn_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.ClassesTimingConboBox = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 4:
            this.EditedGrid = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 5:
            this.SubjectColumn = ((System.Windows.Controls.DataGridTemplateColumn)(target));
            return;
            case 6:
            this.TutorColumn = ((System.Windows.Controls.DataGridComboBoxColumn)(target));
            return;
            case 7:
            this.CabinetColumn = ((System.Windows.Controls.DataGridComboBoxColumn)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
