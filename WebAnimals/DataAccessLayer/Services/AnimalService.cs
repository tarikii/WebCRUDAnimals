using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAnimals.Models;

namespace WebAnimals.DataAccessLayer.Services
{
    public class AnimalService
    {
        private readonly AnimalRepository _animalRepository;

        public AnimalService()
        {
            _animalRepository = new AnimalRepository();
        }

        public List<Animal> GetAllAnimals()
        {
            return _animalRepository.GetAllAnimalsDB();
        }

        public bool CreateAnimal(Animal animal)
        {
            return _animalRepository.CreateAnimalDB(animal);
        }

        public bool UpdateAnimal(Animal animal)
        {
            return _animalRepository.UpdateAnimalDB(animal);
        }

        public bool DeleteAnimal(int idAnimal)
        {
            return _animalRepository.DeleteAnimalDB(idAnimal);
        }
    }
}