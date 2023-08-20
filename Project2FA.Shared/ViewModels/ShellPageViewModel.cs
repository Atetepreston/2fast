﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using Project2FA.Core.Messenger;
using System;
using System.Collections.Generic;
using System.Text;
#if WINDOWS_UWP
using Windows.UI.Xaml.Controls;
#else
using Microsoft.UI.Xaml.Controls;
#endif

namespace Project2FA.ViewModels
{
    public class ShellPageViewModel : ObservableRecipient
    {
        private bool _navigationIsAllowed = true;
        private string _title;
        private bool _isScreenCaptureEnabled;
        public bool NavigationIsAllowed
        {
            get => _navigationIsAllowed;
            set
            {
                if(SetProperty(ref _navigationIsAllowed, value))
                {
#if ANDROID || IOS
                    OnPropertyChanged(nameof(IsMobile));
#endif
                }
            }
        }
        public string Title { get => _title; set => SetProperty(ref _title, value); }
        private int _selectedIndex;
        public bool IsScreenCaptureEnabled
        {
            get => _isScreenCaptureEnabled;
            set
            {
                if (SetProperty(ref _isScreenCaptureEnabled, value))
                {
                    Windows.UI.ViewManagement.ApplicationView.GetForCurrentView().IsScreenCaptureEnabled = value;
                    Messenger.Send(new IsScreenCaptureEnabledChangedMessage(value));
                }
            }
        }

#if !WINDOWS_UWP
        /// <summary>
        /// 
        /// </summary>
        public bool IsMobile 
        {
            get
            {
#if ANDROID || IOS
                return true && NavigationIsAllowed;
#else
                return false;
#endif
            }
        }

        public int SelectedIndex 
        { 
            get => _selectedIndex;
            set
            {
                SetProperty(ref _selectedIndex, value);
            }
        }
#endif
    }



}
