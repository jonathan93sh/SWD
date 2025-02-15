﻿/*
  In App.xaml:
  <Application.Resources>
      <vm:DenKreditorSkylderListViewModelLocator xmlns:vm="clr-namespace:DenSorteBog.Locators"
                                   x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"
*/

using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using DenSorteBog.Model;
using DenSorteBog.ViewModel;

// Toolkit namespace
using SimpleMvvmToolkit;

namespace DenSorteBog.Locators
{
    /// <summary>
    /// This class creates ViewModels on demand for Views, supplying a
    /// ServiceAgent to the ViewModel if required.
    /// <para>
    /// Place the ViewModelLocator in the App.xaml resources:
    /// </para>
    /// <code>
    /// &lt;Application.Resources&gt;
    ///     &lt;vm:DenKreditorSkylderListViewModelLocator xmlns:vm="clr-namespace:DenSorteBog.Locators"
    ///                                  x:Key="Locator" /&gt;
    /// &lt;/Application.Resources&gt;
    /// </code>
    /// <para>
    /// Then use:
    /// </para>
    /// <code>
    /// DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"
    /// </code>
    /// <para>
    /// Use the <strong>mvvmlocator</strong> or <strong>mvvmlocatornosa</strong>
    /// code snippets to add ViewModels to this locator.
    /// </para>
    /// </summary>
    public class DenSorteBogViewModelLocator
    {
        // TODO: Use mvvmlocator or mvvmlocatornosa code snippets
        // to add ViewModels to the locator.
        //ISorteBogServiceAgent serviceAgent = new MockSorteBogServiceAgent();
        // Create KreditorSkylderListViewModel on demand

        static SorteBogModel model = new SorteBogModel();
        
        public KreditorSkylderListViewModel KreditorSkylderListViewModel
        {
            get
            {

                return new KreditorSkylderListViewModel(model);
                //return new KreditorSkylderListViewModel(new KreditorSkylderListModel());
            }
        }

        // Create GaeldsposterViewModel on demand
        public GaeldsposterViewModel GaeldsposterViewModel
        {
            get
            {
               // IServiceAgent serviceAgent = new ServiceAgentFake();
                return new GaeldsposterViewModel(model);
                //return new GaeldsposterViewModel(new GaeldsposterModel());
            }
        }
    }
}