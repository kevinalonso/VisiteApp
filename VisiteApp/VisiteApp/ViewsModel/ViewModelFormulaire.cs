using System;
using System.Collections.Generic;
using System.Text;
using VisiteApp.Views;
using Xamarin.Forms;

namespace VisiteApp.ViewsModel
{
    public class ViewModelFormulaire
    {
        #region Fields
        private INavigation _Navigation;
        private Formulaire _Formulaire;
        #endregion

        #region Properties

        #endregion

        #region Constructor
        public ViewModelFormulaire(INavigation nav)
        {
            _Navigation = nav;
        }

        public INavigation Navigation
        {
            get
            {
                return _Navigation;
            }

            set
            {
                _Navigation = value;
            }
        }

        public Formulaire Formulaire
        {
            get
            {
                return _Formulaire;
            }

            set
            {
                _Formulaire = value;
            }
        }

        #endregion

        #region Methods

        #endregion
    }
}
