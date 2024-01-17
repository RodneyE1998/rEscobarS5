using rEscobarS5.Models;

namespace rEscobarS5.Views;

public partial class vPrincipal : ContentPage
{
	public vPrincipal()
	{
		InitializeComponent();
	}

    private void btnInsertar_Clicked(object sender, EventArgs e)
    {
        statusMessage.Text = "";
        App.personRepo.AddNewPerson(txtName.Text);
        statusMessage.Text = App.personRepo.StatusMessage;
    }

    private void btnMostrar_Clicked(object sender, EventArgs e)
    {
        statusMessage.Text = "";
        List<Persona> people = App.personRepo.GetAllPeorle();
        ListaPersona.ItemsSource= people;
    }

    private void btnActualizar_Clicked(object sender, EventArgs e)
    {
        statusMessage.Text = "";
        if(ListaPersona.SelectedItem is Persona selectedPerson)
        {
            string updateName = txtUpdatedNmae.Text;

            if(!string.IsNullOrEmpty(updateName) )
            {
                Persona updatedPerson = new Persona
                {
                    Id = selectedPerson.Id,
                    Name = updateName
                };

                App.personRepo.UpdatePerson(updatedPerson);
                RefreshCollectionView();
                DisplayAlert("ALERTA", "Estudiante Actualizado", "Cerrar");
            }
            else
            {
                statusMessage.Text = "Ingrese un nuevo nombre antes de actualizar";
            }
        }
        else
        {
            statusMessage.Text = "Seleccione una persona para actualizar";
        }

    }

    private void btnEliminar_Clicked(object sender, EventArgs e)
    {
        statusMessage.Text = "";
        if(ListaPersona.SelectedItem is Persona selectPerson)
        {
            App.personRepo.DeletePerson(selectPerson.Id);
            RefreshCollectionView();
            DisplayAlert("ALERTA", "Estudiante Eliminado", "Cerrar");
        }
        else
        {
            statusMessage.Text = "Seleccione una persona para eliminar";
        }
    }

    private void RefreshCollectionView()
    {
        List<Persona> people = App.personRepo.GetAllPeorle();
        ListaPersona.ItemsSource = people;

    }
}