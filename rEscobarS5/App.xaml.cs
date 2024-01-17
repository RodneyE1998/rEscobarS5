namespace rEscobarS5;

public partial class App : Application
{
	public static PersonaRepository personRepo { get; set; }
	public App(PersonaRepository personRepository)
	{
		InitializeComponent();

		MainPage = new Views.vPrincipal();
		personRepo= personRepository;
	}
}
