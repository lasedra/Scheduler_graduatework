﻿#pragma checksum "..\..\..\..\Pages\RegistrationPage.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "4DE1A536034993759F1E1DB392FD29EF117D1EFF"
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


namespace Scheduler.Pages {
    
    
    /// <summary>
    /// RegistrationPage
    /// </summary>
    public partial class RegistrationPage : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 41 "..\..\..\..\Pages\RegistrationPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox NameTextBox;
        
        #line default
        #line hidden
        
        
        #line 58 "..\..\..\..\Pages\RegistrationPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox PhoneTextBox;
        
        #line default
        #line hidden
        
        
        #line 88 "..\..\..\..\Pages\RegistrationPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TelegramIDTextBox;
        
        #line default
        #line hidden
        
        
        #line 107 "..\..\..\..\Pages\RegistrationPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox EmailTextBox;
        
        #line default
        #line hidden
        
        
        #line 122 "..\..\..\..\Pages\RegistrationPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton ManagerRadioBttn;
        
        #line default
        #line hidden
        
        
        #line 131 "..\..\..\..\Pages\RegistrationPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton TutorRadioBttn;
        
        #line default
        #line hidden
        
        
        #line 153 "..\..\..\..\Pages\RegistrationPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox LoginTextBox;
        
        #line default
        #line hidden
        
        
        #line 171 "..\..\..\..\Pages\RegistrationPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox PasswordTextBox;
        
        #line default
        #line hidden
        
        
        #line 180 "..\..\..\..\Pages\RegistrationPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button CreateAccountBttn;
        
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
            System.Uri resourceLocater = new System.Uri("/Scheduler;component/pages/registrationpage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Pages\RegistrationPage.xaml"
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
            this.NameTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.PhoneTextBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 59 "..\..\..\..\Pages\RegistrationPage.xaml"
            this.PhoneTextBox.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.PhoneTextBox_PreviewTextInput);
            
            #line default
            #line hidden
            
            #line 60 "..\..\..\..\Pages\RegistrationPage.xaml"
            this.PhoneTextBox.PreviewKeyDown += new System.Windows.Input.KeyEventHandler(this.AccesNoSpaceInput);
            
            #line default
            #line hidden
            return;
            case 3:
            this.TelegramIDTextBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 89 "..\..\..\..\Pages\RegistrationPage.xaml"
            this.TelegramIDTextBox.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.TelegramIDTextBox_PreviewTextInput);
            
            #line default
            #line hidden
            
            #line 90 "..\..\..\..\Pages\RegistrationPage.xaml"
            this.TelegramIDTextBox.PreviewKeyDown += new System.Windows.Input.KeyEventHandler(this.AccesNoSpaceInput);
            
            #line default
            #line hidden
            return;
            case 4:
            this.EmailTextBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 108 "..\..\..\..\Pages\RegistrationPage.xaml"
            this.EmailTextBox.PreviewKeyDown += new System.Windows.Input.KeyEventHandler(this.AccesNoSpaceInput);
            
            #line default
            #line hidden
            
            #line 109 "..\..\..\..\Pages\RegistrationPage.xaml"
            this.EmailTextBox.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.EmailTextBox_PreviewTextInput);
            
            #line default
            #line hidden
            return;
            case 5:
            this.ManagerRadioBttn = ((System.Windows.Controls.RadioButton)(target));
            return;
            case 6:
            this.TutorRadioBttn = ((System.Windows.Controls.RadioButton)(target));
            return;
            case 7:
            this.LoginTextBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 154 "..\..\..\..\Pages\RegistrationPage.xaml"
            this.LoginTextBox.PreviewKeyDown += new System.Windows.Input.KeyEventHandler(this.AccesNoSpaceInput);
            
            #line default
            #line hidden
            return;
            case 8:
            this.PasswordTextBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 172 "..\..\..\..\Pages\RegistrationPage.xaml"
            this.PasswordTextBox.PreviewKeyDown += new System.Windows.Input.KeyEventHandler(this.AccesNoSpaceInput);
            
            #line default
            #line hidden
            return;
            case 9:
            this.CreateAccountBttn = ((System.Windows.Controls.Button)(target));
            
            #line 180 "..\..\..\..\Pages\RegistrationPage.xaml"
            this.CreateAccountBttn.Click += new System.Windows.RoutedEventHandler(this.CreateAccountBttn_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

