using Repositories.Local;
using UnityEngine;
using UnityWeld.Binding;

namespace ViewModels
{
    [Binding]
    public class GamesViewModel : MonoBehaviour
    {
        [SerializeField] private SoloGameItemParametersRepository itemParametersRepository;
    }
}