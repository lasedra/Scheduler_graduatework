﻿#pragma checksum "..\..\..\..\Services\ScheduleUserControl.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "27FDEFB076C5F38093390B4D0EE4CACE5F1A66F8"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

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


namespace Scheduler.Services {
    
    
    /// <summary>
    /// ScheduleUserControl
    /// </summary>
    public partial class ScheduleUserControl : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 22 "..\..\..\..\Services\ScheduleUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid MondayGrid;
        
        #line default
        #line hidden
        
        
        #line 49 "..\..\..\..\Services\ScheduleUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid TuesdayGrid;
        
        #line default
        #line hidden
        
        
        #line 76 "..\..\..\..\Services\ScheduleUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid WednesdayGrid;
        
        #line default
        #line hidden
        
        
        #line 103 "..\..\..\..\Services\ScheduleUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid ThursdayGrid;
        
        #line default
        #line hidden
        
        
        #line 130 "..\..\..\..\Services\ScheduleUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid FridayGrid;
        
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
            System.Uri resourceLocater = new System.Uri("/Scheduler;component/services/scheduleusercontrol.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Services\ScheduleUserControl.xaml"
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
            this.MondayGrid = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 2:
            
            #line 44 "..\..\..\..\Services\ScheduleUserControl.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.EditTabBttn_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.TuesdayGrid = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 4:
            
            #line 71 "..\..\..\..\Services\ScheduleUserControl.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.EditTabBttn_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.WednesdayGrid = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 6:
            
            #line 98 "..\..\..\..\Services\ScheduleUserControl.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.EditTabBttn_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.ThursdayGrid = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 8:
            
            #line 125 "..\..\..\..\Services\ScheduleUserControl.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.EditTabBttn_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.FridayGrid = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 10:
            
            #line 152 "..\..\..\..\Services\ScheduleUserControl.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.EditTabBttn_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

