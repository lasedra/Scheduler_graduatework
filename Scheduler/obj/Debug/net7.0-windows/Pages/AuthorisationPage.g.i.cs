﻿#pragma checksum "..\..\..\..\Pages\AuthorisationPage.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "114B17A3BAAB4B6E16C8CBDEC7ED42B382ED80D3"
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
    /// AuthorisationPage
    /// </summary>
    public partial class AuthorisationPage : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 22 "..\..\..\..\Pages\AuthorisationPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox LoginTextBox;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\..\..\Pages\AuthorisationPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox PasswordTextBox;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\..\..\Pages\AuthorisationPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button LogInButton;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\..\..\Pages\AuthorisationPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock RegistrationLink;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.9.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Scheduler;component/pages/authorisationpage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Pages\AuthorisationPage.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.9.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 10 "..\..\..\..\Pages\AuthorisationPage.xaml"
            ((Scheduler.Pages.AuthorisationPage)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Page_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.LoginTextBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 23 "..\..\..\..\Pages\AuthorisationPage.xaml"
            this.LoginTextBox.PreviewKeyDown += new System.Windows.Input.KeyEventHandler(this.AccesNoSpaceInput);
            
            #line default
            #line hidden
            return;
            case 3:
            this.PasswordTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.LogInButton = ((System.Windows.Controls.Button)(target));
            
            #line 37 "..\..\..\..\Pages\AuthorisationPage.xaml"
            this.LogInButton.Click += new System.Windows.RoutedEventHandler(this.LogInButton_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.RegistrationLink = ((System.Windows.Controls.TextBlock)(target));
            
            #line 42 "..\..\..\..\Pages\AuthorisationPage.xaml"
            this.RegistrationLink.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.RegistrationLink_MouseLeftButtonDown);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

