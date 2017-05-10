using Xamarin.Forms;

namespace Visite.App.Core
{
    public interface INotifyPropertyChanging
    {
        //
        // Résumé :
        //     Se produit en cas de modification d'une valeur de propriété.
        event PropertyChangingEventHandler PropertyChanging;
    }
}