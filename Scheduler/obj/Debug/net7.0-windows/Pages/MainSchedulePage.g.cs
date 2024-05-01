﻿#pragma checksum "..\..\..\..\Pages\MainSchedulePage.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "A1F290716B42660FF7BCDB2BDE5F2FB407D85F07"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Scheduler.Services;
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
    /// MainSchedulePage
    /// </summary>
    public partial class MainSchedulePage : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 30 "..\..\..\..\Pages\MainSchedulePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox StudentGroupComboBox;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\..\..\Pages\MainSchedulePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BackOnTimelineBttn;
        
        #line default
        #line hidden
        
        
        #line 52 "..\..\..\..\Pages\MainSchedulePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock ScheduleWeekSpanTB;
        
        #line default
        #line hidden
        
        
        #line 59 "..\..\..\..\Pages\MainSchedulePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ForwardOnTimelineBttn;
        
        #line default
        #line hidden
        
        
        #line 70 "..\..\..\..\Pages\MainSchedulePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Scheduler.Services.ScheduleUserControl Schedule;
        
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
            System.Uri resourceLocater = new System.Uri("/Scheduler;component/pages/mainschedulepage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Pages\MainSchedulePage.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.3.0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal System.Delegate _CreateDelegate(System.Type delegateType, string handler) {
            return System.Delegate.CreateDelegate(delegateType, this, handler);
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
            
            #line 10 "..\..\..\..\Pages\MainSchedulePage.xaml"
            ((Scheduler.Pages.MainSchedulePage)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Page_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.StudentGroupComboBox = ((System.Windows.Controls.ComboBox)(target));
            
            #line 30 "..\..\..\..\Pages\MainSchedulePage.xaml"
            this.StudentGroupComboBox.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.StudentGroupComboBox_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.BackOnTimelineBttn = ((System.Windows.Controls.Button)(target));
            
            #line 45 "..\..\..\..\Pages\MainSchedulePage.xaml"
            this.BackOnTimelineBttn.Click += new System.Windows.RoutedEventHandler(this.BackOnTimelineBttn_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.ScheduleWeekSpanTB = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 5:
            this.ForwardOnTimelineBttn = ((System.Windows.Controls.Button)(target));
            
            #line 59 "..\..\..\..\Pages\MainSchedulePage.xaml"
            this.ForwardOnTimelineBttn.Click += new System.Windows.RoutedEventHandler(this.ForwardOnTimelineBttn_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.Schedule = ((Scheduler.Services.ScheduleUserControl)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

