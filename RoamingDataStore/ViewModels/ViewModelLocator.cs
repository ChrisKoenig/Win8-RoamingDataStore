using System;
using System.Linq;
using GalaSoft.MvvmLight.Ioc;
using System.Collections.Generic;

namespace RoamingDataStore.ViewModels
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            SimpleIoc.Default.Register<MainViewModel>();
        }

        public MainViewModel Main
        {
            get
            {
                return SimpleIoc.Default.GetInstance<MainViewModel>();
            }
        }
    }
}
