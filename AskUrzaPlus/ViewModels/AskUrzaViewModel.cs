using AskUrzaPlus.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AskUrzaPlus.ViewModels;
public partial class AskUrzaViewModel : ObservableObject
{
    [ObservableProperty]
    string abilityText = "";

    [ObservableProperty]
    List<Ability> plusAbilities = new List<Ability>();

    [ObservableProperty]
    List<Ability> minusAbilities = new List<Ability>();

    [ObservableProperty]
    List<Ability> ultAbilities = new List<Ability>();

    static Random random = new Random();

    private readonly Task initTask;

    public AskUrzaViewModel() 
    { 
        initTask = InitAsync();
    }


    private async Task InitAsync()
    {
        List<Ability> abilities = await App.UrzaRepo.GetAllAbilitiesForGroup(1);
        foreach (Ability ability in abilities)
        {
            switch (ability.AbilityType)
            {
                case 1:
                    PlusAbilities.Add(ability);
                    break;
                case 2:
                    MinusAbilities.Add(ability);
                    break;
                case 3:
                    UltAbilities.Add(ability);
                    break;
                default:
                    break;
            }
        }
    }

    [RelayCommand]
    void PlusAbility()
    {
        AbilityText = PlusAbilities[random.Next(PlusAbilities.Count)].AbilityText;
    }
    [RelayCommand]
    void MinusAbility()
    {
        AbilityText = MinusAbilities[random.Next(MinusAbilities.Count)].AbilityText;
    }
    [RelayCommand]
    void UltAbility()
    {
        AbilityText = UltAbilities[random.Next(UltAbilities.Count)].AbilityText;
    }
}